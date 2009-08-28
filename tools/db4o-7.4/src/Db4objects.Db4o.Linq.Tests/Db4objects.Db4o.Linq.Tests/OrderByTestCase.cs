/* Copyright (C) 2004 - 2008  db4objects Inc.  http://www.db4o.com

This file is part of the db4o open source object database.

db4o is free software; you can redistribute it and/or modify it under
the terms of version 2 of the GNU General Public License as published
by the Free Software Foundation and as clarified by db4objects' GPL 
interpretation policy, available at
http://www.db4o.com/about/company/legalpolicies/gplinterpretation/
Alternatively you can write to db4objects, Inc., 1900 S Norfolk Street,
Suite 350, San Mateo, CA 94403, USA.

db4o is distributed in the hope that it will be useful, but WITHOUT ANY
WARRANTY; without even the implied warranty of MERCHANTABILITY or
FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License
for more details.

You should have received a copy of the GNU General Public License along
with this program; if not, write to the Free Software Foundation, Inc.,
59 Temple Place - Suite 330, Boston, MA  02111-1307, USA. */
using System;
using System.Collections.Generic;
using System.Linq;

using Db4objects.Db4o;
using Db4objects.Db4o.Linq;

using Db4oUnit;
using Db4oUnit.Extensions;

namespace Db4objects.Db4o.Linq.Tests
{
	public class OrderByTestCase : AbstractDb4oLinqTestCase
	{
		public class Person
		{
			public string Name;
			public int Age;

			public int UnoptimizableAgeProperty
			{
				get
				{
					return Age + 1;
				}
			}

			public string UnoptimizableNameProperty
			{
				get
				{
					if (string.IsNullOrEmpty(Name))
					{
						return Age.ToString();
					}
					return Name + " (" + Age + ")";
				}
			}

			public string OptimizableNameProperty
			{
				get { return Name; }
			}

			public int OptimizableAgeProperty
			{
				get { return Age; }
			}

			public override bool Equals(object obj)
			{
				Person p = obj as Person;
				if (p == null) return false;

				return p.Name == this.Name && p.Age == this.Age;
			}

			public override int GetHashCode()
			{
				return this.Age ^ this.Name.GetHashCode();
			}

			public override string ToString()
			{
				return "Person(" + Name + ", " + Age + ")";
			}
		}

		protected override void Store()
		{
			var people = new[] {
				new Person { Name = "jb", Age = 24 },
				new Person { Name = "ana", Age = 24 },
				new Person { Name = "reg", Age = 25 },
				new Person { Name = "ro", Age = 25 },
				new Person { Name = "jb", Age = 7 }
			};
			foreach (var person in people)
			{
				Store(person);
			}
		}

		public void TestOrderByOnUnoptimizableStringProperty()
		{
			AssertQuery("(Person(Name not 'jb'))",
				delegate
				{
					var jbs = from Person p in Db()
							  where p.Name != "jb"
							  orderby p.UnoptimizableNameProperty
							  select p;
					AssertSequence(new[] {
						new Person { Name = "ana", Age = 24 },
						new Person { Name = "reg", Age = 25 },
						new Person { Name = "ro", Age = 25 },
					}, jbs);
				});
		}

		public void TestOrderByOnUnoptimizableProperty()
		{
			AssertQuery("(Person(Name == 'jb'))",
				delegate
				{
					var jbs = from Person p in Db()
							  where p.Name == "jb"
							  orderby p.UnoptimizableAgeProperty
							  select p;
					AssertSequence(new[] {
						new Person { Name = "jb", Age = 7 },
						new Person { Name = "jb", Age = 24 },
					}, jbs);
				});
		}

		public void TestOrderByDescendingOnWhere()
		{
			AssertQuery("(Person(Name == 'jb')(orderby Age desc))",
				delegate
				{
					var jbs = from Person p in Db()
							  where p.Name == "jb"
							  orderby p.Age descending
							  select p;
					AssertSequence(new[] {
						new Person { Name = "jb", Age = 24 },
						new Person { Name = "jb", Age = 7 },
					}, jbs);
				});
		}

		public void TestOrderByDescendingOnUnoptimizableProperty()
		{
			AssertQuery("(Person(Name == 'jb'))",
				delegate
				{
					var jbs = from Person p in Db()
							  where p.Name == "jb"
							  orderby p.UnoptimizableAgeProperty descending
							  select p;
					AssertSequence(new[] {
						new Person { Name = "jb", Age = 24 },
						new Person { Name = "jb", Age = 7 },
					}, jbs);
				});
		}

		public void _TestUnoptimizableThenByOnOptimizedOrderBy()
		{
			var query = from Person p in Db()
						orderby p.OptimizableAgeProperty ascending,
							p.UnoptimizableNameProperty descending
						select p;
			AssertOrderByNameDescAgeAsc("(Person)(orderby Age asc)", query);
		}

		public void TestUnoptimizableOrderByAscendingThenDescendingOnProperties()
		{
			var query = from Person p in Db()
						orderby p.UnoptimizableAgeProperty ascending,
							p.UnoptimizableNameProperty descending
						select p;
			AssertOrderByNameDescAgeAsc("(Person)", query);
		}

		public void TestSimpleOrderByAscendingThenDescendingProperties()
		{
			var query = from Person p in Db()
						orderby p.OptimizableAgeProperty ascending,
							p.OptimizableNameProperty descending
						select p;
			AssertOrderByNameDescAgeAsc(query);
		}

		public void TestSimpleOrderByAscendingThenDescendingFields()
		{
			var query = from Person p in Db()
					  orderby p.Age ascending, p.Name descending
					  select p;
			AssertOrderByNameDescAgeAsc(query);
		}

		private void AssertOrderByNameDescAgeAsc(IDb4oLinqQuery<Person> query)
		{
			string expectedQuery = "(Person(orderby Name desc)(orderby Age asc))";
			AssertOrderByNameDescAgeAsc(expectedQuery, query);
		}

		private void AssertOrderByNameDescAgeAsc(string expectedQuery, IDb4oLinqQuery<Person> query)
		{	
			AssertQuery(expectedQuery,
				delegate
				{	
					AssertSequence(new[]
						{
							new Person { Name = "jb", Age = 7 },
							new Person { Name = "jb", Age = 24 },
							new Person { Name = "ana", Age = 24 },
							new Person { Name = "ro", Age = 25 },
							new Person { Name = "reg", Age = 25 }
						}, query);
				});
		}

		public void TestSimpleOrderByDescendingThenAscending()
		{
			AssertQuery("(Person(orderby Name asc)(orderby Age desc))",
				delegate
				{
					var jbs = from Person p in Db()
							  orderby p.Age descending, p.Name ascending
							  select p;

					AssertSequence(new[]
						{
							new Person { Name = "reg", Age = 25 },
							new Person { Name = "ro", Age = 25 },
							new Person { Name = "ana", Age = 24 },
							new Person { Name = "jb", Age = 24 },
							new Person { Name = "jb", Age = 7 },
						}, jbs);
				});
		}
	}
}

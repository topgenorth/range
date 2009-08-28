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
	public class ComposedQueryTestCase : AbstractDb4oLinqTestCase
	{
		public class Person
		{
			public string Name;
			public int Age;

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
		}

		protected override void Store()
		{
			var people = new[] 
			{
				new Person { Name = "Malkovitch", Age = 24 },
				new Person { Name = "Malkovitch", Age = 20 },
				new Person { Name = "Malkovitch", Age = 25 },
				new Person { Name = "Malkovitch", Age = 32 },
				new Person { Name = "Malkovitch", Age = 7 },
			};
			foreach (var person in people)
			{
				Store(person);
			}
		}

		public void TestWhereComposition()
		{
			var adults = from Person p in Db()
						 where p.Age > 18
						 select p;

			var johns = from p in adults
						where p.Age < 30
						select p;

			AssertQuery("(Person(Age < 30)(Age > 18))",
				delegate
				{
					AssertSet(new[]
						{
							new Person { Name = "Malkovitch", Age = 24 },
							new Person { Name = "Malkovitch", Age = 20 },
							new Person { Name = "Malkovitch", Age = 25 }
						}, johns);
				});

			AssertQuery("(Person(Age > 18))",
				delegate
				{
					AssertSet(new[]
					{
						new Person { Name = "Malkovitch", Age = 24 },
						new Person { Name = "Malkovitch", Age = 20 },
						new Person { Name = "Malkovitch", Age = 25 },
						new Person { Name = "Malkovitch", Age = 32 },
					}, adults);
				});
		}

		public void TestOrderedWhereComposition()
		{
			var adults = from Person p in Db()
						 where p.Age > 21
						 orderby p.Age
						 select p;

			var johns = from p in adults
						where p.Age < 31
						select p;

			AssertQuery("(Person(Age < 31)(Age > 21)(orderby Age asc))",
				delegate
				{
					AssertSequence(new[]
						{	
							new Person { Name = "Malkovitch", Age = 24 },
							new Person { Name = "Malkovitch", Age = 25 }
						}, johns);
				});

			AssertQuery("(Person(Age > 21)(orderby Age asc))",
				delegate
				{
					AssertSequence(new[]
					{
						new Person { Name = "Malkovitch", Age = 24 },
						new Person { Name = "Malkovitch", Age = 25 },
						new Person { Name = "Malkovitch", Age = 32 },
					}, adults);
				});
		}
	}
}

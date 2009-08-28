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
	public class PartiallyOptimizedQueryTestCase : AbstractDb4oLinqTestCase
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
			Store(new Person { Name = "jb", Age = 24 });
			Store(new Person { Name = "ana", Age = 20 });
			Store(new Person { Name = "reg", Age = 25 });
			Store(new Person { Name = "ro", Age = 32 });
			Store(new Person { Name = "jb", Age = 7 });
			Store(new Person { Name = "jb", Age = 28 });
			Store(new Person { Name = "jb", Age = 34 });
			Store(new Person { Name = "alice", Age = 35 });
		}

		public void TestProjection()
		{
			AssertQuery("(Person(Name == 'jb'))",
				delegate
				{
					var peoples = from Person p in Db()
								  where p.Name == "jb"
								  select new { p.Age };

					var ages = peoples.ToList().Select(t => t.Age);

					AssertSet(new[] { 24, 7, 28, 34 }, ages);
				});
		}

		public void TestUnoptimizableWhere()
		{
			AssertQuery("(Person)",
				delegate
				{
					var a = from Person p in Db()
							 where p.Name[0] == 'a'
							 select p;

					AssertSet(new[]
						{
							new Person { Name = "ana", Age = 20 },
							new Person { Name = "alice", Age = 35 }
						}, a);
				});
		}

		public void TestLet()
		{
			AssertQuery("(Person)",
				delegate
				{
					var ages = from Person p in Db()
							   let uname = p.Name.ToUpper()
							   where uname == "JB"
							   select p;

					AssertSet(new[]
						{
							new Person { Name = "jb", Age = 24 },
							new Person { Name = "jb", Age = 7 },
							new Person { Name = "jb", Age = 28 },
							new Person { Name = "jb", Age = 34 },
						}, ages);
				});
		}

		public void TestAverage()
		{
			AssertQuery("(Person(Name == 'jb'))",
				delegate
				{
					var ages = from Person p in Db()
							  where p.Name == "jb"
							  select p.Age;

					Assert.AreEqual(23.25, ages.Average());
				});
		}
	}
}

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
	public class StringMethodTestCase : AbstractDb4oLinqTestCase
	{
		public class Person
		{
			public string Name;

			public override bool Equals(object obj)
			{
				Person p = obj as Person;
				if (p == null) return false;

				return p.Name == this.Name;
			}

			public override int GetHashCode()
			{
				return this.Name.GetHashCode();
			}
		}

		protected override void Store()
		{
			Store(new Person { Name = "BiroBiro" });
			Store(new Person { Name = "Luna" });
			Store(new Person { Name = "Loustic" });
			Store(new Person { Name = "Loupiot" });
			Store(new Person { Name = "LeMiro" });
			Store(new Person { Name = "Tounage" });
		}

		public void TestStartsWith()
		{
			AssertQuery("(Person(Name startswith 'Lo'))",
				delegate
				{
					var los = from Person p in Db()
								where p.Name.StartsWith("Lo")
								select p;

					AssertSet(new[]
						{
							new Person { Name = "Loustic" },
							new Person { Name = "Loupiot" }
						}, los);
				});
		}

		public void TestEndsWith()
		{
			AssertQuery("(Person(Name endswith 'iro'))",
				delegate
				{
					var los = from Person p in Db()
							  where p.Name.EndsWith("iro")
							  select p;

					AssertSet(new[]
						{
							new Person { Name = "BiroBiro" },
							new Person { Name = "LeMiro" }
						}, los);
				});
		}

		public void TestContains()
		{
			AssertQuery("(Person(Name contains 'una'))",
				delegate
				{
					var los = from Person p in Db()
							  where p.Name.Contains("una")
							  select p;

					AssertSet(new[]
						{
							new Person { Name = "Luna" },
							new Person { Name = "Tounage" }
						}, los);
				});
		}
	}
}

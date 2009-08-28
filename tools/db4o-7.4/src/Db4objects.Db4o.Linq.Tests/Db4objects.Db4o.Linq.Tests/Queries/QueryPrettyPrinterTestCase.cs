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
using System.Text;

using Db4objects.Db4o;
using Db4objects.Db4o.Query;

using Db4oUnit;
using Db4oUnit.Extensions;

namespace Db4objects.Db4o.Linq.Tests.Queries
{
	public class QueryPrettyPrinterTestCase : AbstractDb4oTestCase
	{
		public class Person
		{
			public int Age;
			public string Name;
		}

		public void TestConstrainOnType()
		{
			var query = CreateQuery();
			query.Constrain(typeof(Person));

			AssertQueryString("(Person)", query);
		}

		public void TestConstrainEqualsOnField()
		{
			var query = CreateQuery();
			query.Constrain(typeof(Person));
			query.Descend("Name").Constrain("jb");

			AssertQueryString("(Person(Name == 'jb'))", query);
		}

		public void TestConstrainLessThanOnField()
		{
			var query = CreateQuery();
			query.Constrain(typeof(Person));
			query.Descend("Age").Constrain(12).Smaller();

			AssertQueryString("(Person(Age < 12))", query);
		}

		public void TestAndContraints()
		{
			var query = CreateQuery();
			query.Constrain(typeof(Person));

			query.Descend("Age").Constrain(22).Greater().And(query.Descend("Name").Constrain("jb"));

			AssertQueryString("(Person((Name == 'jb') and (Age > 22)))", query);
		}

		public void TestOrContraints()
		{
			var query = CreateQuery();
			query.Constrain(typeof(Person));

			query.Descend("Age").Constrain(22).Greater().Equal().Or(query.Descend("Name").Constrain("ro"));

			AssertQueryString("(Person((Name == 'ro') or (Age >= 22)))", query);
		}

		public void TestComplexConstraint()
		{
			var query = CreateQuery();
			query.Constrain(typeof(Person));

			query.Descend("Age").Constrain(22).Greater().Equal().And(query.Descend("Age").Constrain("32").Smaller().Equal()).Or(query.Descend("Name").Constrain("jb"));

			AssertQueryString("(Person((Name == 'jb') or ((Age <= 32) and (Age >= 22))))", query);
		}

		public void TestNot()
		{
			var query = CreateQuery();
			query.Constrain(typeof(Person));

			query.Descend("Name").Constrain("jb").Not();

			AssertQueryString("(Person(Name not 'jb'))", query);
		}

		public void TestStartsWith()
		{
			var query = CreateQuery();
			query.Constrain(typeof(Person));

			query.Descend("Name").Constrain("jb").StartsWith(true);

			AssertQueryString("(Person(Name startswith 'jb'))", query);
		}

		public void TestEndsWith()
		{
			var query = CreateQuery();
			query.Constrain(typeof(Person));

			query.Descend("Name").Constrain("jb").EndsWith(true);

			AssertQueryString("(Person(Name endswith 'jb'))", query);
		}

		public void TestContains()
		{
			var query = CreateQuery();
			query.Constrain(typeof(Person));

			query.Descend("Name").Constrain("jb").Contains();

			AssertQueryString("(Person(Name contains 'jb'))", query);
		}

		public void TestOrderByAscending()
		{
			var query = CreateQuery();
			query.Constrain(typeof(Person));

			query.Descend("Age").OrderAscending();

			AssertQueryString("(Person(orderby Age asc))", query);
		}

		public void TestOrderByAscendingGreaterOnSameField()
		{
			var query = CreateQuery();
			query.Constrain(typeof(Person));
			
			query.Descend("Age").Constrain(21).Greater();
			query.Descend("Age").OrderAscending();

			AssertQueryString("(Person(Age > 21)(orderby Age asc))", query);
		}

		public void TestOrderByDescending()
		{
			var query = CreateQuery();
			query.Constrain(typeof(Person));

			query.Descend("Age").OrderDescending();

			AssertQueryString("(Person(orderby Age desc))", query);
		}

		private IQuery CreateQuery()
		{
			return Db().Query();
		}

		private static void AssertQueryString(string expected, IQuery query)
		{
			Assert.AreEqual(expected, query.ToQueryString());
		}
	}
}

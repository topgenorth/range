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
#if CF_3_5

using System.Collections.Generic;
using Db4oUnit;
using Db4oUnit.Extensions;
using System.Linq;
using Db4objects.Db4o.Linq;

namespace Db4objects.Db4o.Tests.Compact
{
	class UnoptimizedLinqTestCase : AbstractDb4oTestCase
	{
		protected override void Store()
		{
			TestSubItem bar = new TestSubItem("bar", 1);
			
			Store(new TestSubject("foo", bar));
			Store(new TestSubject("baz", bar));
		}

		public void Test()
		{
			var result = from TestSubject subject in Db() where subject._name == "baz" select subject;

			Assert.IsTrue(result.GetType().FullName.Contains("System.Linq.Enumerable+<WhereIterator>"));
			AssertItemCount(result, 1);

			TestSubject item = GetFirstItem(result);

			Assert.IsNotNull(item);
			Assert.AreEqual("baz", item._name);
			Assert.IsNotNull(item._item);
			Assert.AreEqual("bar", item._item._name);
		}

		private TestSubject GetFirstItem(IEnumerable<TestSubject> result)
		{
			if (result == null) return null;

			IEnumerator<TestSubject> enumerator = result.GetEnumerator();
			enumerator.MoveNext();
			return enumerator.Current;
		}

		private void AssertItemCount(IEnumerable<TestSubject> result, int expected)
		{
			int actual = 0;
			foreach(TestSubject subject in result)
			{
				actual++;
			}

			Assert.AreEqual(expected, actual);
		}
	}

	public class TestSubject
	{
		public TestSubject(string name, TestSubItem item)
		{
			_name = name;
			_item = item;
		}

		public string _name;
		public TestSubItem _item;
	}

	public class TestSubItem
	{
		public TestSubItem(string name, int value)
		{
			_name = name;
			_value = value;
		}

		public string _name;
		public int _value;
	}
}

#endif
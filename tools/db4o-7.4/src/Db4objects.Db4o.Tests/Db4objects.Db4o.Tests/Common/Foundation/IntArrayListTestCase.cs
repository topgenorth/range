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
using Db4oUnit;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Tests.Common.Foundation;

namespace Db4objects.Db4o.Tests.Common.Foundation
{
	/// <exclude></exclude>
	public class IntArrayListTestCase : ITestCase
	{
		public static void Main(string[] args)
		{
			new ConsoleTestRunner(typeof(IntArrayListTestCase)).Run();
		}

		public virtual void TestIteratorGoesForwards()
		{
			IntArrayList list = new IntArrayList();
			AssertIterator(new int[] {  }, list.IntIterator());
			list.Add(1);
			AssertIterator(new int[] { 1 }, list.IntIterator());
			list.Add(2);
			AssertIterator(new int[] { 1, 2 }, list.IntIterator());
		}

		private void AssertIterator(int[] expected, IIntIterator4 iterator)
		{
			for (int i = 0; i < expected.Length; ++i)
			{
				Assert.IsTrue(iterator.MoveNext());
				Assert.AreEqual(expected[i], iterator.CurrentInt());
				Assert.AreEqual(expected[i], iterator.Current);
			}
			Assert.IsFalse(iterator.MoveNext());
		}

		//test mthod add(int,int)
		public virtual void TestAddAtIndex()
		{
			IntArrayList list = new IntArrayList();
			for (int i = 0; i < 10; i++)
			{
				list.Add(i);
			}
			list.Add(3, 100);
			Assert.AreEqual(100, list.Get(3));
			for (int i = 4; i < 11; i++)
			{
				Assert.AreEqual(i - 1, list.Get(i));
			}
		}
	}
}

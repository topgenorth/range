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
using System.Collections;
using Db4oUnit;
using Db4objects.Db4o.Internal.Btree;
using Db4objects.Db4o.Tests.Common.Btree;
using Db4objects.Db4o.Tests.Common.Foundation;

namespace Db4objects.Db4o.Tests.Common.Btree
{
	/// <exclude></exclude>
	public class BTreePointerTestCase : BTreeTestCaseBase
	{
		public static void Main(string[] args)
		{
			new BTreePointerTestCase().RunSolo();
		}

		private readonly int[] keys = new int[] { -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 7, 9
			 };

		/// <exception cref="Exception"></exception>
		protected override void Db4oSetupAfterStore()
		{
			base.Db4oSetupAfterStore();
			Add(keys);
			Commit();
		}

		public virtual void TestLastPointer()
		{
			BTreePointer pointer = _btree.LastPointer(Trans());
			AssertPointerKey(9, pointer);
		}

		public virtual void TestPrevious()
		{
			BTreePointer pointer = GetPointerForKey(3);
			BTreePointer previousPointer = pointer.Previous();
			AssertPointerKey(2, previousPointer);
		}

		public virtual void TestNextOperatesInReadMode()
		{
			BTreePointer pointer = _btree.FirstPointer(Trans());
			AssertReadModePointerIteration(keys, pointer);
		}

		public virtual void TestSearchOperatesInReadMode()
		{
			BTreePointer pointer = GetPointerForKey(3);
			AssertReadModePointerIteration(new int[] { 3, 4, 7, 9 }, pointer);
		}

		private BTreePointer GetPointerForKey(int key)
		{
			IBTreeRange range = Search(key);
			IEnumerator pointers = range.Pointers();
			Assert.IsTrue(pointers.MoveNext());
			BTreePointer pointer = (BTreePointer)pointers.Current;
			return pointer;
		}

		private void AssertReadModePointerIteration(int[] expectedKeys, BTreePointer pointer
			)
		{
			object[] expected = IntArrays4.ToObjectArray(expectedKeys);
			for (int i = 0; i < expected.Length; i++)
			{
				Assert.IsNotNull(pointer, "Expected '" + expected[i] + "'");
				Assert.AreNotSame(_btree.Root(), pointer.Node());
				AssertInReadMode(pointer.Node());
				Assert.AreEqual(expected[i], pointer.Key());
				AssertInReadMode(pointer.Node());
				pointer = pointer.Next();
			}
		}

		private void AssertInReadMode(BTreeNode node)
		{
			Assert.IsFalse(node.CanWrite());
		}

		protected override BTree NewBTree()
		{
			return NewBTreeWithNoNodeCaching();
		}

		private BTree NewBTreeWithNoNodeCaching()
		{
			return BTreeAssert.CreateIntKeyBTree(Container(), 0, 0, BtreeNodeSize);
		}
	}
}

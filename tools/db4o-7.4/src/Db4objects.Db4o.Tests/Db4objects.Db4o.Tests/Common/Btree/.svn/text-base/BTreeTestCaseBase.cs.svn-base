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
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Btree;
using Db4objects.Db4o.Tests.Common.Btree;

namespace Db4objects.Db4o.Tests.Common.Btree
{
	public abstract class BTreeTestCaseBase : AbstractDb4oTestCase, IOptOutCS
	{
		protected const int BtreeNodeSize = 4;

		protected BTree _btree;

		/// <exception cref="Exception"></exception>
		protected override void Db4oSetupAfterStore()
		{
			_btree = NewBTree();
		}

		protected virtual BTree NewBTree()
		{
			return BTreeAssert.CreateIntKeyBTree(Container(), 0, BtreeNodeSize);
		}

		protected virtual IBTreeRange Range(int lower, int upper)
		{
			IBTreeRange lowerRange = Search(lower);
			IBTreeRange upperRange = Search(upper);
			return lowerRange.ExtendToLastOf(upperRange);
		}

		protected virtual IBTreeRange Search(int key)
		{
			return Search(Trans(), key);
		}

		protected virtual void Add(int[] keys)
		{
			for (int i = 0; i < keys.Length; ++i)
			{
				Add(keys[i]);
			}
		}

		protected virtual IBTreeRange Search(Transaction trans, int key)
		{
			return _btree.Search(trans, key);
		}

		protected virtual void Commit(Transaction trans)
		{
			_btree.Commit(trans);
		}

		protected virtual void Commit()
		{
			Commit(Trans());
		}

		protected virtual void Remove(Transaction transaction, int[] keys)
		{
			for (int i = 0; i < keys.Length; i++)
			{
				Remove(transaction, keys[i]);
			}
		}

		protected virtual void Add(Transaction transaction, int[] keys)
		{
			for (int i = 0; i < keys.Length; i++)
			{
				Add(transaction, keys[i]);
			}
		}

		protected virtual void AssertEmpty(Transaction transaction)
		{
			BTreeAssert.AssertEmpty(transaction, _btree);
		}

		protected virtual void Add(Transaction transaction, int element)
		{
			_btree.Add(transaction, element);
		}

		protected virtual void Remove(int element)
		{
			Remove(Trans(), element);
		}

		protected virtual void Remove(Transaction trans, int element)
		{
			_btree.Remove(trans, element);
		}

		protected virtual void Add(int element)
		{
			Add(Trans(), element);
		}

		private int Size()
		{
			return _btree.Size(Trans());
		}

		protected virtual void AssertSize(int expected)
		{
			Assert.AreEqual(expected, Size());
		}

		protected virtual void AssertSingleElement(int element)
		{
			AssertSingleElement(Trans(), element);
		}

		protected virtual void AssertSingleElement(Transaction trans, int element)
		{
			BTreeAssert.AssertSingleElement(trans, _btree, element);
		}

		protected virtual void AssertPointerKey(int key, BTreePointer pointer)
		{
			Assert.AreEqual(key, pointer.Key());
		}
	}
}

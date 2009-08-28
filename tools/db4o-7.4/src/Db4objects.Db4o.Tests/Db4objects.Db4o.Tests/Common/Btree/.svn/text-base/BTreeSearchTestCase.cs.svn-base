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
using Db4oUnit.Extensions;
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Btree;
using Db4objects.Db4o.Tests.Common.Btree;
using Db4objects.Db4o.Tests.Common.Foundation;

namespace Db4objects.Db4o.Tests.Common.Btree
{
	public class BTreeSearchTestCase : AbstractDb4oTestCase, IOptOutDefragSolo, IOptOutCS
	{
		protected const int BtreeNodeSize = 4;

		public static void Main(string[] arguments)
		{
			new BTreeSearchTestCase().RunSolo();
		}

		/// <exception cref="Exception"></exception>
		public virtual void Test()
		{
			CycleIntKeys(new int[] { 3, 5, 7, 10, 11, 12, 14, 15, 17, 20, 21, 25 });
		}

		/// <exception cref="Exception"></exception>
		private void CycleIntKeys(int[] values)
		{
			BTree btree = BTreeAssert.CreateIntKeyBTree(Container(), 0, BtreeNodeSize);
			for (int i = 0; i < 5; i++)
			{
				btree = CycleIntKeys(btree, values);
			}
		}

		/// <exception cref="Exception"></exception>
		private BTree CycleIntKeys(BTree btree, int[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				btree.Add(Trans(), values[i]);
			}
			ExpectKeysSearch(Trans(), btree, values);
			btree.Commit(Trans());
			int id = btree.GetID();
			Container().Commit(Trans());
			Reopen();
			btree = BTreeAssert.CreateIntKeyBTree(Container(), id, BtreeNodeSize);
			ExpectKeysSearch(Trans(), btree, values);
			for (int i = 0; i < values.Length; i++)
			{
				btree.Remove(Trans(), values[i]);
			}
			BTreeAssert.AssertEmpty(Trans(), btree);
			btree.Commit(Trans());
			BTreeAssert.AssertEmpty(Trans(), btree);
			return btree;
		}

		private void ExpectKeysSearch(Transaction trans, BTree btree, int[] keys)
		{
			int lastValue = int.MinValue;
			for (int i = 0; i < keys.Length; i++)
			{
				if (keys[i] != lastValue)
				{
					ExpectingVisitor expectingVisitor = BTreeAssert.CreateExpectingVisitor(keys[i], IntArrays4
						.Occurences(keys, keys[i]));
					IBTreeRange range = btree.Search(trans, keys[i]);
					BTreeAssert.TraverseKeys(range, expectingVisitor);
					expectingVisitor.AssertExpectations();
					lastValue = keys[i];
				}
			}
		}
	}
}

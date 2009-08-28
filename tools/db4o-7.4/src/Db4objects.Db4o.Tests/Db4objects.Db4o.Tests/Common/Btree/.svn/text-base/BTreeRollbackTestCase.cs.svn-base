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
using Db4objects.Db4o.Tests.Common.Btree;

namespace Db4objects.Db4o.Tests.Common.Btree
{
	public class BTreeRollbackTestCase : BTreeTestCaseBase
	{
		public static void Main(string[] args)
		{
			new BTreeRollbackTestCase().RunSolo();
		}

		private static readonly int[] CommittedValues = new int[] { 6, 8, 15, 45, 43, 9, 
			23, 25, 7, 3, 2 };

		private static readonly int[] RolledBackValues = new int[] { 16, 18, 115, 19, 17, 
			13, 12 };

		public virtual void Test()
		{
			Add(CommittedValues);
			CommitBTree();
			for (int i = 0; i < 5; i++)
			{
				Add(RolledBackValues);
				RollbackBTree();
			}
			BTreeAssert.AssertKeys(Trans(), _btree, CommittedValues);
		}

		private void CommitBTree()
		{
			_btree.Commit(Trans());
			Trans().Commit();
		}

		private void RollbackBTree()
		{
			_btree.Rollback(Trans());
			Trans().Rollback();
		}
	}
}

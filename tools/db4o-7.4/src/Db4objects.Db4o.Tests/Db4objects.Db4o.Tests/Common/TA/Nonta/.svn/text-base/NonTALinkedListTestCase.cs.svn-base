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
using Db4objects.Db4o.Tests.Common.TA;
using Db4objects.Db4o.Tests.Common.TA.Nonta;

namespace Db4objects.Db4o.Tests.Common.TA.Nonta
{
	/// <exclude></exclude>
	public class NonTALinkedListTestCase : NonTAItemTestCaseBase
	{
		private static readonly LinkedList List = LinkedList.NewList(10);

		public static void Main(string[] args)
		{
			new NonTALinkedListTestCase().RunAll();
		}

		protected override void AssertItemValue(object obj)
		{
			Assert.AreEqual(List, ((LinkedListItem)obj).list);
		}

		protected override object CreateItem()
		{
			LinkedListItem item = new LinkedListItem();
			item.list = List;
			return item;
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestDeactivateDepth()
		{
			LinkedListItem item = QueryItem();
			LinkedList level1 = item.list;
			LinkedList level2 = level1.NextN(1);
			LinkedList level3 = level1.NextN(2);
			LinkedList level4 = level1.NextN(3);
			LinkedList level5 = level1.NextN(4);
			Assert.IsNotNull(level1.next);
			Assert.IsNotNull(level2.next);
			Assert.IsNotNull(level3.next);
			Assert.IsNotNull(level4.next);
			Assert.IsNotNull(level5.next);
			Db().Deactivate(level1, 4);
			AssertDeactivated(level1);
			AssertDeactivated(level2);
			AssertDeactivated(level3);
			AssertDeactivated(level4);
			Assert.IsNotNull(level5.next);
		}

		private void AssertDeactivated(LinkedList list)
		{
			Assert.IsNull(list.next);
			Assert.AreEqual(0, list.value);
		}

		private LinkedListItem QueryItem()
		{
			return (LinkedListItem)RetrieveOnlyInstance();
		}
	}
}

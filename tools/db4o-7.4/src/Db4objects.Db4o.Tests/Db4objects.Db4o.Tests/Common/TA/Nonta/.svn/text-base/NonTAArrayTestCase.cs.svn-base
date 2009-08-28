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
using Db4objects.Db4o.Tests.Common.TA;
using Db4objects.Db4o.Tests.Common.TA.Nonta;

namespace Db4objects.Db4o.Tests.Common.TA.Nonta
{
	/// <exclude></exclude>
	public class NonTAArrayTestCase : NonTAItemTestCaseBase
	{
		private static readonly int[] Ints1 = new int[] { 1, 2, 3 };

		private static readonly int[] Ints2 = new int[] { 4, 5, 6 };

		private static readonly LinkedList[] List1 = new LinkedList[] { LinkedList.NewList
			(5), LinkedList.NewList(5) };

		private static readonly LinkedList[] List2 = new LinkedList[] { LinkedList.NewList
			(5), LinkedList.NewList(5) };

		public static void Main(string[] args)
		{
			new NonTAArrayTestCase().RunAll();
		}

		protected override void AssertItemValue(object obj)
		{
			ArrayItem item = (ArrayItem)obj;
			ArrayAssert.AreEqual(Ints1, item.Value());
			ArrayAssert.AreEqual(Ints2, (int[])item.Object());
			ArrayAssert.AreEqual(List1, item.Lists());
			ArrayAssert.AreEqual(List2, (LinkedList[])item.ListsObject());
		}

		protected override object CreateItem()
		{
			ArrayItem item = new ArrayItem();
			item.value = Ints1;
			item.obj = Ints2;
			item.lists = List1;
			item.listsObject = List2;
			return item;
		}
	}
}

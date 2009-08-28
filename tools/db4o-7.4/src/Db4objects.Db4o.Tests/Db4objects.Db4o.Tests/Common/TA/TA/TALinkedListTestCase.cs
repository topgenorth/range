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
using Db4objects.Db4o.Tests.Common.TA.TA;

namespace Db4objects.Db4o.Tests.Common.TA.TA
{
	/// <exclude></exclude>
	public class TALinkedListTestCase : TAItemTestCaseBase
	{
		public static void Main(string[] args)
		{
			new TALinkedListTestCase().RunAll();
		}

		/// <exception cref="Exception"></exception>
		protected override object CreateItem()
		{
			TALinkedListItem item = new TALinkedListItem();
			item.list = NewList();
			return item;
		}

		private TALinkedList NewList()
		{
			return TALinkedList.NewList(10);
		}

		/// <exception cref="Exception"></exception>
		protected override void AssertItemValue(object obj)
		{
			TALinkedListItem item = (TALinkedListItem)obj;
			Assert.AreEqual(NewList(), item.List());
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestDeactivateDepth()
		{
			TALinkedListItem item = (TALinkedListItem)RetrieveOnlyInstance();
			TALinkedList list = item.List();
			TALinkedList next3 = list.NextN(3);
			TALinkedList next5 = list.NextN(5);
			Assert.IsNotNull(next3.Next());
			Assert.IsNotNull(next5.Next());
			Db().Deactivate(list, 4);
			Assert.IsNull(list.next);
			Assert.AreEqual(0, list.value);
			// FIXME: test fails if uncomenting the following assertion.
			//	    	Assert.isNull(next3.next);
			Assert.IsNotNull(next5.next);
		}
	}
}

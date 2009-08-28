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
using Db4objects.Db4o.Tests.Common.TA.Mixed;

namespace Db4objects.Db4o.Tests.Common.TA.Mixed
{
	/// <exclude></exclude>
	public class NTNTestCase : ItemTestCaseBase
	{
		public static void Main(string[] args)
		{
			new NTNTestCase().RunAll();
		}

		/// <exception cref="Exception"></exception>
		protected override object CreateItem()
		{
			return new NTNItem(42);
		}

		/// <exception cref="Exception"></exception>
		protected override void AssertRetrievedItem(object obj)
		{
			NTNItem item = (NTNItem)obj;
			Assert.IsNotNull(item.tnItem);
			Assert.IsNull(item.tnItem.list);
		}

		/// <exception cref="Exception"></exception>
		protected override void AssertItemValue(object obj)
		{
			NTNItem item = (NTNItem)obj;
			Assert.AreEqual(LinkedList.NewList(42), item.tnItem.Value());
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestDeactivateDepth()
		{
			NTNItem item = (NTNItem)RetrieveOnlyInstance();
			TNItem tnItem = item.tnItem;
			tnItem.Value();
			Assert.IsNotNull(tnItem.list);
			// item.tnItem.list
			Db().Deactivate(item, 2);
			// FIXME: failure 
			// Assert.isNull(tnItem.list);
			Db().Activate(item, 42);
			Db().Deactivate(item, 10);
			Assert.IsNull(tnItem.list);
		}
	}
}

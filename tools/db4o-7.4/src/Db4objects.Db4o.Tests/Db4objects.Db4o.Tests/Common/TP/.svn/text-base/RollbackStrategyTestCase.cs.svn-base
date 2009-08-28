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
using Db4oUnit.Mocking;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.TA;
using Db4objects.Db4o.Tests.Common.TP;

namespace Db4objects.Db4o.Tests.Common.TP
{
	public class RollbackStrategyTestCase : AbstractDb4oTestCase
	{
		private readonly RollbackStrategyMock _mock = new RollbackStrategyMock();

		/// <exception cref="Exception"></exception>
		protected override void Configure(IConfiguration config)
		{
			config.Add(new TransparentPersistenceSupport(_mock));
		}

		public virtual void TestRollbackStrategyIsCalledForChangedObjects()
		{
			Item item1 = StoreItem("foo");
			Item item2 = StoreItem("bar");
			StoreItem("baz");
			Change(item1);
			Change(item2);
			_mock.Verify(new MethodCall[0]);
			Db().Rollback();
			_mock.Verify(new MethodCall[] { new MethodCall("rollback", Db(), item2), new MethodCall
				("rollback", Db(), item1) });
		}

		private void Change(Item item)
		{
			item.SetName(item.GetName() + "*");
		}

		private Item StoreItem(string name)
		{
			Item item = new Item(name);
			Store(item);
			return item;
		}

		public static void Main(string[] args)
		{
			new RollbackStrategyTestCase().RunAll();
		}
	}
}

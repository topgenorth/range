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
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Freespace;
using Db4objects.Db4o.Tests.Common.Handlers;

namespace Db4objects.Db4o.Tests.Common.Freespace
{
	public class IxFreespaceMigrationTestCase : FormatMigrationTestCaseBase
	{
		protected override void ConfigureForStore(IConfiguration config)
		{
			config.Freespace().UseIndexSystem();
		}

		protected override void Store(IExtObjectContainer objectContainer)
		{
			IxFreespaceMigrationTestCase.Item nextItem = null;
			for (int i = 9; i >= 0; i--)
			{
				IxFreespaceMigrationTestCase.Item storedItem = new IxFreespaceMigrationTestCase.Item
					("item" + i, nextItem);
				StoreObject(objectContainer, storedItem);
				nextItem = storedItem;
			}
			objectContainer.Commit();
			IxFreespaceMigrationTestCase.Item item = QueryForItem(objectContainer, 0);
			for (int i = 0; i < 5; i++)
			{
				objectContainer.Delete(item);
				item = item._next;
			}
			objectContainer.Commit();
		}

		private IxFreespaceMigrationTestCase.Item QueryForItem(IExtObjectContainer objectContainer
			, int n)
		{
			IQuery q = objectContainer.Query();
			q.Constrain(typeof(IxFreespaceMigrationTestCase.Item));
			q.Descend("_name").Constrain("item" + n);
			return (IxFreespaceMigrationTestCase.Item)q.Execute().Next();
		}

		protected override void AssertObjectsAreReadable(IExtObjectContainer objectContainer
			)
		{
			AssertItemCount(objectContainer, 5);
			IxFreespaceMigrationTestCase.Item item = QueryForItem(objectContainer, 5);
			for (int i = 5; i < 10; i++)
			{
				Assert.AreEqual("item" + i, item._name);
				item = item._next;
			}
		}

		private void AssertItemCount(IExtObjectContainer objectContainer, int i)
		{
			IQuery q = objectContainer.Query();
			q.Constrain(typeof(IxFreespaceMigrationTestCase.Item));
			Assert.AreEqual(i, q.Execute().Size());
		}

		public class Item
		{
			public string _name;

			public IxFreespaceMigrationTestCase.Item _next;

			public Item(string name)
			{
				_name = name;
			}

			public Item(string name, IxFreespaceMigrationTestCase.Item next_)
			{
				_name = name;
				_next = next_;
			}
		}

		protected override string FileNamePrefix()
		{
			return "migrate_freespace_ix_";
		}
	}
}

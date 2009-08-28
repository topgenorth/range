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
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal.Freespace;
using Db4objects.Db4o.Tests.Common.Freespace;

namespace Db4objects.Db4o.Tests.Common.Freespace
{
	public class FreespaceManagerTypeChangeTestCase : FreespaceManagerTestCaseBase, IOptOutCS
		, IOptOutDefragSolo
	{
		private const bool Verbose = false;

		private IConfiguration configuration;

		private static string ItemName = "one";

		public class Item
		{
			public string _name;

			public Item(string name)
			{
				_name = name;
			}
		}

		public static void Main(string[] args)
		{
			new FreespaceManagerTypeChangeTestCase().RunSolo();
		}

		/// <exception cref="Exception"></exception>
		protected override void Configure(IConfiguration config)
		{
			base.Configure(config);
			config.Freespace().UseBTreeSystem();
			configuration = config;
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestSwitchingBackAndForth()
		{
			ProduceSomeFreeSpace();
			Db().Commit();
			StoreItem();
			for (int i = 0; i < 50; i++)
			{
				PrintStatus();
				AssertFreespaceSlotsAvailable();
				configuration.Freespace().UseRamSystem();
				Reopen();
				AssertFreespaceManagerClass(typeof(RamFreespaceManager));
				AssertItemAvailable();
				DeleteItem();
				StoreItem();
				AssertFreespaceSlotsAvailable();
				configuration.Freespace().UseBTreeSystem();
				Reopen();
				AssertFreespaceManagerClass(typeof(BTreeFreespaceManager));
				AssertItemAvailable();
				DeleteItem();
				StoreItem();
			}
		}

		private void StoreItem()
		{
			Store(new FreespaceManagerTypeChangeTestCase.Item(ItemName));
		}

		private void DeleteItem()
		{
			Db().Delete(RetrieveOnlyInstance(typeof(FreespaceManagerTypeChangeTestCase.Item))
				);
		}

		private void AssertItemAvailable()
		{
			FreespaceManagerTypeChangeTestCase.Item item = (FreespaceManagerTypeChangeTestCase.Item
				)RetrieveOnlyInstance(typeof(FreespaceManagerTypeChangeTestCase.Item));
			Assert.AreEqual(ItemName, item._name);
		}

		private void AssertFreespaceSlotsAvailable()
		{
			Assert.IsGreater(3, FreespaceSlots().Size());
		}

		private void PrintStatus()
		{
			return;
			Print("fileSize " + FileSession().FileLength());
			Print("slot count " + CurrentFreespaceManager().SlotCount());
			Print("current freespace " + CurrentFreespace());
		}

		private Collection4 FreespaceSlots()
		{
			Collection4 collectionOfSlots = new Collection4();
			CurrentFreespaceManager().Traverse(new _IVisitor4_101(collectionOfSlots));
			return collectionOfSlots;
		}

		private sealed class _IVisitor4_101 : IVisitor4
		{
			public _IVisitor4_101(Collection4 collectionOfSlots)
			{
				this.collectionOfSlots = collectionOfSlots;
			}

			public void Visit(object obj)
			{
				collectionOfSlots.Add(obj);
			}

			private readonly Collection4 collectionOfSlots;
		}

		private void AssertFreespaceManagerClass(Type clazz)
		{
			Assert.IsInstanceOf(clazz, CurrentFreespaceManager());
		}

		private int CurrentFreespace()
		{
			return CurrentFreespaceManager().TotalFreespace();
		}

		private static void Print(string str)
		{
		}
	}
}

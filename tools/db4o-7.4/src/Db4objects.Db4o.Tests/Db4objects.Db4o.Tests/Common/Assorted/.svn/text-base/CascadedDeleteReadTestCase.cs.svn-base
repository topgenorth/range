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
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Assorted;

namespace Db4objects.Db4o.Tests.Common.Assorted
{
	public class CascadedDeleteReadTestCase : AbstractDb4oTestCase
	{
		public class Item
		{
			public CascadedDeleteReadTestCase.Item _child1;

			public CascadedDeleteReadTestCase.Item _child2;

			public string _name;

			public Item()
			{
			}

			public Item(string name)
			{
				_name = name;
			}

			public Item(CascadedDeleteReadTestCase.Item child1, CascadedDeleteReadTestCase.Item
				 child2, string name)
			{
				_child1 = child1;
				_child2 = child2;
				_name = name;
			}
		}

		public static void Main(string[] args)
		{
			new CascadedDeleteReadTestCase().RunSoloAndClientServer();
		}

		/// <exception cref="Exception"></exception>
		protected override void Configure(IConfiguration config)
		{
			base.Configure(config);
			config.ObjectClass(typeof(CascadedDeleteReadTestCase.Item)).ObjectField("_child1"
				).CascadeOnDelete(true);
			config.ObjectClass(typeof(CascadedDeleteReadTestCase.Item)).ObjectField("_child2"
				).CascadeOnDelete(true);
			config.ObjectClass(typeof(CascadedDeleteReadTestCase.Item)).ObjectField("_child1"
				).CascadeOnUpdate(true);
			config.ObjectClass(typeof(CascadedDeleteReadTestCase.Item)).ObjectField("_child2"
				).CascadeOnUpdate(true);
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			Store(new CascadedDeleteReadTestCase.Item(new CascadedDeleteReadTestCase.Item("1"
				), null, "parent"));
		}

		public virtual void Test()
		{
			CascadedDeleteReadTestCase.Item item = ParentItem();
			item._child2 = item._child1;
			item._child1 = null;
			Store(item);
			Db().Delete(item);
			AssertItemCount(0);
		}

		private CascadedDeleteReadTestCase.Item ParentItem()
		{
			IQuery q = Db().Query();
			q.Constrain(typeof(CascadedDeleteReadTestCase.Item));
			q.Descend("_name").Constrain("parent");
			return (CascadedDeleteReadTestCase.Item)q.Execute().Next();
		}

		private void AssertItemCount(int count)
		{
			IQuery q = Db().Query();
			q.Constrain(typeof(CascadedDeleteReadTestCase.Item));
			IObjectSet objectSet = q.Execute();
			Assert.AreEqual(count, objectSet.Size());
		}
	}
}

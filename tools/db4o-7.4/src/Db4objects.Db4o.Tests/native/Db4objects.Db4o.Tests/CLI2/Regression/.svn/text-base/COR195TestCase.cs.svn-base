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
namespace Db4objects.Db4o.Tests.CLI2.Regression
{
	using System.Collections.Generic;

	using Db4objects.Db4o.Config;

	using Db4oUnit;
	using Db4oUnit.Extensions;

	class COR195TestCase : AbstractDb4oTestCase
	{
		public class Item
		{
			private int i;

			public Item(int i)
			{
				this.i = i;
			}

			public int I
			{
				get { return i; }
				set { i = value; }
			}
		}

		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(typeof(Item)).ObjectField("i").Indexed(true);
		}

		protected override void Store()
		{
			for (int i = 0; i < 1000; i++) Store(new Item(i));
		}

		public void TestNativeQueryOnIndex()
		{
			IList<Item> list = Db().Query<Item>(delegate(Item i) { return i.I > 100 && i.I <= 200; });
			Assert.AreEqual(100, list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				Assert.AreEqual(i + 101, list[i].I);
			}
		}
	}
}

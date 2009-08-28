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
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Tests.Common.Assorted;

namespace Db4objects.Db4o.Tests.Common.Assorted
{
	public class DatabaseGrowthSizeTestCase : AbstractDb4oTestCase, IOptOutCS
	{
		private const int Size = 10000;

		private const int ApproximateHeaderSize = 100;

		public static void Main(string[] args)
		{
			new DatabaseGrowthSizeTestCase().RunSolo();
		}

		/// <exception cref="Exception"></exception>
		protected override void Configure(IConfiguration config)
		{
			config.DatabaseGrowthSize(Size);
			config.BlockSize(3);
		}

		public virtual void Test()
		{
			Assert.IsGreater(Size, FileSession().FileLength());
			Assert.IsSmaller(Size + ApproximateHeaderSize, FileSession().FileLength());
			DatabaseGrowthSizeTestCase.Item item = DatabaseGrowthSizeTestCase.Item.NewItem(Size
				);
			Store(item);
			Assert.IsGreater(Size * 2, FileSession().FileLength());
			Assert.IsSmaller(Size * 2 + ApproximateHeaderSize, FileSession().FileLength());
			object retrievedItem = RetrieveOnlyInstance(typeof(DatabaseGrowthSizeTestCase.Item
				));
			Assert.AreSame(item, retrievedItem);
		}

		public class Item
		{
			public byte[] _payload;

			public Item()
			{
			}

			public static DatabaseGrowthSizeTestCase.Item NewItem(int payloadSize)
			{
				DatabaseGrowthSizeTestCase.Item item = new DatabaseGrowthSizeTestCase.Item();
				item._payload = new byte[payloadSize];
				return item;
			}
		}
	}
}

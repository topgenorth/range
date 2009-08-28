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
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Assorted;

namespace Db4objects.Db4o.Tests.Common.Assorted
{
	public class IndexedBlockSizeQueryTestCase : AbstractDb4oTestCase
	{
		public static void Main(string[] args)
		{
			new IndexedBlockSizeQueryTestCase().RunSolo();
		}

		/// <exception cref="Exception"></exception>
		protected override void Configure(IConfiguration config)
		{
			config.BlockSize(10);
			config.ObjectClass(typeof(IndexedBlockSizeQueryTestCase.Item)).ObjectField("_name"
				).Indexed(true);
		}

		public class Item
		{
			public object _untypedMember;

			public string _name;

			public Item(string name)
			{
				_untypedMember = name;
				_name = name;
			}
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			Store(new IndexedBlockSizeQueryTestCase.Item("one"));
		}

		public virtual void Test()
		{
			IQuery q = NewQuery(typeof(IndexedBlockSizeQueryTestCase.Item));
			q.Descend("_name").Constrain("one");
			Assert.AreEqual(1, q.Execute().Size());
		}
	}
}

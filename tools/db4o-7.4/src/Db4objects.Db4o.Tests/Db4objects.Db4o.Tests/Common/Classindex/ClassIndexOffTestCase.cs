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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Classindex;

namespace Db4objects.Db4o.Tests.Common.Classindex
{
	public class ClassIndexOffTestCase : AbstractDb4oTestCase, IOptOutCS
	{
		internal static string Name = "1";

		public class Holder
		{
			public ClassIndexOffTestCase.Item _item;

			public ClassIndexOffTestCase.Item _nullItem;

			public Holder(ClassIndexOffTestCase.Item item)
			{
				_item = item;
			}
		}

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
			new ClassIndexOffTestCase().RunSolo();
		}

		/// <exception cref="Exception"></exception>
		protected override void Configure(IConfiguration config)
		{
			base.Configure(config);
			config.ObjectClass(typeof(ClassIndexOffTestCase.Item)).Indexed(false);
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			ClassIndexOffTestCase.Item item = new ClassIndexOffTestCase.Item(Name);
			Store(new ClassIndexOffTestCase.Holder(item));
		}

		public virtual void TestNoItemInIndex()
		{
			IStoredClass storedClass = Db().StoredClass(typeof(ClassIndexOffTestCase.Item));
			Assert.IsFalse(storedClass.HasClassIndex());
			AssertNoItemFoundByQuery();
			Db().Commit();
			AssertNoItemFoundByQuery();
		}

		private void AssertNoItemFoundByQuery()
		{
			IQuery q = Db().Query();
			q.Constrain(typeof(ClassIndexOffTestCase.Item));
			Assert.AreEqual(0, q.Execute().Size());
		}

		public virtual void TestRetrievalThroughHolder()
		{
			AssertData();
		}

		private void AssertData()
		{
			ClassIndexOffTestCase.Holder holder = (ClassIndexOffTestCase.Holder)RetrieveOnlyInstance
				(typeof(ClassIndexOffTestCase.Holder));
			Assert.IsNotNull(holder._item);
			Assert.AreEqual(Name, holder._item._name);
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestDefragment()
		{
			Defragment();
			AssertData();
		}
	}
}

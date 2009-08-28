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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Querying;

namespace Db4objects.Db4o.Tests.Common.Querying
{
	public class QueryingVersionFieldTestCase : AbstractDb4oTestCase
	{
		public static void Main(string[] arguments)
		{
			new QueryingVersionFieldTestCase().RunSolo();
		}

		public class Item
		{
			public string name;

			public Item(string name_)
			{
				name = name_;
			}
		}

		/// <exception cref="Exception"></exception>
		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(typeof(QueryingVersionFieldTestCase.Item)).GenerateVersionNumbers
				(true);
		}

		public virtual void Test()
		{
			StoreItems(new string[] { "1", "2", "3" });
			Db().Commit();
			long initialTransactionVersionNumber = Db().Version();
			UpdateItem("2", "modified2");
			Db().Commit();
			long updatedTransactionVersionNumber = Db().Version();
			IQuery q = Db().Query();
			q.Constrain(typeof(QueryingVersionFieldTestCase.Item));
			q.Descend(VirtualField.Version).Constrain(initialTransactionVersionNumber).Greater
				();
			// This part really isn't needed for this test case, but it shows, how changes
			// between two specific transaction commits can be queried.
			q.Descend(VirtualField.Version).Constrain(updatedTransactionVersionNumber).Smaller
				().Equal();
			IObjectSet objectSet = q.Execute();
			Assert.AreEqual(1, objectSet.Size());
			QueryingVersionFieldTestCase.Item item = (QueryingVersionFieldTestCase.Item)objectSet
				.Next();
			Assert.AreEqual("modified2", item.name);
		}

		private void UpdateItem(string originalName, string updatedName)
		{
			QueryingVersionFieldTestCase.Item item = QueryForItem(originalName);
			item.name = updatedName;
			Store(item);
		}

		private QueryingVersionFieldTestCase.Item QueryForItem(string name)
		{
			IQuery q = NewQuery(typeof(QueryingVersionFieldTestCase.Item));
			q.Descend("name").Constrain(name);
			IObjectSet objectSet = q.Execute();
			Assert.AreEqual(1, objectSet.Size());
			return (QueryingVersionFieldTestCase.Item)objectSet.Next();
		}

		private void StoreItems(string[] names)
		{
			for (int i = 0; i < names.Length; i++)
			{
				QueryingVersionFieldTestCase.Item item = new QueryingVersionFieldTestCase.Item(names
					[i]);
				Store(item);
			}
		}
	}
}

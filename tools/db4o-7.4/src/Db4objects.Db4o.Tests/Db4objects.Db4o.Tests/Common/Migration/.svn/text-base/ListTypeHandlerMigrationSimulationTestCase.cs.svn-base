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
using System.Collections;
using System.IO;
using Db4oUnit;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Foundation.IO;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Migration;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Tests.Common.Migration
{
	/// <exclude></exclude>
	public class ListTypeHandlerMigrationSimulationTestCase : ITestLifeCycle
	{
		public class Item
		{
			public IList list;
		}

		private string _fileName;

		internal bool _useListTypeHandler;

		/// <exception cref="Exception"></exception>
		public virtual void SetUp()
		{
			_fileName = Path.GetTempFileName();
			File4.Delete(_fileName);
		}

		/// <exception cref="Exception"></exception>
		public virtual void TearDown()
		{
			File4.Delete(_fileName);
		}

		public virtual void TestMigration()
		{
			_useListTypeHandler = false;
			StoreItemWithListElement("one");
			StoreItemWithListElement("two");
			StoreItemWithListElement("three");
			StoreItemWithListElement(42);
			AssertSingleItemElementQuery("one");
			AssertNoItemFoundByElement("four");
			_useListTypeHandler = true;
			AssertSingleItemElementQuery("one");
			AssertSingleItemElementQuery(42);
			UpdateItemByListElement("one", "newOne");
			AssertNoItemFoundByElement("one");
			AssertSingleItemElementQuery("two");
		}

		private void AssertSingleItemElementQuery(object element)
		{
			IObjectContainer db = OpenContainer();
			try
			{
				ListTypeHandlerMigrationSimulationTestCase.Item item = RetrieveItemByElement(element
					, db);
				object listElement = item.list[0];
				Assert.AreEqual(element, listElement);
			}
			finally
			{
				db.Close();
			}
		}

		private ListTypeHandlerMigrationSimulationTestCase.Item RetrieveItemByElement(object
			 element, IObjectContainer db)
		{
			IQuery q = db.Query();
			q.Constrain(typeof(ListTypeHandlerMigrationSimulationTestCase.Item));
			q.Descend("list").Constrain(element);
			IObjectSet objectSet = q.Execute();
			Assert.AreEqual(1, objectSet.Size());
			ListTypeHandlerMigrationSimulationTestCase.Item item = (ListTypeHandlerMigrationSimulationTestCase.Item
				)objectSet.Next();
			return item;
		}

		private void AssertNoItemFoundByElement(object element)
		{
			IObjectContainer db = OpenContainer();
			try
			{
				IQuery q = db.Query();
				q.Constrain(typeof(ListTypeHandlerMigrationSimulationTestCase.Item));
				q.Descend("list").Constrain(element);
				IObjectSet objectSet = q.Execute();
				Assert.AreEqual(0, objectSet.Size());
			}
			finally
			{
				db.Close();
			}
		}

		private void UpdateItemByListElement(object oldElement, object newElement)
		{
			IObjectContainer db = OpenContainer();
			try
			{
				ListTypeHandlerMigrationSimulationTestCase.Item item = RetrieveItemByElement(oldElement
					, db);
				item.list.Clear();
				item.list.Add(newElement);
				db.Store(item.list);
				db.Store(item);
			}
			finally
			{
				db.Close();
			}
		}

		private void StoreItemWithListElement(object element)
		{
			ListTypeHandlerMigrationSimulationTestCase.Item item = new ListTypeHandlerMigrationSimulationTestCase.Item
				();
			item.list = new ArrayList();
			item.list.Add(element);
			IObjectContainer db = OpenContainer();
			try
			{
				db.Store(item);
			}
			finally
			{
				db.Close();
			}
		}

		private void Store(ListTypeHandlerMigrationSimulationTestCase.Item item)
		{
			IObjectContainer db = OpenContainer();
			try
			{
				db.Store(item);
			}
			finally
			{
				db.Close();
			}
		}

		private void UpdateItem()
		{
			IObjectContainer db = OpenContainer();
			try
			{
				IObjectSet objectSet = db.Query(typeof(ListTypeHandlerMigrationSimulationTestCase.Item
					));
				db.Store(objectSet.Next());
			}
			finally
			{
				db.Close();
			}
		}

		private IObjectContainer OpenContainer()
		{
			IConfiguration configuration = Db4oFactory.NewConfiguration();
			if (_useListTypeHandler)
			{
				configuration.RegisterTypeHandler(new SingleClassTypeHandlerPredicate(typeof(ArrayList
					)), new ListTypeHandler());
			}
			IObjectContainer db = Db4oFactory.OpenFile(configuration, _fileName);
			return db;
		}
	}
}

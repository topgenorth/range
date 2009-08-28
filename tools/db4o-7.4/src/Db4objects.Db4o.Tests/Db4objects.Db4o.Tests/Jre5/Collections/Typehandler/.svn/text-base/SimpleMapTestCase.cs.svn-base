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
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Jre5.Collections.Typehandler;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Tests.Jre5.Collections.Typehandler
{
	/// <exclude></exclude>
	public class SimpleMapTestCase : AbstractDb4oTestCase
	{
		public class Item
		{
			public IDictionary map;
		}

		public class FirstClassElement
		{
			public string name;

			public FirstClassElement(string name_)
			{
				name = name_;
			}
		}

		/// <exception cref="Exception"></exception>
		protected override void Configure(IConfiguration config)
		{
			config.RegisterTypeHandler(new SingleClassTypeHandlerPredicate(typeof(Hashtable))
				, new MapTypeHandler());
			config.ObjectClass(typeof(SimpleMapTestCase.Item)).CascadeOnDelete(true);
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			SimpleMapTestCase.Item item = new SimpleMapTestCase.Item();
			item.map = new Hashtable();
			item.map.Add("zero", "zero");
			item.map.Add(new SimpleMapTestCase.FirstClassElement("one"), "one");
			Store(item);
		}

		public virtual void TestRetrieveInstance()
		{
			SimpleMapTestCase.Item item = (SimpleMapTestCase.Item)RetrieveOnlyInstance(typeof(
				SimpleMapTestCase.Item));
			Assert.AreEqual("zero", item.map["zero"]);
		}

		public virtual void TestQuery()
		{
			IQuery q = Db().Query();
			q.Constrain(typeof(SimpleMapTestCase.Item));
			q.Descend("map").Constrain("zero");
			IObjectSet objectSet = q.Execute();
			Assert.AreEqual(1, objectSet.Size());
			SimpleMapTestCase.Item item = (SimpleMapTestCase.Item)objectSet.Next();
			Assert.AreEqual("zero", item.map["zero"]);
		}

		public virtual void TestDeletion()
		{
			AssertObjectCount(typeof(SimpleMapTestCase.FirstClassElement), 1);
			SimpleMapTestCase.Item item = (SimpleMapTestCase.Item)RetrieveOnlyInstance(typeof(
				SimpleMapTestCase.Item));
			Db().Delete(item);
			AssertObjectCount(typeof(SimpleMapTestCase.FirstClassElement), 0);
		}

		private void AssertObjectCount(Type clazz, int count)
		{
			Assert.AreEqual(count, Db().Query(clazz).Size());
		}
	}
}

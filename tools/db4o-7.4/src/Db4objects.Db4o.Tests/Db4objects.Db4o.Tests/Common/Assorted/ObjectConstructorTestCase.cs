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
using Db4objects.Db4o.Tests.Common.Assorted;

namespace Db4objects.Db4o.Tests.Common.Assorted
{
	public class ObjectConstructorTestCase : AbstractDb4oTestCase
	{
		public class Item
		{
			internal readonly string _name;

			public Item(string name)
			{
				_name = name;
			}
		}

		public class ItemConstructor : IObjectConstructor
		{
			public virtual object OnInstantiate(IObjectContainer container, object storedObject
				)
			{
				return new ObjectConstructorTestCase.Item((string)storedObject);
			}

			public virtual void OnActivate(IObjectContainer container, object applicationObject
				, object storedObject)
			{
			}

			public virtual object OnStore(IObjectContainer container, object applicationObject
				)
			{
				return ((ObjectConstructorTestCase.Item)applicationObject)._name;
			}

			public virtual Type StoredClass()
			{
				return typeof(string);
			}
		}

		/// <exception cref="Exception"></exception>
		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(typeof(ObjectConstructorTestCase.Item)).Translate(new ObjectConstructorTestCase.ItemConstructor
				());
		}

		protected override void Store()
		{
			Store(new ObjectConstructorTestCase.Item("one"));
		}

		public virtual void Test()
		{
			ObjectConstructorTestCase.Item item = (ObjectConstructorTestCase.Item)RetrieveOnlyInstance
				(typeof(ObjectConstructorTestCase.Item));
			Assert.AreEqual("one", item._name);
		}
	}
}

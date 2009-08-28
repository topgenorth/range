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
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Tests.Common.Config;

namespace Db4objects.Db4o.Tests.Common.Config
{
	public class ObjectTranslatorTestCase : AbstractDb4oTestCase
	{
		public class Thing
		{
			public string name;

			public Thing(string name)
			{
				this.name = name;
			}
		}

		public class ThingCounterTranslator : IObjectConstructor
		{
			private Hashtable4 _countCache = new Hashtable4();

			public virtual void OnActivate(IObjectContainer container, object applicationObject
				, object storedObject)
			{
			}

			public virtual object OnStore(IObjectContainer container, object applicationObject
				)
			{
				ObjectTranslatorTestCase.Thing t = (ObjectTranslatorTestCase.Thing)applicationObject;
				AddToCache(t);
				return t.name;
			}

			private void AddToCache(ObjectTranslatorTestCase.Thing t)
			{
				object o = (object)_countCache.Get(t.name);
				if (o == null)
				{
					o = 0;
				}
				_countCache.Put(t.name, ((int)o) + 1);
			}

			public virtual int GetCount(ObjectTranslatorTestCase.Thing t)
			{
				object o = (int)_countCache.Get(t.name);
				if (o == null)
				{
					return 0;
				}
				return ((int)o);
			}

			public virtual object OnInstantiate(IObjectContainer container, object storedObject
				)
			{
				string name = (string)storedObject;
				return new ObjectTranslatorTestCase.Thing(name);
			}

			public virtual Type StoredClass()
			{
				return typeof(string);
			}
		}

		private ObjectTranslatorTestCase.ThingCounterTranslator _trans;

		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(typeof(ObjectTranslatorTestCase.Thing)).Translate(_trans = new 
				ObjectTranslatorTestCase.ThingCounterTranslator());
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			Db().Store(new ObjectTranslatorTestCase.Thing("jbe"));
		}

		public virtual void _testTranslationCount()
		{
			ObjectTranslatorTestCase.Thing t = (ObjectTranslatorTestCase.Thing)RetrieveOnlyInstance
				(typeof(ObjectTranslatorTestCase.Thing));
			Assert.IsNotNull(t);
			Assert.AreEqual("jbe", t.name);
			Assert.AreEqual(1, _trans.GetCount(t));
		}

		public static void Main(string[] args)
		{
			new ObjectTranslatorTestCase().RunSolo();
		}
	}
}

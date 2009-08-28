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
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Handlers;
using Db4objects.Db4o.Tests.Common.Handlers;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Tests.Common.Handlers
{
	public class SecondClassTestCase : AbstractDb4oTestCase
	{
		internal static Hashtable4 objectIsSecondClass;

		public class Item
		{
		}

		public class CustomSecondClassItem
		{
		}

		public class CustomFirstClassItem
		{
		}

		static SecondClassTestCase()
		{
			objectIsSecondClass = new Hashtable4();
			Register(1, true);
			Register(new DateTime(), true);
			Register("astring", true);
			Register(new SecondClassTestCase.Item(), false);
			Register(new int[] { 1 }, false);
			Register(new DateTime[] { new DateTime() }, false);
			Register(new SecondClassTestCase.Item[] { new SecondClassTestCase.Item() }, false
				);
			Register(new SecondClassTestCase.CustomFirstClassItem(), false);
			Register(new SecondClassTestCase.CustomSecondClassItem(), true);
		}

		private static void Register(object obj, bool isSecondClass)
		{
			objectIsSecondClass.Put(obj, isSecondClass);
		}

		public class FirstClassTypeHandler : FirstClassObjectHandler
		{
		}

		public class SecondClassTypeHandler : FirstClassObjectHandler, IEmbeddedTypeHandler
		{
		}

		/// <exception cref="Exception"></exception>
		protected override void Configure(IConfiguration config)
		{
			config.RegisterTypeHandler(new SingleClassTypeHandlerPredicate(typeof(SecondClassTestCase.CustomFirstClassItem
				)), new SecondClassTestCase.FirstClassTypeHandler());
			config.RegisterTypeHandler(new SingleClassTypeHandlerPredicate(typeof(SecondClassTestCase.CustomSecondClassItem
				)), new SecondClassTestCase.SecondClassTypeHandler());
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			Store(new SecondClassTestCase.Item());
			Store(new SecondClassTestCase.CustomFirstClassItem());
			Store(new SecondClassTestCase.CustomSecondClassItem());
		}

		public virtual void Test()
		{
			IEnumerator i = objectIsSecondClass.Keys();
			while (i.MoveNext())
			{
				object currentObject = i.Current;
				bool isSecondClass = ((bool)objectIsSecondClass.Get(currentObject));
				ClassMetadata classMetadata = Container().ClassMetadataForObject(currentObject);
				Assert.AreEqual(isSecondClass, classMetadata.IsSecondClass());
			}
		}
	}
}

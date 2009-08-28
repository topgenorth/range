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
using Db4objects.Db4o.Tests.Common.Handlers;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Tests.Common.Handlers
{
	public class IgnoreFieldsTypeHandlerTestCase : AbstractDb4oTestCase
	{
		public class Item1
		{
			public int id1;
		}

		public class Item2 : IgnoreFieldsTypeHandlerTestCase.Item1
		{
			public int id2;
		}

		public class Item3 : IgnoreFieldsTypeHandlerTestCase.Item2
		{
			public int id3;
		}

		public class Item4 : IgnoreFieldsTypeHandlerTestCase.Item3
		{
			public int id4;
		}

		public class Item5 : IgnoreFieldsTypeHandlerTestCase.Item4
		{
			public int id5;
		}

		/// <exception cref="Exception"></exception>
		protected override void Configure(IConfiguration config)
		{
			config.RegisterTypeHandler(new SingleClassTypeHandlerPredicate(typeof(IgnoreFieldsTypeHandlerTestCase.Item2
				)), new IgnoreFieldsTypeHandler());
			config.RegisterTypeHandler(new SingleClassTypeHandlerPredicate(typeof(IgnoreFieldsTypeHandlerTestCase.Item4
				)), new IgnoreFieldsTypeHandler());
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			IgnoreFieldsTypeHandlerTestCase.Item5 item = new IgnoreFieldsTypeHandlerTestCase.Item5
				();
			item.id1 = 1;
			item.id2 = 2;
			item.id3 = 3;
			item.id4 = 4;
			item.id5 = 5;
			Store(item);
		}

		public virtual void Test()
		{
			IgnoreFieldsTypeHandlerTestCase.Item5 item = (IgnoreFieldsTypeHandlerTestCase.Item5
				)RetrieveOnlyInstance(typeof(IgnoreFieldsTypeHandlerTestCase.Item5));
			Assert.AreEqual(1, item.id1);
			Assert.AreEqual(0, item.id2);
			Assert.AreEqual(3, item.id3);
			Assert.AreEqual(0, item.id4);
			Assert.AreEqual(5, item.id5);
		}
	}
}

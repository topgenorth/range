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
using Db4oUnit.Extensions;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Tests.Jre5.Collections.Typehandler;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Tests.Jre5.Collections.Typehandler
{
	/// <exclude></exclude>
	public class ListTypeHandlerCascadedDeleteTestCase : AbstractDb4oTestCase
	{
		/// <param name="args"></param>
		public static void Main(string[] args)
		{
			new ListTypeHandlerCascadedDeleteTestCase().RunSolo();
		}

		public class Item
		{
			public object _untypedList;

			public ArrayList _typedList;
		}

		public class Element
		{
		}

		/// <exception cref="Exception"></exception>
		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(typeof(ListTypeHandlerCascadedDeleteTestCase.Item)).CascadeOnDelete
				(true);
			config.ObjectClass(typeof(ArrayList)).CascadeOnDelete(true);
			config.RegisterTypeHandler(new SingleClassTypeHandlerPredicate(typeof(ArrayList))
				, new ListTypeHandler());
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			ListTypeHandlerCascadedDeleteTestCase.Item item = new ListTypeHandlerCascadedDeleteTestCase.Item
				();
			item._untypedList = new ArrayList();
			((IList)item._untypedList).Add(new ListTypeHandlerCascadedDeleteTestCase.Element(
				));
			item._typedList = new ArrayList();
			item._typedList.Add(new ListTypeHandlerCascadedDeleteTestCase.Element());
			Store(item);
		}

		public virtual void TestCascadedDelete()
		{
			ListTypeHandlerCascadedDeleteTestCase.Item item = (ListTypeHandlerCascadedDeleteTestCase.Item
				)RetrieveOnlyInstance(typeof(ListTypeHandlerCascadedDeleteTestCase.Item));
			Db4oAssert.PersistedCount(2, typeof(ListTypeHandlerCascadedDeleteTestCase.Element
				));
			Db().Delete(item);
			Db().Purge();
			Db().Commit();
			Db4oAssert.PersistedCount(0, typeof(ListTypeHandlerCascadedDeleteTestCase.Item));
			Db4oAssert.PersistedCount(0, typeof(ArrayList));
			Db4oAssert.PersistedCount(0, typeof(ListTypeHandlerCascadedDeleteTestCase.Element
				));
		}

		public virtual void TestArrayListCount()
		{
			Db4oAssert.PersistedCount(2, typeof(ArrayList));
		}
	}
}

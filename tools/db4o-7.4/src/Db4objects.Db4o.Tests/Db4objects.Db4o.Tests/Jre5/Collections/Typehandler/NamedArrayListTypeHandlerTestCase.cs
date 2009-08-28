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
	public class NamedArrayListTypeHandlerTestCase : AbstractDb4oTestCase
	{
		private static string Name = "listname";

		private static object[] Data = new object[] { "one", "two", 3, System.Convert.ToInt64
			(4), null };

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			Store(CreateNamedArrayList());
		}

		/// <exception cref="Exception"></exception>
		protected override void Configure(IConfiguration config)
		{
			config.RegisterTypeHandler(new SingleClassTypeHandlerPredicate(typeof(ArrayList))
				, new ListTypeHandler());
		}

		private NamedArrayList CreateNamedArrayList()
		{
			NamedArrayList namedArrayList = new NamedArrayList();
			namedArrayList.name = Name;
			for (int i = 0; i < Data.Length; i++)
			{
				namedArrayList.Add(Data[i]);
			}
			return namedArrayList;
		}

		private void AssertRetrievedOK(NamedArrayList namedArrayList)
		{
			Assert.AreEqual(Name, namedArrayList.name);
			object[] listElements = new object[namedArrayList.Count];
			int idx = 0;
			IEnumerator i = namedArrayList.GetEnumerator();
			while (i.MoveNext())
			{
				listElements[idx++] = i.Current;
			}
			ArrayAssert.AreEqual(Data, listElements);
		}

		public virtual void TestRetrieve()
		{
			NamedArrayList namedArrayList = (NamedArrayList)RetrieveOnlyInstance(typeof(NamedArrayList
				));
			AssertRetrievedOK(namedArrayList);
		}

		public virtual void TestQuery()
		{
			IQuery query = NewQuery(typeof(NamedArrayList));
			query.Descend("name").Constrain(Name);
			IObjectSet objectSet = query.Execute();
			Assert.AreEqual(1, objectSet.Size());
			NamedArrayList namedArrayList = (NamedArrayList)objectSet.Next();
			AssertRetrievedOK(namedArrayList);
		}
	}
}

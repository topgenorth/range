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
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Querying;

namespace Db4objects.Db4o.Tests.Common.Querying
{
	/// <exclude></exclude>
	public class ObjectSetTestCase : AbstractDb4oTestCase
	{
		public static void Main(string[] args)
		{
			new ObjectSetTestCase().RunSoloAndClientServer();
		}

		public class Item
		{
			public string name;

			public Item()
			{
			}

			public Item(string name_)
			{
				name = name_;
			}

			public override string ToString()
			{
				return "Item(\"" + name + "\")";
			}
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			Db().Store(new ObjectSetTestCase.Item("foo"));
			Db().Store(new ObjectSetTestCase.Item("bar"));
			Db().Store(new ObjectSetTestCase.Item("baz"));
		}

		public virtual void TestObjectsCantBeSeenAfterDelete()
		{
			Transaction trans1 = NewTransaction();
			Transaction trans2 = NewTransaction();
			IObjectSet os = QueryItems(trans1);
			DeleteItemAndCommit(trans2, "foo");
			AssertItems(new string[] { "bar", "baz" }, os);
		}

		public virtual void TestAccessOrder()
		{
			IObjectSet result = NewQuery(typeof(ObjectSetTestCase.Item)).Execute();
			for (int i = 0; i < result.Size(); ++i)
			{
				Assert.IsTrue(result.HasNext());
				Assert.AreSame(result.Ext().Get(i), result.Next());
			}
			Assert.IsFalse(result.HasNext());
		}

		private void AssertItems(string[] expectedNames, IObjectSet actual)
		{
			for (int i = 0; i < expectedNames.Length; i++)
			{
				Assert.IsTrue(actual.HasNext());
				Assert.AreEqual(expectedNames[i], ((ObjectSetTestCase.Item)actual.Next()).name);
			}
			Assert.IsFalse(actual.HasNext());
		}

		private void DeleteItemAndCommit(Transaction trans, string name)
		{
			Container().Delete(trans, QueryItem(trans, name));
			trans.Commit();
		}

		private ObjectSetTestCase.Item QueryItem(Transaction trans, string name)
		{
			IQuery q = NewQuery(trans, typeof(ObjectSetTestCase.Item));
			q.Descend("name").Constrain(name);
			return (ObjectSetTestCase.Item)q.Execute().Next();
		}

		private IObjectSet QueryItems(Transaction trans)
		{
			IQuery q = NewQuery(trans, typeof(ObjectSetTestCase.Item));
			q.Descend("name").OrderAscending();
			return q.Execute();
		}
	}
}

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
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.TA;
using Db4objects.Db4o.Tests.Common.TA.Nonta;

namespace Db4objects.Db4o.Tests.Common.TA.Nonta
{
	public class NonTARefreshTestCase : TransparentActivationTestCaseBase, IOptOutSolo
	{
		public static void Main(string[] args)
		{
			new NonTARefreshTestCase().RunClientServer();
		}

		private const int ItemDepth = 10;

		private Type _class;

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			NonTARefreshTestCase.TAItem item = NonTARefreshTestCase.TAItem.NewTAItem(ItemDepth
				);
			item._isRoot = true;
			_class = item.GetType();
			Store(item);
		}

		public virtual void TestRefresh()
		{
			IExtObjectContainer client1 = OpenNewClient();
			IExtObjectContainer client2 = OpenNewClient();
			NonTARefreshTestCase.TAItem item1 = (NonTARefreshTestCase.TAItem)RetrieveInstance
				(client1);
			NonTARefreshTestCase.TAItem item2 = (NonTARefreshTestCase.TAItem)RetrieveInstance
				(client2);
			AssertDescendingRange(10, item1);
			AssertDescendingRange(10, item2);
			item1.Value(100);
			item1.Next().Value(200);
			client1.Store(item1, 2);
			client1.Commit();
			Assert.AreEqual(100, item1.Value());
			Assert.AreEqual(200, item1.Next().Value());
			Assert.AreEqual(10, item2.Value());
			Assert.AreEqual(9, item2.Next().Value());
			//refresh 0
			client2.Refresh(item2, 0);
			Assert.AreEqual(10, item2.Value());
			Assert.AreEqual(9, item2.Next().Value());
			//refresh 1
			client2.Refresh(item2, 1);
			Assert.AreEqual(100, item2.Value());
			Assert.AreEqual(9, item2.Next().Value());
			//refresh 2
			client2.Refresh(item2, 2);
			Assert.AreEqual(100, item2.Value());
			Assert.AreEqual(200, item2.Next().Value());
			UpdateAscendingWithRange(item1, 1000);
			client1.Store(item1, 5);
			client1.Commit();
			client2.Refresh(item2, 5);
			NonTARefreshTestCase.TAItem next2 = item2;
			for (int i = 1000; i < 1005; i++)
			{
				Assert.AreEqual(i, next2.Value());
				next2 = next2.Next();
			}
			client1.Close();
			client2.Close();
		}

		private void UpdateAscendingWithRange(NonTARefreshTestCase.TAItem item, int startingValue
			)
		{
			NonTARefreshTestCase.TAItem current = item;
			while (current != null)
			{
				current.Value(startingValue);
				current = current.Next();
				startingValue++;
			}
		}

		private void AssertDescendingRange(int startingValue, NonTARefreshTestCase.TAItem
			 item)
		{
			NonTARefreshTestCase.TAItem current = item;
			while (current != null)
			{
				Assert.AreEqual(startingValue, current.Value());
				current = current.Next();
				startingValue--;
			}
		}

		private object RetrieveInstance(IExtObjectContainer client)
		{
			IQuery query = client.Query();
			query.Constrain(_class);
			query.Descend("_isRoot").Constrain(true);
			return query.Execute().Next();
		}

		private IExtObjectContainer OpenNewClient()
		{
			return ((IDb4oClientServerFixture)Fixture()).OpenNewClient();
		}

		public class TAItem
		{
			public int _value;

			public NonTARefreshTestCase.TAItem _next;

			public bool _isRoot;

			public static NonTARefreshTestCase.TAItem NewTAItem(int depth)
			{
				if (depth == 0)
				{
					return null;
				}
				NonTARefreshTestCase.TAItem root = new NonTARefreshTestCase.TAItem();
				root._value = depth;
				root._next = NewTAItem(depth - 1);
				return root;
			}

			public virtual int Value()
			{
				return _value;
			}

			public virtual void Value(int value)
			{
				_value = value;
			}

			public virtual NonTARefreshTestCase.TAItem Next()
			{
				return _next;
			}
		}
	}
}

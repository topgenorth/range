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
using Db4objects.Db4o.Activation;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.TA;
using Db4objects.Db4o.Tests.Common.TA.TA;

namespace Db4objects.Db4o.Tests.Common.TA.TA
{
	public class TAActivateTestCase : TAItemTestCaseBase
	{
		public static void Main(string[] args)
		{
			new TAActivateTestCase().RunAll();
		}

		private const int ItemDepth = 10;

		/// <exception cref="Exception"></exception>
		protected override void AssertItemValue(object obj)
		{
			TAActivateTestCase.TAItem taItem = (TAActivateTestCase.TAItem)obj;
			for (int i = 0; i < ItemDepth - 1; i++)
			{
				Assert.AreEqual("TAItem " + (ItemDepth - i), taItem.GetName());
				Assert.AreEqual(ItemDepth - i, taItem.GetValue());
				Assert.IsNotNull(taItem.Next());
				taItem = taItem.Next();
			}
			Assert.AreEqual("TAItem 1", taItem.GetName());
			Assert.AreEqual(1, taItem.GetValue());
			Assert.IsNull(taItem.Next());
		}

		/// <exception cref="Exception"></exception>
		protected override void AssertRetrievedItem(object obj)
		{
			TAActivateTestCase.TAItem taItem = (TAActivateTestCase.TAItem)obj;
			AssertNullItem(taItem);
			// depth = 0, no effect
			Db().Activate(taItem, 0);
			AssertNullItem(taItem);
			// depth = 1
			Db().Activate(taItem, 1);
			AssertActivatedItem(taItem, 0, 1);
			// depth = 5
			Db().Activate(taItem, 5);
			AssertActivatedItem(taItem, 0, 5);
			Db().Activate(taItem, ItemDepth + 100);
			AssertActivatedItem(taItem, 0, ItemDepth);
		}

		private void AssertActivatedItem(TAActivateTestCase.TAItem item, int from, int depth
			)
		{
			if (depth > ItemDepth)
			{
				throw new ArgumentException("depth should not be greater than ITEM_DEPTH.");
			}
			TAActivateTestCase.TAItem next = item;
			for (int i = from; i < depth; i++)
			{
				Assert.AreEqual("TAItem " + (ItemDepth - i), next._name);
				Assert.AreEqual(ItemDepth - i, next._value);
				if (i < ItemDepth - 1)
				{
					Assert.IsNotNull(next._next);
				}
				next = next._next;
			}
			if (depth < ItemDepth)
			{
				AssertNullItem(next);
			}
		}

		private void AssertNullItem(TAActivateTestCase.TAItem taItem)
		{
			Assert.IsNull(taItem._name);
			Assert.IsNull(taItem._next);
			Assert.AreEqual(0, taItem._value);
		}

		public override object RetrieveOnlyInstance(Type clazz)
		{
			IQuery q = Db().Query();
			q.Constrain(clazz);
			q.Descend("_isRoot").Constrain(true);
			return q.Execute().Next();
		}

		/// <exception cref="Exception"></exception>
		protected override object CreateItem()
		{
			TAActivateTestCase.TAItem taItem = TAActivateTestCase.TAItem.NewTAItem(ItemDepth);
			taItem._isRoot = true;
			return taItem;
		}

		public class TAItem : ActivatableImpl
		{
			public string _name;

			public int _value;

			public TAActivateTestCase.TAItem _next;

			public bool _isRoot;

			public static TAActivateTestCase.TAItem NewTAItem(int depth)
			{
				if (depth == 0)
				{
					return null;
				}
				TAActivateTestCase.TAItem root = new TAActivateTestCase.TAItem();
				root._name = "TAItem " + depth;
				root._value = depth;
				root._next = NewTAItem(depth - 1);
				return root;
			}

			public virtual string GetName()
			{
				Activate(ActivationPurpose.Read);
				return _name;
			}

			public virtual int GetValue()
			{
				Activate(ActivationPurpose.Read);
				return _value;
			}

			public virtual TAActivateTestCase.TAItem Next()
			{
				Activate(ActivationPurpose.Read);
				return _next;
			}
		}
	}
}

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
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Jre5.Collections.Typehandler;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Tests.Jre5.Collections.Typehandler
{
	public abstract class TypeHandlerTestUnitBase : AbstractDb4oTestCase, IOptOutDefragSolo
	{
		protected abstract AbstractItemFactory ItemFactory();

		protected abstract ITypeHandler4 TypeHandler();

		protected abstract void FillItem(object item);

		protected abstract void AssertContent(object item);

		protected abstract ListTypeHandlerTestElementsSpec ElementsSpec();

		/// <exception cref="Exception"></exception>
		protected override void Configure(IConfiguration config)
		{
			ITypeHandler4 typeHandler = TypeHandler();
			if (typeHandler != null)
			{
				config.RegisterTypeHandler(new SingleClassTypeHandlerPredicate(ItemFactory().ContainerClass
					()), typeHandler);
			}
			config.ObjectClass(ItemFactory().ItemClass()).CascadeOnDelete(true);
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			AbstractItemFactory factory = ItemFactory();
			object item = factory.NewItem();
			FillItem(item);
			Store(item);
		}

		protected virtual int ExpectedElementCount()
		{
			return Elements().Length + 1;
		}

		protected virtual object[] Elements()
		{
			return ElementsSpec()._elements;
		}

		protected virtual object[] Values()
		{
			return ValuesSpec()._elements;
		}

		protected virtual object NotContained()
		{
			return ElementsSpec()._notContained;
		}

		protected virtual object LargeElement()
		{
			return ElementsSpec()._largeElement;
		}

		protected virtual Type ElementClass()
		{
			return ElementsSpec()._notContained.GetType();
		}

		private ListTypeHandlerTestElementsSpec ValuesSpec()
		{
			return (ListTypeHandlerTestElementsSpec)MapTypeHandlerTestVariables.MapValuesSpec
				.Value;
		}

		protected virtual void AssertQueryResult(IQuery q, bool successful)
		{
			if (successful)
			{
				AssertSuccessfulQueryResult(q);
			}
			else
			{
				AssertEmptyQueryResult(q);
			}
		}

		protected virtual IList ListFromItem(object item)
		{
			try
			{
				return (IList)item.GetType().GetField(AbstractItemFactory.ListFieldName).GetValue
					(item);
			}
			catch (Exception exc)
			{
				throw new Exception(string.Empty, exc);
			}
		}

		protected virtual IDictionary MapFromItem(object item)
		{
			try
			{
				return (IDictionary)item.GetType().GetField(AbstractItemFactory.MapFieldName).GetValue
					(item);
			}
			catch (Exception exc)
			{
				throw new Exception(string.Empty, exc);
			}
		}

		private void AssertEmptyQueryResult(IQuery q)
		{
			IObjectSet set = q.Execute();
			Assert.AreEqual(0, set.Size());
		}

		private void AssertSuccessfulQueryResult(IQuery q)
		{
			IObjectSet set = q.Execute();
			Assert.AreEqual(1, set.Size());
			object item = set.Next();
			AssertContent(item);
		}

		protected virtual void FillListItem(object item)
		{
			IList list = ListFromItem(item);
			for (int eltIdx = 0; eltIdx < Elements().Length; eltIdx++)
			{
				list.Add(Elements()[eltIdx]);
			}
			list.Add(null);
		}

		protected virtual void FillMapItem(object item)
		{
			IDictionary map = MapFromItem(item);
			for (int eltIdx = 0; eltIdx < Elements().Length; eltIdx++)
			{
				map.Add(Elements()[eltIdx], Values()[eltIdx]);
			}
		}

		protected virtual void AssertListContent(object item)
		{
			IList list = ListFromItem(item);
			Assert.AreEqual(ItemFactory().ContainerClass(), list.GetType());
			Assert.AreEqual(ExpectedElementCount(), list.Count);
			for (int eltIdx = 0; eltIdx < Elements().Length; eltIdx++)
			{
				Assert.AreEqual(Elements()[eltIdx], list[eltIdx]);
			}
			Assert.IsNull(list[Elements().Length]);
		}

		protected virtual void AssertMapContent(object item)
		{
			IDictionary map = MapFromItem(item);
			Assert.AreEqual(ItemFactory().ContainerClass(), map.GetType());
			Assert.AreEqual(Elements().Length, map.Count);
			for (int eltIdx = 0; eltIdx < Elements().Length; eltIdx++)
			{
				Assert.AreEqual(Values()[eltIdx], map[Elements()[eltIdx]]);
			}
		}
	}
}

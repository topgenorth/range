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
	/// <exclude></exclude>
	public class SimpleListQueryTestCase : AbstractDb4oTestCase
	{
		public class Item
		{
			public IList list;
		}

		public class FirstClassElement
		{
			public string name;

			public FirstClassElement(string name_)
			{
				name = name_;
			}

			public override bool Equals(object obj)
			{
				if (!(obj is SimpleListQueryTestCase.FirstClassElement))
				{
					return false;
				}
				SimpleListQueryTestCase.FirstClassElement other = (SimpleListQueryTestCase.FirstClassElement
					)obj;
				if (name == null)
				{
					return other.name == null;
				}
				return name.Equals(other.name);
			}
		}

		internal static readonly object[] Data = new object[] { "one", "two", 1, 2, 42, new 
			SimpleListQueryTestCase.FirstClassElement("one"), new SimpleListQueryTestCase.FirstClassElement
			("fortytwo") };

		/// <exception cref="Exception"></exception>
		protected override void Configure(IConfiguration config)
		{
			config.RegisterTypeHandler(new SingleClassTypeHandlerPredicate(typeof(ArrayList))
				, new ListTypeHandler());
			config.ObjectClass(typeof(SimpleListQueryTestCase.Item)).CascadeOnDelete(true);
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			for (int i = 0; i < Data.Length; i++)
			{
				StoreItem(Data[i]);
			}
		}

		private void StoreItem(object listElement)
		{
			SimpleListQueryTestCase.Item item = new SimpleListQueryTestCase.Item();
			item.list = new ArrayList();
			item.list.Add(listElement);
			Store(item);
		}

		public virtual void TestListConstrainQuery()
		{
			for (int i = 0; i < Data.Length; i++)
			{
				AssertSingleElementQuery(Data[i]);
			}
		}

		private void AssertSingleElementQuery(object element)
		{
			IQuery q = Db().Query();
			q.Constrain(typeof(SimpleListQueryTestCase.Item));
			q.Descend("list").Constrain(element);
			AssertSingleElementQueryResult(q, element);
		}

		private void AssertSingleElementQueryResult(IQuery query, object element)
		{
			IObjectSet objectSet = query.Execute();
			Assert.AreEqual(1, objectSet.Size());
			SimpleListQueryTestCase.Item item = (SimpleListQueryTestCase.Item)objectSet.Next(
				);
			Assert.AreEqual(element, item.list[0]);
		}
	}
}

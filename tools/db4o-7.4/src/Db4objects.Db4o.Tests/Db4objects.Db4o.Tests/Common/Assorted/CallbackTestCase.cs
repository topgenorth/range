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
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Assorted;

namespace Db4objects.Db4o.Tests.Common.Assorted
{
	/// <summary>Regression test case for COR-1117</summary>
	public class CallbackTestCase : AbstractDb4oTestCase
	{
		public static void Main(string[] args)
		{
			new CallbackTestCase().RunAll();
		}

		public virtual void Test()
		{
			CallbackTestCase.Item item = new CallbackTestCase.Item();
			Store(item);
			Db().Commit();
			Assert.IsTrue(item.IsStored());
			Assert.IsTrue(Db().Ext().IsStored(item));
			IObjectSet result = RetrieveItems();
			Assert.AreEqual(1, result.Size());
			CallbackTestCase.Item retrievedItem = (CallbackTestCase.Item)result.Next();
			retrievedItem.Save();
			result = RetrieveItems();
			Assert.AreEqual(1, result.Size());
		}

		internal virtual IObjectSet RetrieveItems()
		{
			IQuery q = NewQuery();
			q.Constrain(typeof(CallbackTestCase.Item));
			return q.Execute();
		}

		public class Item
		{
			public string test;

			[System.NonSerialized]
			public IObjectContainer _objectContainer;

			public virtual void ObjectOnNew(IObjectContainer container)
			{
				_objectContainer = container;
			}

			public virtual bool IsStored()
			{
				return _objectContainer.Ext().IsStored(this);
			}

			public virtual void Save()
			{
				_objectContainer.Store(this);
				_objectContainer.Commit();
			}
		}
	}
}

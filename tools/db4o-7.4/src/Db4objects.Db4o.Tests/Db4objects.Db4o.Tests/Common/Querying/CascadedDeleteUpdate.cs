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
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Querying;

namespace Db4objects.Db4o.Tests.Common.Querying
{
	public class CascadedDeleteUpdate : AbstractDb4oTestCase
	{
		public class ParentItem
		{
			public object child;
		}

		public class ChildItem
		{
			public object parent1;

			public object parent2;
		}

		public static void Main(string[] arguments)
		{
			//		new CascadedDeleteUpdate().runSolo();
			new CascadedDeleteUpdate().RunClientServer();
		}

		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(typeof(CascadedDeleteUpdate.ParentItem)).CascadeOnDelete(true);
		}

		protected override void Store()
		{
			CascadedDeleteUpdate.ParentItem parentItem1 = new CascadedDeleteUpdate.ParentItem
				();
			CascadedDeleteUpdate.ParentItem parentItem2 = new CascadedDeleteUpdate.ParentItem
				();
			CascadedDeleteUpdate.ChildItem child = new CascadedDeleteUpdate.ChildItem();
			child.parent1 = parentItem1;
			child.parent2 = parentItem2;
			parentItem1.child = child;
			parentItem2.child = child;
			Db().Store(parentItem1);
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestAllObjectStored()
		{
			AssertAllObjectStored();
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestUpdate()
		{
			IQuery q = NewQuery(typeof(CascadedDeleteUpdate.ParentItem));
			IObjectSet objectSet = q.Execute();
			while (objectSet.HasNext())
			{
				Db().Store(objectSet.Next());
			}
			Db().Commit();
			AssertAllObjectStored();
		}

		/// <exception cref="Exception"></exception>
		private void AssertAllObjectStored()
		{
			Reopen();
			IQuery q = NewQuery(typeof(CascadedDeleteUpdate.ParentItem));
			IObjectSet objectSet = q.Execute();
			while (objectSet.HasNext())
			{
				CascadedDeleteUpdate.ParentItem parentItem = (CascadedDeleteUpdate.ParentItem)objectSet
					.Next();
				Db().Refresh(parentItem, 3);
				Assert.IsNotNull(parentItem.child);
			}
		}
	}
}

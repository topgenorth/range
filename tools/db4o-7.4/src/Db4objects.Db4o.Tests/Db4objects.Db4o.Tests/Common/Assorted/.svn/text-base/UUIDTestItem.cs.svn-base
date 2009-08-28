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
using Db4objects.Db4o;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Query;

namespace Db4objects.Db4o.Tests.Common.Assorted
{
	/// <exclude></exclude>
	public class UUIDTestItem
	{
		public string name;

		public UUIDTestItem()
		{
		}

		public UUIDTestItem(string name)
		{
			this.name = name;
		}

		public static void AssertItemsCanBeRetrievedByUUID(IExtObjectContainer container, 
			Hashtable4 uuidCache)
		{
			IQuery q = container.Query();
			q.Constrain(typeof(Db4objects.Db4o.Tests.Common.Assorted.UUIDTestItem));
			IObjectSet objectSet = q.Execute();
			while (objectSet.HasNext())
			{
				Db4objects.Db4o.Tests.Common.Assorted.UUIDTestItem item = (Db4objects.Db4o.Tests.Common.Assorted.UUIDTestItem
					)objectSet.Next();
				Db4oUUID uuid = container.GetObjectInfo(item).GetUUID();
				Assert.IsNotNull(uuid);
				Assert.AreSame(item, container.GetByUUID(uuid));
				Db4oUUID cached = (Db4oUUID)uuidCache.Get(item.name);
				if (cached != null)
				{
					Assert.AreEqual(cached, uuid);
				}
				else
				{
					uuidCache.Put(item.name, uuid);
				}
			}
		}
	}
}

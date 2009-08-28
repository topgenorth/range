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
using Db4objects.Db4o.Tests.Common.Assorted;

namespace Db4objects.Db4o.Tests.Common.Assorted
{
	public class ReAddCascadedDeleteTestCase : AbstractDb4oTestCase
	{
		public static void Main(string[] args)
		{
			new ReAddCascadedDeleteTestCase().RunClientServer();
		}

		public class Item
		{
			public string _name;

			public ReAddCascadedDeleteTestCase.Item _member;

			public Item()
			{
			}

			public Item(string name)
			{
				_name = name;
			}

			public Item(string name, ReAddCascadedDeleteTestCase.Item member)
			{
				_name = name;
				_member = member;
			}
		}

		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(typeof(ReAddCascadedDeleteTestCase.Item)).CascadeOnDelete(true
				);
		}

		protected override void Store()
		{
			Db().Store(new ReAddCascadedDeleteTestCase.Item("parent", new ReAddCascadedDeleteTestCase.Item
				("child")));
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestDeletingAndReaddingMember()
		{
			DeleteParentAndReAddChild();
			Reopen();
			Assert.IsNotNull(Query("child"));
			Assert.IsNull(Query("parent"));
		}

		private void DeleteParentAndReAddChild()
		{
			ReAddCascadedDeleteTestCase.Item i = Query("parent");
			Db().Delete(i);
			Db().Store(i._member);
			Db().Commit();
		}

		private ReAddCascadedDeleteTestCase.Item Query(string name)
		{
			IObjectSet objectSet = Db().QueryByExample(new ReAddCascadedDeleteTestCase.Item(name
				));
			if (!objectSet.HasNext())
			{
				return null;
			}
			return (ReAddCascadedDeleteTestCase.Item)objectSet.Next();
		}
	}
}

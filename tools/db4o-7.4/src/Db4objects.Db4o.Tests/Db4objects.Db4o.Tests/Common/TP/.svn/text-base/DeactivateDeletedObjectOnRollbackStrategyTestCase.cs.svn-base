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
using Db4objects.Db4o.TA;
using Db4objects.Db4o.Tests.Common.TP;

namespace Db4objects.Db4o.Tests.Common.TP
{
	public class DeactivateDeletedObjectOnRollbackStrategyTestCase : AbstractDb4oTestCase
	{
		/// <exception cref="Exception"></exception>
		protected override void Configure(IConfiguration config)
		{
			base.Configure(config);
			config.Add(new TransparentPersistenceSupport(new _IRollbackStrategy_23()));
		}

		private sealed class _IRollbackStrategy_23 : IRollbackStrategy
		{
			public _IRollbackStrategy_23()
			{
			}

			public void Rollback(IObjectContainer container, object obj)
			{
				container.Ext().Deactivate(obj);
			}
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			Db().Store(new Item("foo.tbd"));
		}

		public virtual void Test()
		{
			Item tbd = InsertAndRetrieve();
			tbd.SetName("foo.deleted");
			Db().Delete(tbd);
			Db().Rollback();
			Assert.AreEqual("foo.tbd", tbd.GetName());
		}

		private Item InsertAndRetrieve()
		{
			IQuery query = NewQuery(typeof(Item));
			query.Descend("name").Constrain("foo.tbd");
			IObjectSet set = query.Execute();
			Assert.AreEqual(1, set.Size());
			return (Item)set.Next();
		}

		public static void Main(string[] args)
		{
			new DeactivateDeletedObjectOnRollbackStrategyTestCase().RunAll();
		}
	}
}

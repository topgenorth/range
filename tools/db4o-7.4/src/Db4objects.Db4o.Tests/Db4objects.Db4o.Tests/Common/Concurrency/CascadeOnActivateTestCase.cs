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
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Concurrency;

namespace Db4objects.Db4o.Tests.Common.Concurrency
{
	public class CascadeOnActivateTestCase : Db4oClientServerTestCase
	{
		public static void Main(string[] args)
		{
			new CascadeOnActivateTestCase().RunConcurrency();
		}

		public class Item
		{
			public string name;

			public CascadeOnActivateTestCase.Item child;
		}

		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(typeof(CascadeOnActivateTestCase.Item)).CascadeOnActivate(true
				);
		}

		protected override void Store()
		{
			CascadeOnActivateTestCase.Item item = new CascadeOnActivateTestCase.Item();
			item.name = "1";
			item.child = new CascadeOnActivateTestCase.Item();
			item.child.name = "2";
			item.child.child = new CascadeOnActivateTestCase.Item();
			item.child.child.name = "3";
			Store(item);
		}

		public virtual void Conc(IExtObjectContainer oc)
		{
			IQuery q = oc.Query();
			q.Constrain(typeof(CascadeOnActivateTestCase.Item));
			q.Descend("name").Constrain("1");
			IObjectSet os = q.Execute();
			CascadeOnActivateTestCase.Item item = (CascadeOnActivateTestCase.Item)os.Next();
			CascadeOnActivateTestCase.Item item3 = item.child.child;
			Assert.AreEqual("3", item3.name);
			oc.Deactivate(item, int.MaxValue);
			Assert.IsNull(item3.name);
			oc.Activate(item, 1);
			Assert.AreEqual("3", item3.name);
		}
	}
}

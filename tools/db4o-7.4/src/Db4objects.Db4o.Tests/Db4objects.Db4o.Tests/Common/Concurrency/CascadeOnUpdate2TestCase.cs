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
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Tests.Common.Concurrency;
using Db4objects.Db4o.Tests.Common.Persistent;

namespace Db4objects.Db4o.Tests.Common.Concurrency
{
	public class CascadeOnUpdate2TestCase : Db4oClientServerTestCase
	{
		public static void Main(string[] args)
		{
			new CascadeOnUpdate2TestCase().RunConcurrency();
		}

		private const int AtomCount = 10;

		public class Item
		{
			public Atom[] child;
		}

		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(typeof(CascadeOnUpdate2TestCase.Item)).CascadeOnUpdate(true);
			config.ObjectClass(typeof(Atom)).CascadeOnUpdate(false);
		}

		protected override void Store()
		{
			CascadeOnUpdate2TestCase.Item item = new CascadeOnUpdate2TestCase.Item();
			item.child = new Atom[AtomCount];
			for (int i = 0; i < AtomCount; i++)
			{
				item.child[i] = new Atom(new Atom("storedChild"), "stored");
			}
			Store(item);
		}

		public virtual void Conc(IExtObjectContainer oc, int seq)
		{
			CascadeOnUpdate2TestCase.Item item = (CascadeOnUpdate2TestCase.Item)RetrieveOnlyInstance
				(oc, typeof(CascadeOnUpdate2TestCase.Item));
			for (int i = 0; i < AtomCount; i++)
			{
				item.child[i].name = "updated" + seq;
				item.child[i].child.name = "updated" + seq;
				oc.Store(item);
			}
		}

		public virtual void Check(IExtObjectContainer oc)
		{
			CascadeOnUpdate2TestCase.Item item = (CascadeOnUpdate2TestCase.Item)RetrieveOnlyInstance
				(oc, typeof(CascadeOnUpdate2TestCase.Item));
			string name = item.child[0].name;
			Assert.IsTrue(name.StartsWith("updated"));
			for (int i = 0; i < AtomCount; i++)
			{
				Assert.AreEqual(name, item.child[i].name);
				Assert.AreEqual("storedChild", item.child[i].child.name);
			}
		}
	}
}

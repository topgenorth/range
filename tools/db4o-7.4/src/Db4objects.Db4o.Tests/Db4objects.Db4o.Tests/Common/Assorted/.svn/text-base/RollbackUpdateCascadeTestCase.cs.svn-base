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
using Db4objects.Db4o.Tests.Common.Assorted;

namespace Db4objects.Db4o.Tests.Common.Assorted
{
	public class RollbackUpdateCascadeTestCase : Db4oClientServerTestCase
	{
		public static void Main(string[] args)
		{
			new RollbackUpdateCascadeTestCase().RunClientServer();
		}

		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(typeof(Atom)).CascadeOnUpdate(true);
			config.ObjectClass(typeof(Atom)).CascadeOnDelete(true);
		}

		protected override void Store()
		{
			Atom atom = new Atom("root");
			atom.child = new Atom("child");
			atom.child.child = new Atom("child.child");
			Store(atom);
		}

		public virtual void Test()
		{
			IExtObjectContainer oc1 = OpenNewClient();
			IExtObjectContainer oc2 = OpenNewClient();
			IExtObjectContainer oc3 = OpenNewClient();
			try
			{
				IQuery query1 = oc1.Query();
				query1.Descend("name").Constrain("root");
				IObjectSet os1 = query1.Execute();
				Assert.AreEqual(1, os1.Size());
				Atom o1 = (Atom)os1.Next();
				o1.child.child.name = "o1";
				oc1.Store(o1);
				IQuery query2 = oc2.Query();
				query2.Descend("name").Constrain("root");
				IObjectSet os2 = query2.Execute();
				Assert.AreEqual(1, os2.Size());
				Atom o2 = (Atom)os2.Next();
				Assert.AreEqual("child.child", o2.child.child.name);
				oc1.Rollback();
				oc2.Purge(o2);
				os2 = query2.Execute();
				Assert.AreEqual(1, os2.Size());
				o2 = (Atom)os2.Next();
				Assert.AreEqual("child.child", o2.child.child.name);
				oc1.Store(o1);
				oc1.Commit();
				os2 = query2.Execute();
				Assert.AreEqual(1, os2.Size());
				o2 = (Atom)os2.Next();
				oc2.Refresh(o2, int.MaxValue);
				Assert.AreEqual("o1", o2.child.child.name);
				IQuery query3 = oc3.Query();
				query3.Descend("name").Constrain("root");
				IObjectSet os3 = query1.Execute();
				Assert.AreEqual(1, os3.Size());
				Atom o3 = (Atom)os3.Next();
				Assert.AreEqual("o1", o3.child.child.name);
			}
			finally
			{
				oc1.Close();
				oc2.Close();
				oc3.Close();
			}
		}
	}
}

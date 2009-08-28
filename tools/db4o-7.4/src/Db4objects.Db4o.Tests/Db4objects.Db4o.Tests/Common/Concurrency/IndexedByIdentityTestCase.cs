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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Concurrency;
using Db4objects.Db4o.Tests.Common.Persistent;
using Sharpen.Lang;

namespace Db4objects.Db4o.Tests.Common.Concurrency
{
	public class IndexedByIdentityTestCase : Db4oClientServerTestCase
	{
		public static void Main(string[] args)
		{
			new IndexedByIdentityTestCase().RunConcurrency();
		}

		public Atom atom;

		internal const int Count = 10;

		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(this).ObjectField("atom").Indexed(true);
			config.ObjectClass(typeof(IndexedByIdentityTestCase)).CascadeOnUpdate(true);
		}

		protected override void Store()
		{
			for (int i = 0; i < Count; i++)
			{
				IndexedByIdentityTestCase ibi = new IndexedByIdentityTestCase();
				ibi.atom = new Atom("ibi" + i);
				Store(ibi);
			}
		}

		public virtual void ConcRead(IExtObjectContainer oc)
		{
			for (int i = 0; i < Count; i++)
			{
				IQuery q = oc.Query();
				q.Constrain(typeof(Atom));
				q.Descend("name").Constrain("ibi" + i);
				IObjectSet objectSet = q.Execute();
				Assert.AreEqual(1, objectSet.Size());
				Atom child = (Atom)objectSet.Next();
				q = oc.Query();
				q.Constrain(typeof(IndexedByIdentityTestCase));
				q.Descend("atom").Constrain(child).Identity();
				objectSet = q.Execute();
				Assert.AreEqual(1, objectSet.Size());
				IndexedByIdentityTestCase ibi = (IndexedByIdentityTestCase)objectSet.Next();
				Assert.AreSame(child, ibi.atom);
			}
		}

		/// <exception cref="Exception"></exception>
		public virtual void ConcUpdate(IExtObjectContainer oc, int seq)
		{
			IQuery q = oc.Query();
			q.Constrain(typeof(IndexedByIdentityTestCase));
			IObjectSet os = q.Execute();
			Assert.AreEqual(Count, os.Size());
			while (os.HasNext())
			{
				IndexedByIdentityTestCase idi = (IndexedByIdentityTestCase)os.Next();
				idi.atom.name = "updated" + seq;
				oc.Store(idi);
				Thread.Sleep(100);
			}
		}

		public virtual void CheckUpdate(IExtObjectContainer oc)
		{
			IQuery q = oc.Query();
			q.Constrain(typeof(IndexedByIdentityTestCase));
			IObjectSet os = q.Execute();
			Assert.AreEqual(Count, os.Size());
			string expected = null;
			while (os.HasNext())
			{
				IndexedByIdentityTestCase idi = (IndexedByIdentityTestCase)os.Next();
				if (expected == null)
				{
					expected = idi.atom.name;
					Assert.IsTrue(expected.StartsWith("updated"));
					Assert.IsTrue(expected.Length > "updated".Length);
				}
				Assert.AreEqual(expected, idi.atom.name);
			}
		}
	}
}

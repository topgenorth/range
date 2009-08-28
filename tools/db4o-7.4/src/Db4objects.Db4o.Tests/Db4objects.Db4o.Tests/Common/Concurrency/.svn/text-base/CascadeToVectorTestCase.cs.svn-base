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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Tests.Common.Concurrency;
using Db4objects.Db4o.Tests.Common.Persistent;
using Sharpen.Lang;

namespace Db4objects.Db4o.Tests.Common.Concurrency
{
	public class CascadeToVectorTestCase : Db4oClientServerTestCase
	{
		public static void Main(string[] args)
		{
			new CascadeToVectorTestCase().RunConcurrency();
		}

		public ArrayList vec;

		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(this).CascadeOnUpdate(true);
			config.ObjectClass(this).CascadeOnDelete(true);
			config.ObjectClass(typeof(Atom)).CascadeOnDelete(false);
		}

		protected override void Store()
		{
			CascadeToVectorTestCase ctv = new CascadeToVectorTestCase();
			ctv.vec = new ArrayList();
			ctv.vec.Add(new Atom("stored1"));
			ctv.vec.Add(new Atom(new Atom("storedChild1"), "stored2"));
			Store(ctv);
		}

		public virtual void Conc(IExtObjectContainer oc)
		{
			CascadeToVectorTestCase ctv = (CascadeToVectorTestCase)RetrieveOnlyInstance(oc, typeof(
				CascadeToVectorTestCase));
			IEnumerator i = ctv.vec.GetEnumerator();
			while (i.MoveNext())
			{
				Atom atom = (Atom)i.Current;
				atom.name = "updated";
				if (atom.child != null)
				{
					// This one should NOT cascade
					atom.child.name = "updated";
				}
			}
			oc.Store(ctv);
		}

		public virtual void Check(IExtObjectContainer oc)
		{
			CascadeToVectorTestCase ctv = (CascadeToVectorTestCase)RetrieveOnlyInstance(oc, typeof(
				CascadeToVectorTestCase));
			IEnumerator i = ctv.vec.GetEnumerator();
			while (i.MoveNext())
			{
				Atom atom = (Atom)i.Current;
				Assert.AreEqual("updated", atom.name);
				if (atom.child != null)
				{
					Assert.AreEqual("storedChild1", atom.child.name);
				}
			}
		}

		/// <exception cref="Exception"></exception>
		public virtual void ConcDelete(IExtObjectContainer oc, int seq)
		{
			IObjectSet os = oc.Query(typeof(CascadeToVectorTestCase));
			if (os.Size() == 0)
			{
				// already deleted
				return;
			}
			Assert.AreEqual(1, os.Size());
			CascadeToVectorTestCase ctv = (CascadeToVectorTestCase)os.Next();
			// wait for other threads
			Thread.Sleep(500);
			oc.Delete(ctv);
		}

		public virtual void CheckDelete(IExtObjectContainer oc)
		{
			// Cascade-On-Delete Test: We only want one atom to remain.
			AssertOccurrences(oc, typeof(Atom), 1);
		}
	}
}

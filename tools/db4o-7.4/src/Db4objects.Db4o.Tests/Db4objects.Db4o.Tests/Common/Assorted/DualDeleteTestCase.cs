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
using Db4oUnit.Extensions;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Tests.Common.Assorted;
using Sharpen.Lang;

namespace Db4objects.Db4o.Tests.Common.Assorted
{
	public class DualDeleteTestCase : Db4oClientServerTestCase
	{
		public static void Main(string[] args)
		{
			new DualDeleteTestCase().RunClientServer();
		}

		public Atom atom;

		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(this).CascadeOnDelete(true);
			config.ObjectClass(this).CascadeOnUpdate(true);
		}

		protected override void Store()
		{
			DualDeleteTestCase dd1 = new DualDeleteTestCase();
			dd1.atom = new Atom("justone");
			Store(dd1);
			DualDeleteTestCase dd2 = new DualDeleteTestCase();
			dd2.atom = dd1.atom;
			Store(dd2);
		}

		public virtual void Test()
		{
			IExtObjectContainer oc1 = OpenNewClient();
			IExtObjectContainer oc2 = OpenNewClient();
			try
			{
				IObjectSet os1 = oc1.Query(typeof(DualDeleteTestCase));
				IObjectSet os2 = oc2.Query(typeof(DualDeleteTestCase));
				DeleteObjectSet(oc1, os1);
				AssertOccurrences(oc1, typeof(Atom), 0);
				AssertOccurrences(oc2, typeof(Atom), 1);
				DeleteObjectSet(oc2, os2);
				AssertOccurrences(oc1, typeof(Atom), 0);
				AssertOccurrences(oc2, typeof(Atom), 0);
				oc1.Rollback();
				AssertOccurrences(oc1, typeof(Atom), 1);
				AssertOccurrences(oc2, typeof(Atom), 0);
				oc1.Commit();
				AssertOccurrences(oc1, typeof(Atom), 1);
				AssertOccurrences(oc2, typeof(Atom), 0);
				DeleteAll(oc2, typeof(DualDeleteTestCase));
				oc2.Commit();
				AssertOccurrences(oc1, typeof(Atom), 0);
				AssertOccurrences(oc2, typeof(Atom), 0);
			}
			finally
			{
				oc1.Close();
				oc2.Close();
			}
		}

		/// <exception cref="Exception"></exception>
		public virtual void Conc1(IExtObjectContainer oc)
		{
			IObjectSet os = oc.Query(typeof(DualDeleteTestCase));
			Thread.Sleep(500);
			DeleteObjectSet(oc, os);
			oc.Rollback();
		}

		/// <exception cref="Exception"></exception>
		public virtual void Check1(IExtObjectContainer oc)
		{
			AssertOccurrences(oc, typeof(Atom), 1);
		}

		/// <exception cref="Exception"></exception>
		public virtual void Conc2(IExtObjectContainer oc)
		{
			IObjectSet os = oc.Query(typeof(DualDeleteTestCase));
			Thread.Sleep(500);
			DeleteObjectSet(oc, os);
			AssertOccurrences(oc, typeof(Atom), 0);
		}

		/// <exception cref="Exception"></exception>
		public virtual void Check2(IExtObjectContainer oc)
		{
			AssertOccurrences(oc, typeof(Atom), 0);
		}
	}
}

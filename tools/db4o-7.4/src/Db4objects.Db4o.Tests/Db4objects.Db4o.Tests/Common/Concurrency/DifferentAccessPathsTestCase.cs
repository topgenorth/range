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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Concurrency;

namespace Db4objects.Db4o.Tests.Common.Concurrency
{
	public class DifferentAccessPathsTestCase : Db4oClientServerTestCase
	{
		public static void Main(string[] args)
		{
			new DifferentAccessPathsTestCase().RunConcurrency();
		}

		public string foo;

		protected override void Store()
		{
			DifferentAccessPathsTestCase dap = new DifferentAccessPathsTestCase();
			dap.foo = "hi";
			Store(dap);
			dap = new DifferentAccessPathsTestCase();
			dap.foo = "hi too";
			Store(dap);
		}

		/// <exception cref="Exception"></exception>
		public virtual void Conc(IExtObjectContainer oc)
		{
			DifferentAccessPathsTestCase dap = Query(oc);
			for (int i = 0; i < 10; i++)
			{
				Assert.AreSame(dap, Query(oc));
			}
			oc.Purge(dap);
			Assert.AreNotSame(dap, Query(oc));
		}

		private DifferentAccessPathsTestCase Query(IExtObjectContainer oc)
		{
			IQuery q = oc.Query();
			q.Constrain(typeof(DifferentAccessPathsTestCase));
			q.Descend("foo").Constrain("hi");
			IObjectSet os = q.Execute();
			Assert.AreEqual(1, os.Size());
			DifferentAccessPathsTestCase dap = (DifferentAccessPathsTestCase)os.Next();
			return dap;
		}
	}
}

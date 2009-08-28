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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Tests.Common.Concurrency;

namespace Db4objects.Db4o.Tests.Common.Concurrency
{
	public class DeepSetTestCase : Db4oClientServerTestCase
	{
		public static void Main(string[] args)
		{
			new DeepSetTestCase().RunConcurrency();
		}

		public DeepSetTestCase child;

		public string name;

		protected override void Store()
		{
			name = "1";
			child = new DeepSetTestCase();
			child.name = "2";
			child.child = new DeepSetTestCase();
			child.child.name = "3";
			Store(this);
		}

		public virtual void Conc(IExtObjectContainer oc, int seq)
		{
			DeepSetTestCase example = new DeepSetTestCase();
			example.name = "1";
			DeepSetTestCase ds = (DeepSetTestCase)oc.QueryByExample(example).Next();
			Assert.AreEqual("1", ds.name);
			Assert.AreEqual("3", ds.child.child.name);
			ds.name = "1";
			ds.child.name = "12" + seq;
			ds.child.child.name = "13" + seq;
			oc.Store(ds, 2);
		}

		public virtual void Check(IExtObjectContainer oc)
		{
			DeepSetTestCase example = new DeepSetTestCase();
			example.name = "1";
			DeepSetTestCase ds = (DeepSetTestCase)oc.QueryByExample(example).Next();
			Assert.IsTrue(ds.child.name.StartsWith("12"));
			Assert.IsTrue(ds.child.name.Length > "12".Length);
			Assert.AreEqual("3", ds.child.child.name);
		}
	}
}

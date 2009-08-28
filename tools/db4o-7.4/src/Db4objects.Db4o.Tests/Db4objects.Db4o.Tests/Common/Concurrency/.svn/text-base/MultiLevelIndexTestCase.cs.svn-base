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
	public class MultiLevelIndexTestCase : Db4oClientServerTestCase
	{
		public static void Main(string[] args)
		{
			new MultiLevelIndexTestCase().RunConcurrency();
		}

		public MultiLevelIndexTestCase _child;

		public int _i;

		public int _level;

		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(this).ObjectField("_child").Indexed(true);
			config.ObjectClass(this).ObjectField("_i").Indexed(true);
		}

		protected override void Store()
		{
			Store(3);
			Store(2);
			Store(5);
			Store(1);
			for (int i = 6; i < 103; i++)
			{
				Store(i);
			}
		}

		private void Store(int val)
		{
			MultiLevelIndexTestCase root = new MultiLevelIndexTestCase();
			root._i = val;
			root._child = new MultiLevelIndexTestCase();
			root._child._level = 1;
			root._child._i = -val;
			Store(root);
		}

		public virtual void Conc1(IExtObjectContainer oc)
		{
			IQuery q = oc.Query();
			q.Constrain(typeof(MultiLevelIndexTestCase));
			q.Descend("_child").Descend("_i").Constrain(-102);
			IObjectSet objectSet = q.Execute();
			Assert.AreEqual(1, objectSet.Size());
			MultiLevelIndexTestCase mli = (MultiLevelIndexTestCase)objectSet.Next();
			Assert.AreEqual(102, mli._i);
		}

		public virtual void Conc2(IExtObjectContainer oc, int seq)
		{
			oc.Configure().ObjectClass(typeof(MultiLevelIndexTestCase)).CascadeOnUpdate(true);
			IQuery q = oc.Query();
			q.Constrain(typeof(MultiLevelIndexTestCase));
			q.Descend("_child").Descend("_i").Constrain(seq - 102);
			IObjectSet objectSet = q.Execute();
			Assert.AreEqual(1, objectSet.Size());
			MultiLevelIndexTestCase mli = (MultiLevelIndexTestCase)objectSet.Next();
			Assert.AreEqual(102 - seq, mli._i);
			mli._child._i = -(seq + 201);
			oc.Store(mli);
		}

		public virtual void Check2(IExtObjectContainer oc)
		{
			IQuery q = oc.Query();
			q.Constrain(typeof(MultiLevelIndexTestCase));
			q.Descend("_child").Descend("_i").Constrain(-200).Smaller();
			IObjectSet objectSet = q.Execute();
			Assert.AreEqual(ThreadCount(), objectSet.Size());
		}
	}
}

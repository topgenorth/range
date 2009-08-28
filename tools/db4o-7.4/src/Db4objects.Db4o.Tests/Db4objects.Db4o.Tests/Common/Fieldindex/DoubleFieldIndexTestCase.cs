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
using Db4objects.Db4o.Tests.Common.Fieldindex;

namespace Db4objects.Db4o.Tests.Common.Fieldindex
{
	/// <exclude></exclude>
	public class DoubleFieldIndexTestCase : AbstractDb4oTestCase
	{
		public static void Main(string[] args)
		{
			new DoubleFieldIndexTestCase().RunSolo();
		}

		public class Item
		{
			public double value;

			public Item()
			{
			}

			public Item(double value_)
			{
				value = value_;
			}
		}

		protected override void Configure(IConfiguration config)
		{
			IndexField(config, typeof(DoubleFieldIndexTestCase.Item), "value");
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			Db().Store(new DoubleFieldIndexTestCase.Item(0.5));
			Db().Store(new DoubleFieldIndexTestCase.Item(1.1));
			Db().Store(new DoubleFieldIndexTestCase.Item(2));
		}

		public virtual void TestEqual()
		{
			IQuery query = NewQuery(typeof(DoubleFieldIndexTestCase.Item));
			query.Descend("value").Constrain(1.1);
			AssertItems(new double[] { 1.1 }, query.Execute());
		}

		public virtual void TestGreater()
		{
			IQuery query = NewQuery(typeof(DoubleFieldIndexTestCase.Item));
			IQuery descend = query.Descend("value");
			descend.Constrain(System.Convert.ToDouble(1)).Greater();
			descend.OrderAscending();
			AssertItems(new double[] { 1.1, 2 }, query.Execute());
		}

		private void AssertItems(double[] expected, IObjectSet set)
		{
			ArrayAssert.AreEqual(expected, ToDoubleArray(set));
		}

		private double[] ToDoubleArray(IObjectSet set)
		{
			double[] array = new double[set.Size()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = ((DoubleFieldIndexTestCase.Item)set.Next()).value;
			}
			return array;
		}
	}
}

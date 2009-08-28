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
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Soda.Util;

namespace Db4objects.Db4o.Tests.Common.Soda.Ordered
{
	public class STOIntegerWTTestCase : SodaBaseTestCase
	{
		public int i_int;

		public STOIntegerWTTestCase()
		{
		}

		private STOIntegerWTTestCase(int a_int)
		{
			i_int = a_int;
		}

		public override object[] CreateData()
		{
			return new object[] { new Db4objects.Db4o.Tests.Common.Soda.Ordered.STOIntegerWTTestCase
				(1001), new Db4objects.Db4o.Tests.Common.Soda.Ordered.STOIntegerWTTestCase(99), 
				new Db4objects.Db4o.Tests.Common.Soda.Ordered.STOIntegerWTTestCase(1), new Db4objects.Db4o.Tests.Common.Soda.Ordered.STOIntegerWTTestCase
				(909), new Db4objects.Db4o.Tests.Common.Soda.Ordered.STOIntegerWTTestCase(1001), 
				new Db4objects.Db4o.Tests.Common.Soda.Ordered.STOIntegerWTTestCase(0), new Db4objects.Db4o.Tests.Common.Soda.Ordered.STOIntegerWTTestCase
				(1010), new Db4objects.Db4o.Tests.Common.Soda.Ordered.STOIntegerWTTestCase() };
		}

		public virtual void TestDescending()
		{
			IQuery q = NewQuery();
			q.Constrain(typeof(Db4objects.Db4o.Tests.Common.Soda.Ordered.STOIntegerWTTestCase
				));
			q.Descend("i_int").OrderDescending();
			ExpectOrdered(q, new int[] { 6, 4, 0, 3, 1, 2, 5, 7 });
		}

		public virtual void TestAscendingGreater()
		{
			IQuery q = NewQuery();
			q.Constrain(typeof(Db4objects.Db4o.Tests.Common.Soda.Ordered.STOIntegerWTTestCase
				));
			IQuery qInt = q.Descend("i_int");
			qInt.Constrain(100).Greater();
			qInt.OrderAscending();
			ExpectOrdered(q, new int[] { 3, 0, 4, 6 });
		}
	}
}

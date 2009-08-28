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

namespace Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped
{
	public class STFloatWUTestCase : SodaBaseTestCase
	{
		public object i_float;

		public STFloatWUTestCase()
		{
		}

		private STFloatWUTestCase(float a_float)
		{
			i_float = a_float;
		}

		public override object[] CreateData()
		{
			return new object[] { new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STFloatWUTestCase
				(float.MinValue), new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STFloatWUTestCase
				((float)0.0000123), new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STFloatWUTestCase
				((float)1.345), new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STFloatWUTestCase
				(float.MaxValue) };
		}

		public virtual void TestEquals()
		{
			IQuery q = NewQuery();
			q.Constrain(_array[0]);
			SodaTestUtil.ExpectOne(q, _array[0]);
		}

		public virtual void TestGreater()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STFloatWUTestCase
				((float)0.1));
			q.Descend("i_float").Constraints().Greater();
			Expect(q, new int[] { 2, 3 });
		}

		public virtual void TestSmaller()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STFloatWUTestCase
				((float)1.5));
			q.Descend("i_float").Constraints().Smaller();
			Expect(q, new int[] { 0, 1, 2 });
		}
	}
}

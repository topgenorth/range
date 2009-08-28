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
	public class STLongWUTestCase : SodaBaseTestCase
	{
		public object i_long;

		public STLongWUTestCase()
		{
		}

		private STLongWUTestCase(long a_long)
		{
			i_long = a_long;
		}

		public override object[] CreateData()
		{
			return new object[] { new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STLongWUTestCase
				(long.MinValue), new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STLongWUTestCase
				(-1), new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STLongWUTestCase(0), 
				new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STLongWUTestCase(long.MaxValue
				 - 1) };
		}

		public virtual void TestEquals()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STLongWUTestCase
				(long.MinValue));
			SodaTestUtil.Expect(q, new object[] { new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STLongWUTestCase
				(long.MinValue) });
		}

		public virtual void TestGreater()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STLongWUTestCase
				(-1));
			q.Descend("i_long").Constraints().Greater();
			Expect(q, new int[] { 2, 3 });
		}

		public virtual void TestSmaller()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STLongWUTestCase
				(1));
			q.Descend("i_long").Constraints().Smaller();
			Expect(q, new int[] { 0, 1, 2 });
		}

		public virtual void TestBetween()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STLongWUTestCase
				());
			IQuery sub = q.Descend("i_long");
			sub.Constrain(System.Convert.ToInt64(-3)).Greater();
			sub.Constrain(System.Convert.ToInt64(3)).Smaller();
			Expect(q, new int[] { 1, 2 });
		}

		public virtual void TestAnd()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STLongWUTestCase
				());
			IQuery sub = q.Descend("i_long");
			sub.Constrain(System.Convert.ToInt64(-3)).Greater().And(sub.Constrain(System.Convert.ToInt64
				(3)).Smaller());
			Expect(q, new int[] { 1, 2 });
		}

		public virtual void TestOr()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STLongWUTestCase
				());
			IQuery sub = q.Descend("i_long");
			sub.Constrain(System.Convert.ToInt64(3)).Greater().Or(sub.Constrain(System.Convert.ToInt64
				(-3)).Smaller());
			Expect(q, new int[] { 0, 3 });
		}
	}
}

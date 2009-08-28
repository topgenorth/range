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

namespace Db4objects.Db4o.Tests.Common.Soda.Arrays.Untyped
{
	public class STArrMixedTestCase : SodaBaseTestCase
	{
		public object[] arr;

		public STArrMixedTestCase()
		{
		}

		public STArrMixedTestCase(object[] arr)
		{
			this.arr = arr;
		}

		public override object[] CreateData()
		{
			return new object[] { new Db4objects.Db4o.Tests.Common.Soda.Arrays.Untyped.STArrMixedTestCase
				(), new Db4objects.Db4o.Tests.Common.Soda.Arrays.Untyped.STArrMixedTestCase(new 
				object[0]), new Db4objects.Db4o.Tests.Common.Soda.Arrays.Untyped.STArrMixedTestCase
				(new object[] { 0, 0, "foo", false }), new Db4objects.Db4o.Tests.Common.Soda.Arrays.Untyped.STArrMixedTestCase
				(new object[] { 1, 17, int.MaxValue - 1, "foo", "bar" }), new Db4objects.Db4o.Tests.Common.Soda.Arrays.Untyped.STArrMixedTestCase
				(new object[] { 3, 17, 25, int.MaxValue - 2 }) };
		}

		public virtual void TestDefaultContainsInteger()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Arrays.Untyped.STArrMixedTestCase
				(new object[] { 17 }));
			Expect(q, new int[] { 3, 4 });
		}

		public virtual void TestDefaultContainsString()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Arrays.Untyped.STArrMixedTestCase
				(new object[] { "foo" }));
			Expect(q, new int[] { 2, 3 });
		}

		public virtual void TestDefaultContainsBoolean()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Arrays.Untyped.STArrMixedTestCase
				(new object[] { false }));
			Expect(q, new int[] { 2 });
		}

		public virtual void TestDefaultContainsTwo()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Arrays.Untyped.STArrMixedTestCase
				(new object[] { 17, "bar" }));
			Expect(q, new int[] { 3 });
		}

		public virtual void TestDescendOne()
		{
			IQuery q = NewQuery();
			q.Constrain(typeof(Db4objects.Db4o.Tests.Common.Soda.Arrays.Untyped.STArrMixedTestCase
				));
			q.Descend("arr").Constrain(17);
			Expect(q, new int[] { 3, 4 });
		}

		public virtual void TestDescendTwo()
		{
			IQuery q = NewQuery();
			q.Constrain(typeof(Db4objects.Db4o.Tests.Common.Soda.Arrays.Untyped.STArrMixedTestCase
				));
			IQuery qElements = q.Descend("arr");
			qElements.Constrain(17);
			qElements.Constrain("bar");
			Expect(q, new int[] { 3 });
		}

		public virtual void TestDescendSmaller()
		{
			IQuery q = NewQuery();
			q.Constrain(typeof(Db4objects.Db4o.Tests.Common.Soda.Arrays.Untyped.STArrMixedTestCase
				));
			IQuery qElements = q.Descend("arr");
			qElements.Constrain(3).Smaller();
			Expect(q, new int[] { 2, 3 });
		}
	}
}

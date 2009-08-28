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
	public class STBooleanWUTestCase : SodaBaseTestCase
	{
		internal static readonly string Descendant = "i_boolean";

		public object i_boolean;

		public STBooleanWUTestCase()
		{
		}

		private STBooleanWUTestCase(bool a_boolean)
		{
			i_boolean = a_boolean;
		}

		public override object[] CreateData()
		{
			return new object[] { new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STBooleanWUTestCase
				(false), new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STBooleanWUTestCase
				(true), new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STBooleanWUTestCase
				(false), new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STBooleanWUTestCase
				(false), new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STBooleanWUTestCase
				() };
		}

		public virtual void TestEqualsTrue()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STBooleanWUTestCase
				(true));
			SodaTestUtil.ExpectOne(q, new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STBooleanWUTestCase
				(true));
		}

		public virtual void TestEqualsFalse()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STBooleanWUTestCase
				(false));
			q.Descend(Descendant).Constrain(false);
			Expect(q, new int[] { 0, 2, 3 });
		}

		public virtual void TestNull()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STBooleanWUTestCase
				());
			q.Descend(Descendant).Constrain(null);
			SodaTestUtil.ExpectOne(q, new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STBooleanWUTestCase
				());
		}

		public virtual void TestNullOrTrue()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STBooleanWUTestCase
				());
			IQuery qd = q.Descend(Descendant);
			qd.Constrain(null).Or(qd.Constrain(true));
			Expect(q, new int[] { 1, 4 });
		}

		public virtual void TestNotNullAndFalse()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Wrapper.Untyped.STBooleanWUTestCase
				());
			IQuery qd = q.Descend(Descendant);
			qd.Constrain(null).Not().And(qd.Constrain(false));
			Expect(q, new int[] { 0, 2, 3 });
		}
	}
}

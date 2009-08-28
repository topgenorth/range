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
	public class STOStringTestCase : SodaBaseTestCase
	{
		public string foo;

		public STOStringTestCase()
		{
		}

		public STOStringTestCase(string str)
		{
			this.foo = str;
		}

		public override object[] CreateData()
		{
			return new object[] { new Db4objects.Db4o.Tests.Common.Soda.Ordered.STOStringTestCase
				(null), new Db4objects.Db4o.Tests.Common.Soda.Ordered.STOStringTestCase("bbb"), 
				new Db4objects.Db4o.Tests.Common.Soda.Ordered.STOStringTestCase("bbb"), new Db4objects.Db4o.Tests.Common.Soda.Ordered.STOStringTestCase
				("dod"), new Db4objects.Db4o.Tests.Common.Soda.Ordered.STOStringTestCase("aaa"), 
				new Db4objects.Db4o.Tests.Common.Soda.Ordered.STOStringTestCase("Xbb"), new Db4objects.Db4o.Tests.Common.Soda.Ordered.STOStringTestCase
				("bbq") };
		}

		public virtual void TestAscending()
		{
			IQuery q = NewQuery();
			q.Constrain(typeof(Db4objects.Db4o.Tests.Common.Soda.Ordered.STOStringTestCase));
			q.Descend("foo").OrderAscending();
			ExpectOrdered(q, new int[] { 5, 4, 1, 2, 6, 3, 0 });
		}

		public virtual void TestDescending()
		{
			IQuery q = NewQuery();
			q.Constrain(typeof(Db4objects.Db4o.Tests.Common.Soda.Ordered.STOStringTestCase));
			q.Descend("foo").OrderDescending();
			ExpectOrdered(q, new int[] { 3, 6, 2, 1, 4, 5, 0 });
		}

		public virtual void TestAscendingLike()
		{
			IQuery q = NewQuery();
			q.Constrain(typeof(Db4objects.Db4o.Tests.Common.Soda.Ordered.STOStringTestCase));
			IQuery qStr = q.Descend("foo");
			qStr.Constrain("b").Like();
			qStr.OrderAscending();
			ExpectOrdered(q, new int[] { 5, 1, 2, 6 });
		}

		public virtual void TestDescendingContains()
		{
			IQuery q = NewQuery();
			q.Constrain(typeof(Db4objects.Db4o.Tests.Common.Soda.Ordered.STOStringTestCase));
			IQuery qStr = q.Descend("foo");
			qStr.Constrain("b").Contains();
			qStr.OrderDescending();
			ExpectOrdered(q, new int[] { 6, 2, 1, 5 });
		}
	}
}

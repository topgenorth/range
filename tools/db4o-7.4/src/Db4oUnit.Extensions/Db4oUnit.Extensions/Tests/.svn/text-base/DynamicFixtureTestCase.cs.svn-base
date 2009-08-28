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
using System.Collections;
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4oUnit.Extensions.Fixtures;
using Db4oUnit.Extensions.Tests;
using Db4oUnit.Fixtures;

namespace Db4oUnit.Extensions.Tests
{
	public class DynamicFixtureTestCase : ITestSuiteBuilder
	{
		public virtual IEnumerator GetEnumerator()
		{
			// The test case simply runs FooTestSuite
			// with a Db4oInMemory fixture to ensure the 
			// the db4o fixture can be successfully propagated
			// to FooTestUnit#test.
			return new Db4oTestSuiteBuilder(new Db4oInMemory(), typeof(DynamicFixtureTestCase.FooTestSuite
				)).GetEnumerator();
		}

		/// <summary>One of the possibly many test units.</summary>
		/// <remarks>One of the possibly many test units.</remarks>
		public class FooTestUnit : AbstractDb4oTestCase
		{
			private readonly object[] values = MultiValueFixtureProvider.Value();

			public virtual void Test()
			{
				Assert.IsNotNull(Db());
				Assert.IsNotNull(values);
			}
		}

		/// <summary>The test suite which binds together fixture providers and test units.</summary>
		/// <remarks>The test suite which binds together fixture providers and test units.</remarks>
		public class FooTestSuite : FixtureBasedTestSuite
		{
			public override IFixtureProvider[] FixtureProviders()
			{
				return new IFixtureProvider[] { new MultiValueFixtureProvider(new object[][] { new 
					object[] { "foo", "bar" }, new object[] { 1, 42 } }) };
			}

			public override Type[] TestUnits()
			{
				return new Type[] { typeof(DynamicFixtureTestCase.FooTestUnit) };
			}
		}
	}
}

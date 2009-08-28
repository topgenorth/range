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
using Db4oUnit.Extensions.Fixtures;
using Db4oUnit.Extensions.Tests;
using Db4oUnit.Mocking;
using Db4objects.Db4o.Config;

namespace Db4oUnit.Extensions.Tests
{
	public class FixtureConfigurationTestCase : ITestCase
	{
		internal sealed class MockFixtureConfiguration : MethodCallRecorder, IFixtureConfiguration
		{
			public void Configure(Type clazz, IConfiguration config)
			{
				Record(new MethodCall("configure", clazz, config));
			}

			public string GetLabel()
			{
				return "MOCK";
			}
		}

		public sealed class TestCase1 : AbstractDb4oTestCase
		{
			public void Test()
			{
			}
		}

		public sealed class TestCase2 : AbstractDb4oTestCase
		{
			public void Test()
			{
			}
		}

		public virtual void TestSolo()
		{
			AssertFixtureConfiguration(new Db4oSolo());
		}

		public virtual void TestClientServer()
		{
			AssertFixtureConfiguration(new Db4oClientServer(new CachingConfigurationSource(new 
				IndependentConfigurationSource()), false, "C/S"));
		}

		public virtual void TestInMemory()
		{
			AssertFixtureConfiguration(new Db4oInMemory());
		}

		private void AssertFixtureConfiguration(IDb4oFixture fixture)
		{
			FixtureConfigurationTestCase.MockFixtureConfiguration configuration = new FixtureConfigurationTestCase.MockFixtureConfiguration
				();
			fixture.FixtureConfiguration(configuration);
			Assert.IsTrue(fixture.Label().EndsWith(" - " + configuration.GetLabel()), "FixtureConfiguration label must be part of Fixture label."
				);
			new TestRunner(new Db4oTestSuiteBuilder(fixture, new Type[] { typeof(FixtureConfigurationTestCase.TestCase1
				), typeof(FixtureConfigurationTestCase.TestCase2) })).Run(new TestResult());
			configuration.Verify(new MethodCall[] { new MethodCall("configure", typeof(FixtureConfigurationTestCase.TestCase1
				), MethodCall.IgnoredArgument), new MethodCall("configure", typeof(FixtureConfigurationTestCase.TestCase1
				), MethodCall.IgnoredArgument), new MethodCall("configure", typeof(FixtureConfigurationTestCase.TestCase2
				), MethodCall.IgnoredArgument), new MethodCall("configure", typeof(FixtureConfigurationTestCase.TestCase2
				), MethodCall.IgnoredArgument) });
		}
	}
}

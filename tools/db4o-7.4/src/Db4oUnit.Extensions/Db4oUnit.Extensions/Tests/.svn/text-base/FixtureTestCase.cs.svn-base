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
using Db4oUnit.Extensions.Util;
using Db4oUnit.Tests;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Foundation.IO;
using Sharpen.Lang;

namespace Db4oUnit.Extensions.Tests
{
	public class FixtureTestCase : ITestCase
	{
		private sealed class ExcludingInMemoryFixture : Db4oInMemory
		{
			public ExcludingInMemoryFixture(FixtureTestCase _enclosing, IConfigurationSource 
				source) : base(source)
			{
				this._enclosing = _enclosing;
			}

			public override bool Accept(Type clazz)
			{
				return !typeof(IOptOutFromTestFixture).IsAssignableFrom(clazz);
			}

			private readonly FixtureTestCase _enclosing;
		}

		public virtual void TestSingleTestWithDifferentFixtures()
		{
			IConfigurationSource configSource = new IndependentConfigurationSource();
			AssertSimpleDb4o(new Db4oInMemory(configSource));
			AssertSimpleDb4o(new Db4oSolo(configSource));
		}

		public virtual void TestMultipleTestsSingleFixture()
		{
			MultipleDb4oTestCase.ResetConfigureCalls();
			FrameworkTestCase.RunTestAndExpect(new Db4oTestSuiteBuilder(new Db4oInMemory(new 
				IndependentConfigurationSource()), typeof(MultipleDb4oTestCase)), 2, false);
			Assert.AreEqual(2, MultipleDb4oTestCase.ConfigureCalls());
		}

		public virtual void TestSelectiveFixture()
		{
			IDb4oFixture fixture = new FixtureTestCase.ExcludingInMemoryFixture(this, new IndependentConfigurationSource
				());
			IEnumerator tests = new Db4oTestSuiteBuilder(fixture, new Type[] { typeof(AcceptedTestCase
				), typeof(NotAcceptedTestCase) }).GetEnumerator();
			ITest test = NextTest(tests);
			Assert.IsFalse(tests.MoveNext());
			FrameworkTestCase.RunTestAndExpect(test, 0);
		}

		private void AssertSimpleDb4o(IDb4oFixture fixture)
		{
			IEnumerator tests = new Db4oTestSuiteBuilder(fixture, typeof(SimpleDb4oTestCase))
				.GetEnumerator();
			ITest test = NextTest(tests);
			SimpleDb4oTestCase.ExpectedFixtureVariable.With(fixture, new _IRunnable_49(test));
			SimpleDb4oTestCase subject = (SimpleDb4oTestCase)Db4oTestSuiteBuilder.GetTestSubject
				(test);
			Assert.IsTrue(subject.EverythingCalled());
		}

		private sealed class _IRunnable_49 : IRunnable
		{
			public _IRunnable_49(ITest test)
			{
				this.test = test;
			}

			public void Run()
			{
				FrameworkTestCase.RunTestAndExpect(test, 0);
			}

			private readonly ITest test;
		}

		private ITest NextTest(IEnumerator tests)
		{
			return (ITest)Iterators.Next(tests);
		}

		public virtual void TestInterfaceIsAvailable()
		{
			Assert.IsTrue(typeof(IDb4oTestCase).IsAssignableFrom(typeof(AbstractDb4oTestCase)
				));
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestDeleteDir()
		{
			System.IO.Directory.CreateDirectory("a/b/c");
			Assert.IsTrue(System.IO.File.Exists("a"));
			IOUtil.DeleteDir("a");
			Assert.IsFalse(System.IO.File.Exists("a"));
		}
	}
}

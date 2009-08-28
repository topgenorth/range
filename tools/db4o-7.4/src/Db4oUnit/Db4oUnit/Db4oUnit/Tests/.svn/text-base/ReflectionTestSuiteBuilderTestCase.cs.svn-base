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
using Db4oUnit.Tests;
using Db4objects.Db4o.Foundation;

namespace Db4oUnit.Tests
{
	public class ReflectionTestSuiteBuilderTestCase : ITestCase
	{
		private sealed class ExcludingReflectionTestSuiteBuilder : Db4oUnit.ReflectionTestSuiteBuilder
		{
			public ExcludingReflectionTestSuiteBuilder(Type[] classes) : base(classes)
			{
			}

			protected override bool IsApplicable(Type clazz)
			{
				return clazz != typeof(ReflectionTestSuiteBuilderTestCase.NotAccepted);
			}
		}

		public class NonTestFixture
		{
		}

		public virtual void TestUnmarkedTestFixture()
		{
			ReflectionTestSuiteBuilder builder = new ReflectionTestSuiteBuilder(typeof(ReflectionTestSuiteBuilderTestCase.NonTestFixture
				));
			AssertFailingTestCase(typeof(ArgumentException), builder);
		}

		public class Accepted : ITestCase
		{
			public virtual void Test()
			{
			}
		}

		public class NotAccepted : ITestCase
		{
			public virtual void Test()
			{
			}
		}

		public virtual void TestNotAcceptedFixture()
		{
			ReflectionTestSuiteBuilder builder = new ReflectionTestSuiteBuilderTestCase.ExcludingReflectionTestSuiteBuilder
				(new Type[] { typeof(ReflectionTestSuiteBuilderTestCase.Accepted), typeof(ReflectionTestSuiteBuilderTestCase.NotAccepted
				) });
			Assert.AreEqual(1, Iterators.Size(builder.GetEnumerator()));
		}

		public class ConstructorThrows : ITestCase
		{
			public static readonly Exception Error = new Exception("no way");

			public ConstructorThrows()
			{
				throw Error;
			}

			public virtual void Test1()
			{
			}

			public virtual void Test2()
			{
			}
		}

		public virtual void TestConstructorFailuresAppearAsFailedTestCases()
		{
			ReflectionTestSuiteBuilder builder = new ReflectionTestSuiteBuilder(typeof(ReflectionTestSuiteBuilderTestCase.ConstructorThrows
				));
			Assert.AreEqual(2, Iterators.ToArray(builder.GetEnumerator()).Length);
		}

		private Exception AssertFailingTestCase(Type expectedError, ReflectionTestSuiteBuilder
			 builder)
		{
			IEnumerator tests = builder.GetEnumerator();
			FailingTest test = (FailingTest)Iterators.Next(tests);
			Assert.AreSame(expectedError, test.Error().GetType());
			return test.Error();
		}
	}
}

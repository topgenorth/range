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
	public class FrameworkTestCase : ITestCase
	{
		public static readonly Exception Exception = new Exception();

		public virtual void TestRunsGreen()
		{
			TestResult result = new TestResult();
			new TestRunner(Iterators.Cons(new RunsGreen())).Run(result);
			Assert.IsTrue(result.Failures.Size() == 0, "not green");
		}

		public virtual void TestRunsRed()
		{
			TestResult result = new TestResult();
			new TestRunner(Iterators.Cons(new RunsRed(Exception))).Run(result);
			Assert.IsTrue(result.Failures.Size() == 1, "not red");
		}

		public static void RunTestAndExpect(ITest test, int expFailures)
		{
			RunTestAndExpect(test, expFailures, true);
		}

		public static void RunTestAndExpect(ITest test, int expFailures, bool checkException
			)
		{
			RunTestAndExpect(Iterators.Cons(test), expFailures, checkException);
		}

		public static void RunTestAndExpect(IEnumerable tests, int expFailures, bool checkException
			)
		{
			TestResult result = new TestResult();
			new TestRunner(tests).Run(result);
			if (expFailures != result.Failures.Size())
			{
				Assert.Fail(result.Failures.ToString());
			}
			if (checkException)
			{
				for (IEnumerator iter = result.Failures.GetEnumerator(); iter.MoveNext(); )
				{
					TestFailure failure = (TestFailure)iter.Current;
					Assert.AreEqual(Exception, failure.GetFailure());
				}
			}
		}
	}
}

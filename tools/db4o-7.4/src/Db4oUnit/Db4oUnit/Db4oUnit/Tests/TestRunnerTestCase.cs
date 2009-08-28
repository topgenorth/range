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
using Db4oUnit.Mocking;
using Db4oUnit.Tests;
using Db4objects.Db4o.Foundation;

namespace Db4oUnit.Tests
{
	public class TestRunnerTestCase : ITestCase
	{
		internal static readonly Exception FailureException = new Exception();

		public virtual void TestRun()
		{
			RunsGreen greenTest = new RunsGreen();
			RunsRed redTest = new RunsRed(FailureException);
			IEnumerable tests = Iterators.Iterable(new object[] { greenTest, redTest });
			MethodCallRecorder recorder = new MethodCallRecorder();
			ITestListener listener = new _ITestListener_23(recorder);
			new TestRunner(tests).Run(listener);
			recorder.Verify(new MethodCall[] { new MethodCall("runStarted"), new MethodCall("testStarted"
				, greenTest), new MethodCall("testStarted", redTest), new MethodCall("testFailed"
				, redTest, FailureException), new MethodCall("runFinished") });
		}

		private sealed class _ITestListener_23 : ITestListener
		{
			public _ITestListener_23(MethodCallRecorder recorder)
			{
				this.recorder = recorder;
			}

			public void TestStarted(ITest test)
			{
				recorder.Record(new MethodCall("testStarted", test));
			}

			public void TestFailed(ITest test, Exception failure)
			{
				recorder.Record(new MethodCall("testFailed", test, failure));
			}

			public void RunStarted()
			{
				recorder.Record(new MethodCall("runStarted"));
			}

			public void RunFinished()
			{
				recorder.Record(new MethodCall("runFinished"));
			}

			private readonly MethodCallRecorder recorder;
		}

		public virtual void TestRunWithException()
		{
			ITest test = new _ITest_54();
			//$NON-NLS-1$
			IEnumerable tests = Iterators.Iterable(new object[] { test });
			TestResult result = new TestResult();
			new TestRunner(tests).Run(result);
			Assert.AreEqual(1, result.Failures.Size());
		}

		private sealed class _ITest_54 : ITest
		{
			public _ITest_54()
			{
			}

			public string Label()
			{
				return "Test";
			}

			public void Run()
			{
				Assert.AreEqual(0, 1);
			}
		}
	}
}

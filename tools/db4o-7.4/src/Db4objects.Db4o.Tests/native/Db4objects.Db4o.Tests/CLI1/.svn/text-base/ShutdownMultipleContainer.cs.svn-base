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
using System.IO;

using Db4objects.Db4o;

using Db4oUnit;

namespace Db4objects.Db4o.Tests
{
#if !CF
	public class ShutdownMultipleContainer : ITestLifeCycle
	{
		private static readonly string _firstFile = "first.db4o";
		private static readonly string _secondFile = "second.db4o";

		public class Runner : MarshalByRefObject
		{
			public void Run(TextWriter err)
			{
				Console.SetError(err);
				Db4oFactory.OpenFile(_firstFile);
				Db4oFactory.OpenFile(_secondFile);
			}
		}

		public void Test()
		{
			TextWriter stderr = Console.Error;
			StringWriter output = new StringWriter();
			try
			{
				Console.SetError(output);
				RunTestInAnotherDomain();
				CheckOutput(output.ToString());
			}
			finally
			{
				Console.SetError(stderr);
			}
		}

		void CheckOutput(string output)
		{
			Assert.AreNotEqual(-1, output.IndexOf(_firstFile));
			Assert.AreNotEqual(-1, output.IndexOf(_secondFile));
		}

		void RunTestInAnotherDomain()
		{
			AppDomain testDomain = AppDomain.CreateDomain("testDomain");

			Runner r = (Runner)testDomain.CreateInstanceAndUnwrap(
				GetType().Assembly.FullName, typeof(Runner).FullName);

			r.Run(Console.Error);

			AppDomain.Unload(testDomain);
		}

		public void SetUp()
		{
		}

		public void TearDown()
		{
			Clean(_firstFile);
			Clean(_secondFile);
		}

		static void Clean(string file)
		{
			if (File.Exists(file)) File.Delete(file);
		}
	}
#endif
}

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
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Tests.Util;
using Db4oUnit;

namespace Db4objects.Db4o.Tests.CLI1
{
	class JavaSnippet
	{
		public readonly string MainClassName;

		public readonly string SourceCode;

		public JavaSnippet(string mainClassName, string sourceCode)
		{
			this.MainClassName = mainClassName;
			this.SourceCode = sourceCode;
		}

		public string MainClassFile
		{
			get { return MainClassName.Replace('.', '/') + ".java";  }
		}
	}

	internal abstract class JavaCompatibilityTestCaseBase : ITestCase
	{
		protected abstract JavaSnippet JavaCode();

		protected abstract string ExpectedJavaOutput();

		protected abstract void PopulateContainer(IObjectContainer container);

		protected virtual IConfiguration GetConfiguration()
		{
			return Db4oFactory.NewConfiguration();
		}

		protected void RunTest()
		{
			if (!JavaServices.CanRunJavaCompatibilityTests())
			{
				return;
			}

			GenerateDataFile();
			CompileJavaSnippet();
			string output = RunJavaSnippet();
			AssertJavaOutput(output);
		}

		private void AssertJavaOutput(string output)
		{
//			Console.WriteLine(output);
			string actual = Normalize(output);
			string expected = Normalize(ExpectedJavaOutput());
			if (Contains(actual, expected)) return;

			Assert.Fail(string.Format("Expecting '{0}' got '{1}'", expected, actual));
		}

		private bool Contains(string s, string what)
		{
			return -1 != s.IndexOf(what);
		}

		private string Normalize(string output)
		{
			return output.Trim().Replace("\r\n", "\n");
		}

		private string RunJavaSnippet()
		{
			return JavaServices.java(JavaCode().MainClassName, DataFilePath());
		}

		private void CompileJavaSnippet()
		{
			JavaServices.ResetJavaTempPath();
			JavaSnippet program = JavaCode();
			string stdout = JavaServices.CompileJavaCode(program.MainClassFile, program.SourceCode);
			Console.WriteLine(stdout);
		}

		private void GenerateDataFile()
		{
			System.IO.File.Delete(DataFilePath());
			using (IObjectContainer container = Db4oFactory.OpenFile(GetConfiguration(), DataFilePath()))
			{
				PopulateContainer(container);
				container.Commit();
			}
		}

		private string DataFilePath()
		{
			return IOServices.BuildTempPath(GetType().Name + ".db4o");
		}
	}
}
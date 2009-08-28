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
using System.IO;
using Db4oTool.Tests.Core;
using System.Reflection;
using System;

namespace Db4objects.Db4o.Linq.Instrumentation.Tests
{
	// TODO: integrate into the build
	class Program
	{
		const string TestAssemblyFileName = "Db4objects.Db4o.Linq.Tests.exe";

		static void Main(string[] args)
		{
			var path = ExecutingAssemblyPath();
			CopyToTemp(Path.Combine(path, TestAssemblyFileName));
			CopyAllToTemp(Directory.GetFiles(path, "*.dll"));

			var testAssemblyFile = Path.Combine(GetTempPath(), TestAssemblyFileName);
			InstrumentAssembly(testAssemblyFile);

			var domain = AppDomain.CreateDomain("LinqTests", null, new AppDomainSetup { ApplicationBase = GetTempPath() });
			domain.ExecuteAssembly(testAssemblyFile);
		}

		private static string GetTempPath()
		{
			return ShellUtilities.GetTempPath();
		}

		private static void InstrumentAssembly(string testAssemblyFile)
		{
			var options = new Db4oTool.ProgramOptions()
			{
				Assembly = testAssemblyFile,
				TransparentPersistence = true,
			};
			Db4oTool.Program.Run(options);
		}

		private static void CopyAllToTemp(string[] files)
		{
			foreach (var fname in files)
			{
				CopyToTemp(fname);
			}
		}

		private static void CopyToTemp(string fname)
		{
			ShellUtilities.CopyToTemp(fname);
		}

		private static string ExecutingAssemblyPath()
		{
			return Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
		}
	}
}

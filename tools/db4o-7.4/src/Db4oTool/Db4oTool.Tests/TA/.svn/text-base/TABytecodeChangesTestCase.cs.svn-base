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
using Cecil.FlowAnalysis.Utilities;
using Db4oTool.Tests.Core;
using Db4oUnit;
using Mono.Cecil;

namespace Db4oTool.Tests.TA
{
	class TABytecodeChangesTestCase : ITestCase
	{
		public void TestThisFieldPattern()
		{
			string assemblyPath = InstrumentResource();
			AssemblyDefinition assembly = AssemblyFactory.GetAssembly(assemblyPath);
			MethodDefinition method = GetMethodDefinition(assembly, "Subject", "get_Property");
			string expected = @"
System.String Subject::get_Property()
	IL_0000: nop
	IL_0001: ldarg.0
	IL_0002: ldc.i4.0
	IL_0003: callvirt void Db4objects.Db4o.TA.IActivatable::Activate(Db4objects.Db4o.Activation.ActivationPurpose)
	IL_0008: ldarg.0
	IL_0009: ldfld System.String Subject::_field
	IL_000e: stloc.0
	IL_000f: br.s IL_0011
	IL_0011: ldloc.0
	IL_0012: ret";
			AssertIgnoringWhiteSpace(expected, Formatter.FormatMethodBody(method));
		}

		private static void AssertIgnoringWhiteSpace(string expected, string actual)
		{
			Assert.AreEqual(NormalizeWhiteSpace(expected), NormalizeWhiteSpace(actual));
		}

		private static string NormalizeWhiteSpace(string expected)
		{
			return expected.Replace("\r\n", "\n").Trim();
		}

		private MethodDefinition GetMethodDefinition(AssemblyDefinition assembly, string typeName, string methodName)
		{
			return assembly.MainModule.Types[typeName].Methods.GetMethod(methodName)[0];
		}

		private string InstrumentResource()
		{
			string resourceName = ResourceServices.CompleteResourceName(GetType(), "TABytecodeChangesSubject");
			string path = CompilationServices.EmitAssemblyFromResource(resourceName);
			ShellUtilities.ProcessOutput output = InstrumentationServices.InstrumentAssembly("-ta", path);
			Assert.AreEqual(0, output.ExitCode);
			return path;
		}
	}
}

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
using System.Diagnostics;
using System.IO;
using System.Text;
using Db4oTool.Tests.Core;
using Db4oUnit;
using Mono.Cecil;

namespace Db4oTool.Tests.TA
{
	class TAWarningOnSignedAssemblyInstrumentation : TATestCaseBase
	{
		public void TestDelaySign()
		{
			AssertInstrumentingSignedAssembly(true, true);
		}

		public void TestSignedAssembly()
		{
			AssertInstrumentingSignedAssembly(true, false);
		}

		public void TestNoWarningForUnsignedAssemblies()
		{
			AssertInstrumentingSignedAssembly(false, false);
		}

		private void AssertInstrumentingSignedAssembly(bool sign, bool delaySign)
		{
			string signKeyPath = GenerateKeyToSign();
			AssemblyDefinition assembly = null;

			if (sign)
			{
				CompilationServices.KeyFile.Using(
					new SignConfiguration(signKeyPath, delaySign),
					delegate
						{
							assembly = GenerateAssembly(ResourceName);
						});
			}
			else
			{
				assembly = GenerateAssembly(ResourceName);
			}

			Assert.AreEqual(sign, assembly.Name.HasPublicKey);

			TraceListener listener = new TraceListener();
			Trace.Listeners.Add(listener);

			Db4oTool.Program.Main(new string[] { "-ta", assembly.MainModule.Image.FileInformation.FullName });

			Trace.Listeners.Remove(listener);
			Assert.AreEqual(sign, listener.Contents.Contains("has been signed"));
		}

		private static string GenerateKeyToSign()
		{
			string keyPath = Path.Combine(Path.GetTempPath(), "db4otool-test.skn");
			if (!File.Exists(keyPath))
			{
				ProcessStartInfo psi = new ProcessStartInfo("sn.exe", "-k " + AppendDoubleQuotationMarks(keyPath));
				psi.RedirectStandardOutput = true;
				psi.RedirectStandardError = true;
				psi.UseShellExecute = false;

				Process sn = Process.Start(psi);

				if (sn == null)
				{
					Assert.Fail("Failed to generate a key for testing...");
				}
				else
				{
					sn.WaitForExit();
				}

				//string output = sn.StandardOutput.ReadToEnd();
			}
			return AppendDoubleQuotationMarks(keyPath);
		}

		private static string AppendDoubleQuotationMarks(string path)
		{
			return "\"" + path + "\"";
		}

		private const string ResourceName = "TASignedAssemblySubject";
	}

	internal class TraceListener : System.Diagnostics.TraceListener
	{
		private readonly StringBuilder _contents = new StringBuilder();

		public override void Write(string message)
		{
			_contents.Append(message);
		}

		public override void WriteLine(string message)
		{
			Write(message + Environment.NewLine);
		}

		public string Contents
		{
			get { return _contents.ToString(); }
		}
	}
}

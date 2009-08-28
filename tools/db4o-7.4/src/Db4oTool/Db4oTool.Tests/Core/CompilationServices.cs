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
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Text;

namespace Db4oTool.Tests.Core
{
	/// <summary>
	/// Compilation helper.
	/// </summary>
	public class CompilationServices
	{
		public static readonly ContextVariable<bool> Debug = new ContextVariable<bool>(true);
		public static readonly ContextVariable<bool> Unsafe = new ContextVariable<bool>(false);
		public static readonly ContextVariable<SignConfiguration> KeyFile = new ContextVariable<SignConfiguration>(null);
		public static readonly ContextVariable<string> ExtraParameters = new ContextVariable<string>("");

		public static void EmitAssembly(string assemblyFileName, Assembly[] references, params string[] sourceFiles)
		{
			string basePath = Path.GetDirectoryName(assemblyFileName);
			CreateDirectoryIfNeeded(basePath);
			CompileFromFile(assemblyFileName, references, sourceFiles);
		}

		public static void CreateDirectoryIfNeeded(string directory)
		{
			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}
		}

		static CompilerInfo GetCSharpCompilerInfo()
		{
			return CodeDomProvider.GetCompilerInfo(CodeDomProvider.GetLanguageFromExtension(".cs"));
		}

		static CodeDomProvider GetCSharpCodeDomProvider()
		{
			return GetCSharpCompilerInfo().CreateProvider();
		}

		static CompilerParameters CreateDefaultCompilerParameters()
		{
			return GetCSharpCompilerInfo().CreateDefaultCompilerParameters();
		}

		public static void CompileFromFile(string assemblyFName, Assembly[] references, params string[] sourceFiles)
		{
			using (CodeDomProvider provider = GetCSharpCodeDomProvider())
			{
				CompilerParameters parameters = CreateDefaultCompilerParameters();
				// TODO: run test cases in both modes (optimized and debug)
				parameters.IncludeDebugInformation = Debug.Value;
				parameters.OutputAssembly = assemblyFName;
				
				if (Unsafe.Value) parameters.CompilerOptions = "/unsafe";
				if (KeyFile.Value != null)
				{
					parameters.CompilerOptions += " /keyfile:" + KeyFile.Value.KeyFile;
					parameters.CompilerOptions += " /delaysign" + (KeyFile.Value.DelaySign ? '+' : '-');
				}

				parameters.CompilerOptions += " " + ExtraParameters.Value;

				foreach (Assembly reference in references)
				{
					parameters.ReferencedAssemblies.Add(reference.ManifestModule.FullyQualifiedName);
				}
				CompilerResults results = provider.CompileAssemblyFromFile(parameters, sourceFiles);
                if (ContainsErrors(results.Errors))
                {
					throw new ApplicationException(GetErrorString(results.Errors));
				}
			}
		}

        private static Boolean ContainsErrors(CompilerErrorCollection errors)
        {
            foreach (CompilerError error in errors)
            {
                if (!error.IsWarning)
                {
                    return true;
                }
            }
            return false;
        }

        public static string EmitAssemblyFromResource(string resourceName, params Assembly[] references)
        {
            string assemblyFileName = Path.Combine(ShellUtilities.GetTempPath(), resourceName + (Debug.Value ? ".Debug.dll" : ".dll"));
            string sourceFileName = Path.Combine(ShellUtilities.GetTempPath(), resourceName);
            File.WriteAllText(sourceFileName, ResourceServices.GetResourceAsString(resourceName));
            DeleteAssemblyAndPdb(assemblyFileName);
            EmitAssembly(assemblyFileName, references, sourceFileName);
            return assemblyFileName;
        }

        private static void DeleteAssemblyAndPdb(string path)
        {
            ShellUtilities.DeleteFile(Path.ChangeExtension(path, ".pdb"));
            ShellUtilities.DeleteFile(path);
        }

        static string GetErrorString(CompilerErrorCollection errors)
		{
			StringBuilder builder = new StringBuilder();
			foreach (CompilerError error in errors)
			{
				builder.Append(error.ToString());
				builder.Append(Environment.NewLine);
			}
			return builder.ToString();
		}

		private CompilationServices()
		{
		}
	}
}

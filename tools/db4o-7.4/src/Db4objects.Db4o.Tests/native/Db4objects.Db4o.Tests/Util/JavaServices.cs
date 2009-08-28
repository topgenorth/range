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
using System.Text;
using Db4oUnit;

namespace Db4objects.Db4o.Tests.Util
{
	class JavaServices
	{
		public static bool CanRunJavaCompatibilityTests()
		{
#if CF 
			return false;
#else
			if (null == WorkspaceServices.WorkspaceRoot)
			{
				Console.WriteLine("'db4obuild' directory not found, skipping java compatibility test.");
				return false;
			}
			return true;
#endif
		}

		public static string Db4ojarPath()
		{
			string db4oVersion = string.Format("{0}.{1}.{2}.{3}", Db4oVersion.Major, Db4oVersion.Minor,
                Db4oVersion.Iteration, Db4oVersion.Revision);
			string distDir = WorkspaceServices.ReadProperty(WorkspaceServices.MachinePropertiesPath(), "dir.dist", true);
			if(distDir == null || distDir.Length == 0)
			{
				distDir = "db4obuild/dist";
			}
			return WorkspaceServices.WorkspacePath(distDir + "/java/lib/db4o-" + db4oVersion + "-java1.2.jar");
		}

		public static string JavaTempPath
		{
			get { return IOServices.BuildTempPath("java"); }
		}

		public static void ResetJavaTempPath()
		{
			string tempPath = JavaServices.JavaTempPath;
			if (Directory.Exists(tempPath)) Directory.Delete(tempPath, true);
		}

		public static string CompileJavaCode(string fname, string code)
		{
			string srcFile = Path.Combine(JavaServices.JavaTempPath, fname);
			IOServices.WriteFile(srcFile, code);
			return javac(srcFile);
		}

		public static string javac(string srcFile)
		{
#if CF 
            return null;
#else
			string jarPath = JavaServices.Db4ojarPath();
			Assert.IsTrue(File.Exists(jarPath), string.Format("'{0}' not found. Make sure the jar was built before running this test.", jarPath));
			return IOServices.Exec(WorkspaceServices.JavacPath(),
                    "-classpath",
                    jarPath,
                    srcFile);
#endif
		}

		public static string java(string className, params string[] args)
		{
#if CF
            return null;
#else
            return IOServices.Exec(WorkspaceServices.JavaPath(),
                    "-cp",
                    IOServices.JoinQuotedArgs(Path.PathSeparator, JavaServices.JavaTempPath, Db4ojarPath()),
                    className,
                    IOServices.JoinQuotedArgs(args));
#endif
        }
	}
}

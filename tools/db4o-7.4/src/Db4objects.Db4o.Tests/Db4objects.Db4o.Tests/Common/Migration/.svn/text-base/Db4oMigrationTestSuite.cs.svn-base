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
using Db4objects.Db4o.Tests.Common.Freespace;
using Db4objects.Db4o.Tests.Common.Handlers;
using Db4objects.Db4o.Tests.Common.Migration;
using Db4objects.Db4o.Tests.Util;
using Sharpen;

namespace Db4objects.Db4o.Tests.Common.Migration
{
	public class Db4oMigrationTestSuite : ITestSuiteBuilder
	{
		public static void Main(string[] args)
		{
			new ConsoleTestRunner(typeof(Db4oMigrationTestSuite)).Run();
		}

		public virtual IEnumerator GetEnumerator()
		{
			return new Db4oMigrationSuiteBuilder(TestCases(), Libraries()).GetEnumerator();
		}

		private string[] Libraries()
		{
			if (true)
			{
				return Db4oMigrationSuiteBuilder.All;
			}
			if (true)
			{
				// run against specific libraries + the current one
				string netPath = "db4o.archives/net-2.0/7.2/Db4objects.Db4o.dll";
				string javaPath = "db4o.archives/java1.2/db4o-7.2.31.10304-java1.2.jar";
				return new string[] { WorkspaceServices.WorkspacePath(javaPath) };
			}
			return Db4oMigrationSuiteBuilder.Current;
		}

		protected virtual Type[] TestCases()
		{
			Type[] classes = new Type[] { typeof(BooleanHandlerUpdateTestCase), typeof(ByteHandlerUpdateTestCase
				), typeof(CascadedDeleteFileFormatUpdateTestCase), typeof(CharHandlerUpdateTestCase
				), typeof(DateHandlerUpdateTestCase), typeof(DoubleHandlerUpdateTestCase), typeof(
				FloatHandlerUpdateTestCase), typeof(IntHandlerUpdateTestCase), typeof(InterfaceHandlerUpdateTestCase
				), typeof(LongHandlerUpdateTestCase), typeof(MultiDimensionalArrayHandlerUpdateTestCase
				), typeof(NestedArrayUpdateTestCase), typeof(ObjectArrayUpdateTestCase), typeof(
				ShortHandlerUpdateTestCase), typeof(StringHandlerUpdateTestCase), typeof(IxFreespaceMigrationTestCase
				), typeof(FreespaceManagerMigrationTestCase) };
			// Order to run freespace tests last is
			// deliberate. Global configuration Db4o.configure()
			// is changed in the #setUp call and reused.
			return AddJavaTestCases(classes);
		}

		protected virtual Type[] AddJavaTestCases(Type[] classes)
		{
			Type[] javaTestCases = null;
			if (javaTestCases == null)
			{
				return classes;
			}
			int len = javaTestCases.Length;
			Type[] allClasses = new Type[classes.Length + len];
			System.Array.Copy(javaTestCases, 0, allClasses, 0, len);
			System.Array.Copy(classes, 0, allClasses, len, classes.Length);
			return allClasses;
		}
	}
}

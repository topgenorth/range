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
using Db4oUnit;
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation.IO;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Tests.Common.Exceptions;
using Db4objects.Db4o.Tests.Util;

namespace Db4objects.Db4o.Tests.Common.Exceptions
{
	/// <exclude></exclude>
	public class OldFormatExceptionTestCase : ITestCase, IOptOutNoFileSystemData
	{
		public static void Main(string[] args)
		{
			new ConsoleTestRunner(typeof(OldFormatExceptionTestCase)).Run();
		}

		// It is also regression test for COR-634.
		/// <exception cref="Exception"></exception>
		public virtual void Test()
		{
			if (WorkspaceServices.WorkspaceRoot == null)
			{
				Sharpen.Runtime.Err.WriteLine("Build environment not available. Skipping test case..."
					);
				return;
			}
			if (!System.IO.File.Exists(SourceFile()))
			{
				Sharpen.Runtime.Err.WriteLine("Test source file " + SourceFile() + " not available. Skipping test case..."
					);
				return;
			}
			Db4oFactory.Configure().ReflectWith(Platform4.ReflectorForType(typeof(OldFormatExceptionTestCase
				)));
			Db4oFactory.Configure().AllowVersionUpdates(false);
			string oldDatabaseFilePath = OldDatabaseFilePath();
			Assert.Expect(typeof(OldFormatException), new _ICodeBlock_44(oldDatabaseFilePath)
				);
			Db4oFactory.Configure().AllowVersionUpdates(true);
			IObjectContainer container = null;
			try
			{
				container = Db4oFactory.OpenFile(oldDatabaseFilePath);
			}
			finally
			{
				if (container != null)
				{
					container.Close();
				}
			}
		}

		private sealed class _ICodeBlock_44 : ICodeBlock
		{
			public _ICodeBlock_44(string oldDatabaseFilePath)
			{
				this.oldDatabaseFilePath = oldDatabaseFilePath;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				Db4oFactory.OpenFile(oldDatabaseFilePath);
			}

			private readonly string oldDatabaseFilePath;
		}

		/// <exception cref="IOException"></exception>
		protected virtual string OldDatabaseFilePath()
		{
			string oldFile = IOServices.BuildTempPath("old_db.yap");
			File4.Copy(SourceFile(), oldFile);
			return oldFile;
		}

		private string SourceFile()
		{
			return WorkspaceServices.WorkspaceTestFilePath("db4oVersions/db4o_3.0.3");
		}
	}
}

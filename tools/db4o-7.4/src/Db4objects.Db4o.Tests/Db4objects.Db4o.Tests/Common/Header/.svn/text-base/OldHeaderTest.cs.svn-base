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
using Db4oUnit;
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o;
using Db4objects.Db4o.Foundation.IO;
using Db4objects.Db4o.Tests.Util;

namespace Db4objects.Db4o.Tests.Common.Header
{
	public class OldHeaderTest : ITestCase, IOptOutNoFileSystemData
	{
		/// <exception cref="IOException"></exception>
		public virtual void Test()
		{
			string originalFilePath = OriginalFilePath();
			string dbFilePath = DbFilePath();
			if (!System.IO.File.Exists(originalFilePath))
			{
				TestPlatform.EmitWarning(originalFilePath + " does not exist. Can not run " + GetType
					().FullName);
				return;
			}
			File4.Copy(originalFilePath, dbFilePath);
			Db4oFactory.Configure().AllowVersionUpdates(true);
			IObjectContainer oc = Db4oFactory.OpenFile(dbFilePath);
			try
			{
				Assert.IsNotNull(oc);
			}
			finally
			{
				oc.Close();
				Db4oFactory.Configure().AllowVersionUpdates(false);
			}
		}

		private static string OriginalFilePath()
		{
			return WorkspaceServices.WorkspaceTestFilePath("db4oVersions/db4o_5.5.2");
		}

		private static string DbFilePath()
		{
			return WorkspaceServices.WorkspaceTestFilePath("db4oVersions/db4o_5.5.2.yap");
		}
	}
}

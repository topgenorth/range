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
using System.Collections.Generic;
using System.IO;
using Db4objects.Db4o.Config;
using Db4oUnit;

namespace Db4objects.Db4o.Tests.CLI1.Reflect.Net
{
	class UninitializedObjectsWithFinalizerTestCase : ITestCase
	{
#if !CF
		public void TestUninitilizedObjects()
		{
			string databaseFileName = Path.GetTempFileName();
			File.Delete(databaseFileName);

			using (IObjectContainer db = Db4oFactory.OpenFile(NewConfiguration(), databaseFileName))
			{
				db.Store(new TestSubject("Test"));
				
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}

			using (IObjectContainer db = Db4oFactory.OpenFile(NewConfiguration(), databaseFileName))
			{
				IList <TestSubject> result = db.Query<TestSubject>();
				
				Assert.AreEqual(1, result.Count);
				db.Activate(result[0], 2);
				Assert.AreEqual("Test", result[0].name);
            }

			File.Delete(databaseFileName);
		}

		private static IConfiguration NewConfiguration()
		{
			return Db4oFactory.NewConfiguration();
		}
	}

	internal class TestSubject
	{
		public string name;

		public TestSubject(string _name)
		{
			name = _name;
		}

		~TestSubject()
		{
            // Just access an object method...
            name = name.ToUpper();
		}
#endif
    }
}

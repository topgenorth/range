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
using Db4objects.Db4o;
using Db4objects.Db4o.Ext;

namespace Db4objects.Db4o.Tests.Common.CS
{
	public class ObjectServerTestCase : ITestLifeCycle
	{
		private IExtObjectServer server;

		private string fileName;

		/// <exception cref="Exception"></exception>
		public virtual void SetUp()
		{
			fileName = Path.GetTempFileName();
			server = Db4oFactory.OpenServer(fileName, -1).Ext();
			server.GrantAccess(Credentials(), Credentials());
		}

		/// <exception cref="Exception"></exception>
		public virtual void TearDown()
		{
			server.Close();
			new Sharpen.IO.File(fileName).Delete();
		}

		public virtual void TestClientCount()
		{
			AssertClientCount(0);
			IObjectContainer client1 = Db4oFactory.OpenClient("localhost", Port(), Credentials
				(), Credentials());
			AssertClientCount(1);
			IObjectContainer client2 = Db4oFactory.OpenClient("localhost", Port(), Credentials
				(), Credentials());
			AssertClientCount(2);
			client1.Close();
			client2.Close();
		}

		// closing is asynchronous, relying on completion is hard
		// That's why there is no test here. 
		// ClientProcessesTestCase tests closing.
		private void AssertClientCount(int count)
		{
			Assert.AreEqual(count, server.ClientCount());
		}

		private int Port()
		{
			return server.Port();
		}

		private string Credentials()
		{
			return "DB4O";
		}
	}
}

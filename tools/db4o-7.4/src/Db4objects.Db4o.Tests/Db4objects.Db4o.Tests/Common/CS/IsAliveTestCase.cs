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
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Foundation.IO;
using Db4objects.Db4o.Internal.CS;

namespace Db4objects.Db4o.Tests.Common.CS
{
	public class IsAliveTestCase : ITestLifeCycle
	{
		private static readonly string Username = "db4o";

		private static readonly string Password = "db4o";

		private string filePath;

		public virtual void TestIsAlive()
		{
			IObjectServer server = OpenServer();
			int port = server.Ext().Port();
			ClientObjectContainer client = OpenClient(port);
			Assert.IsTrue(client.IsAlive());
			client.Close();
			server.Close();
		}

		public virtual void TestIsNotAlive()
		{
			IObjectServer server = OpenServer();
			int port = server.Ext().Port();
			ClientObjectContainer client = OpenClient(port);
			server.Close();
			Assert.IsFalse(client.IsAlive());
			client.Close();
		}

		/// <exception cref="Exception"></exception>
		public virtual void SetUp()
		{
			filePath = Path.GetTempFileName();
			File4.Delete(filePath);
		}

		/// <exception cref="Exception"></exception>
		public virtual void TearDown()
		{
			File4.Delete(filePath);
		}

		private IConfiguration Config()
		{
			return Db4oFactory.NewConfiguration();
		}

		private IObjectServer OpenServer()
		{
			IObjectServer server = Db4oFactory.OpenServer(Config(), filePath, -1);
			server.GrantAccess(Username, Password);
			return server;
		}

		private ClientObjectContainer OpenClient(int port)
		{
			ClientObjectContainer client = (ClientObjectContainer)Db4oFactory.OpenClient(Config
				(), "localhost", port, Username, Password);
			return client;
		}
	}
}

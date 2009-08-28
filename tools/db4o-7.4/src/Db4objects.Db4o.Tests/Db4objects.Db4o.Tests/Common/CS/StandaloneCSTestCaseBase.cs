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
using Db4oUnit.Extensions;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Foundation.IO;
using Db4objects.Db4o.Internal.CS;

namespace Db4objects.Db4o.Tests.Common.CS
{
	public abstract class StandaloneCSTestCaseBase : ITestCase
	{
		private int _port;

		public sealed class Item
		{
			// TODO fix db4ounit call logic - this should actually be run in C/S mode
		}

		/// <exception cref="Exception"></exception>
		public virtual void Test()
		{
			IConfiguration config = Db4oFactory.Configure();
			Configure(config);
			string fileName = DatabaseFile();
			File4.Delete(fileName);
			IObjectServer server = Db4oFactory.OpenServer(fileName, -1);
			_port = server.Ext().Port();
			try
			{
				server.GrantAccess("db4o", "db4o");
				RunTest();
			}
			finally
			{
				server.Close();
				File4.Delete(fileName);
			}
		}

		/// <exception cref="Exception"></exception>
		protected virtual void WithClient(IContainerBlock block)
		{
			ContainerServices.WithContainer(OpenClient(), block);
		}

		protected virtual ClientObjectContainer OpenClient()
		{
			return (ClientObjectContainer)Db4oFactory.OpenClient("localhost", _port, "db4o", 
				"db4o");
		}

		protected virtual int Port()
		{
			return _port;
		}

		/// <exception cref="Exception"></exception>
		protected abstract void RunTest();

		protected abstract void Configure(IConfiguration config);

		private string DatabaseFile()
		{
			return Path.Combine(Path.GetTempPath(), "cc.db4o");
		}
	}
}

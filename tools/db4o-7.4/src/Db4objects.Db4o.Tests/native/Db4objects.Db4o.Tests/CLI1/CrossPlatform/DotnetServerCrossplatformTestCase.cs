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
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Messaging;

namespace Db4objects.Db4o.Tests.CLI1.CrossPlatform 
{
	class DotnetServerCrossplatformTestCase : CrossplatformTestCaseBase, IMessageRecipient
	{
#if !CF
		public void Test()
		{
			foreach (Person p in persons)
			{
				InsertFromJavaClient(p.Year, p.Name, p.LocalReleaseDate);
			}

			AssertQueryFromJavaClient();
		}

		protected override string GetClientAliases()
		{
			return @"
	config.add(new com.db4o.config.DotnetSupport(true));
	config.addAlias(new com.db4o.config.TypeAlias(""Db4objects.Db4o.Tests.CLI1.CrossPlatform.Person, Db4objects.Db4o.Tests"", Person.class.getName()));
	config.addAlias(new com.db4o.config.TypeAlias(""Db4objects.Db4o.Tests.CLI1.CrossPlatform.Movies, Db4objects.Db4o.Tests"", Movies.class.getName()));";
		}

		protected override void StartServer()
		{
			string databasePath = InitDatabaseFile();
			_server = Db4oFactory.OpenServer(databasePath, HOST_PORT);
			_server.GrantAccess(USER_NAME, USER_PWD);

			_server.Ext().Configure().ClientServer().SetMessageRecipient(this);
		}

		private static string InitDatabaseFile()
		{
			string databaseFile = Path.Combine(Path.GetTempPath(), "CrossplatformDotnetServer.odb");
			if (File.Exists(databaseFile))
			{
				File.Delete(databaseFile);
			}

			return databaseFile;
		}

		protected override IConfiguration Config()
		{
			return Db4oFactory.NewConfiguration();
		}

#endif

		public void ProcessMessage(IMessageContext context, object message)
		{
			_server.Close();
		}

		private IObjectServer _server;
	}
}

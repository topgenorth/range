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
using Db4oUnit;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.CS;
using Db4objects.Db4o.Tests.Common.CS;

namespace Db4objects.Db4o.Tests.Common.CS
{
	public class SwitchingFilesFromMultipleClientsTestCase : StandaloneCSTestCaseBase
		, ITestLifeCycle
	{
		public class Data
		{
			public int _id;

			public Data(int id)
			{
				this._id = id;
			}
		}

		private int _counter;

		protected override void Configure(IConfiguration config)
		{
			config.ReflectWith(Platform4.ReflectorForType(typeof(SwitchingFilesFromMultipleClientsTestCase.Data
				)));
		}

		protected override void RunTest()
		{
			_counter = 0;
			ClientObjectContainer clientA = OpenClient();
			ClientObjectContainer clientB = OpenClient();
			AddData(clientA);
			AssertDataCount(clientA, clientB, 1, 0);
			clientA.Commit();
			AssertDataCount(clientA, clientB, 1, 1);
			clientA.SwitchToFile(SwitchingFilesFromClientUtil.FilenameA);
			AssertDataCount(clientA, clientB, 0, 1);
			AddData(clientA);
			AssertDataCount(clientA, clientB, 1, 1);
			clientA.Commit();
			AssertDataCount(clientA, clientB, 1, 1);
			clientB.SwitchToFile(SwitchingFilesFromClientUtil.FilenameB);
			AssertDataCount(clientA, clientB, 1, 0);
			AddData(clientA);
			AssertDataCount(clientA, clientB, 2, 0);
			clientA.Commit();
			AssertDataCount(clientA, clientB, 2, 0);
			AddData(clientB);
			AssertDataCount(clientA, clientB, 2, 1);
			clientA.SwitchToFile(SwitchingFilesFromClientUtil.FilenameB);
			AssertDataCount(clientA, clientB, 0, 1);
			clientB.Commit();
			AssertDataCount(clientA, clientB, 1, 1);
			AddData(clientA);
			clientA.Commit();
			AssertDataCount(clientA, clientB, 2, 2);
			AddData(clientB);
			clientB.Commit();
			AssertDataCount(clientA, clientB, 3, 3);
			clientB.SwitchToFile(SwitchingFilesFromClientUtil.FilenameA);
			AssertDataCount(clientA, clientB, 3, 2);
			clientA.SwitchToMainFile();
			AssertDataCount(clientA, clientB, 1, 2);
			clientB.SwitchToMainFile();
			AssertDataCount(clientA, clientB, 1, 1);
			clientA.Close();
			clientB.Close();
		}

		/// <exception cref="Exception"></exception>
		public virtual void SetUp()
		{
			SwitchingFilesFromClientUtil.DeleteFiles();
		}

		/// <exception cref="Exception"></exception>
		public virtual void TearDown()
		{
			SwitchingFilesFromClientUtil.DeleteFiles();
		}

		private void AssertDataCount(ClientObjectContainer clientA, ClientObjectContainer
			 clientB, int expectedA, int expectedB)
		{
			AssertDataCount(clientA, expectedA);
			AssertDataCount(clientB, expectedB);
		}

		private void AssertDataCount(ClientObjectContainer client, int expected)
		{
			Assert.AreEqual(expected, client.Query(typeof(SwitchingFilesFromMultipleClientsTestCase.Data
				)).Size());
		}

		private void AddData(ClientObjectContainer client)
		{
			client.Store(new SwitchingFilesFromMultipleClientsTestCase.Data(_counter++));
		}
	}
}

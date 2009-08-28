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
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.IO;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.CS;
using Db4objects.Db4o.Tests.Common.CS;

namespace Db4objects.Db4o.Tests.Common.CS
{
	public class ClientTransactionHandleTestCase : ITestLifeCycle
	{
		public virtual void TestHandles()
		{
			IConfiguration config = Db4oFactory.NewConfiguration();
			config.Io(new MemoryIoAdapter());
			LocalObjectContainer db = (LocalObjectContainer)Db4oFactory.OpenFile(config, SwitchingFilesFromClientUtil
				.MainfileName);
			ClientTransactionPool pool = new ClientTransactionPool(db);
			try
			{
				ClientTransactionHandle handleA = new ClientTransactionHandle(pool);
				Assert.AreEqual(db, handleA.Transaction().Container());
				ClientTransactionHandle handleB = new ClientTransactionHandle(pool);
				Assert.AreNotEqual(handleA.Transaction(), handleB.Transaction());
				Assert.AreEqual(db, handleB.Transaction().Container());
				Assert.AreEqual(2, pool.OpenTransactionCount());
				Assert.AreEqual(1, pool.OpenFileCount());
				handleA.AcquireTransactionForFile(SwitchingFilesFromClientUtil.FilenameA);
				Assert.AreEqual(3, pool.OpenTransactionCount());
				Assert.AreEqual(2, pool.OpenFileCount());
				Assert.AreNotEqual(db, handleA.Transaction().Container());
				handleB.AcquireTransactionForFile(SwitchingFilesFromClientUtil.FilenameA);
				Assert.AreEqual(4, pool.OpenTransactionCount());
				Assert.AreEqual(2, pool.OpenFileCount());
				Assert.AreNotEqual(handleA.Transaction(), handleB.Transaction());
				Assert.AreEqual(handleA.Transaction().Container(), handleB.Transaction().Container
					());
				handleA.ReleaseTransaction();
				Assert.AreEqual(db, handleA.Transaction().Container());
				Assert.AreNotEqual(db, handleB.Transaction().Container());
				Assert.AreEqual(3, pool.OpenTransactionCount());
				Assert.AreEqual(2, pool.OpenFileCount());
				handleB.ReleaseTransaction();
				Assert.AreEqual(db, handleB.Transaction().Container());
				Assert.AreEqual(2, pool.OpenTransactionCount());
				Assert.AreEqual(1, pool.OpenFileCount());
				handleB.Close();
				Assert.AreEqual(1, pool.OpenTransactionCount());
				handleA.Close();
				Assert.AreEqual(0, pool.OpenTransactionCount());
			}
			finally
			{
				pool.Close();
			}
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
	}
}

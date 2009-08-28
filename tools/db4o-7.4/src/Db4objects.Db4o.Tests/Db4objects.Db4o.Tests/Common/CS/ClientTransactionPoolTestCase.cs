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
	public class ClientTransactionPoolTestCase : ITestLifeCycle
	{
		public virtual void TestPool()
		{
			IConfiguration config = Db4oFactory.NewConfiguration();
			config.Io(new MemoryIoAdapter());
			LocalObjectContainer db = (LocalObjectContainer)Db4oFactory.OpenFile(config, SwitchingFilesFromClientUtil
				.MainfileName);
			ClientTransactionPool pool = new ClientTransactionPool(db);
			try
			{
				Assert.AreEqual(0, pool.OpenTransactionCount());
				Assert.AreEqual(1, pool.OpenFileCount());
				Transaction trans1 = pool.Acquire(SwitchingFilesFromClientUtil.MainfileName);
				Assert.AreEqual(db, trans1.Container());
				Assert.AreEqual(1, pool.OpenTransactionCount());
				Assert.AreEqual(1, pool.OpenFileCount());
				Transaction trans2 = pool.Acquire(SwitchingFilesFromClientUtil.FilenameA);
				Assert.AreNotEqual(db, trans2.Container());
				Assert.AreEqual(2, pool.OpenTransactionCount());
				Assert.AreEqual(2, pool.OpenFileCount());
				Transaction trans3 = pool.Acquire(SwitchingFilesFromClientUtil.FilenameA);
				Assert.AreEqual(trans2.Container(), trans3.Container());
				Assert.AreEqual(3, pool.OpenTransactionCount());
				Assert.AreEqual(2, pool.OpenFileCount());
				pool.Release(trans3, true);
				Assert.AreEqual(2, pool.OpenTransactionCount());
				Assert.AreEqual(2, pool.OpenFileCount());
				pool.Release(trans2, true);
				Assert.AreEqual(1, pool.OpenTransactionCount());
				Assert.AreEqual(1, pool.OpenFileCount());
			}
			finally
			{
				Assert.IsFalse(db.IsClosed());
				Assert.IsFalse(pool.IsClosed());
				pool.Close();
				Assert.IsTrue(db.IsClosed());
				Assert.IsTrue(pool.IsClosed());
				Assert.AreEqual(0, pool.OpenFileCount());
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

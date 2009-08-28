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
using Db4oUnit.Extensions;
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Tests.Common.Internal;

namespace Db4objects.Db4o.Tests.Common.Internal
{
	public class StoredClassInstanceCountTestCase : AbstractDb4oTestCase
	{
		public class ItemA
		{
		}

		public class ItemB
		{
		}

		private const int CountA = 5;

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			for (int idx = 0; idx < CountA; idx++)
			{
				Store(new StoredClassInstanceCountTestCase.ItemA());
			}
			Store(new StoredClassInstanceCountTestCase.ItemB());
		}

		public virtual void TestInstanceCount()
		{
			AssertInstanceCount(typeof(StoredClassInstanceCountTestCase.ItemA), CountA);
			AssertInstanceCount(typeof(StoredClassInstanceCountTestCase.ItemB), 1);
			Store(new StoredClassInstanceCountTestCase.ItemA());
			DeleteAll(typeof(StoredClassInstanceCountTestCase.ItemB));
			AssertInstanceCount(typeof(StoredClassInstanceCountTestCase.ItemA), CountA + 1);
			AssertInstanceCount(typeof(StoredClassInstanceCountTestCase.ItemB), 0);
		}

		public virtual void TestTransactionalInstanceCount()
		{
			if (!IsClientServer())
			{
				return;
			}
			IExtObjectContainer otherClient = ((Db4oClientServer)Fixture()).OpenNewClient();
			Store(new StoredClassInstanceCountTestCase.ItemA());
			DeleteAll(typeof(StoredClassInstanceCountTestCase.ItemB));
			AssertInstanceCount(Db(), typeof(StoredClassInstanceCountTestCase.ItemA), CountA 
				+ 1);
			AssertInstanceCount(Db(), typeof(StoredClassInstanceCountTestCase.ItemB), 0);
			AssertInstanceCount(otherClient, typeof(StoredClassInstanceCountTestCase.ItemA), 
				CountA);
			AssertInstanceCount(otherClient, typeof(StoredClassInstanceCountTestCase.ItemB), 
				1);
			Db().Commit();
			AssertInstanceCount(Db(), typeof(StoredClassInstanceCountTestCase.ItemA), CountA 
				+ 1);
			AssertInstanceCount(Db(), typeof(StoredClassInstanceCountTestCase.ItemB), 0);
			AssertInstanceCount(otherClient, typeof(StoredClassInstanceCountTestCase.ItemA), 
				CountA + 1);
			AssertInstanceCount(otherClient, typeof(StoredClassInstanceCountTestCase.ItemB), 
				0);
			otherClient.Store(new StoredClassInstanceCountTestCase.ItemB());
			AssertInstanceCount(Db(), typeof(StoredClassInstanceCountTestCase.ItemB), 0);
			AssertInstanceCount(otherClient, typeof(StoredClassInstanceCountTestCase.ItemB), 
				1);
			otherClient.Commit();
			AssertInstanceCount(Db(), typeof(StoredClassInstanceCountTestCase.ItemB), 1);
			AssertInstanceCount(otherClient, typeof(StoredClassInstanceCountTestCase.ItemB), 
				1);
			otherClient.Close();
		}

		private void AssertInstanceCount(Type clazz, int expectedCount)
		{
			AssertInstanceCount(Db(), clazz, expectedCount);
		}

		private void AssertInstanceCount(IExtObjectContainer container, Type clazz, int expectedCount
			)
		{
			IStoredClass storedClazz = container.Ext().StoredClass(clazz);
			Assert.AreEqual(expectedCount, storedClazz.InstanceCount());
		}

		public static void Main(string[] args)
		{
			new StoredClassInstanceCountTestCase().RunAll();
		}
	}
}

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
using Db4objects.Db4o.Internal;

namespace Db4objects.Db4o.Tests.Common.References
{
	public class ReferenceSystemRegistryTestCase : ITestLifeCycle
	{
		private ReferenceSystemRegistry _registry;

		private IReferenceSystem _referenceSystem1;

		private IReferenceSystem _referenceSystem2;

		private static int TestId = 5;

		/// <exception cref="Exception"></exception>
		public virtual void SetUp()
		{
			_registry = new ReferenceSystemRegistry();
			_referenceSystem1 = new TransactionalReferenceSystem();
			_referenceSystem2 = new TransactionalReferenceSystem();
			_registry.AddReferenceSystem(_referenceSystem1);
			_registry.AddReferenceSystem(_referenceSystem2);
		}

		/// <exception cref="Exception"></exception>
		public virtual void TearDown()
		{
		}

		public virtual void TestRemoveId()
		{
			AddTestReference();
			_registry.RemoveId(TestId);
			AssertTestReferenceNotPresent();
		}

		public virtual void TestRemoveNull()
		{
			_registry.RemoveObject(null);
		}

		public virtual void TestRemoveObject()
		{
			ObjectReference testReference = AddTestReference();
			_registry.RemoveObject(testReference.GetObject());
			AssertTestReferenceNotPresent();
		}

		public virtual void TestRemoveReference()
		{
			ObjectReference testReference = AddTestReference();
			_registry.RemoveReference(testReference);
			AssertTestReferenceNotPresent();
		}

		public virtual void TestRemoveReferenceSystem()
		{
			AddTestReference();
			_registry.RemoveReferenceSystem(_referenceSystem1);
			_registry.RemoveId(TestId);
			Assert.IsNotNull(_referenceSystem1.ReferenceForId(TestId));
			Assert.IsNull(_referenceSystem2.ReferenceForId(TestId));
		}

		private void AssertTestReferenceNotPresent()
		{
			Assert.IsNull(_referenceSystem1.ReferenceForId(TestId));
			Assert.IsNull(_referenceSystem2.ReferenceForId(TestId));
		}

		private ObjectReference AddTestReference()
		{
			ObjectReference @ref = new ObjectReference(TestId);
			@ref.SetObject(new object());
			_referenceSystem1.AddExistingReference(@ref);
			_referenceSystem2.AddExistingReference(@ref);
			return @ref;
		}
	}
}

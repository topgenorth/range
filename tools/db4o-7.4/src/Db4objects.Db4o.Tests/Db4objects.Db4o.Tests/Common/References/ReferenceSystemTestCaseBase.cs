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
using Db4oUnit;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Tests.Common.References;

namespace Db4objects.Db4o.Tests.Common.References
{
	public abstract class ReferenceSystemTestCaseBase : ITestLifeCycle
	{
		private class Data
		{
		}

		private IReferenceSystem _refSys;

		public virtual void TestEmpty()
		{
			AssertNullReference(42, new ReferenceSystemTestCaseBase.Data());
		}

		public virtual void TestAddDeleteReaddOne()
		{
			int id = 42;
			ReferenceSystemTestCaseBase.Data data = new ReferenceSystemTestCaseBase.Data();
			ObjectReference @ref = CreateRef(id, data);
			_refSys.AddNewReference(@ref);
			AssertReference(id, data, @ref);
			_refSys.RemoveReference(@ref);
			AssertNullReference(id, data);
			_refSys.AddNewReference(@ref);
			AssertReference(id, data, @ref);
		}

		public virtual void TestDanglingReferencesAreRemoved()
		{
			int[] id = new int[] { 42, 43 };
			ReferenceSystemTestCaseBase.Data[] data = new ReferenceSystemTestCaseBase.Data[] 
				{ new ReferenceSystemTestCaseBase.Data(), new ReferenceSystemTestCaseBase.Data()
				 };
			ObjectReference ref0 = CreateRef(id[0], data[0]);
			ObjectReference ref1 = CreateRef(id[1], data[1]);
			_refSys.AddNewReference(ref0);
			_refSys.AddNewReference(ref1);
			_refSys.RemoveReference(ref0);
			_refSys.RemoveReference(ref1);
			_refSys.AddNewReference(ref0);
			AssertReference(id[0], data[0], ref0);
			AssertNullReference(id[1], data[1]);
		}

		private void AssertNullReference(int id, ReferenceSystemTestCaseBase.Data data)
		{
			Assert.IsNull(_refSys.ReferenceForId(id));
			Assert.IsNull(_refSys.ReferenceForObject(data));
		}

		private void AssertReference(int id, ReferenceSystemTestCaseBase.Data data, ObjectReference
			 @ref)
		{
			Assert.AreSame(@ref, _refSys.ReferenceForId(id));
			Assert.AreSame(@ref, _refSys.ReferenceForObject(data));
		}

		private ObjectReference CreateRef(int id, ReferenceSystemTestCaseBase.Data data)
		{
			ObjectReference @ref = new ObjectReference(id);
			@ref.SetObject(data);
			return @ref;
		}

		public virtual void SetUp()
		{
			_refSys = CreateReferenceSystem();
		}

		public virtual void TearDown()
		{
		}

		protected abstract IReferenceSystem CreateReferenceSystem();
	}
}

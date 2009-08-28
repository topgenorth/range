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
using Db4oUnit.Extensions;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Tests.Common.Btree;
using Db4objects.Db4o.Tests.Common.References;

namespace Db4objects.Db4o.Tests.Common.References
{
	public class ReferenceSystemIntegrationTestCase : AbstractDb4oTestCase
	{
		private static readonly int[] Ids = new int[] { 100, 134, 689, 666, 775 };

		private static readonly object[] References = CreateReferences();

		public static void Main(string[] args)
		{
			new ReferenceSystemIntegrationTestCase().RunSolo();
		}

		public virtual void TestTransactionalReferenceSystem()
		{
			TransactionalReferenceSystem transactionalReferenceSystem = new TransactionalReferenceSystem
				();
			AssertAllRerefencesAvailableOnNew(transactionalReferenceSystem);
			transactionalReferenceSystem.Rollback();
			AssertEmpty(transactionalReferenceSystem);
			AssertAllRerefencesAvailableOnCommit(transactionalReferenceSystem);
		}

		public virtual void TestHashCodeReferenceSystem()
		{
			IReferenceSystem referenceSystem = new HashcodeReferenceSystem();
			AssertAllRerefencesAvailableOnNew(referenceSystem);
		}

		public virtual void TestHashTableReferenceSystem()
		{
			IReferenceSystem referenceSystem = new HashtableReferenceSystem();
			AssertAllRerefencesAvailableOnNew(referenceSystem);
		}

		private void AssertAllRerefencesAvailableOnCommit(IReferenceSystem referenceSystem
			)
		{
			FillReferenceSystem(referenceSystem);
			referenceSystem.Commit();
			AssertAllReferencesAvailable(referenceSystem);
		}

		private void AssertAllRerefencesAvailableOnNew(IReferenceSystem referenceSystem)
		{
			FillReferenceSystem(referenceSystem);
			AssertAllReferencesAvailable(referenceSystem);
		}

		private void AssertEmpty(IReferenceSystem referenceSystem)
		{
			AssertContains(referenceSystem, new object[] {  });
		}

		private void AssertAllReferencesAvailable(IReferenceSystem referenceSystem)
		{
			AssertContains(referenceSystem, References);
		}

		private void AssertContains(IReferenceSystem referenceSystem, object[] objects)
		{
			ExpectingVisitor expectingVisitor = new ExpectingVisitor(objects);
			referenceSystem.TraverseReferences(expectingVisitor);
			expectingVisitor.AssertExpectations();
		}

		private void FillReferenceSystem(IReferenceSystem referenceSystem)
		{
			for (int i = 0; i < References.Length; i++)
			{
				referenceSystem.AddNewReference((ObjectReference)References[i]);
			}
		}

		private static object[] CreateReferences()
		{
			object[] references = new object[Ids.Length];
			for (int i = 0; i < Ids.Length; i++)
			{
				ObjectReference @ref = new ObjectReference(Ids[i]);
				@ref.SetObject(Ids[i].ToString());
				references[i] = @ref;
			}
			return references;
		}
	}
}

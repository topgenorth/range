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
using Db4oUnit.Extensions;
using Db4objects.Db4o.Tests.Common.Assorted;

namespace Db4objects.Db4o.Tests.Common.Assorted
{
	public class AllTests : Db4oTestSuite
	{
		public static void Main(string[] args)
		{
			new Db4objects.Db4o.Tests.Common.Assorted.AllTests().RunSolo();
		}

		protected override Type[] TestCases()
		{
			return new Type[] { typeof(AliasesTestCase), typeof(BackupStressTestCase), typeof(
				CallbackTestCase), typeof(CanUpdateFalseRefreshTestCase), typeof(CascadeDeleteDeletedTestCase
				), typeof(CascadedDeleteReadTestCase), typeof(ChangeIdentity), typeof(ClassMetadataTestCase
				), typeof(CloseUnlocksFileTestCase), typeof(ComparatorSortTestCase), typeof(DatabaseGrowthSizeTestCase
				), typeof(DatabaseUnicityTest), typeof(DeleteUpdateTestCase), typeof(DescendToNullFieldTestCase
				), typeof(DualDeleteTestCase), typeof(GetSingleSimpleArrayTestCase), typeof(HandlerRegistryTestCase
				), typeof(IndexCreateDropTestCase), typeof(IndexedBlockSizeQueryTestCase), typeof(
				InMemoryObjectContainerTestCase), typeof(LazyObjectReferenceTestCase), typeof(LockedTreeTestCase
				), typeof(LongLinkedListTestCase), typeof(MultiDeleteTestCase), typeof(ObjectConstructorTestCase
				), typeof(PlainObjectTestCase), typeof(PeekPersistedTestCase), typeof(PersistentIntegerArrayTestCase
				), typeof(PersistStaticFieldValuesTestCase), typeof(PersistTypeTestCase), typeof(
				PreventMultipleOpenTestCase), typeof(QueryByInterface), typeof(ReAddCascadedDeleteTestCase
				), typeof(RepeatDeleteReaddTestCase), typeof(RollbackDeleteTestCase), typeof(RollbackTestCase
				), typeof(RollbackUpdateTestCase), typeof(RollbackUpdateCascadeTestCase), typeof(
				SimplestPossibleNullMemberTestCase), typeof(SimplestPossibleTestCase), typeof(SimplestPossibleParentChildTestCase
				), typeof(SystemInfoTestCase), typeof(UpdateDepthTestCase) };
		}
		// FIXME: COR-1060
		//            DeleteSetTestCase.class,
	}
}

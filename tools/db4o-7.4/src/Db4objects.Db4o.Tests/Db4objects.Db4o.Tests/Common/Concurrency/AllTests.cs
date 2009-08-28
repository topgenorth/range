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
using Db4objects.Db4o.Tests.Common.Concurrency;

namespace Db4objects.Db4o.Tests.Common.Concurrency
{
	public class AllTests : Db4oConcurrencyTestSuite
	{
		public static void Main(string[] args)
		{
			new Db4objects.Db4o.Tests.Common.Concurrency.AllTests().RunConcurrency();
		}

		protected override Type[] TestCases()
		{
			return new Type[] { typeof(ArrayNOrderTestCase), typeof(ByteArrayTestCase), typeof(
				CascadeDeleteDeletedTestCase), typeof(CascadeDeleteFalseTestCase), typeof(CascadeOnActivateTestCase
				), typeof(CascadeOnUpdateTestCase), typeof(CascadeOnUpdate2TestCase), typeof(CascadeToVectorTestCase
				), typeof(CaseInsensitiveTestCase), typeof(Circular1TestCase), typeof(ClientDisconnectTestCase
				), typeof(CreateIndexInheritedTestCase), typeof(DeepSetTestCase), typeof(DeleteDeepTestCase
				), typeof(DifferentAccessPathsTestCase), typeof(ExtMethodsTestCase), typeof(GetAllTestCase
				), typeof(GreaterOrEqualTestCase), typeof(IndexedByIdentityTestCase), typeof(IndexedUpdatesWithNullTestCase
				), typeof(InternStringsTestCase), typeof(InvalidUUIDTestCase), typeof(IsStoredTestCase
				), typeof(MessagingTestCase), typeof(MultiDeleteTestCase), typeof(MultiLevelIndexTestCase
				), typeof(NestedArraysTestCase), typeof(ObjectSetIDsTestCase), typeof(ParameterizedEvaluationTestCase
				), typeof(PeekPersistedTestCase), typeof(PersistStaticFieldValuesTestCase), typeof(
				QueryForUnknownFieldTestCase), typeof(QueryNonExistantTestCase), typeof(ReadObjectNQTestCase
				), typeof(ReadObjectQBETestCase), typeof(ReadObjectSODATestCase), typeof(RefreshTestCase
				), typeof(UpdateObjectTestCase) };
		}
	}
}

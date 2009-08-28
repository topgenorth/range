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
using System.Collections;
using System.Collections.Generic;
using Db4oUnit;
using Db4oUnit.Fixtures;
using Db4objects.Drs.Db4o;
using Db4objects.Drs.Inside;
using Db4objects.Drs.Tests;
using Sharpen.Util;

namespace Db4objects.Drs.Tests
{
	public class SingleTypeCollectionReplicationTest : FixtureBasedTestSuite
	{
		public override IFixtureProvider[] FixtureProviders()
		{
			return new IFixtureProvider[] { new SubjectFixtureProvider(new object[] { Collection1
				(), Collection2(), Collection3() }) };
		}

		private object Collection1()
		{
			return Initialize(new CollectionHolder(new Hashtable(), new HashSet(), new ArrayList
				()));
		}

		private object Collection2()
		{
			return Initialize(new CollectionHolder(new Dictionary<string, string>(), new HashSet
				(), new List<string>()));
		}

		private object Collection3()
		{
			return Initialize(new CollectionHolder(new SortedList<string, string>(), new HashSet
				(), new ArrayList()));
		}

		private CollectionHolder Initialize(CollectionHolder h1)
		{
			h1.map.Add("1", "one");
			h1.map.Add("2", "two");
			h1.set.Add("two");
			h1.list.Add("three");
			return h1;
		}

		public override Type[] TestUnits()
		{
			return new Type[] { typeof(SingleTypeCollectionReplicationTest.TestUnit) };
		}

		public class TestUnit : DrsTestCase
		{
			public virtual void Test()
			{
				CollectionHolder h1 = Subject();
				StoreNewAndCommit(A().Provider(), h1);
				ReplicateAll(A().Provider(), B().Provider());
				IEnumerator it = B().Provider().GetStoredObjects(typeof(CollectionHolder)).GetEnumerator
					();
				Assert.IsTrue(it.MoveNext());
				CollectionHolder replica = (CollectionHolder)it.Current;
				AssertSameClassIfDb4o(h1.map, replica.map);
				foreach (object key in h1.map.Keys)
				{
					Assert.AreEqual(h1.map[key], replica.map[key]);
				}
				AssertSameClassIfDb4o(h1.set, replica.set);
				foreach (object element in h1.set)
				{
					Assert.IsTrue(replica.set.Contains(element));
				}
				AssertSameClassIfDb4o(h1.list, replica.list);
				Assert.AreEqual(h1.list.Count, replica.list.Count);
				CollectionAssert.AreEqual(h1.list, replica.list);
			}

			private CollectionHolder Subject()
			{
				return (CollectionHolder)SubjectFixtureProvider.Value();
			}

			private void AssertSameClassIfDb4o(object expectedInstance, object actualInstance
				)
			{
				if (!IsDb4oProvider(A()))
				{
					return;
				}
				if (!IsDb4oProvider(B()))
				{
					return;
				}
				Assert.AreSame(expectedInstance.GetType(), actualInstance.GetType());
			}

			private bool IsDb4oProvider(IDrsFixture fixture)
			{
				return fixture.Provider() is IDb4oReplicationProvider;
			}

			private void StoreNewAndCommit(ITestableReplicationProviderInside provider, CollectionHolder
				 h1)
			{
				provider.StoreNew(h1);
				provider.Activate(h1);
				provider.Commit();
			}
		}
	}
}

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
using Db4objects.Db4o.Events;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation;
using Db4objects.Drs.Db4o;
using Db4objects.Drs.Tests;
using Db4objects.Drs.Tests.Db4o;

namespace Db4objects.Drs.Tests.Db4o
{
	public class PartialCollectionReplicationTestCase : DrsTestCase
	{
		public class Data
		{
			private IList _children;

			private string _id;

			public Data(string id)
			{
				_id = id;
				_children = new ArrayList();
			}

			public virtual object Id()
			{
				return _id;
			}

			public virtual void Id(string id)
			{
				_id = id;
			}

			public virtual void Add(PartialCollectionReplicationTestCase.Data data)
			{
				_children.Add(data);
			}

			public override string ToString()
			{
				return "Data(" + _id + ", " + _children + ")";
			}
		}

		public virtual void TestNoReplicationForUntouchedElements()
		{
			PartialCollectionReplicationTestCase.Data root = new PartialCollectionReplicationTestCase.Data
				("root");
			PartialCollectionReplicationTestCase.Data c1 = new PartialCollectionReplicationTestCase.Data
				("c1");
			PartialCollectionReplicationTestCase.Data c2 = new PartialCollectionReplicationTestCase.Data
				("c2");
			root.Add(c1);
			root.Add(c2);
			Store(root, 1);
			IList<PartialCollectionReplicationTestCase.Data> created = ReplicateAllCapturingCreatedObjects
				();
			AssertData(created, "root", "c1", "c2");
			PartialCollectionReplicationTestCase.Data c3 = new PartialCollectionReplicationTestCase.Data
				("c3");
			root.Add(c3);
			Store(root, 2);
			c2.Id("c2*");
			c2.Add(new PartialCollectionReplicationTestCase.Data("c4"));
			IList<PartialCollectionReplicationTestCase.Data> updated = ReplicateAllCapturingUpdatedObjects
				();
			AssertData(updated, "root", "c3");
		}

		private void AssertData(IEnumerable<PartialCollectionReplicationTestCase.Data> data
			, params string[] expectedIds)
		{
			Iterator4Assert.SameContent(expectedIds, Ids(data));
		}

		private IEnumerator Ids(IEnumerable<PartialCollectionReplicationTestCase.Data> data
			)
		{
			Collection4 ids = new Collection4();
			foreach (PartialCollectionReplicationTestCase.Data d in data)
			{
				ids.Add(d.Id());
			}
			return ids.GetEnumerator();
		}

		private IList<PartialCollectionReplicationTestCase.Data> ReplicateAllCapturingUpdatedObjects
			()
		{
			IList<PartialCollectionReplicationTestCase.Data> changed = new List<PartialCollectionReplicationTestCase.Data
				>();
			ListenToUpdated(changed);
			ListenToCreated(changed);
			ReplicateAll();
			return changed;
		}

		private IList<PartialCollectionReplicationTestCase.Data> ReplicateAllCapturingCreatedObjects
			()
		{
			IList<PartialCollectionReplicationTestCase.Data> created = new List<PartialCollectionReplicationTestCase.Data
				>();
			ListenToCreated(created);
			ReplicateAll();
			return created;
		}

		private void ListenToUpdated(IList<PartialCollectionReplicationTestCase.Data> updated
			)
		{
			EventRegistryFor(B()).Updated += new Db4objects.Db4o.Events.ObjectEventHandler(new 
				_IEventListener4_93(this, updated).OnEvent);
		}

		private sealed class _IEventListener4_93
		{
			public _IEventListener4_93(PartialCollectionReplicationTestCase _enclosing, IList
				<PartialCollectionReplicationTestCase.Data> updated)
			{
				this._enclosing = _enclosing;
				this.updated = updated;
			}

			public void OnEvent(object sender, Db4objects.Db4o.Events.ObjectEventArgs args)
			{
				object o = ((ObjectEventArgs)args).Object;
				if (o is PartialCollectionReplicationTestCase.Data)
				{
					updated.Add((PartialCollectionReplicationTestCase.Data)o);
				}
				this._enclosing.Ods(o);
			}

			private readonly PartialCollectionReplicationTestCase _enclosing;

			private readonly IList<PartialCollectionReplicationTestCase.Data> updated;
		}

		private void ReplicateAll()
		{
			Ods("BEGIN REPLICATION");
			ReplicateAll(A().Provider(), B().Provider());
			Ods("END REPLICATION");
		}

		private void ListenToCreated(IList<PartialCollectionReplicationTestCase.Data> created
			)
		{
			EventRegistryFor(B()).Created += new Db4objects.Db4o.Events.ObjectEventHandler(new 
				_IEventListener4_111(this, created).OnEvent);
		}

		private sealed class _IEventListener4_111
		{
			public _IEventListener4_111(PartialCollectionReplicationTestCase _enclosing, IList
				<PartialCollectionReplicationTestCase.Data> created)
			{
				this._enclosing = _enclosing;
				this.created = created;
			}

			public void OnEvent(object sender, Db4objects.Db4o.Events.ObjectEventArgs args)
			{
				object o = ((ObjectEventArgs)args).Object;
				if (o is PartialCollectionReplicationTestCase.Data)
				{
					created.Add((PartialCollectionReplicationTestCase.Data)o);
				}
				this._enclosing.Ods(o);
			}

			private readonly PartialCollectionReplicationTestCase _enclosing;

			private readonly IList<PartialCollectionReplicationTestCase.Data> created;
		}

		private IEventRegistry EventRegistryFor(IDrsFixture fixture)
		{
			return EventRegistryFactory.ForObjectContainer(ContainerFor(fixture));
		}

		public virtual void TestCollectionUpdateDoesNotTouchExistingElements()
		{
			PartialCollectionReplicationTestCase.Data root = new PartialCollectionReplicationTestCase.Data
				("root");
			PartialCollectionReplicationTestCase.Data c1 = new PartialCollectionReplicationTestCase.Data
				("c1");
			PartialCollectionReplicationTestCase.Data c2 = new PartialCollectionReplicationTestCase.Data
				("c2");
			root.Add(c1);
			root.Add(c2);
			Store(root, 1);
			long c1Version = VersionFor(c1);
			long c2Version = VersionFor(c2);
			PartialCollectionReplicationTestCase.Data c3 = new PartialCollectionReplicationTestCase.Data
				("c3");
			root.Add(c3);
			Store(root, 2);
			Assert.IsGreater(0, VersionFor(c3));
			Assert.AreEqual(c1Version, VersionFor(c1));
			Assert.AreEqual(c2Version, VersionFor(c2));
		}

		private void Store(PartialCollectionReplicationTestCase.Data root, int depth)
		{
			IExtObjectContainer container = ContainerFor(A());
			container.Ext().Store(root, depth);
			container.Commit();
		}

		private IExtObjectContainer ContainerFor(IDrsFixture fixture)
		{
			return ((IDb4oReplicationProvider)fixture.Provider()).GetObjectContainer();
		}

		private long VersionFor(PartialCollectionReplicationTestCase.Data c1)
		{
			return ObjectInfoFor(c1).GetVersion();
		}

		private IObjectInfo ObjectInfoFor(PartialCollectionReplicationTestCase.Data c1)
		{
			return ContainerFor(A()).Ext().GetObjectInfo(c1);
		}

		private void Ods(object o)
		{
		}

		//		System.out.println(o);
		public static void Main(string[] args)
		{
			new ConsoleTestRunner(new DrsTestSuiteBuilder(new Db4oDrsFixture("db4o-a"), new Db4oDrsFixture
				("db4o-b"), typeof(PartialCollectionReplicationTestCase))).Run();
		}
	}
}

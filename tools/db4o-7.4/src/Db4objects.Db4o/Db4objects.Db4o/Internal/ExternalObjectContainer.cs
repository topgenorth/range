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
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Activation;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Replication;
using Db4objects.Db4o.Types;

namespace Db4objects.Db4o.Internal
{
	/// <exclude></exclude>
	public abstract class ExternalObjectContainer : ObjectContainerBase
	{
		public ExternalObjectContainer(IConfiguration config, ObjectContainerBase parentContainer
			) : base(config, parentContainer)
		{
		}

		public sealed override void Activate(object obj)
		{
			Activate(null, obj);
		}

		/// <exception cref="DatabaseClosedException"></exception>
		public sealed override void Activate(object obj, int depth)
		{
			Activate(null, obj, ActivationDepthProvider().ActivationDepth(depth, ActivationMode
				.Activate));
		}

		public sealed override void Deactivate(object obj)
		{
			Deactivate(null, obj);
		}

		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public sealed override void Bind(object obj, long id)
		{
			Bind(null, obj, id);
		}

		[System.ObsoleteAttribute]
		public override IDb4oCollections Collections()
		{
			return Collections(null);
		}

		/// <exception cref="DatabaseReadOnlyException"></exception>
		/// <exception cref="DatabaseClosedException"></exception>
		public sealed override void Commit()
		{
			Commit(null);
		}

		/// <exception cref="DatabaseClosedException"></exception>
		public sealed override void Deactivate(object obj, int depth)
		{
			Deactivate(null, obj, depth);
		}

		public sealed override void Delete(object a_object)
		{
			Delete(null, a_object);
		}

		public override object Descend(object obj, string[] path)
		{
			return Descend(null, obj, path);
		}

		public override IExtObjectContainer Ext()
		{
			return this;
		}

		/// <exception cref="DatabaseClosedException"></exception>
		[System.ObsoleteAttribute(@"Use")]
		public sealed override IObjectSet Get(object template)
		{
			return QueryByExample(template);
		}

		/// <exception cref="DatabaseClosedException"></exception>
		public sealed override IObjectSet QueryByExample(object template)
		{
			return QueryByExample(null, template);
		}

		/// <exception cref="DatabaseClosedException"></exception>
		/// <exception cref="InvalidIDException"></exception>
		public sealed override object GetByID(long id)
		{
			return GetByID(null, id);
		}

		public sealed override object GetByUUID(Db4oUUID uuid)
		{
			return GetByUUID(null, uuid);
		}

		public sealed override long GetID(object obj)
		{
			return GetID(null, obj);
		}

		public sealed override IObjectInfo GetObjectInfo(object obj)
		{
			return GetObjectInfo(null, obj);
		}

		public override bool IsActive(object obj)
		{
			return IsActive(null, obj);
		}

		public override bool IsCached(long id)
		{
			return IsCached(null, id);
		}

		public override bool IsStored(object obj)
		{
			return IsStored(null, obj);
		}

		/// <exception cref="DatabaseClosedException"></exception>
		public sealed override object PeekPersisted(object obj, int depth, bool committed
			)
		{
			return PeekPersisted(null, obj, ActivationDepthProvider().ActivationDepth(depth, 
				ActivationMode.Peek), committed);
		}

		public sealed override void Purge(object obj)
		{
			Purge(null, obj);
		}

		public override IQuery Query()
		{
			return Query((Transaction)null);
		}

		public sealed override IObjectSet Query(Type clazz)
		{
			return QueryByExample(clazz);
		}

		public sealed override IObjectSet Query(Predicate predicate)
		{
			return Query(predicate, (IQueryComparator)null);
		}

		public sealed override IObjectSet Query(Predicate predicate, IQueryComparator comparator
			)
		{
			return Query(null, predicate, comparator);
		}

		public sealed override void Refresh(object obj, int depth)
		{
			Refresh(null, obj, depth);
		}

		public sealed override void Rollback()
		{
			Rollback(null);
		}

		/// <exception cref="DatabaseClosedException"></exception>
		/// <exception cref="DatabaseReadOnlyException"></exception>
		[System.ObsoleteAttribute(@"Use")]
		public sealed override void Set(object obj)
		{
			Store(obj);
		}

		/// <exception cref="DatabaseClosedException"></exception>
		/// <exception cref="DatabaseReadOnlyException"></exception>
		public sealed override void Store(object obj)
		{
			Store(obj, Const4.Unspecified);
		}

		/// <exception cref="DatabaseClosedException"></exception>
		/// <exception cref="DatabaseReadOnlyException"></exception>
		[System.ObsoleteAttribute(@"Use")]
		public sealed override void Set(object obj, int depth)
		{
			Store(obj, depth);
		}

		/// <exception cref="DatabaseClosedException"></exception>
		/// <exception cref="DatabaseReadOnlyException"></exception>
		public sealed override void Store(object obj, int depth)
		{
			Store(null, obj, depth);
		}

		public sealed override IStoredClass StoredClass(object clazz)
		{
			return StoredClass(null, clazz);
		}

		public override IStoredClass[] StoredClasses()
		{
			return StoredClasses(null);
		}

		/// <exception cref="Db4oIOException"></exception>
		/// <exception cref="DatabaseClosedException"></exception>
		/// <exception cref="NotSupportedException"></exception>
		public abstract override void Backup(string path);

		public abstract override Db4oDatabase Identity();

		/// <param name="peerB"></param>
		/// <param name="conflictHandler"></param>
		[System.ObsoleteAttribute]
		public override IReplicationProcess ReplicationBegin(IObjectContainer peerB, IReplicationConflictHandler
			 conflictHandler)
		{
			throw new NotSupportedException();
		}
	}
}

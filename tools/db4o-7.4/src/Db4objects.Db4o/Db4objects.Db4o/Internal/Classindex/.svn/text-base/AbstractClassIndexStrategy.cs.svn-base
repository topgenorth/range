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
using System.Collections;
using Db4objects.Db4o;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Classindex;

namespace Db4objects.Db4o.Internal.Classindex
{
	/// <exclude></exclude>
	public abstract class AbstractClassIndexStrategy : IClassIndexStrategy
	{
		protected readonly ClassMetadata _yapClass;

		public AbstractClassIndexStrategy(ClassMetadata yapClass)
		{
			_yapClass = yapClass;
		}

		protected virtual int YapClassID()
		{
			return _yapClass.GetID();
		}

		public virtual int OwnLength()
		{
			return Const4.IdLength;
		}

		protected abstract void InternalAdd(Transaction trans, int id);

		public void Add(Transaction trans, int id)
		{
			if (DTrace.enabled)
			{
				DTrace.AddToClassIndex.Log(id);
			}
			CheckId(id);
			InternalAdd(trans, id);
		}

		protected abstract void InternalRemove(Transaction ta, int id);

		public void Remove(Transaction ta, int id)
		{
			if (DTrace.enabled)
			{
				DTrace.RemoveFromClassIndex.Log(id);
			}
			CheckId(id);
			InternalRemove(ta, id);
		}

		private void CheckId(int id)
		{
		}

		public abstract IEnumerator AllSlotIDs(Transaction arg1);

		public abstract void DefragIndex(DefragmentContextImpl arg1);

		public abstract void DefragReference(ClassMetadata arg1, DefragmentContextImpl arg2
			, int arg3);

		public abstract void DontDelete(Transaction arg1, int arg2);

		public abstract int EntryCount(Transaction arg1);

		public abstract int Id();

		public abstract void Initialize(ObjectContainerBase arg1);

		public abstract void Purge();

		public abstract void Read(ObjectContainerBase arg1, int arg2);

		public abstract void TraverseAll(Transaction arg1, IVisitor4 arg2);

		public abstract int Write(Transaction arg1);
	}
}

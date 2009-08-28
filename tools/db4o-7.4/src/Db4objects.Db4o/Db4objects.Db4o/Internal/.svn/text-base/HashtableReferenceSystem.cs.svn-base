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

namespace Db4objects.Db4o.Internal
{
	/// <exclude></exclude>
	public class HashtableReferenceSystem : IReferenceSystem
	{
		private Hashtable4 _hashCodeTable = new Hashtable4();

		private Hashtable4 _idTable = new Hashtable4();

		public virtual void AddNewReference(ObjectReference @ref)
		{
			AddReference(@ref);
		}

		public virtual void AddExistingReference(ObjectReference @ref)
		{
			AddReference(@ref);
		}

		private void AddReference(ObjectReference @ref)
		{
			@ref.Ref_init();
			IdAdd(@ref);
			HashCodeAdd(@ref);
		}

		public virtual void Commit()
		{
		}

		// do nothing
		private void HashCodeAdd(ObjectReference @ref)
		{
			_hashCodeTable.Put(HashCode(@ref), @ref);
		}

		private void IdAdd(ObjectReference @ref)
		{
			if (DTrace.enabled)
			{
				DTrace.IdTreeAdd.Log(@ref.GetID());
			}
			_idTable.Put(@ref.GetID(), @ref);
		}

		public virtual ObjectReference ReferenceForId(int id)
		{
			if (DTrace.enabled)
			{
				DTrace.GetYapobject.Log(id);
			}
			if (!ObjectReference.IsValidId(id))
			{
				return null;
			}
			return (ObjectReference)_idTable.Get(id);
		}

		public virtual ObjectReference ReferenceForObject(object obj)
		{
			if (_hashCodeTable == null)
			{
				return null;
			}
			return (ObjectReference)_hashCodeTable.Get(HashCode(obj));
		}

		private static int HashCode(object obj)
		{
			return ObjectReference.Hc_getCode(obj);
		}

		private static int HashCode(ObjectReference @ref)
		{
			return @ref._hcHashcode;
		}

		public virtual void RemoveReference(ObjectReference @ref)
		{
			if (DTrace.enabled)
			{
				DTrace.ReferenceRemoved.Log(@ref.GetID());
			}
			_hashCodeTable.Remove(HashCode(@ref));
			_idTable.Remove(@ref.GetID());
		}

		public virtual void Rollback()
		{
		}

		// do nothing
		public virtual void TraverseReferences(IVisitor4 visitor)
		{
			IEnumerator i = _hashCodeTable.Values();
			while (i.MoveNext())
			{
				visitor.Visit(i.Current);
			}
		}
	}
}

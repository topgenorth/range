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
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;

namespace Db4objects.Db4o.Internal
{
	/// <exclude></exclude>
	public class TransactionalReferenceSystem : IReferenceSystem
	{
		private readonly IReferenceSystem _committedReferences;

		private IReferenceSystem _newReferences;

		public TransactionalReferenceSystem()
		{
			CreateNewReferences();
			_committedReferences = NewReferenceSystem();
		}

		private IReferenceSystem NewReferenceSystem()
		{
			return new HashcodeReferenceSystem();
		}

		// An alternative reference system using a hashtable: 
		// return new HashtableReferenceSystem();
		public virtual void AddExistingReference(ObjectReference @ref)
		{
			_committedReferences.AddExistingReference(@ref);
		}

		public virtual void AddNewReference(ObjectReference @ref)
		{
			_newReferences.AddNewReference(@ref);
		}

		public virtual void Commit()
		{
			TraveseNewReferences(new _IVisitor4_38(this));
			CreateNewReferences();
		}

		private sealed class _IVisitor4_38 : IVisitor4
		{
			public _IVisitor4_38(TransactionalReferenceSystem _enclosing)
			{
				this._enclosing = _enclosing;
			}

			public void Visit(object obj)
			{
				ObjectReference oref = (ObjectReference)obj;
				object referent = oref.GetObject();
				if (referent != null)
				{
					this._enclosing._committedReferences.AddExistingReference(oref);
				}
			}

			private readonly TransactionalReferenceSystem _enclosing;
		}

		public virtual void TraveseNewReferences(IVisitor4 visitor)
		{
			_newReferences.TraverseReferences(visitor);
		}

		private void CreateNewReferences()
		{
			_newReferences = NewReferenceSystem();
		}

		public virtual ObjectReference ReferenceForId(int id)
		{
			ObjectReference @ref = _newReferences.ReferenceForId(id);
			if (@ref != null)
			{
				return @ref;
			}
			return _committedReferences.ReferenceForId(id);
		}

		public virtual ObjectReference ReferenceForObject(object obj)
		{
			ObjectReference @ref = _newReferences.ReferenceForObject(obj);
			if (@ref != null)
			{
				return @ref;
			}
			return _committedReferences.ReferenceForObject(obj);
		}

		public virtual void RemoveReference(ObjectReference @ref)
		{
			_newReferences.RemoveReference(@ref);
			_committedReferences.RemoveReference(@ref);
		}

		public virtual void Rollback()
		{
			CreateNewReferences();
		}

		public virtual void TraverseReferences(IVisitor4 visitor)
		{
			TraveseNewReferences(visitor);
			_committedReferences.TraverseReferences(visitor);
		}
	}
}

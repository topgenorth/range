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
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;

namespace Db4objects.Db4o.Internal
{
	/// <exclude></exclude>
	public class ReferenceSystemRegistry
	{
		private readonly Collection4 _referenceSystems = new Collection4();

		public virtual void RemoveId(int id)
		{
			RemoveReference(new _IReferenceSource_16(id));
		}

		private sealed class _IReferenceSource_16 : ReferenceSystemRegistry.IReferenceSource
		{
			public _IReferenceSource_16(int id)
			{
				this.id = id;
			}

			public ObjectReference ReferenceFrom(IReferenceSystem referenceSystem)
			{
				return referenceSystem.ReferenceForId(id);
			}

			private readonly int id;
		}

		public virtual void RemoveObject(object obj)
		{
			RemoveReference(new _IReferenceSource_24(obj));
		}

		private sealed class _IReferenceSource_24 : ReferenceSystemRegistry.IReferenceSource
		{
			public _IReferenceSource_24(object obj)
			{
				this.obj = obj;
			}

			public ObjectReference ReferenceFrom(IReferenceSystem referenceSystem)
			{
				return referenceSystem.ReferenceForObject(obj);
			}

			private readonly object obj;
		}

		public virtual void RemoveReference(ObjectReference reference)
		{
			RemoveReference(new _IReferenceSource_32(reference));
		}

		private sealed class _IReferenceSource_32 : ReferenceSystemRegistry.IReferenceSource
		{
			public _IReferenceSource_32(ObjectReference reference)
			{
				this.reference = reference;
			}

			public ObjectReference ReferenceFrom(IReferenceSystem referenceSystem)
			{
				return reference;
			}

			private readonly ObjectReference reference;
		}

		private void RemoveReference(ReferenceSystemRegistry.IReferenceSource referenceSource
			)
		{
			IEnumerator i = _referenceSystems.GetEnumerator();
			while (i.MoveNext())
			{
				IReferenceSystem referenceSystem = (IReferenceSystem)i.Current;
				ObjectReference reference = referenceSource.ReferenceFrom(referenceSystem);
				if (reference != null)
				{
					referenceSystem.RemoveReference(reference);
				}
			}
		}

		public virtual void AddReferenceSystem(IReferenceSystem referenceSystem)
		{
			_referenceSystems.Add(referenceSystem);
		}

		public virtual void RemoveReferenceSystem(IReferenceSystem referenceSystem)
		{
			_referenceSystems.Remove(referenceSystem);
		}

		private interface IReferenceSource
		{
			ObjectReference ReferenceFrom(IReferenceSystem referenceSystem);
		}
	}
}

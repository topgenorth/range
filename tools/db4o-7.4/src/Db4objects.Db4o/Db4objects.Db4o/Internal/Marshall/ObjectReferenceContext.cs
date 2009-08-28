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
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Marshall;
using Db4objects.Db4o.Marshall;

namespace Db4objects.Db4o.Internal.Marshall
{
	public class ObjectReferenceContext : ObjectHeaderContext
	{
		protected readonly Db4objects.Db4o.Internal.ObjectReference _reference;

		public ObjectReferenceContext(Transaction transaction, IReadBuffer buffer, ObjectHeader
			 objectHeader, Db4objects.Db4o.Internal.ObjectReference reference) : base(transaction
			, buffer, objectHeader)
		{
			_reference = reference;
		}

		public virtual int ObjectID()
		{
			return _reference.GetID();
		}

		public override Db4objects.Db4o.Internal.ClassMetadata ClassMetadata()
		{
			return _reference.ClassMetadata();
		}

		public virtual Db4objects.Db4o.Internal.ObjectReference ObjectReference()
		{
			return _reference;
		}

		protected virtual Db4objects.Db4o.Internal.ByteArrayBuffer ByteArrayBuffer()
		{
			return (Db4objects.Db4o.Internal.ByteArrayBuffer)Buffer();
		}

		public virtual Db4objects.Db4o.Internal.StatefulBuffer StatefulBuffer()
		{
			Db4objects.Db4o.Internal.StatefulBuffer statefulBuffer = new Db4objects.Db4o.Internal.StatefulBuffer
				(Transaction(), ByteArrayBuffer().Length());
			statefulBuffer.SetID(ObjectID());
			statefulBuffer.SetInstantiationDepth(ActivationDepth());
			ByteArrayBuffer().CopyTo(statefulBuffer, 0, 0, ByteArrayBuffer().Length());
			statefulBuffer.Seek(ByteArrayBuffer().Offset());
			return statefulBuffer;
		}
	}
}

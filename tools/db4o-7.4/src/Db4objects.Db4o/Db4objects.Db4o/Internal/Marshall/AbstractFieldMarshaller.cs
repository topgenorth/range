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

namespace Db4objects.Db4o.Internal.Marshall
{
	/// <exclude></exclude>
	public abstract class AbstractFieldMarshaller : IFieldMarshaller
	{
		protected abstract RawFieldSpec ReadSpec(AspectType aspectType, ObjectContainerBase
			 stream, ByteArrayBuffer reader);

		public virtual RawFieldSpec ReadSpec(ObjectContainerBase stream, ByteArrayBuffer 
			reader)
		{
			return ReadSpec(AspectType.Field, stream, reader);
		}

		public abstract void Defrag(ClassMetadata arg1, ClassAspect arg2, LatinStringIO arg3
			, DefragmentContextImpl arg4);

		public abstract int MarshalledLength(ObjectContainerBase arg1, ClassAspect arg2);

		public abstract FieldMetadata Read(ObjectContainerBase arg1, FieldMetadata arg2, 
			ByteArrayBuffer arg3);

		public abstract void Write(Transaction arg1, ClassMetadata arg2, ClassAspect arg3
			, ByteArrayBuffer arg4);
	}
}

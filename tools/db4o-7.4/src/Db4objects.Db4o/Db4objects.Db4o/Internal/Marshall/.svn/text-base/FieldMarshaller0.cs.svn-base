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
using Db4objects.Db4o.Internal.Handlers;
using Db4objects.Db4o.Internal.Handlers.Array;
using Db4objects.Db4o.Internal.Marshall;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Internal.Marshall
{
	/// <exclude></exclude>
	public class FieldMarshaller0 : AbstractFieldMarshaller
	{
		public override int MarshalledLength(ObjectContainerBase stream, ClassAspect aspect
			)
		{
			int len = stream.StringIO().ShortLength(aspect.GetName());
			if (aspect is FieldMetadata)
			{
				FieldMetadata field = (FieldMetadata)aspect;
				if (field.NeedsArrayAndPrimitiveInfo())
				{
					len += 1;
				}
				if (field.NeedsHandlerId())
				{
					len += Const4.IdLength;
				}
			}
			return len;
		}

		protected override RawFieldSpec ReadSpec(AspectType aspectType, ObjectContainerBase
			 stream, ByteArrayBuffer reader)
		{
			string name = StringHandler.ReadStringNoDebug(stream.Transaction().Context(), reader
				);
			if (!aspectType.IsFieldMetadata())
			{
				return new RawFieldSpec(aspectType, name);
			}
			if (name.IndexOf(Const4.VirtualFieldPrefix) == 0)
			{
				if (stream._handlers.VirtualFieldByName(name) != null)
				{
					return new RawFieldSpec(aspectType, name);
				}
			}
			int handlerID = reader.ReadInt();
			byte attribs = reader.ReadByte();
			return new RawFieldSpec(aspectType, name, handlerID, attribs);
		}

		public sealed override FieldMetadata Read(ObjectContainerBase stream, FieldMetadata
			 field, ByteArrayBuffer reader)
		{
			RawFieldSpec spec = ReadSpec(stream, reader);
			return FromSpec(spec, stream, field);
		}

		protected virtual FieldMetadata FromSpec(RawFieldSpec spec, ObjectContainerBase stream
			, FieldMetadata field)
		{
			if (spec == null)
			{
				return field;
			}
			string name = spec.Name();
			if (!spec.IsFieldMetadata())
			{
				field.Init(field.ContainingClass(), name);
				return field;
			}
			if (spec.IsVirtual())
			{
				return stream._handlers.VirtualFieldByName(name);
			}
			field.Init(field.ContainingClass(), name);
			field.Init(spec.HandlerID(), spec.IsPrimitive(), spec.IsArray(), spec.IsNArray());
			field.LoadHandlerById(stream);
			field.Alive();
			return field;
		}

		public override void Write(Transaction trans, ClassMetadata clazz, ClassAspect aspect
			, ByteArrayBuffer writer)
		{
			writer.WriteShortString(trans, aspect.GetName());
			if (!(aspect is FieldMetadata))
			{
				return;
			}
			FieldMetadata field = (FieldMetadata)aspect;
			field.Alive();
			if (field.IsVirtual())
			{
				return;
			}
			ITypeHandler4 handler = field.GetHandler();
			if (handler is ClassMetadata)
			{
				// TODO: ensure there is a test case, to make this happen 
				if (((ClassMetadata)handler).GetID() == 0)
				{
					trans.Container().NeedsUpdate(clazz);
				}
			}
			writer.WriteInt(field.HandlerID());
			BitMap4 bitmap = new BitMap4(3);
			bitmap.Set(0, field.IsPrimitive());
			bitmap.Set(1, handler is ArrayHandler);
			bitmap.Set(2, handler is MultidimensionalArrayHandler);
			// keep the order
			writer.WriteByte(bitmap.GetByte(0));
		}

		public override void Defrag(ClassMetadata classMetadata, ClassAspect aspect, LatinStringIO
			 sio, DefragmentContextImpl context)
		{
			context.IncrementStringOffset(sio);
			if (!(aspect is FieldMetadata))
			{
				return;
			}
			if (((FieldMetadata)aspect).IsVirtual())
			{
				return;
			}
			// handler ID
			context.CopyID();
			// skip primitive/array/narray attributes
			context.IncrementOffset(1);
		}
	}
}

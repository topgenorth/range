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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Activation;
using Db4objects.Db4o.Internal.Delete;
using Db4objects.Db4o.Internal.Fieldhandlers;
using Db4objects.Db4o.Internal.Marshall;
using Db4objects.Db4o.Marshall;
using Db4objects.Db4o.Reflect;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Internal
{
	public class UntypedFieldHandler : ClassMetadata, IBuiltinTypeHandler, IFieldHandler
	{
		private const int Hashcode = 1003303143;

		public UntypedFieldHandler(ObjectContainerBase container) : base(container, container
			._handlers.IclassObject)
		{
		}

		public override void CascadeActivation(ActivationContext4 context)
		{
			ITypeHandler4 typeHandler = TypeHandlerForObject(context.TargetObject());
			if (typeHandler is IFirstClassHandler)
			{
				((IFirstClassHandler)typeHandler).CascadeActivation(context);
			}
		}

		private Db4objects.Db4o.Internal.HandlerRegistry HandlerRegistry()
		{
			return Container()._handlers;
		}

		/// <exception cref="Db4oIOException"></exception>
		public override void Delete(IDeleteContext context)
		{
			int payLoadOffset = context.ReadInt();
			if (context.IsLegacyHandlerVersion())
			{
				context.DefragmentRecommended();
				return;
			}
			if (payLoadOffset <= 0)
			{
				return;
			}
			int linkOffset = context.Offset();
			context.Seek(payLoadOffset);
			int classMetadataID = context.ReadInt();
			ITypeHandler4 typeHandler = ConfiguredHandler(Container().ClassMetadataForId(classMetadataID
				).ClassReflector());
			if (typeHandler == null)
			{
				typeHandler = ((ObjectContainerBase)context.ObjectContainer()).TypeHandlerForId(classMetadataID
					);
			}
			if (typeHandler != null)
			{
				context.Delete(typeHandler);
			}
			context.Seek(linkOffset);
		}

		public override int GetID()
		{
			return Handlers4.UntypedId;
		}

		public override bool HasField(ObjectContainerBase a_stream, string a_path)
		{
			return a_stream.ClassCollection().FieldExists(a_path);
		}

		public override bool HasClassIndex()
		{
			return false;
		}

		public override bool HoldsAnyClass()
		{
			return true;
		}

		public override bool IsStrongTyped()
		{
			return false;
		}

		public override ITypeHandler4 ReadCandidateHandler(QueryingReadContext context)
		{
			int payLoadOffSet = context.ReadInt();
			if (payLoadOffSet == 0)
			{
				return null;
			}
			context.Seek(payLoadOffSet);
			int classMetadataID = context.ReadInt();
			ClassMetadata classMetadata = context.Container().ClassMetadataForId(classMetadataID
				);
			if (classMetadata == null)
			{
				return null;
			}
			return classMetadata.ReadCandidateHandler(context);
		}

		public override ObjectID ReadObjectID(IInternalReadContext context)
		{
			int payloadOffset = context.ReadInt();
			if (payloadOffset == 0)
			{
				return ObjectID.IsNull;
			}
			int savedOffset = context.Offset();
			ITypeHandler4 typeHandler = ReadTypeHandler(context, payloadOffset);
			if (typeHandler == null)
			{
				context.Seek(savedOffset);
				return ObjectID.IsNull;
			}
			SeekSecondaryOffset(context, typeHandler);
			if (typeHandler is IReadsObjectIds)
			{
				ObjectID readObjectID = ((IReadsObjectIds)typeHandler).ReadObjectID(context);
				context.Seek(savedOffset);
				return readObjectID;
			}
			context.Seek(savedOffset);
			return ObjectID.NotPossible;
		}

		public override void Defragment(IDefragmentContext context)
		{
			int payLoadOffSet = context.ReadInt();
			if (payLoadOffSet == 0)
			{
				return;
			}
			int savedOffSet = context.Offset();
			context.Seek(payLoadOffSet);
			int typeHandlerId = context.CopyIDReturnOriginalID();
			ITypeHandler4 typeHandler = context.TypeHandlerForId(typeHandlerId);
			if (typeHandler != null)
			{
				SeekSecondaryOffset(context, typeHandler);
				context.Defragment(typeHandler);
			}
			context.Seek(savedOffSet);
		}

		private ITypeHandler4 ReadTypeHandler(IInternalReadContext context, int payloadOffset
			)
		{
			context.Seek(payloadOffset);
			ITypeHandler4 typeHandler = Container().TypeHandlerForId(context.ReadInt());
			return Handlers4.CorrectHandlerVersion(context, typeHandler);
		}

		/// <param name="buffer"></param>
		/// <param name="typeHandler"></param>
		protected virtual void SeekSecondaryOffset(IReadBuffer buffer, ITypeHandler4 typeHandler
			)
		{
		}

		// do nothing, no longer needed in current implementation.
		protected virtual bool IsPrimitiveArray(ITypeHandler4 classMetadata)
		{
			return classMetadata is PrimitiveFieldHandler && ((PrimitiveFieldHandler)classMetadata
				).IsArray();
		}

		public override object Read(IReadContext readContext)
		{
			IInternalReadContext context = (IInternalReadContext)readContext;
			int payloadOffset = context.ReadInt();
			if (payloadOffset == 0)
			{
				return null;
			}
			int savedOffSet = context.Offset();
			ITypeHandler4 typeHandler = ReadTypeHandler(context, payloadOffset);
			if (typeHandler == null)
			{
				context.Seek(savedOffSet);
				return null;
			}
			SeekSecondaryOffset(context, typeHandler);
			object obj = context.ReadAtCurrentSeekPosition(typeHandler);
			context.Seek(savedOffSet);
			return obj;
		}

		public virtual ITypeHandler4 ReadTypeHandlerRestoreOffset(IInternalReadContext context
			)
		{
			int savedOffset = context.Offset();
			int payloadOffset = context.ReadInt();
			ITypeHandler4 typeHandler = payloadOffset == 0 ? null : ReadTypeHandler(context, 
				payloadOffset);
			context.Seek(savedOffset);
			return typeHandler;
		}

		public override void Write(IWriteContext context, object obj)
		{
			if (obj == null)
			{
				context.WriteInt(0);
				return;
			}
			MarshallingContext marshallingContext = (MarshallingContext)context;
			ITypeHandler4 typeHandler = TypeHandlerForObject(obj);
			if (typeHandler == null)
			{
				context.WriteInt(0);
				return;
			}
			int id = HandlerRegistry().TypeHandlerID(typeHandler);
			MarshallingContextState state = marshallingContext.CurrentState();
			marshallingContext.CreateChildBuffer(false, false);
			context.WriteInt(id);
			if (!IsPrimitiveArray(typeHandler))
			{
				marshallingContext.DoNotIndirectWrites();
			}
			WriteObject(context, typeHandler, obj);
			marshallingContext.RestoreState(state);
		}

		private void WriteObject(IWriteContext context, ITypeHandler4 typeHandler, object
			 obj)
		{
			if (FieldMetadata.UseDedicatedSlot(context, typeHandler))
			{
				context.WriteObject(obj);
			}
			else
			{
				typeHandler.Write(context, obj);
			}
		}

		public virtual ITypeHandler4 TypeHandlerForObject(object obj)
		{
			IReflectClass claxx = Reflector().ForObject(obj);
			if (claxx.IsArray())
			{
				return HandlerRegistry().UntypedArrayHandler(claxx);
			}
			return Container().TypeHandlerForReflectClass(claxx);
		}

		private ITypeHandler4 ConfiguredHandler(IReflectClass claxx)
		{
			ITypeHandler4 configuredHandler = Container().ConfigImpl().TypeHandlerForClass(claxx
				, Db4objects.Db4o.Internal.HandlerRegistry.HandlerVersion);
			return configuredHandler;
		}

		public override IReflectClass ClassReflector()
		{
			return base.ClassReflector();
		}

		public override bool Equals(object obj)
		{
			return obj is Db4objects.Db4o.Internal.UntypedFieldHandler;
		}

		public override int GetHashCode()
		{
			return Hashcode;
		}

		public virtual void RegisterReflector(IReflector reflector)
		{
		}
		// nothing to do
	}
}

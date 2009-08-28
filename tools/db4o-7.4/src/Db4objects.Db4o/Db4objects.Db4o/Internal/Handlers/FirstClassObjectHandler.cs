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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Activation;
using Db4objects.Db4o.Internal.Delete;
using Db4objects.Db4o.Internal.Handlers;
using Db4objects.Db4o.Internal.Marshall;
using Db4objects.Db4o.Internal.Slots;
using Db4objects.Db4o.Marshall;
using Db4objects.Db4o.Reflect;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Internal.Handlers
{
	/// <exclude></exclude>
	public class FirstClassObjectHandler : IFieldAwareTypeHandler
	{
		private const int HashcodeForNull = 72483944;

		private Db4objects.Db4o.Internal.ClassMetadata _classMetadata;

		public FirstClassObjectHandler(Db4objects.Db4o.Internal.ClassMetadata classMetadata
			)
		{
			_classMetadata = classMetadata;
		}

		public FirstClassObjectHandler()
		{
		}

		public virtual void Defragment(IDefragmentContext context)
		{
			FirstClassObjectHandler.TraverseAspectCommand command = new _TraverseAspectCommand_35
				(context);
			TraverseAllAspects(context, command);
		}

		private sealed class _TraverseAspectCommand_35 : FirstClassObjectHandler.TraverseAspectCommand
		{
			public _TraverseAspectCommand_35(IDefragmentContext context)
			{
				this.context = context;
			}

			public override int AspectCount(Db4objects.Db4o.Internal.ClassMetadata classMetadata
				, ByteArrayBuffer reader)
			{
				return context.ReadInt();
			}

			public override void ProcessAspect(ClassAspect aspect, int currentSlot, bool isNull
				, Db4objects.Db4o.Internal.ClassMetadata containingClass)
			{
				if (!isNull)
				{
					aspect.DefragAspect(context);
				}
			}

			public override bool Accept(ClassAspect aspect)
			{
				return aspect.Enabled(context);
			}

			private readonly IDefragmentContext context;
		}

		/// <exception cref="Db4oIOException"></exception>
		public virtual void Delete(IDeleteContext context)
		{
			context.DeleteObject();
		}

		public void InstantiateAspects(UnmarshallingContext context)
		{
			BooleanByRef updateFieldFound = new BooleanByRef();
			ContextState savedState = context.SaveState();
			FirstClassObjectHandler.TraverseAspectCommand command = new _TraverseAspectCommand_61
				(context, updateFieldFound);
			TraverseAllAspects(context, command);
			if (updateFieldFound.value)
			{
				context.RestoreState(savedState);
				command = new _TraverseFieldCommand_84(context);
				TraverseAllAspects(context, command);
			}
		}

		private sealed class _TraverseAspectCommand_61 : FirstClassObjectHandler.TraverseAspectCommand
		{
			public _TraverseAspectCommand_61(UnmarshallingContext context, BooleanByRef updateFieldFound
				)
			{
				this.context = context;
				this.updateFieldFound = updateFieldFound;
			}

			public override void ProcessAspect(ClassAspect aspect, int currentSlot, bool isNull
				, Db4objects.Db4o.Internal.ClassMetadata containingClass)
			{
				if (!aspect.Enabled(context))
				{
					return;
				}
				if (aspect is FieldMetadata)
				{
					FieldMetadata field = (FieldMetadata)aspect;
					if (field.Updating())
					{
						updateFieldFound.value = true;
					}
					if (isNull)
					{
						field.Set(context.PersistentObject(), null);
						return;
					}
				}
				aspect.Instantiate(context);
			}

			private readonly UnmarshallingContext context;

			private readonly BooleanByRef updateFieldFound;
		}

		private sealed class _TraverseFieldCommand_84 : FirstClassObjectHandler.TraverseFieldCommand
		{
			public _TraverseFieldCommand_84(UnmarshallingContext context)
			{
				this.context = context;
			}

			public override void ProcessAspect(ClassAspect aspect, int currentSlot, bool isNull
				, Db4objects.Db4o.Internal.ClassMetadata containingClass)
			{
				if (!isNull)
				{
					((FieldMetadata)aspect).AttemptUpdate(context);
				}
			}

			private readonly UnmarshallingContext context;
		}

		public virtual object Read(IReadContext context)
		{
			UnmarshallingContext unmarshallingContext = (UnmarshallingContext)context;
			// FIXME: Commented out code below is the implementation plan to let
			//        FirstClassObjectHandler take responsibility of fieldcount
			//        and null Bitmap.        
			//        BitMap4 nullBitMap = unmarshallingContext.readBitMap(fieldCount);
			//        int fieldCount = context.readInt();
			InstantiateAspects(unmarshallingContext);
			return unmarshallingContext.PersistentObject();
		}

		public virtual void Write(IWriteContext context, object obj)
		{
			//        int fieldCount = _classMetadata.fieldCount();
			//        context.writeInt(fieldCount);
			//        final BitMap4 nullBitMap = new BitMap4(fieldCount);
			//        ReservedBuffer bitMapBuffer = context.reserve(nullBitMap.marshalledLength());
			MarshallAspects(obj, (MarshallingContext)context);
		}

		//        bitMapBuffer.writeBytes(nullBitMap.bytes());
		public virtual void MarshallAspects(object obj, MarshallingContext context)
		{
			Transaction trans = context.Transaction();
			FirstClassObjectHandler.TraverseAspectCommand command = new _TraverseAspectCommand_127
				(context, obj, trans);
			TraverseAllAspects(context, command);
		}

		private sealed class _TraverseAspectCommand_127 : FirstClassObjectHandler.TraverseAspectCommand
		{
			public _TraverseAspectCommand_127(MarshallingContext context, object obj, Transaction
				 trans)
			{
				this.context = context;
				this.obj = obj;
				this.trans = trans;
			}

			public override int AspectCount(Db4objects.Db4o.Internal.ClassMetadata classMetadata
				, ByteArrayBuffer buffer)
			{
				int fieldCount = classMetadata._aspects.Length;
				context.FieldCount(fieldCount);
				return fieldCount;
			}

			public override void ProcessAspect(ClassAspect aspect, int currentSlot, bool isNull
				, Db4objects.Db4o.Internal.ClassMetadata containingClass)
			{
				if (!aspect.Enabled(context))
				{
					return;
				}
				object marshalledObject = obj;
				if (aspect is FieldMetadata)
				{
					FieldMetadata field = (FieldMetadata)aspect;
					marshalledObject = field.GetOrCreate(trans, obj);
					if (marshalledObject == null)
					{
						context.IsNull(currentSlot, true);
						field.AddIndexEntry(trans, context.ObjectID(), null);
						return;
					}
					if (marshalledObject is IDb4oTypeImpl)
					{
						marshalledObject = ((IDb4oTypeImpl)marshalledObject).StoredTo(trans);
					}
				}
				aspect.Marshall(context, marshalledObject);
			}

			private readonly MarshallingContext context;

			private readonly object obj;

			private readonly Transaction trans;
		}

		public virtual IPreparedComparison PrepareComparison(IContext context, object source
			)
		{
			if (source == null)
			{
				return new _IPreparedComparison_161();
			}
			int id = 0;
			IReflectClass claxx = null;
			if (source is int)
			{
				id = ((int)source);
			}
			else
			{
				if (source is TransactionContext)
				{
					TransactionContext tc = (TransactionContext)source;
					object obj = tc._object;
					id = _classMetadata.Stream().GetID(tc._transaction, obj);
					claxx = _classMetadata.Reflector().ForObject(obj);
				}
				else
				{
					throw new IllegalComparisonException();
				}
			}
			return new ClassMetadata.PreparedComparisonImpl(id, claxx);
		}

		private sealed class _IPreparedComparison_161 : IPreparedComparison
		{
			public _IPreparedComparison_161()
			{
			}

			public int CompareTo(object obj)
			{
				if (obj == null)
				{
					return 0;
				}
				return -1;
			}
		}

		public abstract class TraverseAspectCommand
		{
			private bool _cancelled = false;

			public virtual int AspectCount(ClassMetadata classMetadata, ByteArrayBuffer reader
				)
			{
				return classMetadata.ReadAspectCount(reader);
			}

			public virtual bool Cancelled()
			{
				return _cancelled;
			}

			protected virtual void Cancel()
			{
				_cancelled = true;
			}

			public virtual bool Accept(ClassAspect aspect)
			{
				return true;
			}

			public abstract void ProcessAspect(ClassAspect aspect, int currentSlot, bool isNull
				, ClassMetadata containingClass);
		}

		public abstract class TraverseFieldCommand : FirstClassObjectHandler.TraverseAspectCommand
		{
			public override bool Accept(ClassAspect aspect)
			{
				return aspect is FieldMetadata;
			}
		}

		protected void TraverseAllAspects(IMarshallingInfo context, FirstClassObjectHandler.TraverseAspectCommand
			 command)
		{
			int currentSlot = 0;
			ClassMetadata classMetadata = ClassMetadata();
			while (classMetadata != null)
			{
				int fieldCount = command.AspectCount(classMetadata, ((ByteArrayBuffer)context.Buffer
					()));
				context.AspectCount(fieldCount);
				for (int i = 0; i < fieldCount && !command.Cancelled(); i++)
				{
					if (command.Accept(classMetadata._aspects[i]))
					{
						command.ProcessAspect(classMetadata._aspects[i], currentSlot, IsNull(context, currentSlot
							), classMetadata);
					}
					context.BeginSlot();
					currentSlot++;
				}
				if (command.Cancelled())
				{
					return;
				}
				classMetadata = classMetadata.i_ancestor;
			}
		}

		protected virtual bool IsNull(IFieldListInfo fieldList, int fieldIndex)
		{
			return fieldList.IsNull(fieldIndex);
		}

		public virtual ClassMetadata ClassMetadata()
		{
			return _classMetadata;
		}

		public virtual void ClassMetadata(ClassMetadata classMetadata)
		{
			_classMetadata = classMetadata;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is FirstClassObjectHandler))
			{
				return false;
			}
			FirstClassObjectHandler other = (FirstClassObjectHandler)obj;
			if (_classMetadata == null)
			{
				return other._classMetadata == null;
			}
			return _classMetadata.Equals(other._classMetadata);
		}

		public override int GetHashCode()
		{
			if (_classMetadata != null)
			{
				return _classMetadata.GetHashCode();
			}
			return HashcodeForNull;
		}

		public virtual ITypeHandler4 UnversionedTemplate()
		{
			return new FirstClassObjectHandler(null);
		}

		public virtual object DeepClone(object context)
		{
			TypeHandlerCloneContext typeHandlerCloneContext = (TypeHandlerCloneContext)context;
			FirstClassObjectHandler cloned = (FirstClassObjectHandler)Reflection4.NewInstance
				(this);
			if (typeHandlerCloneContext.original is FirstClassObjectHandler)
			{
				FirstClassObjectHandler original = (FirstClassObjectHandler)typeHandlerCloneContext
					.original;
				cloned._classMetadata = original._classMetadata;
			}
			else
			{
				if (_classMetadata == null)
				{
					throw new InvalidOperationException();
				}
				cloned._classMetadata = _classMetadata;
			}
			return cloned;
		}

		public virtual void CollectIDs(CollectIdContext context, string fieldName)
		{
			FirstClassObjectHandler.TraverseAspectCommand command = new _TraverseAspectCommand_290
				(fieldName, context);
			TraverseAllAspects(context, command);
		}

		private sealed class _TraverseAspectCommand_290 : FirstClassObjectHandler.TraverseAspectCommand
		{
			public _TraverseAspectCommand_290(string fieldName, CollectIdContext context)
			{
				this.fieldName = fieldName;
				this.context = context;
			}

			public override void ProcessAspect(ClassAspect aspect, int currentSlot, bool isNull
				, ClassMetadata containingClass)
			{
				if (isNull)
				{
					return;
				}
				if (fieldName.Equals(aspect.GetName()))
				{
					aspect.CollectIDs(context);
				}
				else
				{
					aspect.IncrementOffset(context);
				}
			}

			private readonly string fieldName;

			private readonly CollectIdContext context;
		}

		public virtual void CascadeActivation(ActivationContext4 context)
		{
			context.CascadeActivationToTarget(ClassMetadata(), ClassMetadata().DescendOnCascadingActivation
				());
		}

		public virtual ITypeHandler4 ReadCandidateHandler(QueryingReadContext context)
		{
			if (ClassMetadata().IsArray())
			{
				return ClassMetadata();
			}
			return null;
		}

		/// <exception cref="Db4oIOException"></exception>
		public virtual void CollectIDs(QueryingReadContext context)
		{
			int id = context.CollectionID();
			if (id == 0)
			{
				return;
			}
			Transaction transaction = context.Transaction();
			ObjectContainerBase container = context.Container();
			object obj = container.GetByID(transaction, id);
			if (obj == null)
			{
				return;
			}
			// FIXME: [TA] review activation depth
			int depth = ClassMetadata().AdjustDepthToBorders(2);
			container.Activate(transaction, obj, container.ActivationDepthProvider().ActivationDepth
				(depth, ActivationMode.Activate));
			Platform4.ForEachCollectionElement(obj, new _IVisitor4_333(context));
		}

		private sealed class _IVisitor4_333 : IVisitor4
		{
			public _IVisitor4_333(QueryingReadContext context)
			{
				this.context = context;
			}

			public void Visit(object elem)
			{
				context.Add(elem);
			}

			private readonly QueryingReadContext context;
		}

		public virtual void ReadVirtualAttributes(ObjectReferenceContext context)
		{
			FirstClassObjectHandler.TraverseAspectCommand command = new _TraverseAspectCommand_342
				(context);
			TraverseAllAspects(context, command);
		}

		private sealed class _TraverseAspectCommand_342 : FirstClassObjectHandler.TraverseAspectCommand
		{
			public _TraverseAspectCommand_342(ObjectReferenceContext context)
			{
				this.context = context;
			}

			public override void ProcessAspect(ClassAspect aspect, int currentSlot, bool isNull
				, ClassMetadata containingClass)
			{
				if (!isNull)
				{
					if (aspect is VirtualFieldMetadata)
					{
						((VirtualFieldMetadata)aspect).ReadVirtualAttribute(context);
					}
					else
					{
						aspect.IncrementOffset(context);
					}
				}
			}

			private readonly ObjectReferenceContext context;
		}

		public virtual void AddFieldIndices(ObjectIdContextImpl context, Slot oldSlot)
		{
			FirstClassObjectHandler.TraverseAspectCommand command = new _TraverseAspectCommand_357
				(context, oldSlot);
			TraverseAllAspects(context, command);
		}

		private sealed class _TraverseAspectCommand_357 : FirstClassObjectHandler.TraverseAspectCommand
		{
			public _TraverseAspectCommand_357(ObjectIdContextImpl context, Slot oldSlot)
			{
				this.context = context;
				this.oldSlot = oldSlot;
			}

			public override void ProcessAspect(ClassAspect aspect, int currentSlot, bool isNull
				, ClassMetadata containingClass)
			{
				if (aspect is FieldMetadata)
				{
					FieldMetadata field = (FieldMetadata)aspect;
					if (isNull)
					{
						field.AddIndexEntry(context.Transaction(), context.Id(), null);
					}
					else
					{
						field.AddFieldIndex(context, oldSlot);
					}
				}
				else
				{
					aspect.IncrementOffset(context.Buffer());
				}
			}

			private readonly ObjectIdContextImpl context;

			private readonly Slot oldSlot;
		}

		public virtual void DeleteMembers(DeleteContextImpl context, bool isUpdate)
		{
			FirstClassObjectHandler.TraverseAspectCommand command = new _TraverseAspectCommand_375
				(context, isUpdate);
			TraverseAllAspects(context, command);
		}

		private sealed class _TraverseAspectCommand_375 : FirstClassObjectHandler.TraverseAspectCommand
		{
			public _TraverseAspectCommand_375(DeleteContextImpl context, bool isUpdate)
			{
				this.context = context;
				this.isUpdate = isUpdate;
			}

			public override void ProcessAspect(ClassAspect aspect, int currentSlot, bool isNull
				, ClassMetadata containingClass)
			{
				if (isNull)
				{
					if (aspect is FieldMetadata)
					{
						FieldMetadata field = (FieldMetadata)aspect;
						field.RemoveIndexEntry(context.Transaction(), context.Id(), null);
					}
					return;
				}
				aspect.Delete(context, isUpdate);
			}

			private readonly DeleteContextImpl context;

			private readonly bool isUpdate;
		}

		public virtual bool SeekToField(ObjectHeaderContext context, FieldMetadata field)
		{
			BooleanByRef found = new BooleanByRef(false);
			FirstClassObjectHandler.TraverseAspectCommand command = new _TraverseAspectCommand_392
				(field, found, context);
			TraverseAllAspects(context, command);
			return found.value;
		}

		private sealed class _TraverseAspectCommand_392 : FirstClassObjectHandler.TraverseAspectCommand
		{
			public _TraverseAspectCommand_392(FieldMetadata field, BooleanByRef found, ObjectHeaderContext
				 context)
			{
				this.field = field;
				this.found = found;
				this.context = context;
			}

			public override void ProcessAspect(ClassAspect curField, int currentSlot, bool isNull
				, ClassMetadata containingClass)
			{
				if (curField == field)
				{
					found.value = !isNull;
					this.Cancel();
					return;
				}
				if (!isNull)
				{
					curField.IncrementOffset(context);
				}
			}

			private readonly FieldMetadata field;

			private readonly BooleanByRef found;

			private readonly ObjectHeaderContext context;
		}
	}
}

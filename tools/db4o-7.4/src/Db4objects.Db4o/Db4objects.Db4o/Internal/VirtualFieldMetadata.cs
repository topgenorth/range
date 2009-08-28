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
using Db4objects.Db4o.Internal.Activation;
using Db4objects.Db4o.Internal.Delete;
using Db4objects.Db4o.Internal.Marshall;
using Db4objects.Db4o.Internal.Query.Processor;
using Db4objects.Db4o.Internal.Replication;
using Db4objects.Db4o.Internal.Slots;
using Db4objects.Db4o.Marshall;
using Db4objects.Db4o.Reflect;

namespace Db4objects.Db4o.Internal
{
	/// <summary>
	/// TODO: refactor for symmetric inheritance - don't inherit from YapField and override,
	/// instead extract an abstract superclass from YapField and let both YapField and this class implement
	/// </summary>
	/// <exclude></exclude>
	public abstract class VirtualFieldMetadata : FieldMetadata
	{
		private static readonly object AnyObject = new object();

		private IReflectClass _classReflector;

		internal VirtualFieldMetadata(int handlerID, IBuiltinTypeHandler handler) : base(
			handlerID, handler)
		{
		}

		/// <exception cref="FieldIndexException"></exception>
		public abstract override void AddFieldIndex(ObjectIdContextImpl context, Slot oldSlot
			);

		public override bool Alive()
		{
			return true;
		}

		internal override bool CanAddToQuery(string fieldName)
		{
			return fieldName.Equals(GetName());
		}

		public override bool CanBeDisabled()
		{
			return false;
		}

		public override bool CanUseNullBitmap()
		{
			return false;
		}

		public virtual IReflectClass ClassReflector(IReflector reflector)
		{
			if (_classReflector == null)
			{
				_classReflector = ((IBuiltinTypeHandler)GetHandler()).ClassReflector();
			}
			return _classReflector;
		}

		internal override void CollectConstraints(Transaction a_trans, QConObject a_parent
			, object a_template, IVisitor4 a_visitor)
		{
		}

		// QBE constraint collection call
		// There isn't anything useful to do here, since virtual fields
		// are not on the actual object.
		public override void Deactivate(Transaction a_trans, object a_onObject, IActivationDepth
			 a_depth)
		{
		}

		// do nothing
		public abstract override void Delete(DeleteContextImpl context, bool isUpdate);

		public override object GetOrCreate(Transaction a_trans, object a_OnObject)
		{
			// This is the first part of marshalling
			// Virtual fields do it all in #marshall(), the object is never used.
			// Returning any object here prevents triggering null handling.
			return AnyObject;
		}

		public override bool NeedsArrayAndPrimitiveInfo()
		{
			return false;
		}

		public override bool NeedsHandlerId()
		{
			return false;
		}

		public override void Instantiate(UnmarshallingContext context)
		{
			context.ObjectReference().ProduceVirtualAttributes();
			Instantiate1(context);
		}

		internal abstract void Instantiate1(ObjectReferenceContext context);

		public override void LoadHandlerById(ObjectContainerBase container)
		{
		}

		// do nothing
		public override void Marshall(MarshallingContext context, object obj)
		{
			context.DoNotIndirectWrites();
			Marshall(context.Transaction(), context.Reference(), context, context.IsNew());
		}

		private void Marshall(Transaction trans, ObjectReference @ref, IWriteBuffer buffer
			, bool isNew)
		{
			if (!trans.SupportsVirtualFields())
			{
				MarshallIgnore(buffer);
				return;
			}
			ObjectContainerBase stream = trans.Container();
			HandlerRegistry handlers = stream._handlers;
			bool migrating = false;
			if (stream._replicationCallState != Const4.None)
			{
				if (stream._replicationCallState == Const4.Old)
				{
					// old replication code 
					migrating = true;
					if (@ref.VirtualAttributes() == null)
					{
						object obj = @ref.GetObject();
						ObjectReference migratingRef = null;
						MigrationConnection mgc = handlers.i_migration;
						if (mgc != null)
						{
							migratingRef = mgc.ReferenceFor(obj);
							if (migratingRef == null)
							{
								ObjectContainerBase peer = mgc.Peer(stream);
								migratingRef = peer.Transaction().ReferenceForObject(obj);
							}
						}
						if (migratingRef != null)
						{
							VirtualAttributes migrateAttributes = migratingRef.VirtualAttributes();
							if (migrateAttributes != null && migrateAttributes.i_database != null)
							{
								migrating = true;
								@ref.SetVirtualAttributes((VirtualAttributes)migrateAttributes.ShallowClone());
								migrateAttributes.i_database.Bind(trans);
							}
						}
					}
				}
				else
				{
					// new dRS replication
					IDb4oReplicationReferenceProvider provider = handlers._replicationReferenceProvider;
					object parentObject = @ref.GetObject();
					IDb4oReplicationReference replicationReference = provider.ReferenceFor(parentObject
						);
					if (replicationReference != null)
					{
						migrating = true;
						VirtualAttributes va = @ref.ProduceVirtualAttributes();
						va.i_version = replicationReference.Version();
						va.i_uuid = replicationReference.LongPart();
						va.i_database = replicationReference.SignaturePart();
					}
				}
			}
			if (@ref.VirtualAttributes() == null)
			{
				@ref.ProduceVirtualAttributes();
				migrating = false;
			}
			Marshall(trans, @ref, buffer, migrating, isNew);
		}

		internal abstract void Marshall(Transaction trans, ObjectReference @ref, IWriteBuffer
			 buffer, bool migrating, bool isNew);

		internal abstract void MarshallIgnore(IWriteBuffer writer);

		public virtual void ReadVirtualAttribute(ObjectReferenceContext context)
		{
			if (!context.Transaction().SupportsVirtualFields())
			{
				IncrementOffset(context);
				return;
			}
			Instantiate1(context);
		}

		public override bool IsVirtual()
		{
			return true;
		}

		protected override object IndexEntryFor(object indexEntry)
		{
			return indexEntry;
		}

		protected override IIndexable4 IndexHandler(ObjectContainerBase stream)
		{
			return (IIndexable4)_handler;
		}
	}
}

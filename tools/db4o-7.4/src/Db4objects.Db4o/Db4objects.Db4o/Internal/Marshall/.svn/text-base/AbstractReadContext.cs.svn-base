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
using Db4objects.Db4o.Internal.Marshall;
using Db4objects.Db4o.Marshall;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Internal.Marshall
{
	/// <exclude></exclude>
	public abstract class AbstractReadContext : Db4objects.Db4o.Internal.AbstractBufferContext
		, IInternalReadContext
	{
		protected IActivationDepth _activationDepth = UnknownActivationDepth.Instance;

		protected AbstractReadContext(Transaction transaction, IReadBuffer buffer) : base
			(transaction, buffer)
		{
		}

		protected AbstractReadContext(Transaction transaction) : this(transaction, null)
		{
		}

		public object Read(ITypeHandler4 handlerType)
		{
			return ReadObject(handlerType);
		}

		public object ReadObject(ITypeHandler4 handlerType)
		{
			ITypeHandler4 handler = Handlers4.CorrectHandlerVersion(this, handlerType);
			return SlotFormat().DoWithSlotIndirection(this, handler, new _IClosure4_33(this, 
				handler));
		}

		private sealed class _IClosure4_33 : IClosure4
		{
			public _IClosure4_33(AbstractReadContext _enclosing, ITypeHandler4 handler)
			{
				this._enclosing = _enclosing;
				this.handler = handler;
			}

			public object Run()
			{
				return this._enclosing.ReadAtCurrentSeekPosition(handler);
			}

			private readonly AbstractReadContext _enclosing;

			private readonly ITypeHandler4 handler;
		}

		public virtual object ReadAtCurrentSeekPosition(ITypeHandler4 handler)
		{
			if (handler is ClassMetadata)
			{
				ClassMetadata classMetadata = (ClassMetadata)handler;
				if (classMetadata.IsValueType())
				{
					return classMetadata.ReadValueType(Transaction(), ReadInt(), ActivationDepth().Descend
						(classMetadata));
				}
			}
			if (UseDedicatedSlot(handler))
			{
				return ReadObject();
			}
			return handler.Read(this);
		}

		public virtual bool UseDedicatedSlot(ITypeHandler4 handler)
		{
			return FieldMetadata.UseDedicatedSlot(this, handler);
		}

		public object ReadObject()
		{
			int id = ReadInt();
			if (id == 0)
			{
				return null;
			}
			ClassMetadata classMetadata = ClassMetadataForId(id);
			if (null == classMetadata)
			{
				// TODO: throw here
				return null;
			}
			IActivationDepth depth = ActivationDepth().Descend(classMetadata);
			if (PeekPersisted())
			{
				return Container().PeekPersisted(Transaction(), id, depth, false);
			}
			object obj = Container().GetByID2(Transaction(), id);
			if (null == obj)
			{
				return null;
			}
			// this is OK for primitive YapAnys. They will not be added
			// to the list, since they will not be found in the ID tree.
			Container().StillToActivate(Transaction(), obj, depth);
			return obj;
		}

		private ClassMetadata ClassMetadataForId(int id)
		{
			// TODO: This method is *very* costly as is, since it reads
			//       the whole slot once and doesn't reuse it. Optimize.
			HardObjectReference hardRef = Container().GetHardObjectReferenceById(Transaction(
				), id);
			if (null == hardRef || hardRef._reference == null)
			{
				// com.db4o.db4ounit.common.querying.CascadeDeleteDeleted
				return null;
			}
			return hardRef._reference.ClassMetadata();
		}

		protected virtual bool PeekPersisted()
		{
			return false;
		}

		public virtual IActivationDepth ActivationDepth()
		{
			return _activationDepth;
		}

		public virtual void ActivationDepth(IActivationDepth depth)
		{
			_activationDepth = depth;
		}

		public virtual IReadWriteBuffer ReadIndirectedBuffer()
		{
			int address = ReadInt();
			int length = ReadInt();
			if (address == 0)
			{
				return null;
			}
			return Container().BufferByAddress(address, length);
		}
	}
}

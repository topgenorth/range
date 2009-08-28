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
using Db4objects.Db4o;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Marshall;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Tests.Common.Handlers
{
	public class MockWriteContext : Db4objects.Db4o.Tests.Common.Handlers.MockMarshallingContext
		, IWriteContext
	{
		public MockWriteContext(IObjectContainer objectContainer) : base(objectContainer)
		{
		}

		public virtual void WriteObject(ITypeHandler4 handler, object obj)
		{
			handler.Write(this, obj);
		}

		public virtual void WriteAny(object obj)
		{
			ClassMetadata classMetadata = Container().ClassMetadataForObject(obj);
			WriteInt(classMetadata.GetID());
			classMetadata.Write(this, obj);
		}

		public virtual IReservedBuffer Reserve(int length)
		{
			IReservedBuffer reservedBuffer = new _IReservedBuffer_28(this);
			Seek(Offset() + length);
			return reservedBuffer;
		}

		private sealed class _IReservedBuffer_28 : IReservedBuffer
		{
			public _IReservedBuffer_28(MockWriteContext _enclosing)
			{
				this._enclosing = _enclosing;
				this.reservedOffset = this._enclosing.Offset();
			}

			private readonly int reservedOffset;

			public void WriteBytes(byte[] bytes)
			{
				int currentOffset = this._enclosing.Offset();
				this._enclosing.Seek(this.reservedOffset);
				this._enclosing.WriteBytes(bytes);
				this._enclosing.Seek(currentOffset);
			}

			private readonly MockWriteContext _enclosing;
		}
	}
}

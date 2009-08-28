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
	/// <exclude></exclude>
	public class ObjectHeaderContext : AbstractReadContext, IMarshallingInfo, IHandlerVersionContext
	{
		protected ObjectHeader _objectHeader;

		private int _aspectCount;

		public ObjectHeaderContext(Transaction transaction, IReadBuffer buffer, ObjectHeader
			 objectHeader) : base(transaction, buffer)
		{
			_objectHeader = objectHeader;
		}

		public ObjectHeaderAttributes HeaderAttributes()
		{
			return _objectHeader._headerAttributes;
		}

		public bool IsNull(int fieldIndex)
		{
			return HeaderAttributes().IsNull(fieldIndex);
		}

		public sealed override int HandlerVersion()
		{
			return _objectHeader.HandlerVersion();
		}

		public virtual void BeginSlot()
		{
		}

		// do nothing
		public virtual ContextState SaveState()
		{
			return new ContextState(Offset());
		}

		public virtual void RestoreState(ContextState state)
		{
			Seek(state._offset);
		}

		public virtual object ReadFieldValue(Db4objects.Db4o.Internal.ClassMetadata classMetadata
			, FieldMetadata field)
		{
			if (!classMetadata.SeekToField(this, field))
			{
				return null;
			}
			return field.Read(this);
		}

		public virtual Db4objects.Db4o.Internal.ClassMetadata ClassMetadata()
		{
			return _objectHeader.ClassMetadata();
		}

		public virtual int AspectCount()
		{
			return _aspectCount;
		}

		public virtual void AspectCount(int count)
		{
			_aspectCount = count;
		}
	}
}

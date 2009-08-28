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

namespace Db4objects.Db4o.Internal
{
	public class FrozenObjectInfo : IObjectInfo
	{
		private readonly Db4oDatabase _sourceDatabase;

		private readonly long _uuidLongPart;

		private readonly long _id;

		private readonly long _version;

		private readonly object _object;

		public FrozenObjectInfo(object @object, long id, Db4oDatabase sourceDatabase, long
			 uuidLongPart, long version)
		{
			_sourceDatabase = sourceDatabase;
			_uuidLongPart = uuidLongPart;
			_id = id;
			_version = version;
			_object = @object;
		}

		private FrozenObjectInfo(ObjectReference @ref, VirtualAttributes virtualAttributes
			) : this(@ref == null ? null : @ref.GetObject(), @ref == null ? -1 : @ref.GetID(
			), virtualAttributes == null ? null : virtualAttributes.i_database, virtualAttributes
			 == null ? -1 : virtualAttributes.i_uuid, @ref == null ? 0 : @ref.GetVersion())
		{
		}

		public FrozenObjectInfo(Transaction trans, ObjectReference @ref) : this(@ref, @ref
			 == null ? null : @ref.VirtualAttributes(trans, true))
		{
		}

		public virtual long GetInternalID()
		{
			return _id;
		}

		public virtual object GetObject()
		{
			return _object;
		}

		public virtual Db4oUUID GetUUID()
		{
			if (_sourceDatabase == null)
			{
				return null;
			}
			return new Db4oUUID(_uuidLongPart, _sourceDatabase.GetSignature());
		}

		public virtual long GetVersion()
		{
			return _version;
		}

		public virtual long SourceDatabaseId(Transaction trans)
		{
			if (_sourceDatabase == null)
			{
				return -1;
			}
			return _sourceDatabase.GetID(trans);
		}

		public virtual long UuidLongPart()
		{
			return _uuidLongPart;
		}
	}
}

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
	/// <exclude></exclude>
	public class LazyObjectReference : IObjectInfo
	{
		private readonly Transaction _transaction;

		private readonly int _id;

		public LazyObjectReference(Transaction transaction, int id)
		{
			_transaction = transaction;
			_id = id;
		}

		public virtual long GetInternalID()
		{
			return _id;
		}

		public virtual object GetObject()
		{
			lock (ContainerLock())
			{
				return Reference().GetObject();
			}
		}

		public virtual Db4oUUID GetUUID()
		{
			lock (ContainerLock())
			{
				return Reference().GetUUID();
			}
		}

		public virtual long GetVersion()
		{
			lock (ContainerLock())
			{
				return Reference().GetVersion();
			}
		}

		public virtual ObjectReference Reference()
		{
			HardObjectReference hardRef = _transaction.Container().GetHardObjectReferenceById
				(_transaction, _id);
			return hardRef._reference;
		}

		private object ContainerLock()
		{
			_transaction.Container().CheckClosed();
			return _transaction.Container().Lock();
		}
	}
}

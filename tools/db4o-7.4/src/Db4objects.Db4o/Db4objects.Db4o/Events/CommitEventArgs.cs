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
using Db4objects.Db4o.Events;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Internal;

namespace Db4objects.Db4o.Events
{
	/// <summary>Arguments for commit time related events.</summary>
	/// <remarks>Arguments for commit time related events.</remarks>
	/// <seealso cref="IEventRegistry">IEventRegistry</seealso>
	public class CommitEventArgs : TransactionalEventArgs
	{
		private readonly CallbackObjectInfoCollections _collections;

		public CommitEventArgs(Transaction transaction, CallbackObjectInfoCollections collections
			) : base(transaction)
		{
			_collections = collections;
		}

		/// <summary>Returns a iteration</summary>
		public virtual IObjectInfoCollection Added
		{
			get
			{
				return _collections.added;
			}
		}

		public virtual IObjectInfoCollection Deleted
		{
			get
			{
				return _collections.deleted;
			}
		}

		public virtual IObjectInfoCollection Updated
		{
			get
			{
				return _collections.updated;
			}
		}
	}
}

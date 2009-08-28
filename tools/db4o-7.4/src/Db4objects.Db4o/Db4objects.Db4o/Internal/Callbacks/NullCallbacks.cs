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
using Db4objects.Db4o.Internal.Callbacks;
using Db4objects.Db4o.Query;

namespace Db4objects.Db4o.Internal.Callbacks
{
	public class NullCallbacks : ICallbacks
	{
		public virtual void QueryOnFinished(Transaction transaction, IQuery query)
		{
		}

		public virtual void QueryOnStarted(Transaction transaction, IQuery query)
		{
		}

		public virtual bool ObjectCanNew(Transaction transaction, object obj)
		{
			return true;
		}

		public virtual bool ObjectCanActivate(Transaction transaction, object obj)
		{
			return true;
		}

		public virtual bool ObjectCanUpdate(Transaction transaction, object obj)
		{
			return true;
		}

		public virtual bool ObjectCanDelete(Transaction transaction, object obj)
		{
			return true;
		}

		public virtual bool ObjectCanDeactivate(Transaction transaction, object obj)
		{
			return true;
		}

		public virtual void ObjectOnNew(Transaction transaction, object obj)
		{
		}

		public virtual void ObjectOnActivate(Transaction transaction, object obj)
		{
		}

		public virtual void ObjectOnUpdate(Transaction transaction, object obj)
		{
		}

		public virtual void ObjectOnDelete(Transaction transaction, object obj)
		{
		}

		public virtual void ObjectOnDeactivate(Transaction transaction, object obj)
		{
		}

		public virtual void ObjectOnInstantiate(Transaction transaction, object obj)
		{
		}

		public virtual void CommitOnStarted(Transaction transaction, CallbackObjectInfoCollections
			 objectInfoCollections)
		{
		}

		public virtual void CommitOnCompleted(Transaction transaction, CallbackObjectInfoCollections
			 objectInfoCollections)
		{
		}

		public virtual bool CaresAboutCommitting()
		{
			return false;
		}

		public virtual bool CaresAboutCommitted()
		{
			return false;
		}

		public virtual void ClassOnRegistered(ClassMetadata clazz)
		{
		}

		public virtual bool CaresAboutDeleting()
		{
			return false;
		}

		public virtual bool CaresAboutDeleted()
		{
			return false;
		}

		public virtual void CloseOnStarted(IObjectContainer container)
		{
		}
	}
}

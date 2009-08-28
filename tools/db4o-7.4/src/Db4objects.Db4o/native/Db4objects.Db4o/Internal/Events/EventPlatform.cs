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

namespace Db4objects.Db4o.Internal.Events
{
	internal class EventPlatform
	{
		public static void TriggerClassEvent(ClassEventHandler e, Db4objects.Db4o.Internal.ClassMetadata klass)
		{
			if (null == e) return;
			e(klass, new ClassEventArgs(klass));
		}
		
		public static void TriggerQueryEvent(Transaction transaction, QueryEventHandler e, Db4objects.Db4o.Query.IQuery q)
		{
			if (null == e) return;
			e(q, new QueryEventArgs(transaction, q));
		}

		public static bool TriggerCancellableObjectEventArgs(Transaction transaction, CancellableObjectEventHandler e, object o)
		{
			if (null == e) return true;
			CancellableObjectEventArgs coea = new CancellableObjectEventArgs(transaction, o);
            try
            {
                e(o, coea);
            }
            catch (Db4oException)
            {
                throw;
            }
            catch (System.Exception exception)
            {
                throw new EventException(exception);
            }
			return !coea.IsCancelled;
		}

		public static void TriggerObjectEvent(Transaction transaction, ObjectEventHandler e, object o)
		{
			if (null == e) return;
			e(o, new ObjectEventArgs(transaction, o));
		}
		
		public static void TriggerCommitEvent(Transaction transaction, CommitEventHandler e, CallbackObjectInfoCollections objectInfoCollections)
		{
			if (null == e) return;
            e(null, new CommitEventArgs(transaction, objectInfoCollections));
		}
		
		public static bool HasListeners(System.Delegate e)
		{
			return null != e;
		}

		public static void TriggerObjectContainerEvent(IObjectContainer container, ObjectContainerEventHandler e)
		{
			if (null == e) return;
			e(container, new ObjectContainerEventArgs(container));
		}
	}
}

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
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Types;

namespace Db4objects.Db4o
{
	[Obsolete("since 7.0")]
	internal class P2Collections : IDb4oCollections
	{
		internal Transaction _transaction;

		internal P2Collections(Transaction transaction)
			: base()
		{
			_transaction = transaction;
		}

		public IDb4oList NewLinkedList()
		{
			lock (Lock())
			{
				if (CanCreateCollection(Container()))
				{
					IDb4oList l = new P2LinkedList();
					Container().Store(_transaction, l);
					return l;
				}
				return null;
			}
		}

		public IDb4oMap NewHashMap(int size)
		{
			lock (Lock())
			{
				if (CanCreateCollection(Container()))
				{
					return new P2HashMap(size);
				}
				return null;
			}
		}

		public IDb4oMap NewIdentityHashMap(int size)
		{
			lock (Lock())
			{
				if (CanCreateCollection(Container()))
				{
					P2HashMap m = new P2HashMap(size);
					m.i_type = 1;
					Container().Store(_transaction, m);
					return m;
				}
				return null;
			}
		}

		private Object Lock()
		{
			return Container().Lock();
		}

		private ObjectContainerBase Container()
		{
			return _transaction.Container();
		}

		private bool CanCreateCollection(ObjectContainerBase container)
		{
			container.CheckClosed();
			return !container.IsInstantiating();
		}
	}
}
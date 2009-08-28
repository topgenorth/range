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
using System.Collections;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal.Query.Result;
using Db4objects.Db4o.Query;

namespace Db4objects.Db4o.Internal.Query.Result
{
	/// <exclude></exclude>
	public class StatefulQueryResult : IEnumerable
	{
		private readonly IQueryResult _delegate;

		private readonly Iterable4Adaptor _iterable;

		public StatefulQueryResult(IQueryResult queryResult)
		{
			_delegate = queryResult;
			_iterable = new Iterable4Adaptor(queryResult);
		}

		public virtual object Get(int index)
		{
			lock (Lock())
			{
				return _delegate.Get(index);
			}
		}

		public virtual long[] GetIDs()
		{
			lock (Lock())
			{
				long[] ids = new long[Size()];
				int i = 0;
				IIntIterator4 iterator = _delegate.IterateIDs();
				while (iterator.MoveNext())
				{
					ids[i++] = iterator.CurrentInt();
				}
				return ids;
			}
		}

		public virtual bool HasNext()
		{
			lock (Lock())
			{
				return _iterable.HasNext();
			}
		}

		public virtual object Next()
		{
			lock (Lock())
			{
				return _iterable.Next();
			}
		}

		public virtual void Reset()
		{
			lock (Lock())
			{
				_iterable.Reset();
			}
		}

		public virtual int Size()
		{
			lock (Lock())
			{
				return _delegate.Size();
			}
		}

		public virtual void Sort(IQueryComparator cmp)
		{
			lock (Lock())
			{
				_delegate.Sort(cmp);
			}
		}

		public virtual object Lock()
		{
			return _delegate.Lock();
		}

		internal virtual IExtObjectContainer ObjectContainer()
		{
			return _delegate.ObjectContainer();
		}

		public virtual int IndexOf(object a_object)
		{
			lock (Lock())
			{
				int id = (int)ObjectContainer().GetID(a_object);
				if (id <= 0)
				{
					return -1;
				}
				return _delegate.IndexOf(id);
			}
		}

		public virtual IEnumerator IterateIDs()
		{
			lock (Lock())
			{
				return _delegate.IterateIDs();
			}
		}

		public virtual IEnumerator GetEnumerator()
		{
			lock (Lock())
			{
				return _delegate.GetEnumerator();
			}
		}
	}
}

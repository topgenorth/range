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
using System.Collections;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Internal.Query.Result;

namespace Db4objects.Db4o.Internal.Query
{
	/// <summary>
	/// List based objectSet implementation
	/// </summary>
	/// <exclude />
	public class ObjectSetFacade : IExtObjectSet, System.Collections.IList
	{
		public readonly StatefulQueryResult _delegate;

        public ObjectSetFacade(IQueryResult qr)
		{
            _delegate = new StatefulQueryResult(qr);
		}

		#region IObjectSet Members
		
		public Object Get(int index)
		{
            return _delegate.Get(index);
        }

		public void Sort(Db4objects.Db4o.Query.IQueryComparator cmp)
		{
			throw new NotImplementedException();
		}

		public long[] GetIDs() 
		{
			return _delegate.GetIDs();
		}

		public IExtObjectSet Ext() 
		{
			return this;
		}

		public bool HasNext() 
		{
			return _delegate.HasNext();
		}

		public Object Next() 
		{
			return _delegate.Next();
		}

		public void Reset() 
		{
			_delegate.Reset();
		}

		public int Size() 
		{
			return _delegate.Size();
		}
    
		private Object Lock()
		{
			return _delegate.Lock();
		}
    
		private IObjectContainer ObjectContainer()
		{
			return _delegate.ObjectContainer();
		}

		public StatefulQueryResult Delegate_()
		{
			return _delegate;
		}
		#endregion

		#region IList Members

		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		public object this[int index]
		{
			get
			{
				return _delegate.Get(index);
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		public void Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		public void Remove(object value)
		{
			throw new NotSupportedException();
		}

		public bool Contains(object value)
		{
			return IndexOf(value) >= 0;
		}

		public void Clear()
		{
			throw new NotSupportedException();
		}

		public int IndexOf(object value)
		{
			return _delegate.IndexOf(value);
		}

		public int Add(object value)
		{
			throw new NotSupportedException();
		}

		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		#endregion

		#region ICollection Members
		public bool IsSynchronized
		{
			get
			{
				return true;
			}
		}

		public int Count
		{
			get
			{
				return Size();
			}
		}

        public void CopyTo(Array array, int index)
        {
            lock (Lock())
            {
                int i = 0;
                int s = _delegate.Size();
                while (i < s)
                {
                    array.SetValue(_delegate.Get(i), index + i);
                    i++;
                }
            }
        }

        public object SyncRoot
		{
			get
			{
				return Lock();
			}
		}

		#endregion
		
		public System.Collections.IEnumerator GetEnumerator()
		{
			IEnumerator enumerator = _delegate.GetEnumerator();
			object current;
			while (MoveNext(enumerator, out current))
			{
				yield return current;
			}
		}

		private bool MoveNext(IEnumerator enumerator, out object current)
		{
			lock (_delegate.Lock())
			{
				if (enumerator.MoveNext())
				{
					current = enumerator.Current;
					return true;
				}
			}
			current = null;
			return false;
		}
	}
}



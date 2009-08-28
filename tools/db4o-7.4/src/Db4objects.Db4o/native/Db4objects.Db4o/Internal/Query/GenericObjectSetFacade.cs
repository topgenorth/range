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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Internal.Query.Result;

namespace Db4objects.Db4o.Internal.Query
{
    /// <summary>
    /// List based objectSet implementation
    /// </summary>
    /// <exclude />
    public class GenericObjectSetFacade<T> : System.Collections.Generic.IList<T>
    {
        public readonly StatefulQueryResult _delegate;

        public GenericObjectSetFacade(IQueryResult qr)
        {
            _delegate = new StatefulQueryResult(qr);
        }

        #region IList<T> Members
        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public T this[int index]
        {
            get
            {
                return (T)_delegate.Get(index);
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

        public void Insert(int index, T value)
        {
            throw new NotSupportedException();
        }

        public bool Remove(T value)
        {
            throw new NotSupportedException();
        }

        public bool Contains(T value)
        {
            return IndexOf(value) >= 0;
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public int IndexOf(T value)
        {
            return _delegate.IndexOf(value);
        }

        public void Add(T value)
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

        #region ICollection<T> Members
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
                return _delegate.Size();
            }
        }

        public void CopyTo(T[] array, int index)
        {
            lock (this.SyncRoot)
            {
                int i = 0;
                int s = this.Count;
                while (i < s)
                {
                    array[index + i] = this[i];
                    i++;
                }
            }
        }

        public object SyncRoot
        {
            get
            {
                return _delegate.Lock();
            }
        }

        #endregion

        #region IEnumerable Members

        class ObjectSetImplEnumerator<T> : System.Collections.IEnumerator, System.Collections.Generic.IEnumerator<T>
        {
            System.Collections.Generic.IList<T> _result;
            int _next = 0;

            public ObjectSetImplEnumerator(System.Collections.Generic.IList<T> result)
            {
                _result = result;
            }

            public void Reset()
            {
                _next = 0;
            }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                     return _result[_next - 1];
                }
            }

            public bool MoveNext()
            {
                if (_next < _result.Count)
                {
                    ++_next;
                    return true;
                }
                return false;
            }
            
            public T Current
            {
                get
                {
                     return _result[_next - 1];
                }
            }

            public void Dispose()
            {
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new ObjectSetImplEnumerator<T>(this);
        }
        #endregion

        #region IEnumerable<T> implementation
        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            return new ObjectSetImplEnumerator<T>(this);
        }
        #endregion
    }
}


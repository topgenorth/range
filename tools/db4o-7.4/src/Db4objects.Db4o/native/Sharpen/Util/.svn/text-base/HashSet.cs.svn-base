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

namespace Sharpen.Util
{   
    public class HashSet : ISet, /* IList required for dRS */ IList
    {   
        private readonly static object _object = new object();

		// FIXME: dRS doesn't like using a dictionary here
		private ArrayList _elements = new ArrayList();

        public HashSet()
        {
        }

        public HashSet(ICollection initialValues)
        {
            AddAll(initialValues);
        }

        public bool Add(object o)
        {
			if (Contains(o)) return false;

			_elements.Add(o);
			return true;
        }

        public bool AddAll(ICollection c)
        {
            bool changed = false;
			foreach (object o in c)
			{
				changed |= Add(o);
			}
        	return changed;
        }

        public void Clear()
        {
            _elements.Clear();
        }

        public bool Contains(object o)
        {
        	return _elements.Contains(o);
        }

        public bool ContainsAll(ICollection c)
        {
            foreach (object o in c)
			{
                if (!Contains(o))
                {
                	return false;
                }
            }
            return true;
        }

        public bool IsEmpty
        {
            get { return _elements.Count == 0; }
        }

        public bool Remove(object o)
        {
			if (!Contains(o)) return false;
            
			_elements.Remove(o);
        	return true;
        }

        public bool RemoveAll(ICollection c)
        {
            bool changed = false;
			foreach (object o in c)
			{
				changed |= Remove(o);
			}
        	return changed;
        }

        public void CopyTo(Array array, int index)
        {
            _elements.CopyTo(array, index);
        }

        public int Count
        {
            get { return _elements.Count; }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return _elements.SyncRoot; }
        }

        public IEnumerator GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

    	int IList.Add(object value)
    	{
    		((ISet) this).Add(value);
    		return 0;
    	}

		void IList.Remove(object value)
		{
			((ISet)this).Remove(value);
		}

    	int IList.IndexOf(object value)
    	{
    		throw new NotImplementedException();
    	}

    	void IList.Insert(int index, object value)
    	{
    		throw new NotImplementedException();
    	}

    	void IList.RemoveAt(int index)
    	{
    		throw new NotImplementedException();
    	}

    	object IList.this[int index]
    	{
    		get { throw new NotImplementedException(); }
    		set { throw new NotImplementedException(); }
    	}

    	bool IList.IsReadOnly
    	{
    		get { return false; }
    	}

    	bool IList.IsFixedSize
    	{
    		get { return false; }
    	}
    }
}

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
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Types;

namespace Db4objects.Db4o.Foundation
{
	/// <summary>Fast linked list for all usecases.</summary>
	/// <remarks>Fast linked list for all usecases.</remarks>
	/// <exclude></exclude>
	public class Collection4 : ISequence4, IEnumerable, IDeepClone, IUnversioned
	{
		/// <summary>first element of the linked list</summary>
		private List4 _first;

		private List4 _last;

		/// <summary>number of elements collected</summary>
		private int _size;

		private int _version;

		public Collection4()
		{
		}

		public Collection4(object[] elements)
		{
			// FIELDS ARE PUBLIC SO THEY CAN BE REFLECTED ON IN JDKs <= 1.1
			AddAll(elements);
		}

		public Collection4(IEnumerable other)
		{
			AddAll(other);
		}

		public Collection4(IEnumerator iterator)
		{
			AddAll(iterator);
		}

		public virtual object SingleElement()
		{
			if (Size() != 1)
			{
				throw new InvalidOperationException();
			}
			return _first._element;
		}

		/// <summary>Adds an element to the end of this collection.</summary>
		/// <remarks>Adds an element to the end of this collection.</remarks>
		/// <param name="element"></param>
		public void Add(object element)
		{
			DoAdd(element);
			Changed();
		}

		public void Prepend(object element)
		{
			DoPrepend(element);
			Changed();
		}

		private void DoPrepend(object element)
		{
			if (_first == null)
			{
				DoAdd(element);
			}
			else
			{
				_first = new List4(_first, element);
				_size++;
			}
		}

		private void DoAdd(object element)
		{
			if (_last == null)
			{
				_first = new List4(element);
				_last = _first;
			}
			else
			{
				_last._next = new List4(element);
				_last = _last._next;
			}
			_size++;
		}

		public void AddAll(object[] elements)
		{
			AssertNotNull(elements);
			for (int i = 0; i < elements.Length; i++)
			{
				Add(elements[i]);
			}
		}

		public void AddAll(IEnumerable other)
		{
			AssertNotNull(other);
			AddAll(other.GetEnumerator());
		}

		public void AddAll(IEnumerator iterator)
		{
			AssertNotNull(iterator);
			while (iterator.MoveNext())
			{
				Add(iterator.Current);
			}
		}

		public void Clear()
		{
			_first = null;
			_last = null;
			_size = 0;
			Changed();
		}

		public bool Contains(object element)
		{
			return Find(element) != null;
		}

		public virtual bool ContainsAll(IEnumerator iter)
		{
			AssertNotNull(iter);
			while (iter.MoveNext())
			{
				if (!Contains(iter.Current))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>tests if the object is in the Collection.</summary>
		/// <remarks>tests if the object is in the Collection. == comparison.</remarks>
		public bool ContainsByIdentity(object element)
		{
			IEnumerator i = InternalIterator();
			while (i.MoveNext())
			{
				object current = i.Current;
				if (current == element)
				{
					return true;
				}
			}
			return false;
		}

		private List4 Find(object obj)
		{
			List4 current = _first;
			while (current != null)
			{
				if (current.Holds(obj))
				{
					return current;
				}
				current = current._next;
			}
			return null;
		}

		/// <summary>
		/// returns the first object found in the Collections that equals() the
		/// passed object
		/// </summary>
		public object Get(object element)
		{
			List4 holder = Find(element);
			return holder == null ? null : holder._element;
		}

		public virtual object DeepClone(object newParent)
		{
			Db4objects.Db4o.Foundation.Collection4 col = new Db4objects.Db4o.Foundation.Collection4
				();
			object element = null;
			IEnumerator i = InternalIterator();
			while (i.MoveNext())
			{
				element = i.Current;
				if (element is IDeepClone)
				{
					col.Add(((IDeepClone)element).DeepClone(newParent));
				}
				else
				{
					col.Add(element);
				}
			}
			return col;
		}

		/// <summary>makes sure the passed object is in the Collection.</summary>
		/// <remarks>makes sure the passed object is in the Collection. equals() comparison.</remarks>
		public object Ensure(object element)
		{
			List4 list = Find(element);
			if (list == null)
			{
				Add(element);
				return element;
			}
			return list._element;
		}

		/// <summary>
		/// Iterates through the collection in reversed insertion order which happens
		/// to be the fastest.
		/// </summary>
		/// <remarks>
		/// Iterates through the collection in reversed insertion order which happens
		/// to be the fastest.
		/// </remarks>
		/// <returns></returns>
		public IEnumerator GetEnumerator()
		{
			return _first == null ? Iterators.EmptyIterator : new Collection4Iterator(this, _first
				);
		}

		public virtual object Get(int index)
		{
			if (index < 0)
			{
				throw new ArgumentException();
			}
			List4 cur = _first;
			while (index > 0 && cur != null)
			{
				cur = cur._next;
				index--;
			}
			if (cur == null)
			{
				throw new ArgumentException();
			}
			return cur._element;
		}

		/// <summary>
		/// Removes all the elements from this collection that are returned by
		/// iterable.
		/// </summary>
		/// <remarks>
		/// Removes all the elements from this collection that are returned by
		/// iterable.
		/// </remarks>
		/// <param name="iterable"></param>
		public virtual void RemoveAll(IEnumerable iterable)
		{
			RemoveAll(iterable.GetEnumerator());
		}

		/// <summary>
		/// Removes all the elements from this collection that are returned by
		/// iterator.
		/// </summary>
		/// <remarks>
		/// Removes all the elements from this collection that are returned by
		/// iterator.
		/// </remarks>
		/// <param name="iterable"></param>
		public virtual void RemoveAll(IEnumerator iterator)
		{
			while (iterator.MoveNext())
			{
				Remove(iterator.Current);
			}
		}

		/// <summary>
		/// removes an object from the Collection equals() comparison returns the
		/// removed object or null, if none found
		/// </summary>
		public virtual object Remove(object a_object)
		{
			List4 previous = null;
			List4 current = _first;
			while (current != null)
			{
				if (current.Holds(a_object))
				{
					_size--;
					AdjustOnRemoval(previous, current);
					Changed();
					return current._element;
				}
				previous = current;
				current = current._next;
			}
			return null;
		}

		public virtual void Replace(object oldObject, object newObject)
		{
			List4 list = Find(oldObject);
			if (list != null)
			{
				list._element = newObject;
			}
		}

		private void AdjustOnRemoval(List4 previous, List4 removed)
		{
			if (removed == _first)
			{
				_first = removed._next;
			}
			else
			{
				previous._next = removed._next;
			}
			if (removed == _last)
			{
				_last = previous;
			}
		}

		public int Size()
		{
			return _size;
		}

		public virtual int IndexOf(object obj)
		{
			int index = 0;
			List4 current = _first;
			while (current != null)
			{
				if (current.Holds(obj))
				{
					return index;
				}
				index++;
				current = current._next;
			}
			return -1;
		}

		public bool IsEmpty()
		{
			return _size == 0;
		}

		/// <summary>This is a non reflection implementation for more speed.</summary>
		/// <remarks>
		/// This is a non reflection implementation for more speed. In contrast to
		/// the JDK behaviour, the passed array has to be initialized to the right
		/// length.
		/// </remarks>
		public object[] ToArray(object[] a_array)
		{
			int j = 0;
			IEnumerator i = InternalIterator();
			while (i.MoveNext())
			{
				a_array[j++] = i.Current;
			}
			return a_array;
		}

		public object[] ToArray()
		{
			object[] array = new object[_size];
			ToArray(array);
			return array;
		}

		public override string ToString()
		{
			return Iterators.ToString(InternalIterator());
		}

		private void Changed()
		{
			++_version;
		}

		internal virtual int Version()
		{
			return _version;
		}

		private void AssertNotNull(object element)
		{
			if (element == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>
		/// Leaner iterator for faster iteration (but unprotected against
		/// concurrent modifications).
		/// </summary>
		/// <remarks>
		/// Leaner iterator for faster iteration (but unprotected against
		/// concurrent modifications).
		/// </remarks>
		private IEnumerator InternalIterator()
		{
			return new Iterator4Impl(_first);
		}
	}
}

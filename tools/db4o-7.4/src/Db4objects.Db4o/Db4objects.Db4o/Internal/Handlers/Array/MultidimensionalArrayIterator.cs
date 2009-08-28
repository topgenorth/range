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
using Db4objects.Db4o.Internal.Handlers.Array;
using Db4objects.Db4o.Reflect;

namespace Db4objects.Db4o.Internal.Handlers.Array
{
	/// <exclude></exclude>
	public class MultidimensionalArrayIterator : IEnumerator
	{
		private readonly IReflectArray _reflectArray;

		private readonly object[] _array;

		private int _currentElement;

		private IEnumerator _delegate;

		public MultidimensionalArrayIterator(IReflectArray reflectArray, object[] array)
		{
			_reflectArray = reflectArray;
			_array = array;
			Reset();
		}

		public virtual object Current
		{
			get
			{
				if (_delegate == null)
				{
					return _array[_currentElement];
				}
				return _delegate.Current;
			}
		}

		public virtual bool MoveNext()
		{
			if (_delegate != null)
			{
				if (_delegate.MoveNext())
				{
					return true;
				}
				_delegate = null;
			}
			_currentElement++;
			if (_currentElement >= _array.Length)
			{
				return false;
			}
			object obj = _array[_currentElement];
			Type clazz = obj.GetType();
			if (clazz.IsArray)
			{
				if (clazz.GetElementType().IsArray)
				{
					_delegate = new Db4objects.Db4o.Internal.Handlers.Array.MultidimensionalArrayIterator
						(_reflectArray, (object[])obj);
				}
				else
				{
					_delegate = new ReflectArrayIterator(_reflectArray, obj);
				}
				return MoveNext();
			}
			return true;
		}

		public virtual void Reset()
		{
			_currentElement = -1;
			_delegate = null;
		}
	}
}

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

namespace Db4objects.Db4o.Foundation
{
	/// <exclude></exclude>
	public abstract class MappingIterator : IEnumerator
	{
		private readonly IEnumerator _iterator;

		private object _current;

		public MappingIterator(IEnumerator iterator)
		{
			if (null == iterator)
			{
				throw new ArgumentNullException();
			}
			_iterator = iterator;
			_current = Iterators.NoElement;
		}

		protected abstract object Map(object current);

		public virtual bool MoveNext()
		{
			do
			{
				if (!_iterator.MoveNext())
				{
					_current = Iterators.NoElement;
					return false;
				}
				_current = Map(_iterator.Current);
			}
			while (_current == Iterators.Skip);
			return true;
		}

		public virtual void Reset()
		{
			_current = Iterators.NoElement;
			_iterator.Reset();
		}

		public virtual object Current
		{
			get
			{
				if (Iterators.NoElement == _current)
				{
					throw new InvalidOperationException();
				}
				return _current;
			}
		}
	}
}

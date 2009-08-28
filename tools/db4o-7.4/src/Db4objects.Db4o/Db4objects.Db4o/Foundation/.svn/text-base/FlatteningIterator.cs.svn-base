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
using Db4objects.Db4o.Foundation;

namespace Db4objects.Db4o.Foundation
{
	public class FlatteningIterator : Db4objects.Db4o.Foundation.CompositeIterator4
	{
		private class IteratorStack
		{
			public readonly IEnumerator iterator;

			public readonly FlatteningIterator.IteratorStack next;

			public IteratorStack(IEnumerator iterator_, FlatteningIterator.IteratorStack next_
				)
			{
				iterator = iterator_;
				next = next_;
			}
		}

		private FlatteningIterator.IteratorStack _stack;

		public FlatteningIterator(IEnumerator iterators) : base(iterators)
		{
		}

		public override bool MoveNext()
		{
			if (null == _currentIterator)
			{
				if (null == _stack)
				{
					_currentIterator = _iterators;
				}
				else
				{
					_currentIterator = Pop();
				}
			}
			if (!_currentIterator.MoveNext())
			{
				if (_currentIterator == _iterators)
				{
					return false;
				}
				_currentIterator = null;
				return MoveNext();
			}
			object current = _currentIterator.Current;
			if (current is IEnumerator)
			{
				Push(_currentIterator);
				_currentIterator = NextIterator(current);
				return MoveNext();
			}
			return true;
		}

		private void Push(IEnumerator currentIterator)
		{
			_stack = new FlatteningIterator.IteratorStack(currentIterator, _stack);
		}

		private IEnumerator Pop()
		{
			IEnumerator iterator = _stack.iterator;
			_stack = _stack.next;
			return iterator;
		}
	}
}

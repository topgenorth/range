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
using System.Collections.Generic;

using Db4objects.Db4o;

namespace Db4objects.Db4o.Linq.Internals
{
	/// <summary>
	/// A generic wrapper around a not generic IEnumerable,
	/// Faithfully hoping that all items in the enumeration
	/// are of the same kind, otherwise it will throw a
	/// ClassCastException on access.
	/// </summary>
	/// <typeparam name="T">The type of the items</typeparam>
	public class ObjectSequence<T> : IEnumerable<T>
	{
		private IEnumerable _enumerable;

		public ObjectSequence(IEnumerable enumerable)
		{
			_enumerable = enumerable;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return new ObjectSequenceEnumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		internal class ObjectSequenceEnumerator : IEnumerator<T>
		{
			private IEnumerator _enumerator;

			public T Current {
				get { return (T)_enumerator.Current; }
			}

			object IEnumerator.Current
			{
				get { return Current; }
			}

			public ObjectSequenceEnumerator(ObjectSequence<T> sequence)
			{
				_enumerator = sequence._enumerable.GetEnumerator();
			}

			public bool MoveNext()
			{
				return _enumerator.MoveNext();
			}

			public void Reset()
			{
				_enumerator.Reset();
			}

			public void Dispose()
			{
				IDisposable enumerator = _enumerator as IDisposable;
				if (enumerator == null) return;

				enumerator.Dispose();
			}
		}
	}
}

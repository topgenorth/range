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

namespace Db4objects.Db4o.Tests.Util
{
	public class PermutingTestConfig
	{
		private object[][] _values;

		private int[] _indices;

		private bool _started;

		public PermutingTestConfig(object[][] values)
		{
			_values = values;
			_indices = new int[_values.Length];
			_started = false;
		}

		public virtual bool MoveNext()
		{
			if (!_started)
			{
				_started = true;
				return true;
			}
			for (int groupIdx = _indices.Length - 1; groupIdx >= 0; groupIdx--)
			{
				if (_indices[groupIdx] < _values[groupIdx].Length - 1)
				{
					_indices[groupIdx]++;
					for (int resetGroupIdx = groupIdx + 1; resetGroupIdx < _indices.Length; resetGroupIdx
						++)
					{
						_indices[resetGroupIdx] = 0;
					}
					return true;
				}
			}
			return false;
		}

		/// <exception cref="InvalidOperationException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public virtual object Current(int groupIdx)
		{
			if (!_started)
			{
				throw new InvalidOperationException();
			}
			if (groupIdx < 0 || groupIdx >= _indices.Length)
			{
				throw new ArgumentException();
			}
			return _values[groupIdx][_indices[groupIdx]];
		}
	}
}

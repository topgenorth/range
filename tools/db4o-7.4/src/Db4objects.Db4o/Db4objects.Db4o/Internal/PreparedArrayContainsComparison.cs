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
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Handlers.Array;
using Db4objects.Db4o.Marshall;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Internal
{
	/// <exclude></exclude>
	public class PreparedArrayContainsComparison : IPreparedComparison
	{
		private readonly ArrayHandler _arrayHandler;

		private readonly IPreparedComparison _preparedComparison;

		private ObjectContainerBase _container;

		public PreparedArrayContainsComparison(IContext context, ArrayHandler arrayHandler
			, ITypeHandler4 typeHandler, object obj)
		{
			_arrayHandler = arrayHandler;
			_preparedComparison = typeHandler.PrepareComparison(context, obj);
			_container = context.Transaction().Container();
		}

		public virtual int CompareTo(object obj)
		{
			// We never expect this call
			// TODO: The callers of this class should be refactored to pass a matcher and
			//       to expect a PreparedArrayComparison.
			throw new InvalidOperationException();
		}

		public virtual bool IsEqual(object array)
		{
			return IsMatch(array, IntMatcher.Zero);
		}

		public virtual bool IsGreaterThan(object array)
		{
			return IsMatch(array, IntMatcher.Positive);
		}

		public virtual bool IsSmallerThan(object array)
		{
			return IsMatch(array, IntMatcher.Negative);
		}

		private bool IsMatch(object array, IntMatcher matcher)
		{
			if (array == null)
			{
				return false;
			}
			IEnumerator i = _arrayHandler.AllElements(_container, array);
			while (i.MoveNext())
			{
				if (matcher.Match(_preparedComparison.CompareTo(i.Current)))
				{
					return true;
				}
			}
			return false;
		}
	}
}

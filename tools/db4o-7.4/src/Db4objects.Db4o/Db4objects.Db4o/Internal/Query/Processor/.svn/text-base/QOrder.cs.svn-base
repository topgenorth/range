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
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal.Query.Processor;

namespace Db4objects.Db4o.Internal.Query.Processor
{
	/// <exclude></exclude>
	internal class QOrder : Tree
	{
		internal static int equalityIDGenerator = 1;

		internal readonly QConObject _constraint;

		internal readonly QCandidate _candidate;

		private int _equalityID;

		internal QOrder(QConObject a_constraint, QCandidate a_candidate)
		{
			_constraint = a_constraint;
			_candidate = a_candidate;
		}

		public virtual bool IsEqual(Db4objects.Db4o.Internal.Query.Processor.QOrder other
			)
		{
			if (other == null)
			{
				return false;
			}
			return _equalityID != 0 && _equalityID == other._equalityID;
		}

		public override int Compare(Tree a_to)
		{
			int res = InternalCompare();
			if (res != 0)
			{
				return res;
			}
			Db4objects.Db4o.Internal.Query.Processor.QOrder other = (Db4objects.Db4o.Internal.Query.Processor.QOrder
				)a_to;
			int equalityID = _equalityID;
			if (equalityID == 0)
			{
				if (other._equalityID != 0)
				{
					equalityID = other._equalityID;
				}
			}
			if (equalityID == 0)
			{
				equalityID = GenerateEqualityID();
			}
			_equalityID = equalityID;
			other._equalityID = equalityID;
			return res;
		}

		private int InternalCompare()
		{
			int comparisonResult = _constraint._preparedComparison.CompareTo(_candidate.Value
				());
			if (comparisonResult > 0)
			{
				return -_constraint.Ordering();
			}
			if (comparisonResult == 0)
			{
				return 0;
			}
			return _constraint.Ordering();
		}

		public override object ShallowClone()
		{
			Db4objects.Db4o.Internal.Query.Processor.QOrder order = new Db4objects.Db4o.Internal.Query.Processor.QOrder
				(_constraint, _candidate);
			base.ShallowCloneInternal(order);
			return order;
		}

		public override object Key()
		{
			throw new NotImplementedException();
		}

		private static int GenerateEqualityID()
		{
			equalityIDGenerator++;
			if (equalityIDGenerator < 1)
			{
				equalityIDGenerator = 1;
			}
			return equalityIDGenerator;
		}
	}
}

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
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Query.Processor;
using Db4objects.Db4o.Types;

namespace Db4objects.Db4o.Internal.Query.Processor
{
	/// <summary>Query Evaluator - Represents such things as &gt;, &gt;=, &lt;, &lt;=, EQUAL, LIKE, etc.
	/// 	</summary>
	/// <remarks>Query Evaluator - Represents such things as &gt;, &gt;=, &lt;, &lt;=, EQUAL, LIKE, etc.
	/// 	</remarks>
	/// <exclude></exclude>
	public class QE : IUnversioned
	{
		internal static readonly QE Default = new QE();

		public const int Nulls = 0;

		public const int Smaller = 1;

		public const int Equal = 2;

		public const int Greater = 3;

		internal virtual QE Add(QE evaluator)
		{
			return evaluator;
		}

		public virtual bool Identity()
		{
			return false;
		}

		internal virtual bool IsDefault()
		{
			return true;
		}

		internal virtual bool Evaluate(QConObject constraint, QCandidate candidate, object
			 obj)
		{
			IPreparedComparison prepareComparison = constraint.PrepareComparison(candidate);
			if (obj == null)
			{
				return prepareComparison is Null;
			}
			if (prepareComparison is PreparedArrayContainsComparison)
			{
				return ((PreparedArrayContainsComparison)prepareComparison).IsEqual(obj);
			}
			return prepareComparison.CompareTo(obj) == 0;
		}

		public override bool Equals(object obj)
		{
			return obj != null && obj.GetType() == this.GetType();
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode();
		}

		// overridden in QENot 
		internal virtual bool Not(bool res)
		{
			return res;
		}

		/// <summary>Specifies which part of the index to take.</summary>
		/// <remarks>
		/// Specifies which part of the index to take.
		/// Array elements:
		/// [0] - smaller
		/// [1] - equal
		/// [2] - greater
		/// [3] - nulls
		/// </remarks>
		/// <param name="bits"></param>
		public virtual void IndexBitMap(bool[] bits)
		{
			bits[QE.Equal] = true;
		}

		public virtual bool SupportsIndex()
		{
			return true;
		}
	}
}

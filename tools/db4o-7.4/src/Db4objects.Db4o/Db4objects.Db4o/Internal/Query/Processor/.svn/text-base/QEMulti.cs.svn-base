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
using Db4objects.Db4o.Internal.Query.Processor;

namespace Db4objects.Db4o.Internal.Query.Processor
{
	/// <exclude></exclude>
	public class QEMulti : QE
	{
		public Collection4 i_evaluators = new Collection4();

		internal override QE Add(QE evaluator)
		{
			i_evaluators.Ensure(evaluator);
			return this;
		}

		public override bool Identity()
		{
			bool ret = false;
			IEnumerator i = i_evaluators.GetEnumerator();
			while (i.MoveNext())
			{
				if (((QE)i.Current).Identity())
				{
					ret = true;
				}
				else
				{
					return false;
				}
			}
			return ret;
		}

		internal override bool IsDefault()
		{
			return false;
		}

		internal override bool Evaluate(QConObject a_constraint, QCandidate a_candidate, 
			object a_value)
		{
			IEnumerator i = i_evaluators.GetEnumerator();
			while (i.MoveNext())
			{
				if (((QE)i.Current).Evaluate(a_constraint, a_candidate, a_value))
				{
					return true;
				}
			}
			return false;
		}

		public override void IndexBitMap(bool[] bits)
		{
			IEnumerator i = i_evaluators.GetEnumerator();
			while (i.MoveNext())
			{
				((QE)i.Current).IndexBitMap(bits);
			}
		}

		public override bool SupportsIndex()
		{
			IEnumerator i = i_evaluators.GetEnumerator();
			while (i.MoveNext())
			{
				if (!((QE)i.Current).SupportsIndex())
				{
					return false;
				}
			}
			return true;
		}
	}
}

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
	internal class QPending : Tree
	{
		internal readonly QConJoin _join;

		internal QCon _constraint;

		internal int _result;

		internal const int False = -4;

		internal const int Both = 1;

		internal const int True = 2;

		internal QPending(QConJoin a_join, QCon a_constraint, bool a_firstResult)
		{
			// Constants, so QConJoin.evaluatePending is made easy:
			_join = a_join;
			_constraint = a_constraint;
			_result = a_firstResult ? True : False;
		}

		public override int Compare(Tree a_to)
		{
			return _constraint.i_id - ((Db4objects.Db4o.Internal.Query.Processor.QPending)a_to
				)._constraint.i_id;
		}

		internal virtual void ChangeConstraint()
		{
			_constraint = _join.GetOtherConstraint(_constraint);
		}

		public override object ShallowClone()
		{
			Db4objects.Db4o.Internal.Query.Processor.QPending pending = InternalClonePayload(
				);
			base.ShallowCloneInternal(pending);
			return pending;
		}

		internal virtual Db4objects.Db4o.Internal.Query.Processor.QPending InternalClonePayload
			()
		{
			Db4objects.Db4o.Internal.Query.Processor.QPending pending = new Db4objects.Db4o.Internal.Query.Processor.QPending
				(_join, _constraint, false);
			pending._result = _result;
			return pending;
		}

		public override object Key()
		{
			throw new NotImplementedException();
		}
	}
}

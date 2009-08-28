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
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Query.Processor;

namespace Db4objects.Db4o.Internal.Query.Processor
{
	/// <summary>Join constraint on queries</summary>
	/// <exclude></exclude>
	public class QConJoin : QCon
	{
		public bool i_and;

		public QCon i_constraint1;

		public QCon i_constraint2;

		public QConJoin()
		{
		}

		internal QConJoin(Transaction a_trans, QCon a_c1, QCon a_c2, bool a_and) : base(a_trans
			)
		{
			// FIELDS MUST BE PUBLIC TO BE REFLECTED ON UNDER JDK <= 1.1
			// C/S
			i_constraint1 = a_c1;
			i_constraint2 = a_c2;
			i_and = a_and;
		}

		internal override void DoNotInclude(QCandidate a_root)
		{
			i_constraint1.DoNotInclude(a_root);
			i_constraint2.DoNotInclude(a_root);
		}

		internal override void ExchangeConstraint(QCon a_exchange, QCon a_with)
		{
			base.ExchangeConstraint(a_exchange, a_with);
			if (a_exchange == i_constraint1)
			{
				i_constraint1 = a_with;
			}
			if (a_exchange == i_constraint2)
			{
				i_constraint2 = a_with;
			}
		}

		internal virtual void EvaluatePending(QCandidate a_root, QPending a_pending, int 
			a_secondResult)
		{
			bool res = i_evaluator.Not(i_and ? ((a_pending._result + a_secondResult) > 0) : (
				a_pending._result + a_secondResult) > -4);
			if (HasJoins())
			{
				IEnumerator i = IterateJoins();
				while (i.MoveNext())
				{
					Db4objects.Db4o.Internal.Query.Processor.QConJoin qcj = (Db4objects.Db4o.Internal.Query.Processor.QConJoin
						)i.Current;
					a_root.Evaluate(new QPending(qcj, this, res));
				}
			}
			else
			{
				if (!res)
				{
					i_constraint1.DoNotInclude(a_root);
					i_constraint2.DoNotInclude(a_root);
				}
			}
		}

		public virtual QCon GetOtherConstraint(QCon a_constraint)
		{
			if (a_constraint == i_constraint1)
			{
				return i_constraint2;
			}
			else
			{
				if (a_constraint == i_constraint2)
				{
					return i_constraint1;
				}
			}
			throw new ArgumentException();
		}

		internal override string LogObject()
		{
			return string.Empty;
		}

		internal virtual bool RemoveForParent(QCon a_constraint)
		{
			if (i_and)
			{
				QCon other = GetOtherConstraint(a_constraint);
				other.RemoveJoin(this);
				// prevents circular call
				other.Remove();
				return true;
			}
			return false;
		}

		public override string ToString()
		{
			string str = "QConJoin " + (i_and ? "AND " : "OR");
			if (i_constraint1 != null)
			{
				str += "\n   " + i_constraint1;
			}
			if (i_constraint2 != null)
			{
				str += "\n   " + i_constraint2;
			}
			return str;
		}

		public virtual bool IsOr()
		{
			return !i_and;
		}
	}
}

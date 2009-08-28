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
using System.Collections.Generic;
using System.Text;

using Db4objects.Db4o;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Internal.Query.Processor;

namespace Db4objects.Db4o.Linq.Tests.Queries
{
	internal class QueryPrettyPrinter
	{
		private StringBuilder _builder = new StringBuilder();
		private StringBuilder _orderBy = new StringBuilder();
		private HashSet<QConJoin> _visitedJoins = new HashSet<QConJoin>();

		public QueryPrettyPrinter(IQuery query)
		{
			foreach (var constraint in query.Constraints().ToArray())
				Visit(constraint);
		}

		private void Visit(IConstraint constraint)
		{
			switch (constraint.GetType().Name)
			{
				case "QConClass":
					Visit(constraint as QConClass);
					break;
				case "QConObject":
					Visit(constraint as QConObject);
					break;
				case "QConJoin":
					Visit(constraint as QConJoin);
					break;
				case "QConPath":
					Visit(constraint as QConPath);
					break;
			}
		}

		protected virtual void Visit(QConClass klass)
		{
			_builder.Append("(");
			_builder.Append(GetClassName(klass.GetClassName()));

			if (klass._children != null)
			{
				ConsumeList<IConstraint>(klass._children, cons => Visit(cons));
			}
			FlushOrderBy();
			_builder.Append(")");
		}

		private void FlushOrderBy()
		{
			_builder.Append(_orderBy);
			_orderBy.Length = 0;
		}

		private static void ConsumeList<TElement>(List4 list, Action<TElement> action)
		{
			while (list != null)
			{
				action((TElement)list._element);
				list = list._next;
			}
		}

		protected virtual void Visit(QConPath path)
		{
			PrintOrderBy(path);
		}

		private void PrintOrderBy(QConObject path)
		{
			if (path.i_orderID == 0) return;

			_orderBy.AppendFormat("(orderby {0} {1})",
				path.i_field.i_name,
				OrderIdToString(path.i_orderID));
		}

		private static string OrderIdToString(int order)
		{
			return order < 0 ? "desc" : "asc";
		}

		protected virtual void Visit(QConJoin join)
		{
			if (_visitedJoins.Contains(join)) return;
			_builder.Append("(");
			VisitJoinBranch(join.i_constraint2);
			_builder.AppendFormat(" {0} ", join.i_and ? "and" : "or");
			VisitJoinBranch(join.i_constraint1);
			_builder.Append(")");
			_visitedJoins.Add(join);
		}

		private void VisitJoinBranch(QCon branch)
		{
			if (branch is QConJoin)
			{
				Visit(branch as QConJoin);
				return;
			}

			if (branch is QConObject)
			{
				PrintQConObject(branch as QConObject);
				return;
			}

			throw new NotSupportedException();
		}

		protected virtual void Visit(QConObject obj)
		{
			if (obj.i_joins != null)
			{
				foreach (IConstraint constraint in obj.i_joins)
				{
					Visit(constraint);
				}
				return;
			}
			PrintQConObject(obj);
		}

		private void PrintQConObject(QConObject obj)
		{
			PrintOrderBy(obj);
			_builder.AppendFormat("({0} {1} {2})",
				obj.GetField().i_name,
				EvaluatorToString(obj.i_evaluator),
				ValueToString(obj.i_object));
		}

		private static string EvaluatorToString(QE evaluator)
		{
			switch (evaluator.GetType().Name)
			{
				case "QEMulti":
				{
					StringBuilder sb = new StringBuilder();
					foreach (QE qe in ((QEMulti)evaluator).i_evaluators)
					{
						sb.Append(EvaluatorToString(qe));
					}
					return sb.ToString();
				}
				case "QE": return "==";
				case "QESmaller": return "<";
				case "QEGreater": return ">";
				case "QEEqual": return "=";
				case "QENot": return "not";
				case "QEStartsWith": return "startswith";
				case "QEEndsWith": return "endswith";
				case "QEContains": return "contains";
			}

			throw new NotSupportedException();
		}

		private static string ValueToString(object value)
		{	
			if (value is string) return string.Format("'{0}'", value);

			return value.ToString();
		}

		private static string GetClassName(string fullname)
		{
			int pos = fullname.LastIndexOf(",");
			if (pos > -1)
				fullname = fullname.Substring(0, pos);

			pos = fullname.LastIndexOf("+");
			if (pos > -1)
				return ExtractSuffix(fullname, pos);

			pos = fullname.LastIndexOf(".");
			if (pos > -1)
				return ExtractSuffix(fullname, pos);

			return fullname;
		}

		private static string ExtractSuffix(string str, int pos)
		{
			return str.Substring(pos + 1, str.Length - pos - 1);
		}

		public override string ToString()
		{
			return _builder.ToString() + _orderBy.ToString();
		}
	}
}

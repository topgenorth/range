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
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Db4objects.Db4o.Linq.Expressions
{
	internal class HashCodeCalculation : ExpressionVisitor
	{
		private int _hashCode;

		public int HashCode
		{
			get { return _hashCode; }
		}

		public HashCodeCalculation(Expression expression)
		{
			Visit(expression);
		}

		private void Add(int i)
		{
			_hashCode ^= i;
		}

		protected override void Visit(Expression expression)
		{
			if (expression == null) return;

			Add((int)expression.NodeType);
			Add(expression.Type.GetHashCode());

			base.Visit(expression);
		}

		protected override void VisitList<T>(ReadOnlyCollection<T> list, Action<T> visitor)
		{
			Add(list.Count);

			base.VisitList<T>(list, visitor);
		}

		protected override void VisitConstant(ConstantExpression constant)
		{
			if (constant != null) Add(constant.Value.GetHashCode());
		}

		protected override void VisitMemberAccess(MemberExpression member)
		{
			Add(member.Member.GetHashCode());

			base.VisitMemberAccess(member);
		}

		protected override void VisitMethodCall(MethodCallExpression methodCall)
		{
			Add(methodCall.Method.GetHashCode());

			base.VisitMethodCall(methodCall);
		}

		protected override void VisitParameter(ParameterExpression parameter)
		{
			Add(parameter.Name.GetHashCode());
		}

		protected override void VisitTypeIs(TypeBinaryExpression type)
		{
			Add(type.TypeOperand.GetHashCode());

			base.VisitTypeIs(type);
		}

		protected override void VisitBinary(BinaryExpression binary)
		{
			if (binary.Method != null) Add(binary.Method.GetHashCode());
			if (binary.IsLifted) Add(1);
			if (binary.IsLiftedToNull) Add(1);

			base.VisitBinary(binary);
		}

		protected override void VisitUnary(UnaryExpression unary)
		{
			if (unary.Method != null) Add(unary.Method.GetHashCode());
			if (unary.IsLifted) Add(1);
			if (unary.IsLiftedToNull) Add(1);

			base.VisitUnary(unary);
		}

		protected override void VisitNew(NewExpression nex)
		{
			Add(nex.Constructor.GetHashCode());
			VisitList(nex.Members, member => Add(member.GetHashCode()));

			base.VisitNew(nex);
		}
	}
}

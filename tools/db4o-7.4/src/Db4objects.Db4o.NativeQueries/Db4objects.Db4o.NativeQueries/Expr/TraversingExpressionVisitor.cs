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
using Db4objects.Db4o.NativeQueries.Expr;
using Db4objects.Db4o.NativeQueries.Expr.Cmp.Operand;

namespace Db4objects.Db4o.NativeQueries.Expr
{
	public class TraversingExpressionVisitor : IExpressionVisitor, IComparisonOperandVisitor
	{
		public virtual void Visit(AndExpression expression)
		{
			expression.Left().Accept(this);
			expression.Right().Accept(this);
		}

		public virtual void Visit(BoolConstExpression expression)
		{
		}

		public virtual void Visit(OrExpression expression)
		{
			expression.Left().Accept(this);
			expression.Right().Accept(this);
		}

		public virtual void Visit(ComparisonExpression expression)
		{
			expression.Left().Accept(this);
			expression.Right().Accept(this);
		}

		public virtual void Visit(NotExpression expression)
		{
			expression.Expr().Accept(this);
		}

		public virtual void Visit(ArithmeticExpression operand)
		{
			operand.Left().Accept(this);
			operand.Right().Accept(this);
		}

		public virtual void Visit(ConstValue operand)
		{
		}

		public virtual void Visit(FieldValue operand)
		{
			operand.Parent().Accept(this);
		}

		public virtual void Visit(CandidateFieldRoot root)
		{
		}

		public virtual void Visit(PredicateFieldRoot root)
		{
		}

		public virtual void Visit(StaticFieldRoot root)
		{
		}

		public virtual void Visit(ArrayAccessValue operand)
		{
			operand.Parent().Accept(this);
			operand.Index().Accept(this);
		}

		public virtual void Visit(MethodCallValue value)
		{
			value.Parent().Accept(this);
			VisitArgs(value);
		}

		protected virtual void VisitArgs(MethodCallValue value)
		{
			IComparisonOperand[] args = value.Args;
			for (int i = 0; i < args.Length; ++i)
			{
				args[i].Accept(this);
			}
		}
	}
}

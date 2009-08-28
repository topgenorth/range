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
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

using Db4objects.Db4o.Linq.Caching;
using Db4objects.Db4o.Linq.Internals;
using Db4objects.Db4o.Query;

namespace Db4objects.Db4o.Linq.Expressions
{
	internal class WhereClauseVisitor : ExpressionQueryBuilder
	{
		private static ICachingStrategy<Expression, IQueryBuilderRecord> _cache =
			new SingleItemCachingStrategy<Expression, IQueryBuilderRecord>(ExpressionEqualityComparer.Instance);

		public WhereClauseVisitor()
		{
		}

		protected override ICachingStrategy<Expression, IQueryBuilderRecord> GetCachingStrategy()
		{
			return _cache;
		}

		protected override void VisitMethodCall(MethodCallExpression m)
		{
			Visit(m.Object);
			VisitExpressionList(m.Arguments);

			if (IsStringMethod(m.Method))
			{
				ProcessStringMethod(m);
				return;
			}
			
			if (IsIListMethod(m.Method))
			{
				ProcessIListMethod(m);
				return;
			}

			CannotOptimize(m);
		}

		private static bool IsStringMethod(MethodInfo method)
		{
			return method.DeclaringType == typeof(string);
		}

		private void ProcessStringMethod(MethodCallExpression call)
		{
			switch (call.Method.Name)
			{
				case "EndsWith":
					RecordConstraintApplication(c => c.EndsWith(true));
					return;
				case "StartsWith":
					RecordConstraintApplication(c => c.StartsWith(true));
					return;
				case "Contains":
					RecordConstraintApplication(c => c.Contains());
					return;
				case "Equals":
					return;
			}

			CannotOptimize(call);
		}

		private void RecordConstraintApplication(Func<IConstraint, IConstraint> application)
		{
			Recorder.Add(ctx => ctx.ApplyConstraint(application));
		}

		private static bool IsIListMethod(MethodInfo method)
		{
			return method.DeclaringType == typeof (IList);
		}

		private void ProcessIListMethod(MethodCallExpression call)
		{
			switch (call.Method.Name)
			{
				case "Contains":
					RecordConstraintApplication(c => c.Contains());
					return;
			}

			CannotOptimize(call);
		}

		private static bool IsComparisonExpression(Expression expression)
		{
			switch (expression.NodeType)
			{
				case ExpressionType.Equal:
				case ExpressionType.NotEqual:
				case ExpressionType.LessThan:
				case ExpressionType.LessThanOrEqual:
				case ExpressionType.GreaterThan:
				case ExpressionType.GreaterThanOrEqual:
					return true;
				default:
					return false;
			}
		}

		private static bool IsConditionalExpression(Expression expression)
		{
			switch (expression.NodeType)
			{
				case ExpressionType.AndAlso:
				case ExpressionType.OrElse:
					return true;
				default:
					return false;
			}
		}

		protected override void VisitBinary(BinaryExpression b)
		{
			if (IsConditionalExpression(b))
			{
				ProcessConditionalExpression(b);
				return;
			}
			else if (IsComparisonExpression(b))
			{
				ProcessPredicateExpression(b);
				return;
			}

			CannotOptimize(b);
		}

		protected override void VisitUnary(UnaryExpression u)
		{
			if (u.NodeType == ExpressionType.Not)
			{
				Visit(u.Operand);
				RecordConstraintApplication(c => c.Not());
				return;
			}
			else if (u.NodeType == ExpressionType.Convert)
			{
				Visit(u.Operand);
				return;
			}

			CannotOptimize(u);
		}

		private void ProcessConditionalExpression(BinaryExpression b)
		{
			Visit(b.Left);
			Visit(b.Right);

			switch (b.NodeType)
			{
				case ExpressionType.AndAlso:
					Recorder.Add(ctx => ctx.ApplyConstraint(c => c.And(ctx.PopConstraint())));
					break;
				case ExpressionType.OrElse:
					Recorder.Add(ctx => ctx.ApplyConstraint(c => c.Or(ctx.PopConstraint())));
					break;
			}
		}

		private void ProcessPredicateExpression(BinaryExpression b)
		{
			if (ParameterReferenceOnLeftSide(b))
			{
				Visit(b.Left);
				Visit(b.Right);
			}
			else
			{
				Visit(b.Right);
				Visit(b.Left);
			}

			ProcessPredicateExpressionOperator(b);
		}

		protected override void VisitMemberAccess(MemberExpression m)
		{
			if (!IsParameterReference(m)) CannotOptimize(m);

			ProcessMemberAccess(m);
		}

		protected override void VisitConstant(ConstantExpression c)
		{
			var value = c.Value;
			Recorder.Add(ctx => ctx.PushConstraint(ctx.CurrentQuery.Constrain(ctx.ResolveValue(value))));
		}

		static bool ParameterReferenceOnLeftSide(BinaryExpression b)
		{
			if (IsParameterReference(b.Left)) return true;
			if (IsParameterReference(b.Right)) return false;

			CannotOptimize(b);
			return false;
		}

		private void ProcessPredicateExpressionOperator(BinaryExpression b)
		{
			switch (b.NodeType)
			{
				case ExpressionType.Equal:
					break;
				case ExpressionType.NotEqual:
					RecordConstraintApplication(c => c.Not());
					break;
				case ExpressionType.LessThan:
					RecordConstraintApplication(c => c.Smaller());
					break;
				case ExpressionType.LessThanOrEqual:
					RecordConstraintApplication(c => c.Smaller().Equal());
					break;
				case ExpressionType.GreaterThan:
					RecordConstraintApplication(c => c.Greater());
					break;
				case ExpressionType.GreaterThanOrEqual:
					RecordConstraintApplication(c => c.Greater().Equal());
					break;
				default:
					CannotOptimize(b);
					break;
			}
		}
	}
}

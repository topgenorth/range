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
using System.Linq.Expressions;
using Db4objects.Db4o.Linq.Expressions;

namespace Db4objects.Db4o.Linq.Expressions
{
	class ExpressionTreeNormalizer : ExpressionTransformer
	{
		protected override System.Linq.Expressions.Expression VisitBinary(System.Linq.Expressions.BinaryExpression b)
		{
			return NormalizeVisualBasicOperator(b) ?? base.VisitBinary(b);
		}

		private Expression NormalizeVisualBasicOperator(BinaryExpression b)
		{
			var call = b.Left as MethodCallExpression;
			if (call == null) return null;
			if (call.Object != null) return null;
			if (call.Method.DeclaringType.FullName != "Microsoft.VisualBasic.CompilerServices.Operators") return null;

			switch (call.Method.Name)
			{
				case "CompareString":
					{
						switch (b.NodeType)
						{
							case ExpressionType.Equal:
								return ToStringEquals(call);
							case ExpressionType.NotEqual:
								return Expression.Not(ToStringEquals(call));
						}

						return null;
					}
			}
			return null;
		}

		private MethodCallExpression ToStringEquals(MethodCallExpression call)
		{
			var stringEquals = typeof(string).GetMethod("Equals", new[] {typeof(string)});
			return Expression.Call(call.Arguments[0], stringEquals, call.Arguments[1]);
		}

		public Expression Normalize(Expression expression)
		{
			return Visit(expression);
		}
	}
}

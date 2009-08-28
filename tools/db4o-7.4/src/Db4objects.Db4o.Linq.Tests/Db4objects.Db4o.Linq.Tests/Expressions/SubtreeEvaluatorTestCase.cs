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
using System.Text;

using Db4objects.Db4o.Linq.Expressions;

using Db4oUnit;
using Db4oUnit.Extensions;

namespace Db4objects.Db4o.Linq.Tests.Expressions
{
	public class SubtreeEvaluatorTestCase : AbstractDb4oTestCase
	{
		class Parameter
		{
			public int ID { get; set; }
		}

		public void TestReplaceInlineCode()
		{
			var exp = CreateExpression(p => p.ID == (12 + 30));

			AssertExpression("(p.ID = 42)", SubtreeEvaluator.Evaluate(exp));
		}

		public void TestReplaceLocalVariable()
		{
			var id = 42;
			var exp = CreateExpression(p => p.ID == id);

			AssertExpression("(p.ID = 42)", SubtreeEvaluator.Evaluate(exp));
		}

		private int _id = 42;

		public void TestReplaceInstanceField()
		{
			var exp = CreateExpression(p => p.ID == _id);

			AssertExpression("(p.ID = 42)", SubtreeEvaluator.Evaluate(exp));
		}

		private static int _sid = 42;

		public void TestReplaceClassField()
		{
			var exp = CreateExpression(p => p.ID == _sid);

			AssertExpression("(p.ID = 42)", SubtreeEvaluator.Evaluate(exp));
		}

		public void TestComplexReplace()
		{
			var exp = CreateExpression(p => (p.ID == 42 || p.ID == p.ID + 12 / 3) && p.ID.ToString() == 42.ToString());

			AssertExpression("(((p.ID = 42) || (p.ID = (p.ID + 4))) && (p.ID.ToString() = \"42\"))", SubtreeEvaluator.Evaluate(exp));
		}

		static void AssertExpression(string expected, Expression expression)
		{
			if (expression.NodeType == ExpressionType.Lambda)
			{
				expression = ((LambdaExpression)expression).Body;
			}

			Assert.AreEqual(expected, expression.ToString());
		}

		static Expression CreateExpression<T>(Expression<Func<Parameter, T>> expression)
		{
			return expression;
		}
	}
}

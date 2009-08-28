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
using System.Linq.Expressions;

using Db4objects.Db4o.Query;

namespace Db4objects.Db4o.Linq.Expressions
{
	public class SubtreeEvaluator : ExpressionTransformer
	{
		private HashSet<Expression> _candidates;

		private SubtreeEvaluator(HashSet<Expression> candidates)
		{
			_candidates = candidates;
		}

		public static Expression Evaluate(Expression expression)
		{
			var nominator = new Nominator(expression, exp => exp.NodeType != ExpressionType.Parameter);

			return new SubtreeEvaluator(nominator.Candidates).Visit(expression);
		}

		protected override Expression Visit(Expression expression)
		{
			if (expression == null) return null;
			if (_candidates.Contains(expression)) return EvaluateCandidate(expression);

			return base.Visit(expression);
		}

		private Expression EvaluateCandidate(Expression expression)
		{
			if (expression.NodeType == ExpressionType.Constant) return expression;

			var evaluator = Expression.Lambda(expression).Compile();
			return Expression.Constant(evaluator.DynamicInvoke(null), expression.Type);
		}

		class Nominator : ExpressionTransformer
		{
			Func<Expression, bool> _predicate;
			HashSet<Expression> _candidates = new HashSet<Expression>();
			bool cannotBeEvaluated;

			public HashSet<Expression> Candidates
			{
				get { return _candidates; }
			}

			public Nominator(Expression expression, Func<Expression, bool> predicate)
			{
				_predicate = predicate;

				Visit(expression);
			}

			private void AddCandidate(Expression expression)
			{
				_candidates.Add(expression);
			}

			// TODO: refactor
			protected override Expression Visit(Expression expression)
			{
				if (expression == null) return null;

				bool saveCannotBeEvaluated = cannotBeEvaluated;
				cannotBeEvaluated = false;

				base.Visit(expression);

				if (cannotBeEvaluated) return expression;

				if (_predicate(expression))
				{
					AddCandidate(expression);
				}
				else
				{
					cannotBeEvaluated = true;
				}

				cannotBeEvaluated |= saveCannotBeEvaluated;

				return expression;
			}
		}
	}
}

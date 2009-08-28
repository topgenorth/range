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
using System.Reflection;
using Db4objects.Db4o.Activation;
using Db4objects.Db4o.NativeQueries.Expr;
using Db4objects.Db4o.NativeQueries.Expr.Cmp;
using Db4objects.Db4o.NativeQueries.Expr.Cmp.Operand;
using Db4objects.Db4o.TA;
using Db4objects.Db4o.Tests.CLI1.NativeQueries;
using Db4objects.Db4o.NativeQueries;
using Db4objects.Db4o.Tests.NativeQueries.Mocks;
using Db4oUnit;

namespace Db4objects.Db4o.Tests.CLI1.Inside.Query
{	
	public class QueryExpressionBuilderTestCase : ITestCase
	{
		public class Item
		{
			public string name;
		}

		public class ActivatableItem : Item, IActivatable
		{
			public void Bind(IActivator activator) {}
			public void Activate(ActivationPurpose purpose){}
			public void Activate(string str) {}
		}

		bool MatchWithActivate(Item item)
		{
			((IActivatable) item).Activate(ActivationPurpose.Read);
			return item.name.StartsWith("foo");
		}

		bool MatchActivateableCall(ActivatableItem item)
		{
			item.Activate(ActivationPurpose.Read);
			return item.name.StartsWith("foo");
		}
		
		bool NotMatchWrongActivateCall(ActivatableItem item)
		{
			item.Activate("foo");
			return item.name.StartsWith("foo");
		}

		public void TestActivateCallsAreIgnored()
		{
			AssertActivatableCallsAreIgnored("MatchWithActivate");
		}

		public void TestActivateCallsOnOverridenActivateMethodsAreIgnored()
		{
			AssertActivatableCallsAreIgnored("MatchActivateableCall");
		}
		
		public void TestWrongActivateCallAreNotIngnored()
		{
			bool exceptionRaised = false;
			try
			{
				AssertActivatableCallsAreIgnored("NotMatchWrongActivateCall");
			}
			catch(UnsupportedPredicateException upe)
			{
				exceptionRaised = true;
			}

			Assert.IsTrue(exceptionRaised, "Unoptimized predicate exception not raised");
		}

		private void AssertActivatableCallsAreIgnored(string methodName)
		{
			IExpression expression = ExpressionFromMethod(methodName);
			Assert.AreEqual(
				new ComparisonExpression(
					NewFieldValue(CandidateFieldRoot.Instance, "name", typeof(string)),
					new ConstValue("foo"),
					ComparisonOperator.StartsWith),
				expression);
		}

		public void TestNameEqualsTo()
		{
			IExpression expression = ExpressionFromPredicate(typeof(NameEqualsTo));
			Assert.AreEqual(
				new ComparisonExpression(
				NewFieldValue(CandidateFieldRoot.Instance, "name", typeof(string)),
				NewFieldValue(PredicateFieldRoot.Instance, "_name", typeof(string)),
				ComparisonOperator.ValueEquality),
				expression);
		}

		public void TestHasPreviousWithPrevious()
		{
			// candidate.HasPrevious && candidate.Previous.HasPrevious
			IExpression expression = ExpressionFromPredicate(typeof(HasPreviousWithPrevious));
			IExpression expected = 
				new AndExpression(
				new NotExpression(
				new ComparisonExpression(
				NewFieldValue(CandidateFieldRoot.Instance, "previous", typeof(Data)), 
				new ConstValue(null),
				ComparisonOperator.ValueEquality)),
				new NotExpression(
				new ComparisonExpression(
				NewFieldValue(
					NewFieldValue(CandidateFieldRoot.Instance, "previous", typeof(Data)),
					"previous",
					typeof(Data)),
				new ConstValue(null),
				ComparisonOperator.ValueEquality)));

			Assert.AreEqual(expected, expression);
		}
		
		enum MessagePriority
		{
			None,
			Low,
			Normal,
			High
		}
		
		class Message
		{
			private MessagePriority _priority;

			public MessagePriority Priority
			{
				get { return _priority;  }
				set { _priority = value;  }
			}
		}
		
		private bool MatchEnumConstrain(Message message)
		{
			return message.Priority == MessagePriority.High;
		}
		
		public void TestQueryWithEnumConstrain()
		{
			IExpression expression = ExpressionFromMethod("MatchEnumConstrain");
			IExpression expected = new ComparisonExpression(
				NewFieldValue(CandidateFieldRoot.Instance, "_priority", typeof(MessagePriority)),
				new ConstValue(MessagePriority.High),
				ComparisonOperator.ValueEquality);
			Assert.AreEqual(expected, expression);
		}
		
		private IExpression ExpressionFromMethod(string methodName)
		{
			return new QueryExpressionBuilder().FromMethod(GetType().GetMethod(methodName, BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Static));
		}

		private IExpression ExpressionFromPredicate(Type type)
		{
			// XXX: move knowledge about IMethodDefinition to QueryExpressionBuilder
			return (new QueryExpressionBuilder()).FromMethod(type.GetMethod("Match"));
		}

		private FieldValue NewFieldValue(IComparisonOperandAnchor anchor, string name, Type type)
		{
			return new FieldValue(anchor,
				new MockFieldRef(name,
					new MockTypeRef(type)));
		}

	}
}

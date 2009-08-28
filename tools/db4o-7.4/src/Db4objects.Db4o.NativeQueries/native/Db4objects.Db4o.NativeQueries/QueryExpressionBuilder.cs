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
using Db4objects.Db4o.Activation;
using Db4objects.Db4o.Instrumentation.Cecil;
using Db4objects.Db4o.TA;

namespace Db4objects.Db4o.NativeQueries
{
	using System;
	using System.Collections;
	using System.Reflection;

	using Mono.Cecil;

	using Cecil.FlowAnalysis;
	using Cecil.FlowAnalysis.ActionFlow;
	using Cecil.FlowAnalysis.CodeStructure;
	using Ast = Cecil.FlowAnalysis.CodeStructure;

	using Db4objects.Db4o.NativeQueries.Expr;
	using Db4objects.Db4o.NativeQueries.Expr.Cmp;
	using Db4objects.Db4o.NativeQueries.Expr.Cmp.Operand;
	using NQExpression = Db4objects.Db4o.NativeQueries.Expr.IExpression;
	using Db4objects.Db4o.Internal.Query;

	/// <summary>
	/// Build a Db4objects.Db4o.Nativequery.Expr tree out of
	/// a predicate method definition.
	/// </summary>
	public class QueryExpressionBuilder
	{
		protected static ICachingStrategy _assemblyCachingStrategy = new SingleItemCachingStrategy();

		protected static ICachingStrategy _expressionCachingStrategy = new SingleItemCachingStrategy();

		public static ICachingStrategy AssemblyCachingStrategy
		{
			get
			{
				return _assemblyCachingStrategy;
			}

			set
			{
				if (null == value) throw new ArgumentNullException("AssemblyCachingStrategy");
				_assemblyCachingStrategy = value;
			}
		}

		public static ICachingStrategy ExpressionCachingStrategy
		{
			get
			{
				return _expressionCachingStrategy;
			}

			set
			{
				if (null == value) throw new ArgumentNullException("ExpressionCachingStrategy");
				_expressionCachingStrategy = value;
			}
		}

		public NQExpression FromMethod(System.Reflection.MethodBase method)
		{
			if (method == null) throw new ArgumentNullException("method");
			
			NQExpression e = GetCachedExpression(method);
			if (e != null) return e;

			MethodDefinition methodDef = GetMethodDefinition(method);
			e = AdjustBoxedValueTypes(FromMethodDefinition(methodDef));
			CacheExpression(method, e);
			
			return e;
		}

		private static void CacheExpression(MethodBase method, NQExpression e)
		{
			_expressionCachingStrategy.Add(method, e);
		}

		private static NQExpression GetCachedExpression(MethodBase method)
		{
			return (NQExpression)_expressionCachingStrategy.Get(method);
		}

		private static MethodDefinition GetMethodDefinition(MethodBase method)
		{
			string location = GetAssemblyLocation(method);
			AssemblyDefinition assembly = GetAssembly(location);
			TypeDefinition type = FindTypeDefinition(assembly.MainModule, method.DeclaringType);
			if (null == type) UnsupportedPredicate(string.Format("Unable to load type '{0}' from assembly '{1}'", method.DeclaringType.FullName, location));
			MethodDefinition methodDef = type.Methods.GetMethod(method.Name, GetParameterTypes(method));
			if (null == methodDef) UnsupportedPredicate(string.Format("Unable to load the definition of '{0}' from assembly '{1}'", method, location));
			return methodDef;
		}

		private NQExpression AdjustBoxedValueTypes(NQExpression expression)
		{
			expression.Accept(new BoxedValueTypeProcessor());
			return expression;
		}

		private static AssemblyDefinition GetAssembly(string location)
		{
			AssemblyDefinition assembly = (AssemblyDefinition)_assemblyCachingStrategy.Get(location);
			if (null == assembly)
			{
				assembly = AssemblyFactory.GetAssembly(location);
				_assemblyCachingStrategy.Add(location, assembly);
			}
			return assembly;
		}

		private static Type[] GetParameterTypes(MethodBase method)
		{
			ParameterInfo[] parameters = method.GetParameters();
			Type[] types = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; ++i)
			{
				types[i] = parameters[i].ParameterType;
			}
			return types;
		}

		private static TypeDefinition FindTypeDefinition(ModuleDefinition module, Type type)
		{
			return IsNested(type)
				? FindNestedTypeDefinition(module, type)
				: FindTypeDefinition(module, type.FullName);
		}

		private static bool IsNested(Type type)
		{
			return type.IsNestedPublic || type.IsNestedPrivate || type.IsNestedAssembly;
		}

		private static TypeDefinition FindNestedTypeDefinition(ModuleDefinition module, Type type)
		{
			foreach (TypeDefinition td in FindTypeDefinition(module, type.DeclaringType).NestedTypes)
			{
				if (td.Name == type.Name) return td;
			}
			return null;
		}

		private static TypeDefinition FindTypeDefinition(ModuleDefinition module, string fullName)
		{
			return module.Types[fullName];
		}

		private static string GetAssemblyLocation(MethodBase method)
		{
			return method.DeclaringType.Module.FullyQualifiedName;
		}

		public static NQExpression FromMethodDefinition(MethodDefinition method)
		{
			ValidatePredicateMethodDefinition(method);

			Expression expression = GetQueryExpression(method);
			if (null == expression) UnsupportedPredicate("No expression found.");
			
			Visitor visitor = new Visitor(method);
			expression.Accept(visitor);
			return visitor.Expression;
		}

		private static void ValidatePredicateMethodDefinition(MethodDefinition method)
		{
			if (method == null)
				throw new ArgumentNullException("method");
			if (1 != method.Parameters.Count)
				UnsupportedPredicate("A predicate must take a single argument.");
			if (0 != method.Body.ExceptionHandlers.Count)
				UnsupportedPredicate("A predicate can not contain exception handlers.");
			if (method.DeclaringType.Module.Import(typeof(bool)) != method.ReturnType.ReturnType)
				UnsupportedPredicate("A predicate must have a boolean return type.");
		}

		private static Expression GetQueryExpression(MethodDefinition method)
		{
			ActionFlowGraph afg = FlowGraphFactory.CreateActionFlowGraph(FlowGraphFactory.CreateControlFlowGraph(method));
			return GetQueryExpression(afg);
		}

		private static void UnsupportedPredicate(string reason)
		{
			throw new UnsupportedPredicateException(reason);
		}

		private static void UnsupportedExpression(Expression node)
		{
			UnsupportedPredicate("Unsupported expression: " + ExpressionPrinter.ToString(node));
		}

		private static Expression GetQueryExpression(ActionFlowGraph afg)
		{
			Hashtable variables = new Hashtable();
			ActionBlock block = afg.Blocks[0];
			while (block != null)
			{
				switch (block.ActionType)
				{
					case ActionType.Invoke:
						InvokeActionBlock invokeBlock = (InvokeActionBlock)block;
						MethodInvocationExpression invocation = invokeBlock.Expression;
						if (IsActivateInvocation(invocation))
						{
							block = invokeBlock.Next;
							break;
						}
						UnsupportedExpression(invocation);
						break;

					case ActionType.ConditionalBranch:
						UnsupportedPredicate("Conditional blocks are not supported.");
						break;

					case ActionType.Branch:
						block = ((BranchActionBlock)block).Target;
						break;

					case ActionType.Assign:
						{
							AssignActionBlock assignBlock = (AssignActionBlock)block;
							AssignExpression assign = assignBlock.AssignExpression;
							VariableReferenceExpression variable = assign.Target as VariableReferenceExpression;
							if (null == variable)
							{
								UnsupportedExpression(assign);
							}
							else
							{
								if (variables.Contains(variable.Variable.Index))
									UnsupportedExpression(assign.Expression);
								variables.Add(variable.Variable.Index, assign.Expression);
								block = assignBlock.Next;
							}
							break;
						}

					case ActionType.Return:
						{
							Expression expression = ((ReturnActionBlock)block).Expression;
							VariableReferenceExpression variable = expression as VariableReferenceExpression;
							return null == variable
								? expression
								: (Expression)variables[variable.Variable.Index];
						}
				}
			}
			return null;
		}

		private static bool IsActivateInvocation(MethodInvocationExpression invocation)
		{
			MethodReferenceExpression methodRef = invocation.Target as MethodReferenceExpression;
			if (null == methodRef) return false;
			return IsActivateMethod(methodRef.Method);
		}

		private static bool IsActivateMethod(MethodReference method)
		{
			if (method.Name != "Activate") return false;
			return method.DeclaringType.FullName == typeof(IActivatable).FullName ||
				   IsOverridenActivateMethod(method);
		}

		private static bool IsOverridenActivateMethod(MethodReference method)
		{
			TypeDefinition declaringType = FindTypeDefinition(method.DeclaringType.Module, method.DeclaringType.FullName);
			if (!DeclaringTypeImplementsIActivatable(declaringType)) return false;
			if (method.Parameters.Count != 1 || 
				method.Parameters[0].ParameterType.FullName != typeof(ActivationPurpose).FullName) return false;

			return true;
		}

		private static bool DeclaringTypeImplementsIActivatable(TypeDefinition type)
		{
			foreach (TypeReference itf in type.Interfaces)
			{
				if (itf.FullName == typeof (IActivatable).FullName)
				{
					return true;
				}
			}

			return false;
		}

		class Visitor : AbstractCodeStructureVisitor
		{
			object _current;
			private int _insideCandidate = 0;
			Hashtable _assemblies = new Hashtable();
			IList _methodDefinitionStack = new ArrayList();
			private readonly CecilReferenceProvider _referenceProvider;

			public Visitor(MethodDefinition topLevelMethod)
			{
				EnterMethodDefinition(topLevelMethod);
				AssemblyDefinition assembly = topLevelMethod.DeclaringType.Module.Assembly;
				RegisterAssembly(assembly);
				_referenceProvider = CecilReferenceProvider.ForModule(assembly.MainModule);
			}

			private void EnterMethodDefinition(MethodDefinition method)
			{
				_methodDefinitionStack.Add(method);
			}

			private void LeaveMethodDefinition(MethodDefinition method)
			{
				int lastIndex = _methodDefinitionStack.Count - 1;
				object popped = _methodDefinitionStack[lastIndex];
				System.Diagnostics.Debug.Assert(method == popped);
				_methodDefinitionStack.RemoveAt(lastIndex);
			}

			/// <summary>
			/// Registers an assembly so it can be looked up by its assembly name
			/// string later.
			/// </summary>
			/// <param name="assembly"></param>
			private void RegisterAssembly(AssemblyDefinition assembly)
			{
				_assemblies.Add(assembly.Name.FullName, assembly);
			}

			private AssemblyDefinition LookupAssembly(string fullName)
			{
				return (AssemblyDefinition)_assemblies[fullName];
			}

			public NQExpression Expression
			{
				get
				{
				    ConstValue value = _current as ConstValue;
				    if (null != value)
				    {
                        return ToNQExpression(value);
				    }
				    return (NQExpression)_current;
				}
			}

		    private NQExpression ToNQExpression(ConstValue value)
		    {
                if (IsTrue(value.Value())) return BoolConstExpression.True;
		        return BoolConstExpression.False;
		    }

		    private bool IsTrue(object o)
		    {
                return (o as IConvertible).ToBoolean(null);
		    }

		    private bool InsideCandidate
			{
				get { return _insideCandidate > 0; }
			}

			public override void Visit(AssignExpression node)
			{
				UnsupportedExpression(node);
			}

			public override void Visit(VariableReferenceExpression node)
			{
				UnsupportedExpression(node);
			}

			public override void Visit(ArgumentReferenceExpression node)
			{
				UnsupportedExpression(node);
			}

			public override void Visit(UnaryExpression node)
			{
				switch (node.Operator)
				{
					case UnaryOperator.Not:
						Visit(node.Operand);
						Negate();
						break;

					default:
						UnsupportedExpression(node);
						break;
				}
			}

			public override void Visit(Ast.BinaryExpression node)
			{
				switch (node.Operator)
				{
					case BinaryOperator.ValueEquality:
						PushComparison(node.Left, node.Right, ComparisonOperator.ValueEquality);
						break;

					case BinaryOperator.ValueInequality:
						PushComparison(node.Left, node.Right, ComparisonOperator.ValueEquality);
						Negate();
						break;

					case BinaryOperator.LessThan:
						PushComparison(node.Left, node.Right, ComparisonOperator.Smaller);
						break;

					case BinaryOperator.GreaterThan:
						PushComparison(node.Left, node.Right, ComparisonOperator.Greater);
						break;

					case BinaryOperator.GreaterThanOrEqual:
						PushComparison(node.Left, node.Right, ComparisonOperator.Smaller);
						Negate();
						break;

					case BinaryOperator.LessThanOrEqual:
						PushComparison(node.Left, node.Right, ComparisonOperator.Greater);
						Negate();
						break;

					case BinaryOperator.LogicalOr:
						Push(new OrExpression(Convert(node.Left), Convert(node.Right)));
						break;

					case BinaryOperator.LogicalAnd:
						Push(new AndExpression(Convert(node.Left), Convert(node.Right)));
						break;

					default:
						UnsupportedExpression(node);
						break;
				}
			}

			private void Negate()
			{
				NQExpression top = (NQExpression)Pop();
				NotExpression topNot = top as NotExpression;
				if (topNot != null)
				{
					Push(topNot.Expr());
					return;
				}
				Push(new NotExpression(top));
			}

			private void PushComparison(Expression lhs, Expression rhs, ComparisonOperator op)
			{
				Visit(lhs);
				object left = Pop();
				Visit(rhs);
				object right = Pop();

				bool areOperandsSwapped = IsCandidateFieldValue(right);
				if (areOperandsSwapped)
				{
					object temp = left;
					left = right;
					right = temp;
				}

				AssertType(left, typeof(FieldValue), lhs);
				AssertType(right, typeof(IComparisonOperand), rhs);

				Push(new ComparisonExpression((FieldValue)left, (IComparisonOperand)right, op));

				if (areOperandsSwapped && !op.IsSymmetric())
				{
					Negate();
				}
			}

			private bool IsCandidateFieldValue(object o)
			{
				FieldValue value = o as FieldValue;
				if (value == null) return false;
				return value.Root() is CandidateFieldRoot;
			}

			public override void Visit(MethodInvocationExpression node)
			{
				MethodReferenceExpression methodRef = node.Target as MethodReferenceExpression;
				if (null == methodRef)
					UnsupportedExpression(node);

				MethodReference method = methodRef.Method;
				if (IsOperator(method))
				{
					ProcessOperatorMethodInvocation(node, method);
					return;
				}

				if (IsSystemString(method.DeclaringType))
				{
					ProcessStringMethod(node, methodRef);
					return;
				}

				ProcessRegularMethodInvocation(node, methodRef);
			}

			private static bool IsSystemString(TypeReference type)
			{
				return type.FullName == "System.String";
			}

			private void ProcessStringMethod(MethodInvocationExpression node, MethodReferenceExpression methodRef)
			{
				MethodReference method = methodRef.Method;

				if (method.Parameters.Count != 1
					|| !IsSystemString(method.Parameters[0].ParameterType))
				{
					UnsupportedExpression(methodRef);
				}

				switch (method.Name)
				{
					case "Contains":
						PushComparison(methodRef.Target, node.Arguments[0], ComparisonOperator.Contains);
						break;

					case "StartsWith":
						PushComparison(methodRef.Target, node.Arguments[0], ComparisonOperator.StartsWith);
						break;

					case "EndsWith":
						PushComparison(methodRef.Target, node.Arguments[0], ComparisonOperator.EndsWith);
						break;

					case "Equals":
						PushComparison(methodRef.Target, node.Arguments[0], ComparisonOperator.ValueEquality);
						break;

					default:
						UnsupportedExpression(methodRef);
						break;
				}
			}

			private void ProcessRegularMethodInvocation(MethodInvocationExpression node, MethodReferenceExpression methodRef)
			{
				if (node.Arguments.Count != 0)
					UnsupportedExpression(node);

				Expression target = methodRef.Target;
				switch (target.CodeElementType)
				{
					case CodeElementType.ThisReferenceExpression:
						if (!InsideCandidate)
							UnsupportedExpression(node);
						ProcessCandidateMethodInvocation(node, methodRef);
						break;

					case CodeElementType.ArgumentReferenceExpression:
						ProcessCandidateMethodInvocation(node, methodRef);
						break;

					default:
						Push(ToFieldValue(target));
						ProcessCandidateMethodInvocation(node, methodRef);
						break;
				}
			}

			private void ProcessOperatorMethodInvocation(MethodInvocationExpression node, MethodReference method)
			{
				switch (method.Name)
				{
					case "op_Equality":
						PushComparison(node.Arguments[0], node.Arguments[1], ComparisonOperator.ValueEquality);
						break;

					case "op_Inequality":
						PushComparison(node.Arguments[0], node.Arguments[1], ComparisonOperator.ValueEquality);
						Negate();
						break;

					// XXX: check if the operations below are really supported for the
					// data types in question
					case "op_GreaterThanOrEqual":
						PushComparison(node.Arguments[0], node.Arguments[1], ComparisonOperator.Smaller);
						Negate();
						break;

					case "op_LessThanOrEqual":
						PushComparison(node.Arguments[0], node.Arguments[1], ComparisonOperator.Greater);
						Negate();
						break;

					case "op_LessThan":
						PushComparison(node.Arguments[0], node.Arguments[1], ComparisonOperator.Smaller);
						break;

					case "op_GreaterThan":
						PushComparison(node.Arguments[0], node.Arguments[1], ComparisonOperator.Greater);
						break;

					default:
						UnsupportedExpression(node);
						break;
				}
			}

			private void ProcessCandidateMethodInvocation(MethodInvocationExpression node, MethodReferenceExpression methodRef)
			{
				MethodDefinition method = GetMethodDefinition(methodRef);
				if (null == method)
					UnsupportedExpression(node);

				AssertMethodCanBeVisited(node, method);

				Expression expression = GetQueryExpression(method);
				if (null == expression)
					UnsupportedExpression(node);

				EnterCandidateMethod(method);
				try
				{
					Visit(expression);
				}
				finally
				{
					LeaveCandidateMethod(method);
				}
			}

			private void AssertMethodCanBeVisited(MethodInvocationExpression node, MethodDefinition method)
			{
				if (_methodDefinitionStack.Contains(method))
					UnsupportedExpression(node);
			}

			private MethodDefinition GetMethodDefinition(MethodReferenceExpression methodRef)
			{
				MethodDefinition definition = methodRef.Method as MethodDefinition;
				return definition != null
					? definition
					: LoadExternalMethodDefinition(methodRef);
			}

			private MethodDefinition LoadExternalMethodDefinition(MethodReferenceExpression methodRef)
			{
				MethodReference method = methodRef.Method;
				AssemblyDefinition assemblyDef = GetContainingAssembly(method.DeclaringType);
				TypeDefinition type = assemblyDef.MainModule.Types[method.DeclaringType.FullName];
				return type.Methods.GetMethod(method.Name, method.Parameters);
			}

			private AssemblyDefinition GetContainingAssembly(TypeReference type)
			{
				AssemblyNameReference scope = (AssemblyNameReference)type.Scope;
				string assemblyName = scope.FullName;
				AssemblyDefinition definition = LookupAssembly(assemblyName);
				if (null == definition)
				{
					Assembly assembly = Assembly.Load(assemblyName);
					string location = assembly.GetType(type.FullName).Module.FullyQualifiedName;
					definition = QueryExpressionBuilder.GetAssembly(location);
					RegisterAssembly(definition);
				}
				return definition;
			}

			private void EnterCandidateMethod(MethodDefinition method)
			{
				EnterMethodDefinition(method);
				++_insideCandidate;
			}

			private void LeaveCandidateMethod(MethodDefinition method)
			{
				--_insideCandidate;
				LeaveMethodDefinition(method);
			}

			private static bool IsOperator(MethodReference method)
			{
				return !method.HasThis && method.Name.StartsWith("op_") && 2 == method.Parameters.Count;
			}

			public override void Visit(FieldReferenceExpression node)
			{
				Expression target = node.Target;
				switch (target.CodeElementType)
				{
					case CodeElementType.ArgumentReferenceExpression:
						//IArgumentReferenceExpression arg = (IArgumentReferenceExpression)target;
						PushFieldValue(CandidateFieldRoot.Instance, node.Field);
						break;

					case CodeElementType.ThisReferenceExpression:
						if (InsideCandidate)
						{
							if (_current != null)
							{
								FieldValue current = PopFieldValue(node);
								PushFieldValue(current, node.Field);
							}
							else
							{
								PushFieldValue(CandidateFieldRoot.Instance, node.Field);
							}
						}
						else
						{
							PushFieldValue(PredicateFieldRoot.Instance, node.Field);
						}
						break;

					case CodeElementType.MethodInvocationExpression:
					case CodeElementType.FieldReferenceExpression:
						FieldValue value = ToFieldValue(target);
						PushFieldValue(value, node.Field);
						break;

					default:
						UnsupportedExpression(node);
						break;
				}
			}

			private void PushFieldValue(IComparisonOperandAnchor value, FieldReference field)
			{	
				Push(new FieldValue(value, _referenceProvider.ForCecilField(field)));
			}

			public override void Visit(LiteralExpression node)
			{
				Push(new ConstValue(node.Value));
			}

			NQExpression Convert(Expression node)
			{
				return ReconstructNullComparisonIfNecessary(node);
			}

			private NQExpression ReconstructNullComparisonIfNecessary(Expression node)
			{
				Visit(node);

				object top = Pop();
				FieldValue fieldValue = top as FieldValue;
				if (fieldValue == null)
				{
					AssertType(top, typeof(NQExpression), node);
					return (NQExpression)top;
				}

				return
					new NotExpression(
						new ComparisonExpression(
							fieldValue,
							new ConstValue(null),
							ComparisonOperator.ValueEquality));
			}

			FieldValue ToFieldValue(Expression node)
			{
				Visit(node);
				return PopFieldValue(node);
			}

			private FieldValue PopFieldValue(Expression node)
			{
				return (FieldValue)Pop(node, typeof(FieldValue));
			}

			void Push(object value)
			{
				Assert(_current == null, "expression stack must be empty before Push");
				_current = value;
			}

			object Pop(Expression node, System.Type expectedType)
			{
				object value = Pop();
				AssertType(value, expectedType, node);
				return value;
			}

			private static void AssertType(object value, Type expectedType, Expression sourceExpression)
			{
				Type actualType = value.GetType();
				if (!expectedType.IsAssignableFrom(actualType))
				{
					UnsupportedPredicate(
						string.Format("Unsupported expression: {0}. Unexpected type on stack. Expected: {1}, Got: {2}.",
									  ExpressionPrinter.ToString(sourceExpression), expectedType, actualType));
				}
			}

			object Pop()
			{
				Assert(_current != null, "expression stack is empty");
				object value = _current;
				_current = null;
				return value;
			}

			private void Assert(bool condition, string message)
			{
				System.Diagnostics.Debug.Assert(condition, message);
			}
		}
	}

	internal class BoxedValueTypeProcessor : TraversingExpressionVisitor
	{
		override public void Visit(ComparisonExpression expression)
		{
			TypeReference fieldType = GetFieldType(expression.Left());
			if (!fieldType.IsValueType) return;
			
			ConstValue constValue = expression.Right() as ConstValue;
			if (constValue == null) return;

			AdjustConstValue(fieldType, constValue);
		}

		private static TypeReference GetFieldType(FieldValue field)
		{
			return ((CecilFieldRef) field.Field).FieldType;
		}

		private void AdjustConstValue(TypeReference typeRef, ConstValue constValue)
		{
			object value = constValue.Value();
			if (!value.GetType().IsValueType) return;
			
			System.Type type = ResolveTypeReference(typeRef);
			if (!type.IsEnum || value.GetType() == type) return;

			constValue.Value(Enum.ToObject(type, value));
		}

		private static Type ResolveTypeReference(TypeReference typeRef)
		{
			Assembly assembly = LoadAssembly(typeRef.Scope);
			return assembly.GetType(typeRef.FullName.Replace('/', '+'), true);
		}

		private static Assembly LoadAssembly(IMetadataScope scope)
		{
			AssemblyNameReference nameRef = scope as AssemblyNameReference;
			if (null != nameRef) return Assembly.Load(nameRef.FullName);
			ModuleDefinition moduleDef = scope as ModuleDefinition;
			return LoadAssembly(moduleDef.Assembly.Name);
		}
	}
}
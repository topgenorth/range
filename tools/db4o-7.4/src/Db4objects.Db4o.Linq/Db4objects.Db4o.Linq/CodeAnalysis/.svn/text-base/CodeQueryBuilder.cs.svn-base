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

using Db4objects.Db4o;
using Db4objects.Db4o.Linq;
using Db4objects.Db4o.Query;

using Cecil.FlowAnalysis;
using Cecil.FlowAnalysis.ActionFlow;
using Cecil.FlowAnalysis.CodeStructure;
using Cecil.FlowAnalysis.Utilities;

using Mono.Cecil;
using Mono.Cecil.Cil;
using Db4objects.Db4o.Linq.Internals;

namespace Db4objects.Db4o.Linq.CodeAnalysis
{
	internal class CodeQueryBuilder : AbstractCodeStructureVisitor
	{
		private QueryBuilderRecorder _recorder;

		public CodeQueryBuilder(QueryBuilderRecorder recorder)
		{
			_recorder = recorder;
		}

		public override void Visit(ArgumentReferenceExpression node)
		{
			CannotOptimize(node);
		}

		public override void Visit(AssignExpression node)
		{
			CannotOptimize(node);
		}

		public override void Visit(BinaryExpression node)
		{
			CannotOptimize(node);
		}

		public override void Visit(CastExpression node)
		{
			CannotOptimize(node);
		}

		public override void Visit(FieldReferenceExpression node)
		{
            Type descendingEnumType = ResolveDescendingEnumType(node);
            
            _recorder.Add(
                ctx => 
                    {
                        ctx.PushQuery(ctx.RootQuery.Descend(node.Field.Name));
                        ctx.PushDescendigFieldEnumType(descendingEnumType);
                    });
		}

		public override void Visit(LiteralExpression node)
		{
			CannotOptimize(node);
		}

		public override void Visit(MethodInvocationExpression node)
		{
			CannotOptimize(node);
		}

		public override void Visit(MethodReferenceExpression node)
		{
			CannotOptimize(node);
		}

		public override void Visit(PropertyReferenceExpression node)
		{
			CannotOptimize(node);
		}

		public override void Visit(ThisReferenceExpression node)
		{
			CannotOptimize(node);
		}

		public override void Visit(UnaryExpression node)
		{
			CannotOptimize(node);
		}

		public override void Visit(VariableReferenceExpression node)
		{
			CannotOptimize(node);
		}

		private static void CannotOptimize(Expression expression)
		{
			throw new QueryOptimizationException(ExpressionPrinter.ToString(expression));
		}

        private Type ResolveDescendingEnumType(FieldReferenceExpression node)
        {
            TypeDefinition type = ResolveType(node);
            if (type != null && type.IsEnum)
            {
                return Type.GetType((type.FullName + "," + type.Module.Assembly.Name.FullName).Replace('/', '+'));
            }

            return null;
        }

        private static TypeDefinition ResolveType(FieldReferenceExpression node)
        {
            TypeReference t = node.Field.FieldType;
            TypeDefinition typeDefinition = Resolve(t.Module, t.FullName);
            if (typeDefinition != null) return typeDefinition;

            IAssemblyResolver resolver = t.Module.Assembly.Resolver;
            foreach (AssemblyNameReference assembyReference in t.Module.AssemblyReferences)
            {
                foreach (ModuleDefinition module in resolver.Resolve(assembyReference).Modules)
                {
                    typeDefinition = Resolve(module, t.FullName);
                    if (typeDefinition != null) return typeDefinition;
                }
            }

            return null;
        }

        private static TypeDefinition Resolve(ModuleDefinition module, string typeName)
        {
            return module.Types[typeName];
        }
	}
}

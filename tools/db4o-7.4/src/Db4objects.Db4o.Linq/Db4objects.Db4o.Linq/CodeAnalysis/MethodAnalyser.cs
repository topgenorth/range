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
using System.Reflection;

using Db4objects.Db4o;
using Db4objects.Db4o.Linq.Caching;
using Db4objects.Db4o.Query;

using Cecil.FlowAnalysis;
using Cecil.FlowAnalysis.ActionFlow;
using Cecil.FlowAnalysis.CodeStructure;

using Mono.Cecil;
using Db4objects.Db4o.Linq.Internals;

namespace Db4objects.Db4o.Linq.CodeAnalysis
{
	internal class MethodAnalyser
	{
		private static ICachingStrategy<MethodDefinition, ActionFlowGraph> _graphCache =
			new SingleItemCachingStrategy<MethodDefinition, ActionFlowGraph>();

		private ActionFlowGraph _graph;
		private Expression _queryExpression;
		private object[] _parameters;

		public bool IsFieldAccess
		{
			get { return _queryExpression != null && _queryExpression is FieldReferenceExpression; }
		}

		private MethodAnalyser(ActionFlowGraph graph, object[] parameters)
		{
			if (graph == null) throw new ArgumentNullException("graph");
			if (parameters == null) throw new ArgumentNullException("parameters");

			_graph = graph;
			_parameters = parameters;
			_queryExpression = QueryExpressionFinder.FindIn(graph);
		}

		public void AugmentQuery(QueryBuilderRecorder recorder)
		{
			if (_queryExpression == null) throw new QueryOptimizationException("No query expression");

			_queryExpression.Accept(new CodeQueryBuilder(recorder));
		}

		public static MethodAnalyser FromMethod(MethodInfo info, object[] parameters)
		{
			return GetAnalyserFor(ResolveMethod(info), parameters);
		}

		private static MethodDefinition ResolveMethod(MethodInfo info)
		{
			if (info == null) throw new ArgumentNullException("info");

			var method = MetadataResolver.Instance.ResolveMethod(info);

			if (method == null) throw new QueryOptimizationException(
				string.Format("Cannot resolve method {0}", info));

			return method;
		}

		private static MethodAnalyser GetAnalyserFor(MethodDefinition method, object[] parameters)
		{
			var graph = GetCachedGraph(method);
			if (graph != null) return new MethodAnalyser(graph, parameters);

			graph = CreateActionFlowGraph(method);

			CacheGraph(method, graph);

			return new MethodAnalyser(graph, parameters);
		}

		private static ActionFlowGraph GetCachedGraph(MethodDefinition method)
		{
			return _graphCache.Get(method);
		}

		private static void CacheGraph(MethodDefinition method, ActionFlowGraph graph)
		{
			_graphCache.Add(method, graph);
		}

		private static ActionFlowGraph CreateActionFlowGraph(MethodDefinition method)
		{
			return FlowGraphFactory.CreateActionFlowGraph(FlowGraphFactory.CreateControlFlowGraph(method));
		}
	}
}

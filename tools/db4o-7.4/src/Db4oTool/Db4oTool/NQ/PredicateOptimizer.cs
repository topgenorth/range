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
using System.Diagnostics;
using Db4objects.Db4o.NativeQueries.Expr;
using Mono.Cecil;

namespace Db4oTool.NQ
{
	public class PredicateOptimizer : AbstractOptimizer
	{
		int _predicateCount;

		protected override void BeforeAssemblyProcessing()
		{
			_predicateCount = 0;
		}
		
		protected override void  AfterAssemblyProcessing()
		{
			string format = _predicateCount == 1
			                	? "{0} predicate class processed."
			                	: "{0} predicate classes processed.";
			TraceInfo(format, _predicateCount);
		}
		
		protected override void ProcessType(TypeDefinition type)
		{
			if (IsPredicateClass(type))
			{
				InstrumentPredicateClass(type);
			}
		}

		private void InstrumentPredicateClass(TypeDefinition type)
		{
			++_predicateCount;
			
			MethodDefinition match = GetMatchMethod(type);
			IExpression e = GetExpression(match);
			if (null == e) return;

			OptimizePredicate(type, match, e);
		}

		private static MethodDefinition GetMatchMethod(TypeDefinition type)
		{
			MethodDefinition[] methods = type.Methods.GetMethod("Match");
			Debug.Assert(1 == methods.Length);
			return methods[0];
		}

		private static bool IsPredicateClass(TypeReference typeRef)
		{
			TypeDefinition type = typeRef as TypeDefinition;
			if (null == type) return false;
			TypeReference baseType = type.BaseType;
			if (null == baseType) return false;
			if (typeof(Db4objects.Db4o.Query.Predicate).FullName == baseType.FullName) return true;
			return IsPredicateClass(baseType);
		}
	}
}

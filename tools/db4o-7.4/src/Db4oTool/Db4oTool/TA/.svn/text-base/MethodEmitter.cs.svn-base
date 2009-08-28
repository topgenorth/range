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
using Db4oTool.Core;
using Mono.Cecil;
using MethodAttributes=Mono.Cecil.MethodAttributes;

namespace Db4oTool.TA
{
	internal class MethodEmitter
	{
		protected FieldReference _activatorField;
		protected InstrumentationContext _context;

		public MethodEmitter(InstrumentationContext context, FieldReference field)
		{	
			_context = context;
			_activatorField = FieldReferenceFor(field);
		}

		private static FieldReference FieldReferenceFor(FieldReference field)
		{
			if (!IsGeneric(field.DeclaringType)) return field;
			return new FieldReference(field.Name, GenericReferenceFor(field.DeclaringType), field.FieldType);
		}

		private static bool IsGeneric(TypeReference type)
		{
			return type.GenericParameters.Count > 0;
		}

		private static TypeReference GenericReferenceFor(TypeReference type)
		{
			GenericInstanceType instance = new GenericInstanceType(type);
			foreach (GenericParameter param in type.GenericParameters)
			{
				instance.GenericArguments.Add(param);
			}
			return instance;
		}

		protected MethodDefinition NewExplicitMethod(MethodInfo method)
		{
			MethodAttributes attributes = MethodAttributes.SpecialName|MethodAttributes.Private|MethodAttributes.Virtual;
			MethodDefinition definition = new MethodDefinition(method.DeclaringType.FullName + "." + method.Name, attributes, Import(method.ReturnType));
			int parameterIndex = 0;
			foreach (ParameterInfo pi in method.GetParameters())
			{
				definition.Parameters.Add(new ParameterDefinition(pi.Name, ++parameterIndex, Mono.Cecil.ParameterAttributes.None, Import(pi.ParameterType)));
			}
			definition.Overrides.Add(_context.Import(method));
			return definition;
		}

		protected TypeReference Import(Type type)
		{
			return _context.Import(type);
		}
	}
}
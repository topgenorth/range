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
using System.IO;
using System.Reflection;
using Db4objects.Db4o.Instrumentation.Api;
using Mono.Cecil;
using MethodAttributes=Mono.Cecil.MethodAttributes;

namespace Db4objects.Db4o.Instrumentation.Cecil
{
	public class CecilTypeEditor : ITypeEditor
	{
		private readonly TypeDefinition _type;
		private readonly CecilReferenceProvider _references;

		public CecilTypeEditor(TypeDefinition type)
		{
			_type = type;
			_references = CecilReferenceProvider.ForModule(type.Module.Assembly.MainModule);
		}

		public ITypeRef Type
		{
			get { return _references.ForCecilType(_type); }
		}

		public IReferenceProvider References
		{
			get { return _references; }
		}

		public void AddInterface(ITypeRef type)
		{
			_type.Interfaces.Add(GetTypeReference(type));
		}

		public IMethodBuilder NewPublicMethod(string methodName, ITypeRef returnType, ITypeRef[] parameterTypes)
		{
			MethodDefinition method = NewMethod(methodName, parameterTypes, returnType);
			_type.Methods.Add(method);
			return new CecilMethodBuilder(method);
		}

		private static MethodDefinition NewMethod(string methodName, ITypeRef[] parameterTypes, ITypeRef returnType)
		{
			MethodDefinition method = new MethodDefinition(methodName,
			                                               MethodAttributes.Virtual | MethodAttributes.Public,
			                                               GetTypeReference(returnType));
			foreach (ITypeRef paramType in parameterTypes)
			{
				method.Parameters.Add(new ParameterDefinition(GetTypeReference(paramType)));
			}
			return method;
		}

		private static TypeReference GetTypeReference(ITypeRef type)
		{
			return CecilTypeRef.GetReference(type);
		}
	}
}

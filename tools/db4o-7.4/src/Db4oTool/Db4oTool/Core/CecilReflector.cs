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
using Db4objects.Db4o.Foundation;
using Mono.Cecil;

namespace Db4oTool.Core
{
	public class CecilReflector
	{
		private readonly InstrumentationContext _context;
		private readonly IAssemblyResolver _resolver;

		public CecilReflector(InstrumentationContext context)
		{
			_context = context;
			_resolver = new RelativeAssemblyResolver(_context);

			if (_context.AlternateAssemblyLocation != null)
			{
				_resolver = new CompositeAssemblyResolver(
										new RelativeAssemblyResolver(_context.AlternateAssemblyLocation),
										_resolver);
			}
		}

		public bool Implements(TypeDefinition type, Type interfaceType)
		{
			return Implements(type, interfaceType.FullName);
		}

		private bool Implements(TypeDefinition type, string interfaceName)
		{
			if (Contains(type.Interfaces, interfaceName)) return true;
			if (null == type.BaseType) return false;

			TypeDefinition baseType = ResolveTypeReference(type.BaseType);
			if (null != baseType) return Implements(baseType, interfaceName);

			return false;
		}

		public TypeDefinition ResolveTypeReference(TypeReference typeRef)
		{
			if (null == typeRef) throw new ArgumentNullException("typeRef");

			TypeDefinition type = typeRef as TypeDefinition;
			if (null != type) return type;

			GenericParameter parameter = typeRef as GenericParameter;
			if (parameter != null) return null;

			TypeSpecification typeSpecification = typeRef as TypeSpecification;
			if (typeSpecification != null) return ResolveTypeReference(typeSpecification.ElementType);
            
			AssemblyDefinition assembly = ResolveAssembly(typeRef.Scope as AssemblyNameReference);
			if (null == assembly) return null;

			return FindType(assembly, typeRef);
		}

		private AssemblyDefinition ResolveAssembly(AssemblyNameReference assemblyRef)
		{
			return _resolver.Resolve(assemblyRef);
		}

		private TypeDefinition FindType(AssemblyDefinition assembly, TypeReference typeRef)
		{
			foreach (ModuleDefinition m in assembly.Modules)
			{
				foreach (TypeDefinition t in m.Types)
				{
					if (t.FullName == typeRef.FullName) return t;
				}
			}
			return null;
		}

		private static bool Contains(InterfaceCollection collection, string fullName)
		{
			foreach (TypeReference typeRef in collection)
			{
				if (typeRef.FullName == fullName) return true;
			}
			return false;
		}
	}

	internal class CompositeAssemblyResolver : IAssemblyResolver
	{
		private readonly IAssemblyResolver[] _resolvers;

		public CompositeAssemblyResolver(params IAssemblyResolver[] resolvers)
		{
			_resolvers = resolvers;
		}

		public AssemblyDefinition Resolve(string fullName)
		{
			return InternalResolve(delegate(IAssemblyResolver resolver)
			                       	{
			                       		return resolver.Resolve(fullName);
			                       	});
		}

		public AssemblyDefinition Resolve(AssemblyNameReference name)
		{
			return InternalResolve(delegate(IAssemblyResolver resolver)
									{
										return resolver.Resolve(name);
									});
		}

		private AssemblyDefinition InternalResolve(Function<IAssemblyResolver, AssemblyDefinition> @delegate)
		{
			foreach (IAssemblyResolver resolver in _resolvers)
			{
				AssemblyDefinition assemblyDefinition = @delegate(resolver);
				if (assemblyDefinition != null) return assemblyDefinition;
			}

			return null;
		}
	}
}

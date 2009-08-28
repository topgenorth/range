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
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Db4objects.Db4o.Instrumentation.Api;
using Mono.Cecil;

namespace Db4objects.Db4o.Instrumentation.Cecil
{
	public class CecilReferenceProvider : IReferenceProvider
	{
		private static readonly object AnnotationKey = new object();

		public static CecilReferenceProvider ForModule(ModuleDefinition module)
		{
			System.Collections.IDictionary annotations = Annotations(module);
			CecilReferenceProvider provider = (CecilReferenceProvider) annotations[AnnotationKey];
			if (null == provider)
			{
				provider = new CecilReferenceProvider(module);
				annotations[AnnotationKey] = provider;
			}
			return provider;
		}

		private static IDictionary Annotations(ModuleDefinition module)
		{
			return (module as IAnnotationProvider).Annotations;
		}

		private readonly ModuleDefinition _module;
		private readonly Dictionary<TypeReference, ITypeRef> _typeCache = new Dictionary<TypeReference, ITypeRef>();

		private CecilReferenceProvider(ModuleDefinition module)
		{
			if (null == module) throw new ArgumentNullException();
			_module = module;
		}

		public ITypeRef ForType(Type type)
		{
			return ForCecilType(_module.Import(type));
		}

		public ITypeRef ForCecilType(TypeReference type)
		{
			ITypeRef typeRef;
			if (!_typeCache.TryGetValue(type, out typeRef))
			{
				typeRef = new CecilTypeRef(this, type);
				_typeCache.Add(type, typeRef);
			}
			return typeRef;
		}

		public IMethodRef ForMethod(MethodInfo method)
		{
			return new CecilMethodRef(this, _module.Import(method));
		}

		public IMethodRef ForMethod(ITypeRef declaringType, string methodName, ITypeRef[] parameterTypes, ITypeRef returnType)
		{
			throw new NotImplementedException();
		}

		public IFieldRef ForCecilField(FieldReference field)
		{
			return new CecilFieldRef(this, field);
		}
	}
}

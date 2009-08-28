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
using Db4objects.Db4o;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Reflect;
using Db4objects.Db4o.Reflect.Core;

namespace Db4objects.Db4o.Reflect.Core
{
	public class ConstructorSupport
	{
		public static ReflectConstructorSpec CreateConstructor(IConstructorAwareReflectClass
			 claxx, Type clazz, IReflectorConfiguration config, IReflectConstructor[] constructors
			)
		{
			if (claxx == null)
			{
				return ReflectConstructorSpec.InvalidConstructor;
			}
			if (claxx.IsAbstract() || claxx.IsInterface())
			{
				return ReflectConstructorSpec.InvalidConstructor;
			}
			if (!Platform4.CallConstructor())
			{
				bool skipConstructor = !config.CallConstructor(claxx);
				if (!claxx.IsCollection())
				{
					IReflectConstructor serializableConstructor = SkipConstructor(claxx, skipConstructor
						, config.TestConstructors());
					if (serializableConstructor != null)
					{
						return new ReflectConstructorSpec(serializableConstructor, null);
					}
				}
			}
			if (!config.TestConstructors())
			{
				return new ReflectConstructorSpec(new PlatformReflectConstructor(clazz), null);
			}
			if (ReflectPlatform.CreateInstance(clazz) != null)
			{
				return new ReflectConstructorSpec(new PlatformReflectConstructor(clazz), null);
			}
			Tree sortedConstructors = SortConstructorsByParamsCount(constructors);
			return FindConstructor(claxx, sortedConstructors);
		}

		private static ReflectConstructorSpec FindConstructor(IReflectClass claxx, Tree sortedConstructors
			)
		{
			if (sortedConstructors == null)
			{
				return ReflectConstructorSpec.InvalidConstructor;
			}
			IEnumerator iter = new TreeNodeIterator(sortedConstructors);
			while (iter.MoveNext())
			{
				object obj = iter.Current;
				IReflectConstructor constructor = (IReflectConstructor)((TreeIntObject)obj)._object;
				IReflectClass[] paramTypes = constructor.GetParameterTypes();
				object[] @params = new object[paramTypes.Length];
				for (int j = 0; j < @params.Length; j++)
				{
					@params[j] = paramTypes[j].NullValue();
				}
				object res = constructor.NewInstance(@params);
				if (res != null)
				{
					return new ReflectConstructorSpec(constructor, @params);
				}
			}
			return ReflectConstructorSpec.InvalidConstructor;
		}

		private static Tree SortConstructorsByParamsCount(IReflectConstructor[] constructors
			)
		{
			Tree sortedConstructors = null;
			// sort constructors by parameter count
			for (int i = 0; i < constructors.Length; i++)
			{
				int parameterCount = constructors[i].GetParameterTypes().Length;
				sortedConstructors = Tree.Add(sortedConstructors, new TreeIntObject(i + constructors
					.Length * parameterCount, constructors[i]));
			}
			return sortedConstructors;
		}

		public static IReflectConstructor SkipConstructor(IConstructorAwareReflectClass claxx
			, bool skipConstructor, bool testConstructor)
		{
			if (!skipConstructor)
			{
				return null;
			}
			IReflectConstructor serializableConstructor = claxx.GetSerializableConstructor();
			if (serializableConstructor == null)
			{
				return null;
			}
			if (!testConstructor || Deploy.csharp)
			{
				return serializableConstructor;
			}
			object obj = serializableConstructor.NewInstance((object[])null);
			if (obj != null)
			{
				return serializableConstructor;
			}
			return null;
		}
	}
}

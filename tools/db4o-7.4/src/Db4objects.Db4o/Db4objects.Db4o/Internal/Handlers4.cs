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
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Handlers;
using Db4objects.Db4o.Internal.Handlers.Array;
using Db4objects.Db4o.Internal.Marshall;
using Db4objects.Db4o.Reflect;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Internal
{
	/// <exclude></exclude>
	public class Handlers4
	{
		public const int IntId = 1;

		public const int LongId = 2;

		public const int FloatId = 3;

		public const int BooleanId = 4;

		public const int DoubleId = 5;

		public const int ByteId = 6;

		public const int CharId = 7;

		public const int ShortId = 8;

		public const int StringId = 9;

		public const int DateId = 10;

		public const int UntypedId = 11;

		public const int AnyArrayId = 12;

		public const int AnyArrayNId = 13;

		public static ITypeHandler4 CorrectHandlerVersion(IHandlerVersionContext context, 
			ITypeHandler4 handler)
		{
			int version = context.HandlerVersion();
			if (version >= HandlerRegistry.HandlerVersion)
			{
				return handler;
			}
			return context.Transaction().Container().Handlers().CorrectHandlerVersion(handler
				, version);
		}

		public static bool HandlerCanHold(ITypeHandler4 handler, IReflector reflector, IReflectClass
			 claxx)
		{
			ITypeHandler4 baseTypeHandler = BaseTypeHandler(handler);
			if (HandlesSimple(baseTypeHandler))
			{
				return claxx.Equals(((IBuiltinTypeHandler)baseTypeHandler).ClassReflector());
			}
			if (baseTypeHandler is UntypedFieldHandler)
			{
				return true;
			}
			if (handler is ICanHoldAnythingHandler)
			{
				return true;
			}
			ClassMetadata classMetadata = (ClassMetadata)baseTypeHandler;
			IReflectClass classReflector = classMetadata.ClassReflector();
			if (classReflector.IsCollection())
			{
				return true;
			}
			return classReflector.IsAssignableFrom(claxx);
		}

		public static bool HandlesSimple(ITypeHandler4 handler)
		{
			ITypeHandler4 baseTypeHandler = BaseTypeHandler(handler);
			return (baseTypeHandler is PrimitiveHandler) || (baseTypeHandler is StringHandler
				) || (baseTypeHandler is ISecondClassTypeHandler);
		}

		public static bool HandlesClass(ITypeHandler4 handler)
		{
			return BaseTypeHandler(handler) is IFirstClassHandler;
		}

		public static IReflectClass PrimitiveClassReflector(ITypeHandler4 handler, IReflector
			 reflector)
		{
			ITypeHandler4 baseTypeHandler = BaseTypeHandler(handler);
			if (baseTypeHandler is PrimitiveHandler)
			{
				return ((PrimitiveHandler)baseTypeHandler).PrimitiveClassReflector();
			}
			return null;
		}

		public static ITypeHandler4 BaseTypeHandler(ITypeHandler4 handler)
		{
			if (handler is ArrayHandler)
			{
				return ((ArrayHandler)handler).DelegateTypeHandler();
			}
			if (handler is PrimitiveFieldHandler)
			{
				return ((PrimitiveFieldHandler)handler).TypeHandler();
			}
			return handler;
		}

		public static IReflectClass BaseType(IReflectClass clazz)
		{
			if (clazz == null)
			{
				return null;
			}
			if (clazz.IsArray())
			{
				return BaseType(clazz.GetComponentType());
			}
			return clazz;
		}
	}
}

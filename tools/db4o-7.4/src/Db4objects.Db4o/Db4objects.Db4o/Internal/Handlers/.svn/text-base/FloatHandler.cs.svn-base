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
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Handlers;
using Db4objects.Db4o.Internal.Marshall;
using Db4objects.Db4o.Marshall;
using Db4objects.Db4o.Reflect;

namespace Db4objects.Db4o.Internal.Handlers
{
	public class FloatHandler : IntHandler
	{
		private static readonly float Defaultvalue = System.Convert.ToSingle(0);

		public override object Coerce(IReflector reflector, IReflectClass claxx, object obj
			)
		{
			return Coercion4.ToFloat(obj);
		}

		public override object DefaultValue()
		{
			return Defaultvalue;
		}

		public override Type PrimitiveJavaClass()
		{
			return typeof(float);
		}

		/// <exception cref="CorruptionException"></exception>
		public override object Read(MarshallerFamily mf, StatefulBuffer writer, bool redirect
			)
		{
			return mf._primitive.ReadFloat(writer);
		}

		internal override object Read1(ByteArrayBuffer a_bytes)
		{
			return PrimitiveMarshaller().ReadFloat(a_bytes);
		}

		public override void Write(object a_object, ByteArrayBuffer a_bytes)
		{
			WriteInt(Sharpen.Runtime.FloatToIntBits(((float)a_object)), a_bytes);
		}

		public override object Read(IReadContext context)
		{
			return Sharpen.Runtime.IntBitsToFloat(context.ReadInt());
		}

		public override void Write(IWriteContext context, object obj)
		{
			context.WriteInt(Sharpen.Runtime.FloatToIntBits(((float)obj)));
		}

		public override IPreparedComparison InternalPrepareComparison(object source)
		{
			float sourceFloat = ((float)source);
			return new _IPreparedComparison_54(sourceFloat);
		}

		private sealed class _IPreparedComparison_54 : IPreparedComparison
		{
			public _IPreparedComparison_54(float sourceFloat)
			{
				this.sourceFloat = sourceFloat;
			}

			public int CompareTo(object target)
			{
				if (target == null)
				{
					return 1;
				}
				float targetFloat = ((float)target);
				return sourceFloat == targetFloat ? 0 : (sourceFloat < targetFloat ? -1 : 1);
			}

			private readonly float sourceFloat;
		}
	}
}

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
	public class ShortHandler : PrimitiveHandler
	{
		internal const int Length = Const4.ShortBytes + Const4.AddedLength;

		private static readonly short Defaultvalue = (short)0;

		public override object Coerce(IReflector reflector, IReflectClass claxx, object obj
			)
		{
			return Coercion4.ToShort(obj);
		}

		public override object DefaultValue()
		{
			return Defaultvalue;
		}

		public override int LinkLength()
		{
			return Length;
		}

		public override Type PrimitiveJavaClass()
		{
			return typeof(short);
		}

		/// <exception cref="CorruptionException"></exception>
		public override object Read(MarshallerFamily mf, StatefulBuffer buffer, bool redirect
			)
		{
			return mf._primitive.ReadShort(buffer);
		}

		internal override object Read1(ByteArrayBuffer buffer)
		{
			return PrimitiveMarshaller().ReadShort(buffer);
		}

		public override void Write(object a_object, ByteArrayBuffer a_bytes)
		{
			WriteShort(((short)a_object), a_bytes);
		}

		internal static void WriteShort(int a_short, ByteArrayBuffer a_bytes)
		{
			for (int i = 0; i < Const4.ShortBytes; i++)
			{
				a_bytes._buffer[a_bytes._offset++] = (byte)(a_short >> ((Const4.ShortBytes - 1 - 
					i) * 8));
			}
		}

		public override object Read(IReadContext context)
		{
			int value = ((context.ReadByte() & unchecked((int)(0xff))) << 8) + (context.ReadByte
				() & unchecked((int)(0xff)));
			return (short)value;
		}

		public override void Write(IWriteContext context, object obj)
		{
			int shortValue = ((short)obj);
			context.WriteBytes(new byte[] { (byte)(shortValue >> 8), (byte)shortValue });
		}

		public override IPreparedComparison InternalPrepareComparison(object source)
		{
			short sourceShort = ((short)source);
			return new _IPreparedComparison_86(sourceShort);
		}

		private sealed class _IPreparedComparison_86 : IPreparedComparison
		{
			public _IPreparedComparison_86(short sourceShort)
			{
				this.sourceShort = sourceShort;
			}

			public int CompareTo(object target)
			{
				if (target == null)
				{
					return 1;
				}
				short targetShort = ((short)target);
				return sourceShort == targetShort ? 0 : (sourceShort < targetShort ? -1 : 1);
			}

			private readonly short sourceShort;
		}
	}
}

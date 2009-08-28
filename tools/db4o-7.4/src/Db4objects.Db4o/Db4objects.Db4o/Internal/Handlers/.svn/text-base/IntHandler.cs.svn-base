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
	/// <exclude></exclude>
	public class IntHandler : PrimitiveHandler
	{
		private static readonly int Defaultvalue = 0;

		public override object Coerce(IReflector reflector, IReflectClass claxx, object obj
			)
		{
			return Coercion4.ToInt(obj);
		}

		public override object DefaultValue()
		{
			return Defaultvalue;
		}

		public override Type PrimitiveJavaClass()
		{
			return typeof(int);
		}

		public override int LinkLength()
		{
			return Const4.IntLength;
		}

		/// <exception cref="CorruptionException"></exception>
		public override object Read(MarshallerFamily mf, StatefulBuffer writer, bool redirect
			)
		{
			return mf._primitive.ReadInteger(writer);
		}

		internal override object Read1(ByteArrayBuffer a_bytes)
		{
			return a_bytes.ReadInt();
		}

		public override void Write(object obj, ByteArrayBuffer writer)
		{
			Write(((int)obj), writer);
		}

		public virtual void Write(int intValue, ByteArrayBuffer writer)
		{
			WriteInt(intValue, writer);
		}

		public static void WriteInt(int a_int, ByteArrayBuffer a_bytes)
		{
			a_bytes.WriteInt(a_int);
		}

		public override void DefragIndexEntry(DefragmentContextImpl context)
		{
			context.IncrementIntSize();
		}

		public override object Read(IReadContext context)
		{
			return context.ReadInt();
		}

		public override void Write(IWriteContext context, object obj)
		{
			context.WriteInt(((int)obj));
		}

		public override IPreparedComparison InternalPrepareComparison(object source)
		{
			return NewPrepareCompare(((int)source));
		}

		public virtual IPreparedComparison NewPrepareCompare(int i)
		{
			return new IntHandler.PreparedIntComparison(this, i);
		}

		public sealed class PreparedIntComparison : IPreparedComparison
		{
			private readonly int _sourceInt;

			public PreparedIntComparison(IntHandler _enclosing, int sourceInt)
			{
				this._enclosing = _enclosing;
				this._sourceInt = sourceInt;
			}

			public int CompareTo(object target)
			{
				if (target == null)
				{
					return 1;
				}
				int targetInt = ((int)target);
				return this._sourceInt == targetInt ? 0 : (this._sourceInt < targetInt ? -1 : 1);
			}

			private readonly IntHandler _enclosing;
		}
	}
}

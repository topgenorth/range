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
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Handlers;
using Db4objects.Db4o.Marshall;

namespace Db4objects.Db4o.Internal.Handlers
{
	public sealed class CharHandler : PrimitiveHandler
	{
		internal const int Length = Const4.CharBytes + Const4.AddedLength;

		private static readonly char Defaultvalue = (char)0;

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
			return typeof(char);
		}

		internal override object Read1(ByteArrayBuffer a_bytes)
		{
			byte b1 = a_bytes.ReadByte();
			byte b2 = a_bytes.ReadByte();
			char ret = (char)((b1 & unchecked((int)(0xff))) | ((b2 & unchecked((int)(0xff))) 
				<< 8));
			return ret;
		}

		public override void Write(object a_object, ByteArrayBuffer a_bytes)
		{
			char char_ = ((char)a_object);
			a_bytes.WriteByte((byte)(char_ & unchecked((int)(0xff))));
			a_bytes.WriteByte((byte)(char_ >> 8));
		}

		public override object Read(IReadContext context)
		{
			byte b1 = context.ReadByte();
			byte b2 = context.ReadByte();
			char charValue = (char)((b1 & unchecked((int)(0xff))) | ((b2 & unchecked((int)(0xff
				))) << 8));
			return charValue;
		}

		public override void Write(IWriteContext context, object obj)
		{
			char charValue = ((char)obj);
			context.WriteBytes(new byte[] { (byte)(charValue & unchecked((int)(0xff))), (byte
				)(charValue >> 8) });
		}

		public override IPreparedComparison InternalPrepareComparison(object source)
		{
			char sourceChar = ((char)source);
			return new _IPreparedComparison_90(sourceChar);
		}

		private sealed class _IPreparedComparison_90 : IPreparedComparison
		{
			public _IPreparedComparison_90(char sourceChar)
			{
				this.sourceChar = sourceChar;
			}

			public int CompareTo(object target)
			{
				if (target == null)
				{
					return 1;
				}
				char targetChar = ((char)target);
				return sourceChar == targetChar ? 0 : (sourceChar < targetChar ? -1 : 1);
			}

			private readonly char sourceChar;
		}
	}
}

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
namespace Db4objects.Db4o.Foundation
{
	public sealed class PrimitiveCodec
	{
		public const int IntLength = 4;

		public const int LongLength = 8;

		public static int ReadInt(byte[] buffer, int offset)
		{
			offset += 3;
			return (buffer[offset] & 255) | (buffer[--offset] & 255) << 8 | (buffer[--offset]
				 & 255) << 16 | buffer[--offset] << 24;
		}

		public static void WriteInt(byte[] buffer, int offset, int val)
		{
			offset += 3;
			buffer[offset] = (byte)val;
			buffer[--offset] = (byte)(val >>= 8);
			buffer[--offset] = (byte)(val >>= 8);
			buffer[--offset] = (byte)(val >> 8);
		}

		public static void WriteLong(byte[] buffer, long val)
		{
			WriteLong(buffer, 0, val);
		}

		public static void WriteLong(byte[] buffer, int offset, long val)
		{
			for (int i = 0; i < LongLength; i++)
			{
				buffer[offset++] = (byte)(val >> ((7 - i) * 8));
			}
		}

		public static long ReadLong(byte[] buffer, int offset)
		{
			long ret = 0;
			for (int i = 0; i < LongLength; i++)
			{
				ret = (ret << 8) + (buffer[offset++] & unchecked((int)(0xff)));
			}
			return ret;
		}
	}
}

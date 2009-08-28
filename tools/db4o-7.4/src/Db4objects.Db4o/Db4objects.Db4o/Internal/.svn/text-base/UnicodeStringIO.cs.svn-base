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
using Db4objects.Db4o.Marshall;

namespace Db4objects.Db4o.Internal
{
	/// <exclude></exclude>
	public sealed class UnicodeStringIO : LatinStringIO
	{
		public override int BytesPerChar()
		{
			return 2;
		}

		public override byte EncodingByte()
		{
			return Const4.Unicode;
		}

		public override int Length(string str)
		{
			return (str.Length * 2) + Const4.ObjectLength + Const4.IntLength;
		}

		public override string Read(IReadBuffer buffer, int length)
		{
			char[] chars = new char[length];
			for (int ii = 0; ii < length; ii++)
			{
				chars[ii] = (char)((buffer.ReadByte() & unchecked((int)(0xff))) | ((buffer.ReadByte
					() & unchecked((int)(0xff))) << 8));
			}
			return new string(chars, 0, length);
		}

		public override string Read(byte[] bytes)
		{
			int length = bytes.Length / 2;
			char[] chars = new char[length];
			int j = 0;
			for (int ii = 0; ii < length; ii++)
			{
				chars[ii] = (char)((bytes[j++] & unchecked((int)(0xff))) | ((bytes[j++] & unchecked(
					(int)(0xff))) << 8));
			}
			return new string(chars, 0, length);
		}

		public override int ShortLength(string str)
		{
			return (str.Length * 2) + Const4.IntLength;
		}

		public override void Write(IWriteBuffer buffer, string str)
		{
			int length = str.Length;
			char[] chars = new char[length];
			Sharpen.Runtime.GetCharsForString(str, 0, length, chars, 0);
			for (int i = 0; i < length; i++)
			{
				buffer.WriteByte((byte)(chars[i] & unchecked((int)(0xff))));
				buffer.WriteByte((byte)(chars[i] >> 8));
			}
		}

		public override byte[] Write(string str)
		{
			int length = str.Length;
			char[] chars = new char[length];
			Sharpen.Runtime.GetCharsForString(str, 0, length, chars, 0);
			byte[] bytes = new byte[length * 2];
			int j = 0;
			for (int i = 0; i < length; i++)
			{
				bytes[j++] = (byte)(chars[i] & unchecked((int)(0xff)));
				bytes[j++] = (byte)(chars[i] >> 8);
			}
			return bytes;
		}
	}
}

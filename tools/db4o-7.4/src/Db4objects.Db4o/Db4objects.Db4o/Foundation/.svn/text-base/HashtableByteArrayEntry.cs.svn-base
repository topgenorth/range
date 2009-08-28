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
using Db4objects.Db4o.Foundation;

namespace Db4objects.Db4o.Foundation
{
	internal class HashtableByteArrayEntry : HashtableObjectEntry
	{
		public HashtableByteArrayEntry(byte[] bytes, object value) : base(Hash(bytes), bytes
			, value)
		{
		}

		public HashtableByteArrayEntry() : base()
		{
		}

		public override object DeepClone(object obj)
		{
			return DeepCloneInternal(new Db4objects.Db4o.Foundation.HashtableByteArrayEntry()
				, obj);
		}

		public override bool HasKey(object key)
		{
			if (key is byte[])
			{
				return AreEqual((byte[])Key(), (byte[])key);
			}
			return false;
		}

		internal static int Hash(byte[] bytes)
		{
			int ret = 0;
			for (int i = 0; i < bytes.Length; i++)
			{
				ret = ret * 31 + bytes[i];
			}
			return ret;
		}

		internal static bool AreEqual(byte[] lhs, byte[] rhs)
		{
			if (rhs.Length != lhs.Length)
			{
				return false;
			}
			for (int i = 0; i < rhs.Length; i++)
			{
				if (rhs[i] != lhs[i])
				{
					return false;
				}
			}
			return true;
		}
	}
}

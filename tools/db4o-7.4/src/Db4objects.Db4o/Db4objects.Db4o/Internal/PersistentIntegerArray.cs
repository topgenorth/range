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
using Sharpen;

namespace Db4objects.Db4o.Internal
{
	/// <exclude></exclude>
	public class PersistentIntegerArray : PersistentBase
	{
		private int[] _ints;

		public PersistentIntegerArray(int id)
		{
			SetID(id);
		}

		public PersistentIntegerArray(int[] arr)
		{
			_ints = new int[arr.Length];
			System.Array.Copy(arr, 0, _ints, 0, arr.Length);
		}

		public override byte GetIdentifier()
		{
			return Const4.IntegerArray;
		}

		public override int OwnLength()
		{
			return (Const4.IntLength * (Size() + 1)) + Const4.AddedLength;
		}

		public override void ReadThis(Transaction trans, ByteArrayBuffer reader)
		{
			int length = reader.ReadInt();
			_ints = new int[length];
			for (int i = 0; i < length; i++)
			{
				_ints[i] = reader.ReadInt();
			}
		}

		public override void WriteThis(Transaction trans, ByteArrayBuffer writer)
		{
			writer.WriteInt(Size());
			for (int i = 0; i < _ints.Length; i++)
			{
				writer.WriteInt(_ints[i]);
			}
		}

		private int Size()
		{
			return _ints.Length;
		}

		public virtual int[] Array()
		{
			return _ints;
		}
	}
}

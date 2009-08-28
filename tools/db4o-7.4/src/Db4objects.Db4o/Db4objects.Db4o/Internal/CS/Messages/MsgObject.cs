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
using Db4objects.Db4o.Internal.CS.Messages;
using Db4objects.Db4o.Internal.Slots;

namespace Db4objects.Db4o.Internal.CS.Messages
{
	public class MsgObject : MsgD
	{
		private const int LengthForAll = Const4.IdLength + (Const4.IntLength * 2);

		private const int LengthForFirst = LengthForAll;

		private int _id;

		private int _address;

		internal MsgD GetWriter(Transaction trans, Pointer4 pointer, ByteArrayBuffer buffer
			, int[] prependInts)
		{
			int lengthNeeded = buffer.Length() + LengthForFirst;
			if (prependInts != null)
			{
				lengthNeeded += (prependInts.Length * Const4.IntLength);
			}
			MsgD message = GetWriterForLength(trans, lengthNeeded);
			if (prependInts != null)
			{
				for (int i = 0; i < prependInts.Length; i++)
				{
					message._payLoad.WriteInt(prependInts[i]);
				}
			}
			AppendPayLoad(message._payLoad, pointer, buffer);
			return message;
		}

		private void AppendPayLoad(StatefulBuffer target, Pointer4 pointer, ByteArrayBuffer
			 payLoad)
		{
			target.WriteInt(payLoad.Length());
			target.WriteInt(pointer.Id());
			target.WriteInt(pointer.Address());
			target.Append(payLoad._buffer);
		}

		public sealed override MsgD GetWriter(StatefulBuffer buffer)
		{
			return GetWriter(buffer.Transaction(), buffer.Pointer(), buffer, null);
		}

		public MsgD GetWriter(Transaction trans, Pointer4 pointer, ClassMetadata classMetadata
			, ByteArrayBuffer buffer)
		{
			if (classMetadata == null)
			{
				return GetWriter(trans, pointer, buffer, new int[] { 0 });
			}
			return GetWriter(trans, pointer, buffer, new int[] { classMetadata.GetID() });
		}

		public MsgD GetWriter(Transaction trans, Pointer4 pointer, ClassMetadata classMetadata
			, int param, ByteArrayBuffer buffer)
		{
			return GetWriter(trans, pointer, buffer, new int[] { classMetadata.GetID(), param
				 });
		}

		public StatefulBuffer Unmarshall()
		{
			return Unmarshall(0);
		}

		public StatefulBuffer Unmarshall(int addLengthBeforeFirst)
		{
			_payLoad.SetTransaction(Transaction());
			int length = _payLoad.ReadInt();
			if (length == 0)
			{
				return null;
			}
			// does this happen ?
			_id = _payLoad.ReadInt();
			_address = _payLoad.ReadInt();
			_payLoad.RemoveFirstBytes(LengthForFirst + addLengthBeforeFirst);
			_payLoad.UseSlot(_id, _address, length);
			return _payLoad;
		}

		public virtual int GetId()
		{
			return _id;
		}
	}
}

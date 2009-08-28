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
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.CS.Messages;
using Db4objects.Db4o.Internal.Slots;

namespace Db4objects.Db4o.Internal.CS.Messages
{
	public sealed class MReadBytes : MsgD, IServerSideMessage
	{
		public sealed override ByteArrayBuffer GetByteLoad()
		{
			int address = _payLoad.ReadInt();
			int length = _payLoad.Length() - (Const4.IntLength);
			Slot slot = new Slot(address, length);
			_payLoad.RemoveFirstBytes(Const4.IntLength);
			_payLoad.UseSlot(slot);
			return this._payLoad;
		}

		public sealed override MsgD GetWriter(StatefulBuffer bytes)
		{
			MsgD message = GetWriterForLength(bytes.Transaction(), bytes.Length() + Const4.IntLength
				);
			message._payLoad.WriteInt(bytes.GetAddress());
			message._payLoad.Append(bytes._buffer);
			return message;
		}

		public bool ProcessAtServer()
		{
			int address = ReadInt();
			int length = ReadInt();
			lock (StreamLock())
			{
				StatefulBuffer bytes = new StatefulBuffer(this.Transaction(), address, length);
				try
				{
					Stream().ReadBytes(bytes._buffer, address, length);
					Write(GetWriter(bytes));
				}
				catch (Exception)
				{
					// TODO: not nicely handled on the client side yet
					Write(Msg.Null);
				}
			}
			return true;
		}
	}
}

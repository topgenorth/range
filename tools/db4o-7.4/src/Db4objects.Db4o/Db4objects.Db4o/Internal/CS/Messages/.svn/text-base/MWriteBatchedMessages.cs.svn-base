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

namespace Db4objects.Db4o.Internal.CS.Messages
{
	public class MWriteBatchedMessages : MsgD, IServerSideMessage
	{
		public bool ProcessAtServer()
		{
			int count = ReadInt();
			Transaction ta = Transaction();
			lock (StreamLock())
			{
				for (int i = 0; i < count; i++)
				{
					StatefulBuffer writer = _payLoad.ReadYapBytes();
					int messageId = writer.ReadInt();
					Msg message = Msg.GetMessage(messageId);
					Msg clonedMessage = message.PublicClone();
					clonedMessage.SetMessageDispatcher(MessageDispatcher());
					clonedMessage.SetTransaction(ta);
					if (clonedMessage is MsgD)
					{
						MsgD msgd = (MsgD)clonedMessage;
						msgd.PayLoad(writer);
						if (msgd.PayLoad() != null)
						{
							msgd.PayLoad().IncrementOffset(Const4.IntLength);
							Transaction t = CheckParentTransaction(ta, msgd.PayLoad());
							msgd.SetTransaction(t);
							((IServerSideMessage)msgd).ProcessAtServer();
						}
					}
					else
					{
						((IServerSideMessage)clonedMessage).ProcessAtServer();
					}
				}
				return true;
			}
		}
	}
}

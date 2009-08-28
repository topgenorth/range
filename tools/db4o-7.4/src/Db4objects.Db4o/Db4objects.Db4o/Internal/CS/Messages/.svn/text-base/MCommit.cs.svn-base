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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.CS;
using Db4objects.Db4o.Internal.CS.Messages;

namespace Db4objects.Db4o.Internal.CS.Messages
{
	public sealed class MCommit : Msg, IServerSideMessage
	{
		public bool ProcessAtServer()
		{
			try
			{
				CallbackObjectInfoCollections committedInfo = null;
				LocalTransaction serverTransaction = ServerTransaction();
				IServerMessageDispatcher dispatcher = ServerMessageDispatcher();
				lock (StreamLock())
				{
					serverTransaction.Commit(dispatcher);
					committedInfo = dispatcher.CommittedInfo();
				}
				Write(Msg.Ok);
				if (committedInfo != null)
				{
					AddCommittedInfoMsg(committedInfo, serverTransaction);
				}
			}
			catch (Db4oException e)
			{
				WriteException(e);
			}
			return true;
		}

		private void AddCommittedInfoMsg(CallbackObjectInfoCollections committedInfo, LocalTransaction
			 serverTransaction)
		{
			lock (StreamLock())
			{
				Msg.CommittedInfo.SetTransaction(serverTransaction);
				MCommittedInfo message = Msg.CommittedInfo.Encode(committedInfo);
				message.SetMessageDispatcher(ServerMessageDispatcher());
				ServerMessageDispatcher().Server().AddCommittedInfoMsg(message);
			}
		}
	}
}

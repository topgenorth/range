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
using System.IO;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation.Network;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Activation;
using Db4objects.Db4o.Internal.CS.Messages;
using Sharpen.IO;

namespace Db4objects.Db4o.Internal.CS.Messages
{
	public class MWriteBlob : MsgBlob, IServerSideMessage
	{
		/// <exception cref="IOException"></exception>
		public override void ProcessClient(ISocket4 sock)
		{
			Msg message = Msg.ReadMessage(MessageDispatcher(), Transaction(), sock);
			if (message.Equals(Msg.Ok))
			{
				try
				{
					_currentByte = 0;
					_length = this._blob.GetLength();
					_blob.GetStatusFrom(this);
					_blob.SetStatus(Status.Processing);
					FileInputStream inBlob = this._blob.GetClientInputStream();
					Copy(inBlob, sock, true);
					sock.Flush();
					ObjectContainerBase stream = Stream();
					message = Msg.ReadMessage(MessageDispatcher(), Transaction(), sock);
					if (message.Equals(Msg.Ok))
					{
						// make sure to load the filename to i_blob
						// to allow client databasefile switching
						stream.Deactivate(Transaction(), _blob, int.MaxValue);
						stream.Activate(Transaction(), _blob, new FullActivationDepth());
						this._blob.SetStatus(Status.Completed);
					}
					else
					{
						this._blob.SetStatus(Status.Error);
					}
				}
				catch (Exception e)
				{
					Sharpen.Runtime.PrintStackTrace(e);
				}
			}
		}

		public virtual bool ProcessAtServer()
		{
			try
			{
				BlobImpl blobImpl = this.ServerGetBlobImpl();
				if (blobImpl != null)
				{
					blobImpl.SetTrans(Transaction());
					Sharpen.IO.File file = blobImpl.ServerFile(null, true);
					ISocket4 sock = ServerMessageDispatcher().Socket();
					Msg.Ok.Write(sock);
					FileOutputStream fout = new FileOutputStream(file);
					Copy(sock, fout, blobImpl.GetLength(), false);
					Msg.Ok.Write(sock);
				}
			}
			catch (Exception)
			{
			}
			return true;
		}
	}
}

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
using Db4objects.Db4o.Foundation.Network;
using Db4objects.Db4o.Internal.CS;
using Db4objects.Db4o.Internal.CS.Messages;
using Sharpen.Lang;

namespace Db4objects.Db4o.Internal.CS
{
	internal class BlobProcessor : Thread
	{
		private ClientObjectContainer stream;

		private IQueue4 queue = new NonblockingQueue();

		private bool terminated = false;

		internal BlobProcessor(ClientObjectContainer aStream)
		{
			stream = aStream;
			SetPriority(MinPriority);
		}

		internal virtual void Add(MsgBlob msg)
		{
			lock (queue)
			{
				queue.Add(msg);
			}
		}

		internal virtual bool IsTerminated()
		{
			lock (this)
			{
				return terminated;
			}
		}

		public override void Run()
		{
			try
			{
				ISocket4 socket = stream.CreateParalellSocket();
				MsgBlob msg = null;
				// no blobLock synchronisation here, since our first msg is valid
				lock (queue)
				{
					msg = (MsgBlob)queue.Next();
				}
				while (msg != null)
				{
					msg.Write(socket);
					msg.ProcessClient(socket);
					lock (stream.blobLock)
					{
						lock (queue)
						{
							msg = (MsgBlob)queue.Next();
						}
						if (msg == null)
						{
							terminated = true;
							Msg.CloseSocket.Write(socket);
							try
							{
								socket.Close();
							}
							catch (Exception)
							{
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				Sharpen.Runtime.PrintStackTrace(e);
			}
		}
	}
}

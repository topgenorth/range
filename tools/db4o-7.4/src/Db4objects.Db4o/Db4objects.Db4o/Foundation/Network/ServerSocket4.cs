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
using System.IO;
using System.Net.Sockets;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Foundation.Network;
using Sharpen.Net;

namespace Db4objects.Db4o.Foundation.Network
{
	public class ServerSocket4
	{
		private ServerSocket _serverSocket;

		private INativeSocketFactory _factory;

		/// <exception cref="IOException"></exception>
		public ServerSocket4(INativeSocketFactory factory, int port)
		{
			_factory = factory;
			_serverSocket = _factory.CreateServerSocket(port);
		}

		public virtual void SetSoTimeout(int timeout)
		{
			try
			{
				_serverSocket.SetSoTimeout(timeout);
			}
			catch (SocketException e)
			{
				Sharpen.Runtime.PrintStackTrace(e);
			}
		}

		public virtual int GetLocalPort()
		{
			return _serverSocket.GetLocalPort();
		}

		/// <exception cref="IOException"></exception>
		public virtual ISocket4 Accept()
		{
			Sharpen.Net.Socket sock = _serverSocket.Accept();
			// TODO: check connection permissions here
			return new NetworkSocket(_factory, sock);
		}

		/// <exception cref="IOException"></exception>
		public virtual void Close()
		{
			_serverSocket.Close();
		}
	}
}

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
using System.Net.Sockets;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation.Network;
using Db4objects.Db4o.Internal;
using Sharpen.IO;

namespace Db4objects.Db4o.Foundation.Network
{
	public class NetworkSocket : ISocket4
	{
		private Sharpen.Net.Socket _socket;

		private IOutputStream _out;

		private IInputStream _in;

		private string _hostName;

		private INativeSocketFactory _factory;

		/// <exception cref="Db4oIOException"></exception>
		public NetworkSocket(INativeSocketFactory factory, string hostName, int port)
		{
			_factory = factory;
			try
			{
				Sharpen.Net.Socket socket = _factory.CreateSocket(hostName, port);
				InitSocket(socket);
			}
			catch (IOException e)
			{
				throw new Db4oIOException(e);
			}
			_hostName = hostName;
		}

		/// <exception cref="IOException"></exception>
		public NetworkSocket(INativeSocketFactory factory, Sharpen.Net.Socket socket)
		{
			_factory = factory;
			InitSocket(socket);
		}

		/// <exception cref="IOException"></exception>
		private void InitSocket(Sharpen.Net.Socket socket)
		{
			_socket = socket;
			_out = _socket.GetOutputStream();
			_in = _socket.GetInputStream();
		}

		/// <exception cref="Db4oIOException"></exception>
		public virtual void Close()
		{
			try
			{
				_socket.Close();
			}
			catch (IOException e)
			{
				throw new Db4oIOException(e);
			}
		}

		/// <exception cref="Db4oIOException"></exception>
		public virtual void Flush()
		{
			try
			{
				_out.Flush();
			}
			catch (IOException e)
			{
				throw new Db4oIOException(e);
			}
		}

		public virtual bool IsConnected()
		{
			return Platform4.IsConnected(_socket);
		}

		/// <exception cref="Db4oIOException"></exception>
		public virtual int Read()
		{
			try
			{
				int ret = _in.Read();
				CheckEOF(ret);
				return ret;
			}
			catch (IOException e)
			{
				throw new Db4oIOException(e);
			}
		}

		/// <exception cref="Db4oIOException"></exception>
		public virtual int Read(byte[] a_bytes, int a_offset, int a_length)
		{
			try
			{
				int ret = _in.Read(a_bytes, a_offset, a_length);
				CheckEOF(ret);
				return ret;
			}
			catch (IOException e)
			{
				throw new Db4oIOException(e);
			}
		}

		private void CheckEOF(int ret)
		{
			if (ret == -1)
			{
				throw new Db4oIOException();
			}
		}

		public virtual void SetSoTimeout(int timeout)
		{
			try
			{
				_socket.SetSoTimeout(timeout);
			}
			catch (SocketException e)
			{
				Sharpen.Runtime.PrintStackTrace(e);
			}
		}

		/// <exception cref="Db4oIOException"></exception>
		public virtual void Write(byte[] bytes)
		{
			try
			{
				_out.Write(bytes);
			}
			catch (IOException e)
			{
				throw new Db4oIOException(e);
			}
		}

		/// <exception cref="Db4oIOException"></exception>
		public virtual void Write(byte[] bytes, int off, int len)
		{
			try
			{
				_out.Write(bytes, off, len);
			}
			catch (IOException e)
			{
				throw new Db4oIOException(e);
			}
		}

		/// <exception cref="Db4oIOException"></exception>
		public virtual void Write(int i)
		{
			try
			{
				_out.Write(i);
			}
			catch (IOException e)
			{
				throw new Db4oIOException(e);
			}
		}

		/// <exception cref="Db4oIOException"></exception>
		public virtual ISocket4 OpenParalellSocket()
		{
			if (_hostName == null)
			{
				throw new InvalidOperationException();
			}
			return new Db4objects.Db4o.Foundation.Network.NetworkSocket(_factory, _hostName, 
				_socket.GetPort());
		}
	}
}

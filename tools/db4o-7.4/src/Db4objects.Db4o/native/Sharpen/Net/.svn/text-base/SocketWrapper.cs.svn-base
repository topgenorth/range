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
using System.Net.Sockets;
using NativeSocket=System.Net.Sockets.Socket;

namespace Sharpen.Net
{
	public class SocketWrapper
	{
		protected NativeSocket _delegate;

#if CF
	    private int _soTimeout = 0;

        public int SoTimeout
        {
            get { return _soTimeout; }
        }
#endif

		public NativeSocket UnderlyingSocket
		{
			get { return _delegate;  }
		}

	    protected virtual void Initialize(NativeSocket socket)
		{
			_delegate = socket;
		}

		public void SetSoTimeout(int timeout)
		{
#if !CF
			_delegate.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, timeout);
			_delegate.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, timeout);
#else
			_soTimeout = timeout;
#endif
		}

		public void Close()
		{
			if (_delegate.Connected)
			{
				try
				{
					_delegate.Shutdown(SocketShutdown.Both);
				}
				catch (Exception)
				{	
				}
			}
			_delegate.Close();
		}

        public bool IsConnected() 
        {
            return _delegate.Connected;
        }
	}
}

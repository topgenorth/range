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
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Internal.CS;
using Db4objects.Db4o.Tests.Common.CS;

namespace Db4objects.Db4o.Tests.Common.CS
{
	public class ClientDisconnectTestCase : Db4oClientServerTestCase, IOptOutAllButNetworkingCS
	{
		public static void Main(string[] arguments)
		{
			new ClientDisconnectTestCase().RunClientServer();
		}

		public virtual void TestDisconnect()
		{
			IExtObjectContainer oc1 = OpenNewClient();
			IExtObjectContainer oc2 = OpenNewClient();
			try
			{
				ClientObjectContainer client1 = (ClientObjectContainer)oc1;
				ClientObjectContainer client2 = (ClientObjectContainer)oc2;
				client1.Socket().Close();
				Assert.IsFalse(oc1.IsClosed());
				Assert.Expect(typeof(Db4oException), new _ICodeBlock_27(client1));
				// It's ok for client2 to get something.
				client2.QueryByExample(null);
			}
			finally
			{
				oc1.Close();
				oc2.Close();
				Assert.IsTrue(oc1.IsClosed());
				Assert.IsTrue(oc2.IsClosed());
			}
		}

		private sealed class _ICodeBlock_27 : ICodeBlock
		{
			public _ICodeBlock_27(ClientObjectContainer client1)
			{
				this.client1 = client1;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				client1.QueryByExample(null);
			}

			private readonly ClientObjectContainer client1;
		}
	}
}

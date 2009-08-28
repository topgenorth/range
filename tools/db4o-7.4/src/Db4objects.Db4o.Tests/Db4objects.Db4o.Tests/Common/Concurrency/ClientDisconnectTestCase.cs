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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Internal.CS;
using Db4objects.Db4o.Tests.Common.Concurrency;

namespace Db4objects.Db4o.Tests.Common.Concurrency
{
	public class ClientDisconnectTestCase : Db4oClientServerTestCase
	{
		public static void Main(string[] arguments)
		{
			new ClientDisconnectTestCase().RunConcurrency();
			new ClientDisconnectTestCase().RunConcurrency();
		}

		/// <exception cref="Exception"></exception>
		public virtual void _concDelete(IExtObjectContainer oc, int seq)
		{
			ClientObjectContainer client = (ClientObjectContainer)oc;
			try
			{
				if (seq % 2 == 0)
				{
					// ok to get something
					client.QueryByExample(null);
				}
				else
				{
					client.Socket().Close();
					Assert.IsFalse(oc.IsClosed());
					Assert.Expect(typeof(Db4oException), new _ICodeBlock_27(client));
				}
			}
			finally
			{
				oc.Close();
				Assert.IsTrue(oc.IsClosed());
			}
		}

		private sealed class _ICodeBlock_27 : ICodeBlock
		{
			public _ICodeBlock_27(ClientObjectContainer client)
			{
				this.client = client;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				client.QueryByExample(null);
			}

			private readonly ClientObjectContainer client;
		}
	}
}

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
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Messaging;
using Db4objects.Db4o.Tests.Common.CS;
using Sharpen;

namespace Db4objects.Db4o.Tests.Common.CS
{
	public class ClientTimeOutTestCase : Db4oClientServerTestCase, IOptOutAllButNetworkingCS
	{
		private const int Timeout = 500;

		internal static bool _clientWasBlocked;

		internal ClientTimeOutTestCase.TestMessageRecipient recipient = new ClientTimeOutTestCase.TestMessageRecipient
			();

		public static void Main(string[] args)
		{
			new ClientTimeOutTestCase().RunAll();
		}

		public class Item
		{
			public string _name;

			public Item(string name)
			{
				_name = name;
			}
		}

		protected override void Configure(IConfiguration config)
		{
			config.ClientServer().TimeoutClientSocket(Timeout);
		}

		public virtual void TestKeptAliveClient()
		{
			ClientTimeOutTestCase.Item item = new ClientTimeOutTestCase.Item("one");
			Store(item);
			Cool.SleepIgnoringInterruption(Timeout * 2);
			Assert.AreSame(item, RetrieveOnlyInstance(typeof(ClientTimeOutTestCase.Item)));
		}

		public virtual void TestTimedoutAndClosedClient()
		{
			Store(new ClientTimeOutTestCase.Item("one"));
			ClientServerFixture().Server().Ext().Configure().ClientServer().SetMessageRecipient
				(recipient);
			IExtObjectContainer client = ClientServerFixture().Db();
			IMessageSender sender = client.Configure().ClientServer().GetMessageSender();
			_clientWasBlocked = false;
			sender.Send(new ClientTimeOutTestCase.Data());
			long start = Runtime.CurrentTimeMillis();
			Assert.Expect(typeof(DatabaseClosedException), new _ICodeBlock_58(client));
			long stop = Runtime.CurrentTimeMillis();
			long duration = stop - start;
			Assert.IsGreaterOrEqual(Timeout / 2, duration);
			Assert.IsTrue(_clientWasBlocked);
		}

		private sealed class _ICodeBlock_58 : ICodeBlock
		{
			public _ICodeBlock_58(IExtObjectContainer client)
			{
				this.client = client;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				client.QueryByExample(null);
			}

			private readonly IExtObjectContainer client;
		}

		public class TestMessageRecipient : IMessageRecipient
		{
			public virtual void ProcessMessage(IMessageContext con, object message)
			{
				_clientWasBlocked = true;
				Cool.SleepIgnoringInterruption(Timeout * 3);
			}
		}

		public class Data
		{
		}
	}
}

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
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Messaging;
using Db4objects.Db4o.Tests.Common.Staging;

namespace Db4objects.Db4o.Tests.Common.Staging
{
	public class PingTestCase : Db4oClientServerTestCase
	{
		public static void Main(string[] args)
		{
			new PingTestCase().RunAll();
		}

		protected override void Configure(IConfiguration config)
		{
			config.ClientServer().TimeoutClientSocket(1000);
		}

		internal PingTestCase.TestMessageRecipient recipient = new PingTestCase.TestMessageRecipient
			();

		public virtual void Test()
		{
			ClientServerFixture().Server().Ext().Configure().ClientServer().SetMessageRecipient
				(recipient);
			IExtObjectContainer client = ClientServerFixture().Db();
			IMessageSender sender = client.Configure().ClientServer().GetMessageSender();
			if (IsMTOC())
			{
				Assert.Expect(typeof(NotSupportedException), new _ICodeBlock_35(sender));
				return;
			}
			sender.Send(new PingTestCase.Data());
			// The following query will be block by the sender
			IObjectSet os = client.QueryByExample(null);
			while (os.HasNext())
			{
				os.Next();
			}
			Assert.IsFalse(client.IsClosed());
		}

		private sealed class _ICodeBlock_35 : ICodeBlock
		{
			public _ICodeBlock_35(IMessageSender sender)
			{
				this.sender = sender;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				sender.Send(new PingTestCase.Data());
			}

			private readonly IMessageSender sender;
		}

		public class TestMessageRecipient : IMessageRecipient
		{
			public virtual void ProcessMessage(IMessageContext con, object message)
			{
				Cool.SleepIgnoringInterruption(3000);
			}
		}

		public class Data
		{
		}
	}
}

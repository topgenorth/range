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
using Db4oUnit;
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.IO;
using Db4objects.Db4o.Messaging;

namespace Db4objects.Db4o.Tests.Common.CS
{
	public class MessagingTestCaseBase : ITestCase, IOptOutCS
	{
		public sealed class MessageCollector : IMessageRecipient
		{
			public readonly Collection4 messages = new Collection4();

			public void ProcessMessage(IMessageContext context, object message)
			{
				messages.Add(message);
			}
		}

		protected virtual IMessageSender MessageSender(IObjectContainer client)
		{
			return client.Ext().Configure().ClientServer().GetMessageSender();
		}

		protected virtual IObjectContainer OpenClient(string clientId, IObjectServer server
			)
		{
			server.GrantAccess(clientId, "p");
			return Db4oFactory.OpenClient(MultithreadedClientConfig(), "127.0.0.1", server.Ext
				().Port(), clientId, "p");
		}

		private IConfiguration MultithreadedClientConfig()
		{
			IConfiguration config = Db4oFactory.NewConfiguration();
			config.ClientServer().SingleThreadedClient(false);
			return config;
		}

		protected virtual IObjectServer OpenServerWith(IMessageRecipient recipient)
		{
			IConfiguration config = MemoryIoConfiguration();
			SetMessageRecipient(config, recipient);
			return OpenServer(config);
		}

		protected virtual IConfiguration MemoryIoConfiguration()
		{
			IConfiguration config = Db4oFactory.NewConfiguration();
			config.Io(new MemoryIoAdapter());
			return config;
		}

		protected virtual IObjectServer OpenServer(IConfiguration config)
		{
			return Db4oFactory.OpenServer(config, "nofile", unchecked((int)(0xdb40)));
		}

		protected virtual void SetMessageRecipient(IObjectContainer container, IMessageRecipient
			 recipient)
		{
			SetMessageRecipient(container.Ext().Configure(), recipient);
		}

		private void SetMessageRecipient(IConfiguration config, IMessageRecipient recipient
			)
		{
			config.ClientServer().SetMessageRecipient(recipient);
		}
	}
}

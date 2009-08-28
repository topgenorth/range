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
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o;
using Db4objects.Db4o.Tests.Common.CS;

namespace Db4objects.Db4o.Tests.Common.CS
{
	public class ServerRevokeAccessTestCase : Db4oClientServerTestCase, IOptOutAllButNetworkingCS
	{
		private static readonly string ServerHostname = "127.0.0.1";

		public static void Main(string[] args)
		{
			new ServerRevokeAccessTestCase().RunAll();
		}

		#if !CF
		/// <exception cref="IOException"></exception>
		public virtual void Test()
		{
			string user = "hohohi";
			string password = "hohoho";
			IObjectServer server = ClientServerFixture().Server();
			server.GrantAccess(user, password);
			IObjectContainer con = Db4oFactory.OpenClient(ServerHostname, ClientServerFixture
				().ServerPort(), user, password);
			Assert.IsNotNull(con);
			con.Close();
			server.Ext().RevokeAccess(user);
			Assert.Expect(typeof(Exception), new _ICodeBlock_39(this, user, password));
		}
		#endif // !CF

		private sealed class _ICodeBlock_39 : ICodeBlock
		{
			public _ICodeBlock_39(ServerRevokeAccessTestCase _enclosing, string user, string 
				password)
			{
				this._enclosing = _enclosing;
				this.user = user;
				this.password = password;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				Db4oFactory.OpenClient(ServerRevokeAccessTestCase.ServerHostname, this._enclosing
					.ClientServerFixture().ServerPort(), user, password);
			}

			private readonly ServerRevokeAccessTestCase _enclosing;

			private readonly string user;

			private readonly string password;
		}
	}
}

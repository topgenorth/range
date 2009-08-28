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
using System.Collections;
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal.CS;
using Db4objects.Db4o.Tests.Common.CS;

namespace Db4objects.Db4o.Tests.Common.CS
{
	public class ServerClosedTestCase : Db4oClientServerTestCase, IOptOutAllButNetworkingCS
	{
		public static void Main(string[] args)
		{
			new ServerClosedTestCase().RunAll();
		}

		/// <exception cref="Exception"></exception>
		public virtual void Test()
		{
			IExtObjectContainer db = Fixture().Db();
			ObjectServerImpl serverImpl = (ObjectServerImpl)ClientServerFixture().Server();
			try
			{
				IEnumerator iter = serverImpl.IterateDispatchers();
				iter.MoveNext();
				ServerMessageDispatcherImpl serverDispatcher = (ServerMessageDispatcherImpl)iter.
					Current;
				serverDispatcher.Socket().Close();
				Cool.SleepIgnoringInterruption(1000);
				Assert.Expect(typeof(DatabaseClosedException), new _ICodeBlock_30(db));
				Assert.IsTrue(db.IsClosed());
			}
			finally
			{
				serverImpl.Close();
			}
		}

		private sealed class _ICodeBlock_30 : ICodeBlock
		{
			public _ICodeBlock_30(IExtObjectContainer db)
			{
				this.db = db;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				db.QueryByExample(null);
			}

			private readonly IExtObjectContainer db;
		}
	}
}

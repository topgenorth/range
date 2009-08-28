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
using Db4objects.Db4o.Tests.Common.CS;

namespace Db4objects.Db4o.Tests.Common.CS
{
	public class CallConstructorsConfigTestCase : StandaloneCSTestCaseBase
	{
		/// <exception cref="Exception"></exception>
		protected override void RunTest()
		{
			WithClient(new _IContainerBlock_15());
			WithClient(new _IContainerBlock_21());
		}

		private sealed class _IContainerBlock_15 : IContainerBlock
		{
			public _IContainerBlock_15()
			{
			}

			public void Run(IObjectContainer client)
			{
				client.Store(new StandaloneCSTestCaseBase.Item());
			}
		}

		private sealed class _IContainerBlock_21 : IContainerBlock
		{
			public _IContainerBlock_21()
			{
			}

			public void Run(IObjectContainer client)
			{
				Assert.AreEqual(1, client.Query(typeof(StandaloneCSTestCaseBase.Item)).Size());
			}
		}

		protected override void Configure(IConfiguration config)
		{
			config.CallConstructors(true);
			config.ExceptionsOnNotStorable(true);
		}
	}
}

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
using Db4objects.Db4o.Events;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Tests.Common.Events;

namespace Db4objects.Db4o.Tests.Common.Events
{
	public class EventArgsTransactionTestCase : AbstractDb4oTestCase
	{
		public class Item
		{
		}

		public virtual void TestTransactionInEventArgs()
		{
			IEventRegistry factory = EventRegistryFactory.ForObjectContainer(Db());
			BooleanByRef called = new BooleanByRef();
			ObjectByRef foundTrans = new ObjectByRef();
			factory.Creating += new Db4objects.Db4o.Events.CancellableObjectEventHandler(new 
				_IEventListener4_20(called, foundTrans).OnEvent);
			Db().Store(new EventArgsTransactionTestCase.Item());
			Db().Commit();
			Assert.IsTrue(called.value);
			Assert.AreSame(Trans(), foundTrans.value);
		}

		private sealed class _IEventListener4_20
		{
			public _IEventListener4_20(BooleanByRef called, ObjectByRef foundTrans)
			{
				this.called = called;
				this.foundTrans = foundTrans;
			}

			public void OnEvent(object sender, Db4objects.Db4o.Events.CancellableObjectEventArgs
				 args)
			{
				called.value = true;
				foundTrans.value = ((TransactionalEventArgs)args).Transaction();
			}

			private readonly BooleanByRef called;

			private readonly ObjectByRef foundTrans;
		}

		public static void Main(string[] args)
		{
			new EventArgsTransactionTestCase().RunAll();
		}
	}
}

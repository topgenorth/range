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
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Events;
using Db4objects.Db4o.Tests.Common.Events;

namespace Db4objects.Db4o.Tests.Common.Events
{
	public class InstantiationEventsTestCase : EventsTestCaseBase
	{
		protected override void Configure(IConfiguration config)
		{
			config.ActivationDepth(0);
		}

		public virtual void TestInstantiationEvents()
		{
			EventsTestCaseBase.EventLog instantiatedLog = new EventsTestCaseBase.EventLog();
			EventRegistry().Instantiated += new Db4objects.Db4o.Events.ObjectEventHandler(new 
				_IEventListener4_19(this, instantiatedLog).OnEvent);
			RetrieveOnlyInstance(typeof(EventsTestCaseBase.Item));
			Assert.IsFalse(instantiatedLog.xing);
			Assert.IsTrue(instantiatedLog.xed);
		}

		private sealed class _IEventListener4_19
		{
			public _IEventListener4_19(InstantiationEventsTestCase _enclosing, EventsTestCaseBase.EventLog
				 instantiatedLog)
			{
				this._enclosing = _enclosing;
				this.instantiatedLog = instantiatedLog;
			}

			public void OnEvent(object sender, Db4objects.Db4o.Events.ObjectEventArgs args)
			{
				instantiatedLog.xed = true;
				object obj = ((ObjectEventArgs)args).Object;
				Assert.IsNotNull(this._enclosing.Trans().ReferenceSystem().ReferenceForObject(obj
					));
			}

			private readonly InstantiationEventsTestCase _enclosing;

			private readonly EventsTestCaseBase.EventLog instantiatedLog;
		}
	}
}

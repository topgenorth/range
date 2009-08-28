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
using Db4oUnit.Extensions.Util;
using Db4objects.Db4o.Events;
using Db4objects.Db4o.Tests.Common.Events;

namespace Db4objects.Db4o.Tests.Common.Events
{
	public class ClassRegistrationEventsTestCase : AbstractDb4oTestCase
	{
		public class Data
		{
		}

		private class EventFlag
		{
			public bool eventOccurred = false;
		}

		public virtual void TestClassRegistrationEvents()
		{
			ClassRegistrationEventsTestCase.EventFlag eventFlag = new ClassRegistrationEventsTestCase.EventFlag
				();
			IEventRegistry registry = EventRegistryFactory.ForObjectContainer(Db());
			registry.ClassRegistered += new Db4objects.Db4o.Events.ClassEventHandler(new _IEventListener4_23
				(eventFlag).OnEvent);
			Store(new ClassRegistrationEventsTestCase.Data());
			Assert.IsTrue(eventFlag.eventOccurred);
		}

		private sealed class _IEventListener4_23
		{
			public _IEventListener4_23(ClassRegistrationEventsTestCase.EventFlag eventFlag)
			{
				this.eventFlag = eventFlag;
			}

			public void OnEvent(object sender, Db4objects.Db4o.Events.ClassEventArgs args)
			{
				ClassEventArgs classEventArgs = (ClassEventArgs)args;
				Assert.AreEqual(typeof(ClassRegistrationEventsTestCase.Data).FullName, CrossPlatformServices
					.SimpleName(classEventArgs.ClassMetadata().GetName()));
				eventFlag.eventOccurred = true;
			}

			private readonly ClassRegistrationEventsTestCase.EventFlag eventFlag;
		}
	}
}

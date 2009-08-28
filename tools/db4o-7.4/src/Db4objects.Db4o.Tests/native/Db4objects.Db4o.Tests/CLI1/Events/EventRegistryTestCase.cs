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
using Db4objects.Db4o.Events;
using Db4objects.Db4o.Ext;
using Db4oUnit;
using Db4oUnit.Extensions;

namespace Db4objects.Db4o.Tests.CLI1.Events
{
	public class EventRegistryTestCase : AbstractDb4oTestCase
	{
		class Item
		{	
		}
		
		class EventRecorder
		{
			ArrayList _events = new ArrayList();

			public EventRecorder(IExtObjectContainer container)
			{
				IEventRegistry registry = EventRegistryFactory.ForObjectContainer(container);
				registry.Creating += new CancellableObjectEventHandler(OnCreating);
			}
			
			public string this[int index]
			{
				get { return (string)_events[index];  }
			}

			void OnCreating(object sender, CancellableObjectEventArgs args)
			{
				_events.Add("Creating");
				Assert.IsFalse(args.IsCancelled);
				args.Cancel();
			}
		}
		
		public void TestCreating()
		{
			EventRecorder recorder = new EventRecorder(Db());

			Store(new Item());

			Assert.AreEqual(0, Db().Get(typeof(Item)).Count);
			Assert.AreEqual("Creating", recorder[0]);
		}
	}
}

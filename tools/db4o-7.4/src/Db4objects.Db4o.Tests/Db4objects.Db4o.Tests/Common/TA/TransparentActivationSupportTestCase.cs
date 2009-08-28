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
using Db4objects.Db4o.Activation;
using Db4objects.Db4o.Events;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal.Activation;
using Db4objects.Db4o.Tests.Common.TA;

namespace Db4objects.Db4o.Tests.Common.TA
{
	public class TransparentActivationSupportTestCase : TransparentActivationTestCaseBase
	{
		public static void Main(string[] args)
		{
			new TransparentActivationSupportTestCase().RunAll();
		}

		public virtual void TestActivationDepth()
		{
			Assert.IsInstanceOf(typeof(TransparentActivationDepthProvider), Stream().ConfigImpl
				().ActivationDepthProvider());
		}

		public sealed class Item : ActivatableImpl
		{
			public void Update()
			{
				this.Activate(ActivationPurpose.Write);
			}

			internal Item(TransparentActivationSupportTestCase _enclosing)
			{
				this._enclosing = _enclosing;
			}

			private readonly TransparentActivationSupportTestCase _enclosing;
		}

		public virtual void TestTransparentActivationDoesNotImplyTransparentUpdate()
		{
			TransparentActivationSupportTestCase.Item item = new TransparentActivationSupportTestCase.Item
				(this);
			Db().Store(item);
			Db().Commit();
			item.Update();
			Collection4 updated = CommitCapturingUpdatedObjects(Db());
			Assert.AreEqual(0, updated.Size());
		}

		private Collection4 CommitCapturingUpdatedObjects(IExtObjectContainer container)
		{
			Collection4 updated = new Collection4();
			EventRegistryFor(container).Updated += new Db4objects.Db4o.Events.ObjectEventHandler
				(new _IEventListener4_41(updated).OnEvent);
			container.Commit();
			return updated;
		}

		private sealed class _IEventListener4_41
		{
			public _IEventListener4_41(Collection4 updated)
			{
				this.updated = updated;
			}

			public void OnEvent(object sender, Db4objects.Db4o.Events.ObjectEventArgs args)
			{
				ObjectEventArgs objectArgs = (ObjectEventArgs)args;
				updated.Add(objectArgs.Object);
			}

			private readonly Collection4 updated;
		}
	}
}

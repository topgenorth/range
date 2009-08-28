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
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Events;
using Db4objects.Db4o.Tests.Common.Events;

namespace Db4objects.Db4o.Tests.Common.Events
{
	public class DeletionEventExceptionTestCase : EventsTestCaseBase, IOptOutSolo
	{
		public static void Main(string[] args)
		{
			new DeletionEventExceptionTestCase().RunAll();
		}

		protected override void Configure(IConfiguration config)
		{
			config.ActivationDepth(1);
		}

		public virtual void TestDeletionEvents()
		{
			ServerEventRegistry().Deleting += new Db4objects.Db4o.Events.CancellableObjectEventHandler
				(new _IEventListener4_26().OnEvent);
			object item = RetrieveOnlyInstance(typeof(EventsTestCaseBase.Item));
			if (IsMTOC())
			{
				Assert.Expect(typeof(EventException), new _ICodeBlock_33(this, item));
			}
			else
			{
				Db().Delete(item);
			}
			Db().Commit();
		}

		private sealed class _IEventListener4_26
		{
			public _IEventListener4_26()
			{
			}

			public void OnEvent(object sender, Db4objects.Db4o.Events.CancellableObjectEventArgs
				 args)
			{
				throw new Exception();
			}
		}

		private sealed class _ICodeBlock_33 : ICodeBlock
		{
			public _ICodeBlock_33(DeletionEventExceptionTestCase _enclosing, object item)
			{
				this._enclosing = _enclosing;
				this.item = item;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				this._enclosing.Db().Delete(item);
			}

			private readonly DeletionEventExceptionTestCase _enclosing;

			private readonly object item;
		}
	}
}

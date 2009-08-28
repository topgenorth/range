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
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Events;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Tests.Common.TA.Collections;

namespace Db4objects.Db4o.Tests.Common.TA.Collections
{
	/// <summary>Configures the support for paged collections.</summary>
	/// <remarks>Configures the support for paged collections.</remarks>
	public class PagedListSupport : IConfigurationItem
	{
		public virtual void Apply(IInternalObjectContainer db)
		{
			EventRegistry(db).Updating += new Db4objects.Db4o.Events.CancellableObjectEventHandler
				(new _IEventListener4_17().OnEvent);
		}

		private sealed class _IEventListener4_17
		{
			public _IEventListener4_17()
			{
			}

			public void OnEvent(object sender, Db4objects.Db4o.Events.CancellableObjectEventArgs
				 args)
			{
				CancellableObjectEventArgs cancellable = (CancellableObjectEventArgs)args;
				if (cancellable.Object is Page)
				{
					Page page = (Page)cancellable.Object;
					if (!page.IsDirty())
					{
						cancellable.Cancel();
					}
				}
			}
		}

		private static IEventRegistry EventRegistry(IObjectContainer db)
		{
			return EventRegistryFactory.ForObjectContainer(db);
		}

		public virtual void Prepare(IConfiguration configuration)
		{
		}
		// Nothing to do...
	}
}

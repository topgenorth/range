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
using Db4oUnit.Extensions;
using Db4objects.Db4o.Tests.Common.Fatalerror;

namespace Db4objects.Db4o.Tests.Common.Fatalerror
{
	public class FatalExceptionInNestedCallTestCase : AbstractDb4oTestCase
	{
		public static void Main(string[] arguments)
		{
			new FatalExceptionInNestedCallTestCase().RunSolo();
		}

		public class Item
		{
			public FatalExceptionInNestedCallTestCase.Item _child;

			public int _depth;

			public Item()
			{
			}

			public Item(FatalExceptionInNestedCallTestCase.Item child, int depth)
			{
				_child = child;
				_depth = depth;
			}
		}

		[System.Serializable]
		public class FatalError : Exception
		{
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			FatalExceptionInNestedCallTestCase.Item childItem = new FatalExceptionInNestedCallTestCase.Item
				(null, 1);
			FatalExceptionInNestedCallTestCase.Item parentItem = new FatalExceptionInNestedCallTestCase.Item
				(childItem, 0);
			Store(parentItem);
		}

		public virtual void Test()
		{
		}
		//	private EventRegistry eventRegistry() {
		//		return EventRegistryFactory.forObjectContainer(db());
		//	}
	}
}

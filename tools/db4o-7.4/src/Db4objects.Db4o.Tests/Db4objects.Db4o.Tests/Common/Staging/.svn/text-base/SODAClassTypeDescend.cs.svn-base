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
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Staging;

namespace Db4objects.Db4o.Tests.Common.Staging
{
	public class SODAClassTypeDescend : AbstractDb4oTestCase
	{
		public class DataA
		{
			public SODAClassTypeDescend.DataB _val;
			// COR-471
		}

		public class DataB
		{
			public SODAClassTypeDescend.DataA _val;
		}

		public class DataC
		{
			public SODAClassTypeDescend.DataC _next;
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			SODAClassTypeDescend.DataA objectA = new SODAClassTypeDescend.DataA();
			SODAClassTypeDescend.DataB objectB = new SODAClassTypeDescend.DataB();
			objectA._val = objectB;
			objectB._val = objectA;
			Store(objectB);
			// just to show that the descend to "_val" actually is
			// recognized - this one doesn't show up in the result
			Store(new SODAClassTypeDescend.DataC());
		}

		public virtual void TestFieldConstrainedToType()
		{
			IQuery query = NewQuery();
			query.Descend("_val").Constrain(typeof(SODAClassTypeDescend.DataA));
			IObjectSet result = query.Execute();
			Assert.AreEqual(1, result.Size());
			Assert.IsInstanceOf(typeof(SODAClassTypeDescend.DataB), result.Next());
		}
	}
}

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
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Fieldindex;

namespace Db4objects.Db4o.Tests.Common.Fieldindex
{
	public abstract class FieldIndexTestCaseBase : AbstractDb4oTestCase, IOptOutCS
	{
		public FieldIndexTestCaseBase() : base()
		{
		}

		protected override void Configure(IConfiguration config)
		{
			IndexField(config, typeof(FieldIndexItem), "foo");
		}

		protected abstract override void Store();

		protected virtual void StoreItems(int[] foos)
		{
			for (int i = 0; i < foos.Length; i++)
			{
				Store(new FieldIndexItem(foos[i]));
			}
		}

		protected virtual IQuery CreateQuery(int id)
		{
			IQuery q = CreateItemQuery();
			q.Descend("foo").Constrain(id);
			return q;
		}

		protected virtual IQuery CreateItemQuery()
		{
			return CreateQuery(typeof(FieldIndexItem));
		}

		protected virtual IQuery CreateQuery(Type clazz)
		{
			return CreateQuery(Trans(), clazz);
		}

		protected virtual IQuery CreateQuery(Transaction trans, Type clazz)
		{
			IQuery q = CreateQuery(trans);
			q.Constrain(clazz);
			return q;
		}

		protected virtual IQuery CreateItemQuery(Transaction trans)
		{
			IQuery q = CreateQuery(trans);
			q.Constrain(typeof(FieldIndexItem));
			return q;
		}

		private IQuery CreateQuery(Transaction trans)
		{
			return Container().Query(trans);
		}
	}
}

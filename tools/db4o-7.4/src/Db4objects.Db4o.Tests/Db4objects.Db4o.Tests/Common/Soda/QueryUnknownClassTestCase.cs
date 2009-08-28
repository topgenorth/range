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
using System.Collections;
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Soda;

namespace Db4objects.Db4o.Tests.Common.Soda
{
	public class QueryUnknownClassTestCase : AbstractDb4oTestCase
	{
		public class Data
		{
			public int _id;

			public Data(int id)
			{
				_id = id;
			}
		}

		public class DataHolder
		{
			public ArrayList _data;

			public DataHolder(object data)
			{
				_data = new ArrayList();
				_data.Add(data);
			}
		}

		public class Unrelated
		{
			public int _uid;

			public Unrelated(int id)
			{
				_uid = id;
			}
		}

		public virtual void TestQueryUnknownClass()
		{
			IQuery query = NewQuery(typeof(QueryUnknownClassTestCase.Data));
			query.Descend("_id").Constrain(42);
			IObjectSet result = query.Execute();
			Assert.AreEqual(0, result.Size());
		}

		public virtual void TestQueryUnknownClassInUnknownCollection()
		{
			IQuery query = NewQuery(typeof(QueryUnknownClassTestCase.DataHolder));
			query.Descend("_data").Descend("_id").Constrain(42);
			IObjectSet result = query.Execute();
			Assert.AreEqual(0, result.Size());
		}

		public virtual void _testQueryUnknownClassInCollection()
		{
			Store(new QueryUnknownClassTestCase.DataHolder(new QueryUnknownClassTestCase.Unrelated
				(42)));
			IQuery query = NewQuery(typeof(QueryUnknownClassTestCase.DataHolder));
			query.Descend("_data").Descend("_id").Constrain(42);
			IObjectSet result = query.Execute();
			Assert.AreEqual(0, result.Size());
		}

		public virtual void _testQueryUnknownClassInCollectionConjunction()
		{
			Store(new QueryUnknownClassTestCase.DataHolder(new QueryUnknownClassTestCase.Unrelated
				(42)));
			IQuery query = NewQuery(typeof(QueryUnknownClassTestCase.DataHolder));
			query.Descend("_data").Descend("_id").Constrain(42).And(query.Descend("_data").Descend
				("_uid").Constrain(42));
			IObjectSet result = query.Execute();
			Assert.AreEqual(0, result.Size());
		}

		public virtual void TestQueryUnknownClassInCollectionDisjunction()
		{
			Store(new QueryUnknownClassTestCase.DataHolder(new QueryUnknownClassTestCase.Unrelated
				(42)));
			IQuery query = NewQuery(typeof(QueryUnknownClassTestCase.DataHolder));
			query.Descend("_data").Descend("_id").Constrain(42).Or(query.Descend("_data").Descend
				("_uid").Constrain(42));
			IObjectSet result = query.Execute();
			Assert.AreEqual(1, result.Size());
		}
	}
}

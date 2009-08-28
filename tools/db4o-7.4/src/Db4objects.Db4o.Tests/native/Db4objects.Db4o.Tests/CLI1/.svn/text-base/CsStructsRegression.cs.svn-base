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
using Db4objects.Db4o.Query;
using Db4oUnit;
using Db4oUnit.Extensions;

namespace Db4objects.Db4o.Tests.CLI1
{
	public class CsStructsRegression : AbstractDb4oTestCase
	{
		override protected void Store()
		{
			Store(new Item());
			Store(new Item(1));
			Store(new Item(2));
		}

		public void TestConstrainOnNullableValue()
		{
			CheckQueryById(1);
			CheckQueryById(2);
		}

		private void CheckQueryById(int id)
		{
			IObjectSet os = QueryById(id);
			Assert.AreEqual(1, os.Size());
			Assert.AreEqual(id, ((Item)os.Next()).Id);
		}

		private IObjectSet QueryById(int id)
		{
            IQuery q = NewQuery(typeof(Item));
			q.Descend("_id").Descend("_value").Constrain(id);
			return q.Execute();
		}
	}

	public class Item
	{
		NullableInt32 _id;

		public Item(int id)
		{
			_id = new NullableInt32(id);
		}

		public Item()
		{	
		}

		public int Id
		{
			get
			{
				return _id.Value;
			}
		}
	}

	public struct NullableInt32
	{
		private int _value;
		private bool _hasValue;

		public NullableInt32(int value)
		{
			_value = value;
			_hasValue = true;
		}

		public int Value
		{
			get { return _value; }
		}

		public bool HasValue
		{
			get { return _hasValue; }
		}
	}
}

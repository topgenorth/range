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

namespace Db4objects.Db4o.Tests.CLI1.NativeQueries
{
	class DoubleItem
	{
		private string _name;
		private double _value;

		public DoubleItem(string name, double value)
		{
			_name = name;
			_value = value;
		}

		public string Name
		{
			get { return _name;  }
		}

		public double Value
		{
			get { return _value;  }
		}


		public override string ToString()
		{
			return "DoubleItem(" + Name + ", " + Value + ")";
		}
	}

	class DoublePredicate
	{
		public bool Match(DoubleItem item)
		{
			return item.Value == 41.99;
		}
	}

	class DoubleNQTestCase : AbstractNativeQueriesTestCase
	{
		protected override void Store()
		{
			Store(new DoubleItem("foo", 11.5));
			Store(new DoubleItem("bar", 41.99));
		}

		public void Test()
		{
			AssertNQResult(new DoublePredicate(), ItemByName("bar"));
		}

		private object ItemByName(string name)
		{
			IQuery query = NewQuery(typeof(DoubleItem));
			query.Descend("_name").Constrain(name);
			return query.Execute().Next();
		}
	}
}

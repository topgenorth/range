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
namespace Db4objects.Db4o.Linq.Tests
{
	class QueryReuseTestCase : AbstractDb4oLinqTestCase
	{
		public class Item
		{
			public int Id;

			public Item(int id)
			{
				Id = id;
			}
		}

		protected override void Store()
		{	
			Store(new Item(1));
		}

		public void TestQueryCanBeReused()
		{
			// Db().Cast<Item>().Select(item => item.Id)
			//   Db4oQuery() { q => q.Constrain(typeof(Item) }
			//      Db4oProjection() => { query = ...; projection = lambda }
			var query = from Item item in Db() select item.Id;

			AssertItemQuery(query);
			AssertItemQuery(query);
		}

		private void AssertItemQuery(IDb4oLinqQuery<int> query)
		{
			AssertQuery("(Item)", () =>
			{
				AssertSequence(new[] { 1 }, query);
			});
		}

		public void TestQueryCanBeComposed()
		{
		}
	}
}

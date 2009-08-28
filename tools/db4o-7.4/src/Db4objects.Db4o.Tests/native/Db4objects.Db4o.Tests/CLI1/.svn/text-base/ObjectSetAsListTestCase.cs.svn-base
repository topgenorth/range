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
using System.Collections;
using Db4oUnit;
using Db4oUnit.Extensions;

namespace Db4objects.Db4o.Tests.CLI1
{
	/// <summary>
	/// Tests ObjectSet's IList functionality.
	/// </summary>
	public class ObjectSetAsListTestCase : AbstractDb4oTestCase
	{
		public class Item
		{	
			int _value;

			public Item()
			{	
			}

			public Item(int value)
			{
				_value = value;
			}

			public override bool Equals(object other)
			{
				Item rhs = other as Item;
				return null == rhs ? false : rhs._value == _value;
			}

			public int Value
			{
				get { return _value; }
			}

            public override string ToString()
            {
                return "Item(" + _value + ")";
            }
		}

		protected override void Store()
		{
			Db().Store(new Item(42));
			Db().Store(new Item(1));
		}

		public void TestEnumerable()
		{
			IObjectSet os = GetObjectSet();
			for (int i=0; i<2; ++i)
			{
				int sum = 0;
				foreach (Item item in os)
				{
					sum += item.Value;
				}
				Assert.AreEqual(43, sum);
			}
		}

		public void TestList()
		{
			IList os = GetObjectSet();
			Assert.AreEqual(2, os.Count);

			int sum = 0;
			for (int i=0; i<os.Count; ++i)
			{
				Item item = (Item)os[i];
				sum += item.Value;
			}
			Assert.AreEqual(43, sum);
		}
		
		public void TestIndexOfAndContains()
		{
			IList os = GetObjectSet();

			Assert.AreEqual(0, os.IndexOf(os[0]));
			Assert.AreEqual(1, os.IndexOf(os[1]));
			Assert.IsTrue(os.Contains(os[0]));
			Assert.IsTrue(os.Contains(os[1]));
			Assert.IsFalse(os.Contains(new Item(42)), "Contains is not by value");
		}

		public void TestCopyTo()
		{
			IList os = GetObjectSet();
			Item[] items = new Item[2];
			os.CopyTo(items, 0);
		    AssertArray(os, items);
		}
	    
	    private static void AssertArray(IList os, Item[] items)
	    {
	        Assert.AreEqual(items[0], os[0]);
	        Assert.AreEqual(items[1], os[1]);
	    }

	    private IObjectSet GetObjectSet()
		{
			return Db().Get(typeof(Item));
		}
	}
}
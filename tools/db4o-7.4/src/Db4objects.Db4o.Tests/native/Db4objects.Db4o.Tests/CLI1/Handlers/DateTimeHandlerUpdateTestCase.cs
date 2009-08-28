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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Tests.Common.Handlers;
using Db4oUnit;

namespace Db4objects.Db4o.Tests.CLI1.Handlers
{
    class DateTimeHandlerUpdateTestCase : HandlerUpdateTestCaseBase
    {
        public class Item
        {
            public DateTime _dateTime;

            public Object _untyped;
            
			public DateTime? _nullableDateTime;
        }

        public class ItemArrays
        {
            public DateTime[] _dateTimeArray;

            public object[] _untypedObjectArray;

            public object _arrayInObject;

			public DateTime?[] _nullableDateTimeArray;
        }

        private static readonly DateTime[] data = new DateTime[] {new DateTime(DateTime.MinValue.Ticks),
            new DateTime(DateTime.MinValue.Ticks + 1), new DateTime(5), 
            new DateTime(DateTime.MaxValue.Ticks - 1), new DateTime(DateTime.MaxValue.Ticks),
        };

        protected override void AssertArrays(IExtObjectContainer objectContainer, object obj)
        {
            ItemArrays itemArrays = (ItemArrays)obj;
            DateTime[] dateTimeArray = (DateTime[])itemArrays._arrayInObject;
            for (int i = 0; i < data.Length; i++)
            {
                AssertAreEqual(data[i], itemArrays._dateTimeArray[i]);
                AssertAreEqual(data[i], (DateTime) itemArrays._untypedObjectArray[i]);
                AssertAreEqual(data[i], dateTimeArray[i]);
                //FIXME: Cannot retrieve nullable struct array.
                //AssertAreEqual(data[i], (DateTime)itemArrays._nullableDateTimeArray[i]);
            }

            Assert.IsNull(itemArrays._untypedObjectArray[data.Length]);
            AssertAreEqual(new DateTime(0), itemArrays._dateTimeArray[data.Length]);
            AssertAreEqual(new DateTime(0), dateTimeArray[data.Length]);
            //FIXME: Cannot retrieve nullable struct array.
            //Assert.IsNull(itemArrays._nullableDateTimeArray[data.Length]);
        }

        protected override void AssertValues(IExtObjectContainer objectContainer, object[] values)
        {
            for (int i = 0; i < data.Length; i++)
            {
                Item item = (Item)values[i];
                AssertAreEqual(data[i], item._dateTime);
                AssertAreEqual(data[i], (DateTime) item._untyped);
                AssertAreEqual(data[i], (DateTime) item._nullableDateTime);
			}

            Item nullItem = (Item) values[values.Length - 1];

            AssertAreEqual(new DateTime(0), nullItem._dateTime);
            Assert.IsNull(nullItem._untyped);
            Assert.IsNull(nullItem._nullableDateTime);
		}

        private void AssertAreEqual(DateTime expected, DateTime actual)
        {
            Assert.AreEqual(expected, actual);
        }

        protected override object CreateArrays()
        {
            ItemArrays itemArrays = new ItemArrays();
            itemArrays._dateTimeArray = new DateTime[data.Length + 1];
            System.Array.Copy(data, 0, itemArrays._dateTimeArray, 0, data.Length);

            itemArrays._untypedObjectArray = new object[data.Length + 1];
            System.Array.Copy(data, 0, itemArrays._untypedObjectArray, 0, data.Length);

            DateTime[] dateTimeArray = new DateTime[data.Length + 1];
            System.Array.Copy(data, 0, dateTimeArray, 0, data.Length);
            itemArrays._arrayInObject = dateTimeArray;
            
            itemArrays._nullableDateTimeArray = new DateTime?[data.Length + 1];
            for (int i = 0; i < data.Length; i++)
            {
                itemArrays._nullableDateTimeArray[i] = data[i];
            }
			return itemArrays;
        }

        protected override object[] CreateValues()
        {
            Item[] values = new Item[data.Length + 1];
            for (int i = 0; i < data.Length; i++)
            {
                Item item = new Item();
                item._dateTime = data[i];
                item._untyped = data[i];
                item._nullableDateTime = data[i];
				values[i] = item;
            }
            values[values.Length - 1] = new Item();
            return values;
        }

        protected override string TypeName()
        {
            return "datetime";
        }
    }
}

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
using System.Text;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Tests.Common.Handlers;
using Db4oUnit;

namespace Db4objects.Db4o.Tests.CLI1.Handlers
{
    class SByteHandlerUpdateTestCase : HandlerUpdateTestCaseBase
    {
        public class Item
        {
            public sbyte _typedPrimitive;

            public object _untyped;

			public sbyte? _nullablePrimitive;
        }

        public class ItemArrays
        {
            public sbyte[] _typedPrimitiveArray;

            public object _primitiveArrayInObject;

			public sbyte?[] _nullableTypedPrimitiveArray;
        }

        private static readonly sbyte[] data = new sbyte[] {
            sbyte.MinValue,
            sbyte.MinValue + 1,
            -5,
            -1,
            0,
            1,
            5,
            sbyte.MaxValue - 1,
            sbyte.MaxValue,
        };

        protected override void AssertArrays(IExtObjectContainer objectContainer, object obj)
        {
            ItemArrays itemArrays = (ItemArrays)obj;
            for (int i = 0; i < data.Length; i++)
            {
                AssertAreEqual(data[i], itemArrays._typedPrimitiveArray[i]);
                AssertAreEqual(data[i], ((sbyte[])itemArrays._primitiveArrayInObject)[i]);
                //AssertAreEqual(data[i], (sbyte)itemArrays._nullableTypedPrimitiveArray[i]);
            }
            AssertAreEqual(0, itemArrays._typedPrimitiveArray[data.Length]);
            AssertAreEqual(0, ((sbyte[])itemArrays._primitiveArrayInObject)[data.Length]);
        }

        protected override void AssertValues(IExtObjectContainer objectContainer, object[] values)
        {
            for (int i = 0; i < data.Length; i++)
            {
                Item item = (Item)values[i];
                AssertAreEqual(data[i], item._typedPrimitive);
                AssertAreEqual(data[i], (sbyte) item._untyped);
                AssertAreEqual(data[i], (sbyte) item._nullablePrimitive);
            }
            Item nullItem = (Item)values[data.Length];
            AssertAreEqual(0, nullItem._typedPrimitive);
            Assert.IsNull(nullItem._untyped);
            Assert.IsNull(nullItem._nullablePrimitive);
		}

        private void AssertAreEqual(sbyte expected, sbyte actual)
        {
            Assert.AreEqual(expected, actual);
        }

        protected override object CreateArrays()
        {
            ItemArrays itemArrays = new ItemArrays();
            itemArrays._typedPrimitiveArray = new sbyte[data.Length + 1];
            System.Array.Copy(data, 0, itemArrays._typedPrimitiveArray, 0, data.Length);

            sbyte[] sbyteArray = new sbyte[data.Length + 1];
            System.Array.Copy(data, 0, sbyteArray, 0, data.Length);
            itemArrays._primitiveArrayInObject = sbyteArray;
            itemArrays._nullableTypedPrimitiveArray = new sbyte?[data.Length + 1];
            for (int i = 0; i < data.Length; i++)
            {
                itemArrays._nullableTypedPrimitiveArray[i] = data[i];
            }
			return itemArrays;
        }

        protected override object[] CreateValues()
        {
            Item[] values = new Item[data.Length + 1];
            for (int i = 0; i < data.Length; i++)
            {
                Item item = new Item();
                item._typedPrimitive = data[i];
                item._untyped = data[i];
                item._nullablePrimitive = data[i];
				values[i] = item;
            }

            values[data.Length] = new Item();
            return values;
        }

        protected override string TypeName()
        {
            return "sbyte";
        }
    }

}

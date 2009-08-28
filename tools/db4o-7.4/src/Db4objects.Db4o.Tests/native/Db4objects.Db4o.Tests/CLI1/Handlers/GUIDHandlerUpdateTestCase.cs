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
    class GUIDHandlerUpdateTestCase : HandlerUpdateTestCaseBase
    {

        public class Item
        {
            public Guid _Guid;

            public Object _untyped;

			public Guid? _nullableGuid;
        }

        public class ItemArrays
        {
            public Guid[] _GuidArray;

            public object[] _untypedObjectArray;

            public object _arrayInObject;

			public Guid?[] _nullableGuidArray;
        }

        private static readonly Guid[] data = new Guid[] {
            Guid.Empty,
            new Guid("a17b9ce4-e7f7-464c-a7d8-598a229f6a0c"),
            new Guid("c2e5944a-b630-4cad-b49e-8e261a08c14c"),
            new Guid("7e255e48-5320-4b1f-86da-2d680aaed914"),
            new Guid("9d33da58-44ae-44c3-b719-4c006be0cb44"),
        };

        protected override void AssertArrays(IExtObjectContainer objectContainer, object obj)
        {
            ItemArrays itemArrays = (ItemArrays)obj;
            Guid[] GuidArray = (Guid[])itemArrays._arrayInObject;
            for (int i = 0; i < data.Length; i++)
            {
                AssertAreEqual(data[i], itemArrays._GuidArray[i]);
                AssertAreEqual(data[i], (Guid) itemArrays._untypedObjectArray[i]);
                AssertAreEqual(data[i], GuidArray[i]);
                //FIXME: Cannot retrieve nullable struct array.
                //AssertAreEqual(data[i], (Guid)itemArrays._nullableGuidArray[i]);
            }

            Assert.IsNull(itemArrays._untypedObjectArray[data.Length]);
            AssertAreEqual(Guid.Empty, itemArrays._GuidArray[data.Length]);
            AssertAreEqual(Guid.Empty, GuidArray[data.Length]);
            //FIXME: Cannot retrieve nullable struct array.
            //Assert.IsNull(itemArrays._nullableGuidArray[data.Length]);
        }

        protected override void AssertValues(IExtObjectContainer objectContainer, object[] values)
        {
            for (int i = 0; i < data.Length; i++)
            {
                Item item = (Item)values[i];
                AssertAreEqual(data[i], item._Guid);
                AssertAreEqual(data[i], (Guid) item._untyped);
                AssertAreEqual(data[i], (Guid) item._nullableGuid);
			}

            Item nullItem = (Item) values[values.Length - 1];

            AssertAreEqual(Guid.Empty, nullItem._Guid);
            Assert.IsNull(nullItem._untyped);
            Assert.IsNull(nullItem._nullableGuid);
		}

        private void AssertAreEqual(Guid expected, Guid actual)
        {
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        protected override object CreateArrays()
        {
            ItemArrays itemArrays = new ItemArrays();
            itemArrays._GuidArray = new Guid[data.Length + 1];
            System.Array.Copy(data, 0, itemArrays._GuidArray, 0, data.Length);

            itemArrays._untypedObjectArray = new object[data.Length + 1];
            System.Array.Copy(data, 0, itemArrays._untypedObjectArray, 0, data.Length);

            Guid[] GuidArray = new Guid[data.Length + 1];
            System.Array.Copy(data, 0, GuidArray, 0, data.Length);
            itemArrays._arrayInObject = GuidArray;
            
            itemArrays._nullableGuidArray = new Guid?[data.Length + 1];
            for (int i = 0; i < data.Length; i++)
            {
                itemArrays._nullableGuidArray[i] = data[i];
            }
			return itemArrays;
        }

        protected override object[] CreateValues()
        {
            Item[] values = new Item[data.Length + 1];
            for (int i = 0; i < data.Length; i++)
            {
                Item item = new Item();
                item._Guid = data[i];
                item._untyped = data[i];
                item._nullableGuid = data[i];
				values[i] = item;
            }
            values[values.Length - 1] = new Item();
            return values;
        }

        protected override string TypeName()
        {
            return "Guid";
        }

        protected override bool DefragmentInReadWriteMode()
        {
            return true;
        }

    }
}

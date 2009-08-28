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
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Query;
using Db4oUnit;
using Db4oUnit.Extensions;

namespace Db4objects.Db4o.Tests.CLI2.Assorted
{
	class NullableArraysElementsTestCase : AbstractDb4oTestCase
	{
        private static readonly Action<string> warning = delegate(String s) { TestPlatform.EmitWarning( s + "NullableArraysElementsTestCase: Remove the if() when COR-1130 get closed");  };

        private readonly TestSubject[] data = new TestSubject[]
	        {
	            new TestSubject(CreateNullableIntArray(), 0xDB40, new VTTestSubject(0x04BD, "foo", true)),
	            new TestSubject(CreateNullableIntArray(), 0xDB40, new VTTestSubject(0x04BD, "bar", false)),
	            new TestSubject(CreateNullableIntArray(), 42, new VTTestSubject(42, "baz", null)),
	        };
			
        protected override void Store()
		{
            foreach (TestSubject testItem in data)
            {
                Store(testItem);
            }
        }

        public void TestArrayType()
        {
                warning("TestArrayType");
        }

        public void TestNullableArray()
        {
                warning("TestNullableItemsInUntypeArray");
        }

	    private void AssertTestSubject(string name)
	    {
	        AssertNullableType(GetTestItem(name) , QueryByName(name));
	    }

		private static void AssertNullableType(TestSubject expected, TestSubject actual)
		{
			Assert.IsNotNull(actual);

			Assert.IsTrue(actual._nullableInt.HasValue);
			Assert.IsFalse(actual._valueType2.HasValue);
			Assert.IsNotNull(actual._valueType);
			
            Assert.AreEqual(
                expected._valueType.Value._value,
                actual._valueType.Value._value);
			
            Assert.AreEqual(
                expected._valueType.Value._name,
                actual._valueType.Value._name);

            Assert.AreEqual(
                expected._valueType.Value._nullableBool.HasValue, 
                actual._valueType.Value._nullableBool.HasValue);

            if (expected._valueType.Value._nullableBool.HasValue)
            {
                Assert.AreEqual(
                    expected._valueType.Value._nullableBool.Value,
                    actual._valueType.Value._nullableBool.Value);
            }

		    Assert.IsNotNull(actual._elements);
            Assert.IsNotNull(actual._nullableArray);

            warning("AssertNullableType");


            int?[] values = (int?[])actual._nullableArray;
            for (int i = 1; i < actual._elements.Length; i += 2)
			{
				Assert.IsNotNull(actual._elements[i]);
				Assert.AreEqual(i, actual._elements[i]);
                Assert.AreEqual(i, values[i]);
			}
		}

		private static int?[] CreateNullableIntArray()
		{
			int?[] items = new int?[10];
			for (int i = 1; i < items.Length; i += 2)
			{
				items[i] = new Nullable<int>(i);
			}

			return items;
		}

        private TestSubject GetTestItem(string name)
        {
            foreach (TestSubject test in data)
            {
                if (test._valueType.Value._name == name) return test;
            }
            
            return default(TestSubject);
        }

        private TestSubject QueryByName(string name)
        {
            IQuery query = NewQuery(typeof(TestSubject));
            query.Descend("_valueType").Descend("_name").Constrain(name);

            IObjectSet results = query.Execute();
            Assert.AreEqual(1, results.Size());

            return (TestSubject)results[0];
        }
	}

	class TestSubject
	{
		public int?[] _elements;
		public int? _nullableInt;
	    public object _nullableArray;
		
        public VTTestSubject? _valueType;
		public VTTestSubject? _valueType2;

		public TestSubject(int?[] items, int value, VTTestSubject vtt)
		{
			_elements = items;
		    _nullableArray = items;
			_nullableInt = value;
			_valueType = vtt;
			_valueType2 = null;
		}
	}

	struct VTTestSubject
	{
		public int _value;
		public string _name;
		public bool? _nullableBool;

		public VTTestSubject(int value, string name, bool? aBool)
		{
			_value = value;
			_name = name;
			_nullableBool = aBool;
		}
	}
}
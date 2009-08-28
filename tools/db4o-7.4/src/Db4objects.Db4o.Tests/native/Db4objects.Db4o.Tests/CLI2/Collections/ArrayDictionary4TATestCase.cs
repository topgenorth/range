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
using System.Collections.Generic;
using System.Text;
using Db4objects.Db4o.Tests.Common.TA;
using Db4objects.Db4o.Collections;
using Db4objects.Db4o.Reflect;
using Db4oUnit;
using Db4objects.Db4o.Internal;

namespace Db4objects.Db4o.Tests.CLI2.Collections
{
    class ArrayDictionary4TATestCase : TransparentActivationTestCaseBase
    {
        protected override void Store()
        {
            IDictionary<string, int> dict = new ArrayDictionary4<string, int>();
            ArrayDictionary4Asserter.PutData(dict);
            Store(dict);
        }

        private IDictionary<string, int> RetrieveOnlyInstance()
        {
            IDictionary<string, int> dict = (IDictionary<string, int>)RetrieveOnlyInstance(typeof(ArrayDictionary4<string, int>));
            AssertRetrievedItem(dict);
            return dict;
        }

        private void AssertRetrievedItem(IDictionary<string, int> dict)
        {
#if CF
            Assert.IsFalse(Db().IsActive(dict));
            string[] keys = (string[])  Reflection4.GetFieldValue(dict, "_keys");
            AssertInitalArray(keys, 16);
            int[] values = (int[])  Reflection4.GetFieldValue(dict, "_values");
            AssertInitalArray(values, 16);
#else
            Assert.IsNull( Reflection4.GetFieldValue(dict, "_keys"));
            Assert.IsNull( Reflection4.GetFieldValue(dict, "_values"));
#endif
            Assert.AreEqual(default(int),  Reflection4.GetFieldValue(dict, "_size"));
        }

#if CF
        private void AssertInitalArray<T>(T[] array, int length)
        {
            Assert.AreEqual(length, array.Length);
            foreach (T value in array)
            {
                Assert.AreEqual(default(T), value);
            }
        }
#endif
        public void TestItemGet()
        {
            IDictionary<string, int> dict = RetrieveOnlyInstance();
            ArrayDictionary4Asserter.AssertItemGet(dict);
        }

        public void TestItemSet()
        {
            IDictionary<string, int> dict = RetrieveOnlyInstance();
            ArrayDictionary4Asserter.AssertItemSet(dict);
        }

        public void TestKeys()
        {
            IDictionary<string, int> dict = RetrieveOnlyInstance();
            ArrayDictionary4Asserter.AssertKeys(dict);
        }

        public void TestValues()
        {
            IDictionary<string, int> dict = RetrieveOnlyInstance();
            ArrayDictionary4Asserter.AssertValues(dict);
        }

        public void TestAdd()
        {
            IDictionary<string, int> dict = RetrieveOnlyInstance();
            ArrayDictionary4Asserter.AssertAdd(dict);
        }

        public void TestContainsKey()
        {
            IDictionary<string, int> dict = RetrieveOnlyInstance();
            ArrayDictionary4Asserter.TestContainsKey(dict);
        }

        public void TestRemove()
        {
            IDictionary<string, int> dict = RetrieveOnlyInstance();
            ArrayDictionary4Asserter.AssertRemove(dict);
        }

        public void TestTryGetValue()
        {
            IDictionary<string, int> dict = RetrieveOnlyInstance();
            ArrayDictionary4Asserter.AssertTryGetValue(dict);
        }

        public void TestCount()
        {
            IDictionary<string, int> dict = RetrieveOnlyInstance();
            ArrayDictionary4Asserter.AssertCount(dict);
        }

        public void TestIsReadOnly()
        {
            IDictionary<string, int> dict = RetrieveOnlyInstance();
            ArrayDictionary4Asserter.AssertIsReadOnly(dict);
        }

        public void TestAddKeyValuePair()
        {
            IDictionary<string, int> dict = RetrieveOnlyInstance();
            ArrayDictionary4Asserter.AssertAddKeyValuePair(dict);
        }

        public void TestContains()
        {
            IDictionary<string, int> dict = RetrieveOnlyInstance();
            ArrayDictionary4Asserter.AssertContains(dict);
        }

        public void TestCopyTo()
        {
            IDictionary<string, int> dict = RetrieveOnlyInstance();
            ArrayDictionary4Asserter.AssertCopyTo(dict);
        }

        public void TestRemoveKeyValuePair()
        {
            IDictionary<string, int> dict = RetrieveOnlyInstance();
            ArrayDictionary4Asserter.AssertRemoveKeyValuePair(dict);
        }

        public void TestClear()
        {
            IDictionary<string, int> dict = RetrieveOnlyInstance();
            ArrayDictionary4Asserter.AssertClear(dict);
        }

        public void TestGetEnumerator()
        {
            IDictionary<string, int> dict = RetrieveOnlyInstance();
            ArrayDictionary4Asserter.AssertGetEnumerator(dict);
        }
    }
}

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
using Db4objects.Db4o.Collections;
using Db4objects.Db4o.Tests.Common.TA;
using Db4oUnit;

namespace Db4objects.Db4o.Tests.CLI2.Collections
{
	class ArrayDictionary4TransparentPersistenceTestCase : ITestLifeCycle
	{
		private ArrayDictionary4<string, int> dict;
		private MockActivator activator;

		public void TestRemove()
		{
			dict.Remove("foo"); 
			Assert.AreEqual(0, activator.WriteCount(), "removing non-existent element");

			dict.Remove("ltuae");
			Assert.AreEqual(1, activator.WriteCount());
		}

		public void TestRemovePair()
		{
			ICollection<KeyValuePair<string, int>> pairs = (ICollection<KeyValuePair<string, int>>) dict;
			Assert.IsFalse(pairs.Remove(new KeyValuePair<string, int>("ltuae", 41)));

			Assert.AreEqual(0, activator.WriteCount(), "removing non-existent element");

			Assert.IsTrue(pairs.Remove(new KeyValuePair<string, int>("ltuae", 42)));
			Assert.AreEqual(1, activator.WriteCount());
		}

		public void TestIndexer()
		{
			dict["ltuae"] = 44;
			Assert.AreEqual(1, activator.WriteCount(), "changing existing value");

			dict["2+2"] = 4;
			Assert.AreEqual(2, activator.WriteCount(), "adding new value");
		}

		public void SetUp()
		{
			dict = CreateDictionary();
			activator = MockActivator.ActivatorFor(dict);
		}

		public void TearDown()
		{
		}

		private static ArrayDictionary4<string, int> CreateDictionary()
		{
			ArrayDictionary4<string, int> dict = new ArrayDictionary4<string, int>();
			dict["ltuae"] = 42;
			return dict;
		}
	}
}

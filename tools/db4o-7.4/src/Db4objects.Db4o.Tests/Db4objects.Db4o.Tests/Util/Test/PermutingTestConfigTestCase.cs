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
using Db4oUnit;
using Db4objects.Db4o.Tests.Util;

namespace Db4objects.Db4o.Tests.Util.Test
{
	public class PermutingTestConfigTestCase : ITestCase
	{
		public virtual void TestPermutation()
		{
			object[][] data = new object[][] { new object[] { "A", "B" }, new object[] { "X", 
				"Y", "Z" } };
			PermutingTestConfig config = new PermutingTestConfig(data);
			object[][] expected = new object[][] { new object[] { "A", "X" }, new object[] { 
				"A", "Y" }, new object[] { "A", "Z" }, new object[] { "B", "X" }, new object[] { 
				"B", "Y" }, new object[] { "B", "Z" } };
			for (int groupIdx = 0; groupIdx < expected.Length; groupIdx++)
			{
				Assert.IsTrue(config.MoveNext());
				object[] current = new object[] { config.Current(0), config.Current(1) };
				ArrayAssert.AreEqual(expected[groupIdx], current);
			}
			Assert.IsFalse(config.MoveNext());
		}
	}
}

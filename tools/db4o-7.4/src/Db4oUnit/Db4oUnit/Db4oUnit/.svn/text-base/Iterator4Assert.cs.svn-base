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
using System.Collections;
using Db4oUnit;
using Db4objects.Db4o.Foundation;

namespace Db4oUnit
{
	public class Iterator4Assert
	{
		public static void AreEqual(IEnumerator expected, IEnumerator actual)
		{
			if (null == expected)
			{
				Assert.IsNull(actual);
				return;
			}
			Assert.IsNotNull(actual);
			while (expected.MoveNext())
			{
				AssertNext(expected.Current, actual);
			}
			if (actual.MoveNext())
			{
				Unexpected(actual.Current);
			}
		}

		private static void Unexpected(object element)
		{
			Assert.Fail("Unexpected element: " + element);
		}

		public static void AssertNext(object expected, IEnumerator iterator)
		{
			Assert.IsTrue(iterator.MoveNext(), "'" + expected + "' expected.");
			Assert.AreEqual(expected, iterator.Current);
		}

		public static void AreEqual(object[] expected, IEnumerator iterator)
		{
			AreEqual(new ArrayIterator4(expected), iterator);
		}

		public static void SameContent(object[] expected, IEnumerator actual)
		{
			SameContent(new ArrayIterator4(expected), actual);
		}

		public static void SameContent(IEnumerator expected, IEnumerator actual)
		{
			Collection4 allExpected = new Collection4(expected);
			while (actual.MoveNext())
			{
				object current = actual.Current;
				object removed = allExpected.Remove(current);
				if (null == removed)
				{
					Unexpected(current);
				}
			}
			Assert.IsTrue(allExpected.IsEmpty(), allExpected.ToString());
		}
	}
}

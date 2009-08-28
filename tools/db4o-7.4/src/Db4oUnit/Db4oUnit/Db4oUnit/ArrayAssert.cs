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

namespace Db4oUnit
{
	public partial class ArrayAssert
	{
		public static void Contains(long[] array, long expected)
		{
			if (-1 != IndexOf(array, expected))
			{
				return;
			}
			Assert.Fail("Expecting '" + expected + "'.");
		}

		public static void Contains(object[] array, object[] expected)
		{
			for (int i = 0; i < expected.Length; i++)
			{
				if (-1 == IndexOf(array, expected[i]))
				{
					Assert.Fail("Expecting contains '" + expected[i] + "'.");
				}
			}
		}

		public static int IndexOf(object[] array, object expected)
		{
			for (int i = 0; i < array.Length; ++i)
			{
				if (expected.Equals(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		public static void AreEqual(object[] expected, object[] actual)
		{
			AreEqualImpl(expected, actual);
		}

		private static string IndexMessage(int i)
		{
			return "expected[" + i + "]";
		}

		public static void AreEqual(byte[] expected, byte[] actual)
		{
			if (expected == actual)
			{
				return;
			}
			if (expected == null || actual == null)
			{
				Assert.AreSame(expected, actual);
			}
			Assert.AreEqual(expected.Length, actual.Length);
			for (int i = 0; i < expected.Length; i++)
			{
				Assert.AreEqual(expected[i], actual[i], IndexMessage(i));
			}
		}

		public static void AreNotEqual(byte[] expected, byte[] actual)
		{
			Assert.AreNotSame(expected, actual);
			for (int i = 0; i < expected.Length; i++)
			{
				if (expected[i] != actual[i])
				{
					return;
				}
			}
			Assert.IsTrue(false);
		}

		public static void AreEqual(int[] expected, int[] actual)
		{
			if (expected == actual)
			{
				return;
			}
			if (expected == null || actual == null)
			{
				Assert.AreSame(expected, actual);
			}
			Assert.AreEqual(expected.Length, actual.Length);
			for (int i = 0; i < expected.Length; i++)
			{
				Assert.AreEqual(expected[i], actual[i], IndexMessage(i));
			}
		}

		public static void AreEqual(double[] expected, double[] actual)
		{
			if (expected == actual)
			{
				return;
			}
			if (expected == null || actual == null)
			{
				Assert.AreSame(expected, actual);
			}
			Assert.AreEqual(expected.Length, actual.Length);
			for (int i = 0; i < expected.Length; i++)
			{
				Assert.AreEqual(expected[i], actual[i], IndexMessage(i));
			}
		}

		public static void AreEqual(char[] expected, char[] actual)
		{
			if (expected == actual)
			{
				return;
			}
			if (expected == null || actual == null)
			{
				Assert.AreSame(expected, actual);
			}
			Assert.AreEqual(expected.Length, actual.Length);
			for (int i = 0; i < expected.Length; i++)
			{
				Assert.AreEqual(expected[i], actual[i], IndexMessage(i));
			}
		}

		private static int IndexOf(long[] array, long expected)
		{
			for (int i = 0; i < array.Length; ++i)
			{
				if (expected == array[i])
				{
					return i;
				}
			}
			return -1;
		}
	}
}

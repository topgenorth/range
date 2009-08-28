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
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Tests.Common.Foundation;

namespace Db4objects.Db4o.Tests.Common.Foundation
{
	public class Algorithms4TestCase : ITestCase
	{
		public class QuickSortableIntArray : IQuickSortable4
		{
			private int[] ints;

			public QuickSortableIntArray(int[] ints)
			{
				this.ints = ints;
			}

			public virtual int Compare(int leftIndex, int rightIndex)
			{
				return ints[leftIndex] - ints[rightIndex];
			}

			public virtual int Size()
			{
				return ints.Length;
			}

			public virtual void Swap(int leftIndex, int rightIndex)
			{
				int temp = ints[leftIndex];
				ints[leftIndex] = ints[rightIndex];
				ints[rightIndex] = temp;
			}

			public virtual void AssertSorted()
			{
				for (int i = 0; i < ints.Length; i++)
				{
					Assert.AreEqual(i + 1, ints[i]);
				}
			}
		}

		public virtual void TestUnsorted()
		{
			int[] ints = new int[] { 3, 5, 2, 1, 4 };
			AssertQSort(ints);
		}

		public virtual void TestStackUsage()
		{
			int[] ints = new int[50000];
			for (int i = 0; i < ints.Length; i++)
			{
				ints[i] = i + 1;
			}
			AssertQSort(ints);
		}

		private void AssertQSort(int[] ints)
		{
			Algorithms4TestCase.QuickSortableIntArray sample = new Algorithms4TestCase.QuickSortableIntArray
				(ints);
			Algorithms4.Qsort(sample);
			sample.AssertSorted();
		}
	}
}

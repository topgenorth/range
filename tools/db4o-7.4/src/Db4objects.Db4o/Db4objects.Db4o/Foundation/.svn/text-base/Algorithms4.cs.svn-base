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
using Db4objects.Db4o.Foundation;

namespace Db4objects.Db4o.Foundation
{
	/// <exclude></exclude>
	public class Algorithms4
	{
		private class Range
		{
			internal int _from;

			internal int _to;

			public Range(int from, int to)
			{
				_from = from;
				_to = to;
			}
		}

		public static void Qsort(IQuickSortable4 sortable)
		{
			Stack4 stack = new Stack4();
			AddRange(stack, 0, sortable.Size() - 1);
			Qsort(sortable, stack);
		}

		private static void Qsort(IQuickSortable4 sortable, Stack4 stack)
		{
			while (!stack.IsEmpty())
			{
				Algorithms4.Range range = (Algorithms4.Range)stack.Peek();
				stack.Pop();
				int from = range._from;
				int to = range._to;
				int pivot = to;
				int left = from;
				int right = to;
				while (left < right)
				{
					while (left < right && sortable.Compare(left, pivot) < 0)
					{
						left++;
					}
					while (left < right && sortable.Compare(right, pivot) >= 0)
					{
						right--;
					}
					Swap(sortable, left, right);
				}
				Swap(sortable, to, right);
				AddRange(stack, from, right - 1);
				AddRange(stack, right + 1, to);
			}
		}

		private static void AddRange(Stack4 stack, int from, int to)
		{
			if (to - from < 1)
			{
				return;
			}
			stack.Push(new Algorithms4.Range(from, to));
		}

		private static void Swap(IQuickSortable4 sortable, int left, int right)
		{
			if (left == right)
			{
				return;
			}
			sortable.Swap(left, right);
		}
	}
}

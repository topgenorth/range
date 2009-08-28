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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal.Query.Processor;

namespace Db4objects.Db4o.Internal.Query.Processor
{
	internal class Order : IOrderable
	{
		private int i_major;

		private IntArrayList i_minors = new IntArrayList();

		public virtual int CompareTo(object obj)
		{
			if (obj is Order)
			{
				Order other = (Order)obj;
				int res = i_major - other.i_major;
				if (res != 0)
				{
					return res;
				}
				return CompareMinors(other.i_minors);
			}
			return -1;
		}

		public virtual void HintOrder(int a_order, bool a_major)
		{
			if (a_major)
			{
				i_major = a_order;
			}
			else
			{
				AppendMinor(a_order);
			}
		}

		public virtual bool HasDuplicates()
		{
			return true;
		}

		public override string ToString()
		{
			string str = "Order " + i_major;
			for (int i = 0; i < i_minors.Size(); i++)
			{
				str = str + " " + i_minors.Get(i);
			}
			return str;
		}

		public virtual void SwapMajorToMinor()
		{
			InsertMinor(i_major);
			i_major = 0;
		}

		private void AppendMinor(int minor)
		{
			i_minors.Add(minor);
		}

		private void InsertMinor(int minor)
		{
			i_minors.Add(0, minor);
		}

		private int CompareMinors(IntArrayList other)
		{
			if (i_minors.Size() != other.Size())
			{
				throw new Db4oException("Unexpected exception: this..size()=" + i_minors.Size() +
					 ", other.size()=" + other.Size());
			}
			int result = 0;
			for (int i = 0; i < i_minors.Size(); i++)
			{
				if (i_minors.Get(i) == other.Get(i))
				{
					continue;
				}
				return (i_minors.Get(i) - other.Get(i));
			}
			return result;
		}
	}
}

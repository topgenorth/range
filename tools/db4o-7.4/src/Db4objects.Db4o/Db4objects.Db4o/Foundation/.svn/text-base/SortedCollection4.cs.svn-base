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
using System.Collections;
using Db4objects.Db4o.Foundation;

namespace Db4objects.Db4o.Foundation
{
	/// <exclude></exclude>
	public class SortedCollection4
	{
		private readonly IComparison4 _comparison;

		private Tree _tree;

		public SortedCollection4(IComparison4 comparison)
		{
			if (null == comparison)
			{
				throw new ArgumentNullException();
			}
			_comparison = comparison;
			_tree = null;
		}

		public virtual object SingleElement()
		{
			if (1 != Size())
			{
				throw new InvalidOperationException();
			}
			return _tree.Key();
		}

		public virtual void AddAll(IEnumerator iterator)
		{
			while (iterator.MoveNext())
			{
				Add(iterator.Current);
			}
		}

		public virtual void Add(object element)
		{
			_tree = Tree.Add(_tree, new TreeObject(element, _comparison));
		}

		public virtual void Remove(object element)
		{
			_tree = Tree.RemoveLike(_tree, new TreeObject(element, _comparison));
		}

		public virtual object[] ToArray(object[] array)
		{
			Tree.Traverse(_tree, new _IVisitor4_43(array));
			return array;
		}

		private sealed class _IVisitor4_43 : IVisitor4
		{
			public _IVisitor4_43(object[] array)
			{
				this.array = array;
				this.i = 0;
			}

			internal int i;

			public void Visit(object obj)
			{
				array[this.i++] = ((TreeObject)obj).Key();
			}

			private readonly object[] array;
		}

		public virtual int Size()
		{
			return Tree.Size(_tree);
		}
	}
}

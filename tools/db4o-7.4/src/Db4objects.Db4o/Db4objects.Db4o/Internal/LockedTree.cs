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
using Db4objects.Db4o.Internal;

namespace Db4objects.Db4o.Internal
{
	/// <exclude></exclude>
	public class LockedTree
	{
		private Tree _tree;

		private int _version;

		public virtual void Add(Tree tree)
		{
			Changed();
			_tree = _tree == null ? tree : _tree.Add(tree);
		}

		private void Changed()
		{
			_version++;
		}

		public virtual void Clear()
		{
			Changed();
			_tree = null;
		}

		public virtual Tree Find(int key)
		{
			return TreeInt.Find(_tree, key);
		}

		public virtual void Read(ByteArrayBuffer buffer, IReadable template)
		{
			Clear();
			_tree = new TreeReader(buffer, template).Read();
			Changed();
		}

		public virtual void TraverseLocked(IVisitor4 visitor)
		{
			int currentVersion = _version;
			Tree.Traverse(_tree, visitor);
			if (_version != currentVersion)
			{
				throw new InvalidOperationException();
			}
		}

		public virtual void TraverseMutable(IVisitor4 visitor)
		{
			Collection4 currentContent = new Collection4();
			TraverseLocked(new _IVisitor4_51(currentContent));
			IEnumerator i = currentContent.GetEnumerator();
			while (i.MoveNext())
			{
				visitor.Visit(i.Current);
			}
		}

		private sealed class _IVisitor4_51 : IVisitor4
		{
			public _IVisitor4_51(Collection4 currentContent)
			{
				this.currentContent = currentContent;
			}

			public void Visit(object obj)
			{
				currentContent.Add(obj);
			}

			private readonly Collection4 currentContent;
		}
	}
}

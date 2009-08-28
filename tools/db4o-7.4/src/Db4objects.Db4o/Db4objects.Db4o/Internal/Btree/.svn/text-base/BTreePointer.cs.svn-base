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
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Btree;

namespace Db4objects.Db4o.Internal.Btree
{
	/// <exclude></exclude>
	public class BTreePointer
	{
		public static Db4objects.Db4o.Internal.Btree.BTreePointer Max(Db4objects.Db4o.Internal.Btree.BTreePointer
			 x, Db4objects.Db4o.Internal.Btree.BTreePointer y)
		{
			if (x == null)
			{
				return x;
			}
			if (y == null)
			{
				return y;
			}
			if (x.CompareTo(y) > 0)
			{
				return x;
			}
			return y;
		}

		public static Db4objects.Db4o.Internal.Btree.BTreePointer Min(Db4objects.Db4o.Internal.Btree.BTreePointer
			 x, Db4objects.Db4o.Internal.Btree.BTreePointer y)
		{
			if (x == null)
			{
				return y;
			}
			if (y == null)
			{
				return x;
			}
			if (x.CompareTo(y) < 0)
			{
				return x;
			}
			return y;
		}

		private readonly BTreeNode _node;

		private readonly int _index;

		private readonly Db4objects.Db4o.Internal.Transaction _transaction;

		private readonly ByteArrayBuffer _nodeReader;

		public BTreePointer(Db4objects.Db4o.Internal.Transaction transaction, ByteArrayBuffer
			 nodeReader, BTreeNode node, int index)
		{
			if (transaction == null || node == null)
			{
				throw new ArgumentNullException();
			}
			_transaction = transaction;
			_nodeReader = nodeReader;
			_node = node;
			_index = index;
		}

		public Db4objects.Db4o.Internal.Transaction Transaction()
		{
			return _transaction;
		}

		public int Index()
		{
			return _index;
		}

		public BTreeNode Node()
		{
			return _node;
		}

		public object Key()
		{
			return Node().Key(Transaction(), NodeReader(), Index());
		}

		private ByteArrayBuffer NodeReader()
		{
			return _nodeReader;
		}

		public virtual Db4objects.Db4o.Internal.Btree.BTreePointer Next()
		{
			int indexInMyNode = Index() + 1;
			while (indexInMyNode < Node().Count())
			{
				if (Node().IndexIsValid(Transaction(), indexInMyNode))
				{
					return new Db4objects.Db4o.Internal.Btree.BTreePointer(Transaction(), NodeReader(
						), Node(), indexInMyNode);
				}
				indexInMyNode++;
			}
			int newIndex = -1;
			BTreeNode nextNode = Node();
			ByteArrayBuffer nextReader = null;
			while (newIndex == -1)
			{
				nextNode = nextNode.NextNode();
				if (nextNode == null)
				{
					return null;
				}
				nextReader = nextNode.PrepareRead(Transaction());
				newIndex = nextNode.FirstKeyIndex(Transaction());
			}
			return new Db4objects.Db4o.Internal.Btree.BTreePointer(Transaction(), nextReader, 
				nextNode, newIndex);
		}

		public virtual Db4objects.Db4o.Internal.Btree.BTreePointer Previous()
		{
			int indexInMyNode = Index() - 1;
			while (indexInMyNode >= 0)
			{
				if (Node().IndexIsValid(Transaction(), indexInMyNode))
				{
					return new Db4objects.Db4o.Internal.Btree.BTreePointer(Transaction(), NodeReader(
						), Node(), indexInMyNode);
				}
				indexInMyNode--;
			}
			int newIndex = -1;
			BTreeNode previousNode = Node();
			ByteArrayBuffer previousReader = null;
			while (newIndex == -1)
			{
				previousNode = previousNode.PreviousNode();
				if (previousNode == null)
				{
					return null;
				}
				previousReader = previousNode.PrepareRead(Transaction());
				newIndex = previousNode.LastKeyIndex(Transaction());
			}
			return new Db4objects.Db4o.Internal.Btree.BTreePointer(Transaction(), previousReader
				, previousNode, newIndex);
		}

		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			if (!(obj is Db4objects.Db4o.Internal.Btree.BTreePointer))
			{
				return false;
			}
			Db4objects.Db4o.Internal.Btree.BTreePointer other = (Db4objects.Db4o.Internal.Btree.BTreePointer
				)obj;
			if (Index() != other.Index())
			{
				return false;
			}
			return Node().Equals(other.Node());
		}

		public override int GetHashCode()
		{
			return Node().GetHashCode();
		}

		public override string ToString()
		{
			return "BTreePointer(index=" + Index() + ", node=" + Node() + ")";
		}

		public virtual int CompareTo(Db4objects.Db4o.Internal.Btree.BTreePointer y)
		{
			if (null == y)
			{
				throw new ArgumentNullException();
			}
			if (Btree() != y.Btree())
			{
				throw new ArgumentException();
			}
			return Btree().CompareKeys(Transaction().Context(), Key(), y.Key());
		}

		private BTree Btree()
		{
			return Node().Btree();
		}

		public static bool LessThan(Db4objects.Db4o.Internal.Btree.BTreePointer x, Db4objects.Db4o.Internal.Btree.BTreePointer
			 y)
		{
			return Db4objects.Db4o.Internal.Btree.BTreePointer.Min(x, y) == x && !Equals(x, y
				);
		}

		public static bool Equals(Db4objects.Db4o.Internal.Btree.BTreePointer x, Db4objects.Db4o.Internal.Btree.BTreePointer
			 y)
		{
			if (x == null)
			{
				return y == null;
			}
			return x.Equals(y);
		}

		public virtual bool IsValid()
		{
			return Node().IndexIsValid(Transaction(), Index());
		}
	}
}

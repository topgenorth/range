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
	public class BTreeNodeSearchResult
	{
		private readonly Transaction _transaction;

		private readonly BTree _btree;

		private readonly BTreePointer _pointer;

		private readonly bool _foundMatch;

		internal BTreeNodeSearchResult(Transaction transaction, BTree btree, BTreePointer
			 pointer, bool foundMatch)
		{
			if (null == transaction || null == btree)
			{
				throw new ArgumentNullException();
			}
			_transaction = transaction;
			_btree = btree;
			_pointer = pointer;
			_foundMatch = foundMatch;
		}

		internal BTreeNodeSearchResult(Transaction trans, ByteArrayBuffer nodeReader, BTree
			 btree, BTreeNode node, int cursor, bool foundMatch) : this(trans, btree, PointerOrNull
			(trans, nodeReader, node, cursor), foundMatch)
		{
		}

		internal BTreeNodeSearchResult(Transaction trans, ByteArrayBuffer nodeReader, BTree
			 btree, Searcher searcher, BTreeNode node) : this(trans, btree, NextPointerIf(PointerOrNull
			(trans, nodeReader, node, searcher.Cursor()), searcher.IsGreater()), searcher.FoundMatch
			())
		{
		}

		private static BTreePointer NextPointerIf(BTreePointer pointer, bool condition)
		{
			if (null == pointer)
			{
				return null;
			}
			if (condition)
			{
				return pointer.Next();
			}
			return pointer;
		}

		private static BTreePointer PointerOrNull(Transaction trans, ByteArrayBuffer nodeReader
			, BTreeNode node, int cursor)
		{
			return node == null ? null : new BTreePointer(trans, nodeReader, node, cursor);
		}

		public virtual IBTreeRange CreateIncludingRange(Db4objects.Db4o.Internal.Btree.BTreeNodeSearchResult
			 end)
		{
			BTreePointer firstPointer = FirstValidPointer();
			BTreePointer endPointer = end._foundMatch ? end._pointer.Next() : end.FirstValidPointer
				();
			return new BTreeRangeSingle(_transaction, _btree, firstPointer, endPointer);
		}

		public virtual BTreePointer FirstValidPointer()
		{
			if (null == _pointer)
			{
				return null;
			}
			if (_pointer.IsValid())
			{
				return _pointer;
			}
			return _pointer.Next();
		}
	}
}

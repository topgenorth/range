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
using Db4objects.Db4o.Internal.Btree;
using Db4objects.Db4o.Internal.Fieldindex;
using Db4objects.Db4o.Internal.Query.Processor;

namespace Db4objects.Db4o.Internal.Fieldindex
{
	public abstract class IndexedNodeBase : IIndexedNode
	{
		private readonly QConObject _constraint;

		public IndexedNodeBase(QConObject qcon)
		{
			if (null == qcon)
			{
				throw new ArgumentNullException();
			}
			if (null == qcon.GetField())
			{
				throw new ArgumentException();
			}
			_constraint = qcon;
		}

		public virtual TreeInt ToTreeInt()
		{
			return AddToTree(null, this);
		}

		public BTree GetIndex()
		{
			return GetYapField().GetIndex(Transaction());
		}

		private FieldMetadata GetYapField()
		{
			return _constraint.GetField().GetYapField();
		}

		public virtual QCon Constraint()
		{
			return _constraint;
		}

		public virtual bool IsResolved()
		{
			QCon parent = Constraint().Parent();
			return null == parent || !parent.HasParent();
		}

		public virtual IBTreeRange Search(object value)
		{
			return GetYapField().Search(Transaction(), value);
		}

		public static TreeInt AddToTree(TreeInt tree, IIndexedNode node)
		{
			IEnumerator i = node.GetEnumerator();
			while (i.MoveNext())
			{
				FieldIndexKey composite = (FieldIndexKey)i.Current;
				tree = (TreeInt)Tree.Add(tree, new TreeInt(composite.ParentID()));
			}
			return tree;
		}

		public virtual IIndexedNode Resolve()
		{
			if (IsResolved())
			{
				return null;
			}
			return IndexedPath.NewParentPath(this, Constraint());
		}

		private Db4objects.Db4o.Internal.Transaction Transaction()
		{
			return Constraint().Transaction();
		}

		public abstract IEnumerator GetEnumerator();

		public abstract int ResultSize();
	}
}

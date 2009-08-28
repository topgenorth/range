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
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Query.Processor;
using Db4objects.Db4o.Internal.Query.Result;
using Db4objects.Db4o.Query;

namespace Db4objects.Db4o.Internal.Query.Result
{
	/// <exclude></exclude>
	public class HybridQueryResult : AbstractQueryResult
	{
		private AbstractQueryResult _delegate;

		public HybridQueryResult(Transaction transaction, QueryEvaluationMode mode) : base
			(transaction)
		{
			_delegate = ForMode(transaction, mode);
		}

		private static AbstractQueryResult ForMode(Transaction transaction, QueryEvaluationMode
			 mode)
		{
			if (mode == QueryEvaluationMode.Lazy)
			{
				return new LazyQueryResult(transaction);
			}
			if (mode == QueryEvaluationMode.Snapshot)
			{
				return new SnapShotQueryResult(transaction);
			}
			return new IdListQueryResult(transaction);
		}

		public override object Get(int index)
		{
			_delegate = _delegate.SupportElementAccess();
			return _delegate.Get(index);
		}

		public override int GetId(int index)
		{
			_delegate = _delegate.SupportElementAccess();
			return _delegate.GetId(index);
		}

		public override int IndexOf(int id)
		{
			_delegate = _delegate.SupportElementAccess();
			return _delegate.IndexOf(id);
		}

		public override IIntIterator4 IterateIDs()
		{
			return _delegate.IterateIDs();
		}

		public override IEnumerator GetEnumerator()
		{
			return _delegate.GetEnumerator();
		}

		public override void LoadFromClassIndex(ClassMetadata clazz)
		{
			_delegate.LoadFromClassIndex(clazz);
		}

		public override void LoadFromClassIndexes(ClassMetadataIterator iterator)
		{
			_delegate.LoadFromClassIndexes(iterator);
		}

		public override void LoadFromIdReader(ByteArrayBuffer reader)
		{
			_delegate.LoadFromIdReader(reader);
		}

		public override void LoadFromQuery(QQuery query)
		{
			if (query.RequiresSort())
			{
				_delegate = new IdListQueryResult(Transaction());
			}
			_delegate.LoadFromQuery(query);
		}

		public override int Size()
		{
			_delegate = _delegate.SupportSize();
			return _delegate.Size();
		}

		public override void Sort(IQueryComparator cmp)
		{
			_delegate = _delegate.SupportSort();
			_delegate.Sort(cmp);
		}
	}
}

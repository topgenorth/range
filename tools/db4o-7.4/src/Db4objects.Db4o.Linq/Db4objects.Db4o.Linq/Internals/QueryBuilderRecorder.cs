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
using System.Collections.Generic;

using Db4objects.Db4o.Query;

namespace Db4objects.Db4o.Linq.Internals
{
	internal interface IQueryBuilderRecord
	{
		void Playback(IQuery query);
		void Playback(QueryBuilderContext context);
	}

	internal class NullQueryBuilderRecord : IQueryBuilderRecord
	{
		public static readonly NullQueryBuilderRecord Instance = new NullQueryBuilderRecord();

		private NullQueryBuilderRecord()
		{
		}

		public void Playback(IQuery query)
		{
		}

		public void Playback(QueryBuilderContext context)
		{
		}
	}

	internal abstract class QueryBuilderRecordImpl : IQueryBuilderRecord
	{
		public void Playback(IQuery query)
		{
			Playback(new QueryBuilderContext(query));
		}

		public abstract void Playback(QueryBuilderContext context);
	}

	internal class CompositeQueryBuilderRecord : QueryBuilderRecordImpl
	{
		private readonly IQueryBuilderRecord _first;
		private readonly IQueryBuilderRecord _second;

		public CompositeQueryBuilderRecord(IQueryBuilderRecord first, IQueryBuilderRecord second)
		{
			_first = first;
			_second = second;
		}

		override public void Playback(QueryBuilderContext context)
		{
			_first.Playback(context);
			_second.Playback(context);
		}
	}

	internal class ChainedQueryBuilderRecord : QueryBuilderRecordImpl
	{
		private readonly Action<QueryBuilderContext> _action;
		private readonly IQueryBuilderRecord _next;

		public ChainedQueryBuilderRecord(IQueryBuilderRecord next, Action<QueryBuilderContext> action)
		{
			_next = next;
			_action = action;
		}

		override public void Playback(QueryBuilderContext context)
		{
			_next.Playback(context);
			_action(context);
		}
	}

	internal class QueryBuilderRecorder
	{
		private IQueryBuilderRecord _last = NullQueryBuilderRecord.Instance;

		public QueryBuilderRecorder()
		{
		}

		public IQueryBuilderRecord Record
		{
			get { return _last; }
		}

		public void Add(Action<QueryBuilderContext> action)
		{
			_last = new ChainedQueryBuilderRecord(_last, action);
		}
	}
}

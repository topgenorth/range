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
using System.Collections.Generic;
using System.Linq;

using Db4objects.Db4o;
using Db4objects.Db4o.Query;

namespace Db4objects.Db4o.Linq.Internals
{
	internal class Db4oQuery<T> : IDb4oLinqQueryInternal<T>
	{
		private readonly IObjectContainer _container;
		private readonly IQueryBuilderRecord _record;

		public Db4oQuery(IObjectContainer container)
		{
			if (container == null) throw new ArgumentNullException("container");
			_container = container;
			_record = NullQueryBuilderRecord.Instance;
		}

		public Db4oQuery(Db4oQuery<T> parent, IQueryBuilderRecord record)
		{			
			_container = parent.Container;
			_record = new CompositeQueryBuilderRecord(parent.Record, record);
		}

		public IObjectContainer Container
		{
			get { return _container; }
		}

		public IQueryBuilderRecord Record
		{
			get { return _record; }
		}

		public int Count
		{
			get { return Execute().Count; }
		}

		public ObjectSetWrapper<T> GetExtentResult()
		{
			var query = NewQuery();
			return Wrap(query.Execute());
		}

		private IQuery NewQuery()
		{
			var query = _container.Query();
			query.Constrain(typeof(T));
			return query;
		}

		static ObjectSetWrapper<T> Wrap(IObjectSet set)
		{
			return new ObjectSetWrapper<T>(set);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return Wrap(Execute()).GetEnumerator();
		}

		private IObjectSet Execute()
		{
			var query = NewQuery();
			_record.Playback(query);
			return query.Execute();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#region IDb4oLinqQueryInternal<T> Members

		public IEnumerable<T> UnoptimizedThenBy<TKey>(Func<T, TKey> function)
		{
			throw new NotImplementedException("cannot fallback on UnoptimizedThenBy");
		}

		public IEnumerable<T> UnoptimizedThenByDescending<TKey>(Func<T, TKey> function)
		{
			throw new NotImplementedException("cannot fallback on UnoptimizedThenBy");
			/*
			IOrderByRecord record = _orderByRecord;
			IOrderedEnumerable<T> ordered = record.OrderBy(this);

			record = record.Next;
			while (record != null)
			{
				ordered = record.ThenBy(record);
			}
			return ordered.ThenByDescending(function);
			 * */
		}

		public IEnumerable<T> UnoptimizedWhere(Func<T, bool> func)
		{
			return GetExtentResult().Where(func);
		}

		#endregion
	}
}

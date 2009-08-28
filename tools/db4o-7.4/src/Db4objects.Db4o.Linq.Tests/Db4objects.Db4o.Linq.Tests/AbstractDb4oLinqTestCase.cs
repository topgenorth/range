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
using System.Linq;

using Db4objects.Db4o;
using Db4objects.Db4o.Events;
using Db4objects.Db4o.Linq;
using Db4objects.Db4o.Query;

using Db4oUnit;
using Db4oUnit.Extensions;

using Db4objects.Db4o.Linq.Tests.Queries;

namespace Db4objects.Db4o.Linq.Tests
{
	public abstract class AbstractDb4oLinqTestCase : AbstractDb4oTestCase
	{
		public static void AssertSet<T>(IEnumerable<T> expected, IEnumerable<T> candidate)
		{
			var ex = new HashSet<T>(expected);
			var d = new HashSet<T>(candidate);

			Assert.AreEqual(ex.Count, d.Count);
			Assert.IsTrue(ex.SetEquals(d));
		}

		public static void AssertSequence<T>(IEnumerable<T> expected, IEnumerable<T> candidate)
		{
			Iterator4Assert.AreEqual(expected.GetEnumerator(), candidate.GetEnumerator());
		}

		protected void AssertQuery(string expected, Action action)
		{
			using (var recorder = new QueryStringRecorder(Db()))
			{
				action();

				Assert.AreEqual(expected, recorder.QueryString);
			}
		}

		private class QueryStringRecorder : IDisposable
		{
			private string _queryString;
			private IEventRegistry _registry;

			public string QueryString
			{
				get { return _queryString; }
			}

			public QueryStringRecorder(IObjectContainer container)
			{
				_registry = EventRegistryFactory.ForObjectContainer(container);
				_registry.QueryStarted += OnQueryStarted;
			}

			private void OnQueryStarted(object sender, QueryEventArgs args)
			{
				_queryString = args.Query.ToQueryString();
			}

			public void Dispose()
			{
				_registry.QueryStarted -= OnQueryStarted;
			}
		}
	}
}

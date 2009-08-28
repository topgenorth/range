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
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Soda.Util;

namespace Db4objects.Db4o.Tests.Common.Soda.Experiments
{
	public class STCaseInsensitiveTestCase : SodaBaseTestCase
	{
		public string str;

		public STCaseInsensitiveTestCase()
		{
		}

		public STCaseInsensitiveTestCase(string str)
		{
			this.str = str;
		}

		public override object[] CreateData()
		{
			return new object[] { new Db4objects.Db4o.Tests.Common.Soda.Experiments.STCaseInsensitiveTestCase
				("Hihoho"), new Db4objects.Db4o.Tests.Common.Soda.Experiments.STCaseInsensitiveTestCase
				("Hello"), new Db4objects.Db4o.Tests.Common.Soda.Experiments.STCaseInsensitiveTestCase
				("hello") };
		}

		public virtual void Test()
		{
			IQuery q = NewQuery();
			q.Constrain(typeof(Db4objects.Db4o.Tests.Common.Soda.Experiments.STCaseInsensitiveTestCase
				));
			q.Descend("str").Constrain(new _IEvaluation_30());
			Expect(q, new int[] { 1, 2 });
		}

		private sealed class _IEvaluation_30 : IEvaluation
		{
			public _IEvaluation_30()
			{
			}

			public void Evaluate(ICandidate candidate)
			{
				candidate.Include(candidate.GetObject().ToString().ToLower().StartsWith("hell"));
			}
		}
	}
}

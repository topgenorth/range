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
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Concurrency;

namespace Db4objects.Db4o.Tests.Common.Concurrency
{
	public class QueryNonExistantTestCase : Db4oClientServerTestCase
	{
		public static void Main(string[] args)
		{
			new Db4objects.Db4o.Tests.Common.Concurrency.QueryNonExistantTestCase().RunConcurrency
				();
		}

		internal QueryNonExistantTestCase.QueryNonExistant1 member;

		public QueryNonExistantTestCase()
		{
		}

		public QueryNonExistantTestCase(bool createMembers)
		{
			// db4o constructor
			member = new QueryNonExistantTestCase.QueryNonExistant1();
			member.member = new QueryNonExistantTestCase.QueryNonExistant2();
			member.member.member = this;
		}

		// db4o constructor
		public virtual void Conc(IExtObjectContainer oc)
		{
			oc.QueryByExample((new Db4objects.Db4o.Tests.Common.Concurrency.QueryNonExistantTestCase
				(true)));
			AssertOccurrences(oc, typeof(Db4objects.Db4o.Tests.Common.Concurrency.QueryNonExistantTestCase
				), 0);
			IQuery q = oc.Query();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Concurrency.QueryNonExistantTestCase
				(true));
			Assert.AreEqual(0, q.Execute().Size());
		}

		public class QueryNonExistant1
		{
			internal QueryNonExistantTestCase.QueryNonExistant2 member;
		}

		public class QueryNonExistant2 : QueryNonExistantTestCase.QueryNonExistant1
		{
			internal QueryNonExistantTestCase member;
		}
	}
}

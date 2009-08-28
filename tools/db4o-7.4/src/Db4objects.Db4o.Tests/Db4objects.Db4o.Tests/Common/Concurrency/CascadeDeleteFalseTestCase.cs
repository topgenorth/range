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
using Db4oUnit.Extensions;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Tests.Common.Concurrency;

namespace Db4objects.Db4o.Tests.Common.Concurrency
{
	public class CascadeDeleteFalseTestCase : Db4oClientServerTestCase
	{
		public static void Main(string[] args)
		{
			new CascadeDeleteFalseTestCase().RunConcurrency();
		}

		public class Item
		{
			public CascadeDeleteFalseTestCase.CascadeDeleteFalseHelper h1;

			public CascadeDeleteFalseTestCase.CascadeDeleteFalseHelper h2;

			public CascadeDeleteFalseTestCase.CascadeDeleteFalseHelper h3;
		}

		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(typeof(CascadeDeleteFalseTestCase.Item)).CascadeOnDelete(true);
			config.ObjectClass(typeof(CascadeDeleteFalseTestCase.Item)).ObjectField("h3").CascadeOnDelete
				(false);
		}

		protected override void Store()
		{
			CascadeDeleteFalseTestCase.Item item = new CascadeDeleteFalseTestCase.Item();
			item.h1 = new CascadeDeleteFalseTestCase.CascadeDeleteFalseHelper();
			item.h2 = new CascadeDeleteFalseTestCase.CascadeDeleteFalseHelper();
			item.h3 = new CascadeDeleteFalseTestCase.CascadeDeleteFalseHelper();
			Store(item);
		}

		/// <exception cref="Exception"></exception>
		public virtual void ConcDelete(IExtObjectContainer oc)
		{
			IObjectSet os = oc.Query(typeof(CascadeDeleteFalseTestCase.Item));
			if (os.Size() == 0)
			{
				// the object has been deleted
				return;
			}
			if (!os.HasNext())
			{
				// object can be deleted after query 
				return;
			}
			CascadeDeleteFalseTestCase.Item cdf = (CascadeDeleteFalseTestCase.Item)os.Next();
			// sleep 1000 ms, waiting for other threads.
			// Thread.sleep(500);
			oc.Delete(cdf);
			oc.Commit();
			AssertOccurrences(oc, typeof(CascadeDeleteFalseTestCase.Item), 0);
			AssertOccurrences(oc, typeof(CascadeDeleteFalseTestCase.CascadeDeleteFalseHelper)
				, 1);
		}

		public virtual void CheckDelete(IExtObjectContainer oc)
		{
			AssertOccurrences(oc, typeof(CascadeDeleteFalseTestCase), 0);
			AssertOccurrences(oc, typeof(CascadeDeleteFalseTestCase.CascadeDeleteFalseHelper)
				, 1);
		}

		public class CascadeDeleteFalseHelper
		{
		}
	}
}

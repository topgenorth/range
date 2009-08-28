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
using Db4objects.Db4o.Tests.Common.Staging;

namespace Db4objects.Db4o.Tests.Common.Staging
{
	public class LazyQueryDeleteTestCase : AbstractDb4oTestCase
	{
		private const int Count = 3;

		public class Item
		{
			public string _name;

			public Item(string name)
			{
				_name = name;
			}
		}

		protected override void Configure(IConfiguration config)
		{
			config.Queries().EvaluationMode(QueryEvaluationMode.Lazy);
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			for (int i = 0; i < Count; i++)
			{
				Store(new LazyQueryDeleteTestCase.Item(i.ToString()));
				Db().Commit();
			}
		}

		public virtual void Test()
		{
			IObjectSet objectSet = NewQuery(typeof(LazyQueryDeleteTestCase.Item)).Execute();
			for (int i = 0; i < Count; i++)
			{
				Db().Delete(objectSet.Next());
				Db().Commit();
			}
		}

		public static void Main(string[] arguments)
		{
			new LazyQueryDeleteTestCase().RunSolo();
		}
	}
}

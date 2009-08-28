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
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Query;

namespace Db4objects.Db4o.Tests.Common.Concurrency
{
	public class InternStringsTestCase : Db4oClientServerTestCase
	{
		public static void Main(string[] args)
		{
			new Db4objects.Db4o.Tests.Common.Concurrency.InternStringsTestCase().RunConcurrency
				();
		}

		public string _name;

		public InternStringsTestCase() : this(null)
		{
		}

		public InternStringsTestCase(string name)
		{
			_name = name;
		}

		protected override void Configure(IConfiguration config)
		{
			config.InternStrings(true);
		}

		protected override void Store()
		{
			string name = "Foo";
			Store(new Db4objects.Db4o.Tests.Common.Concurrency.InternStringsTestCase(name));
			Store(new Db4objects.Db4o.Tests.Common.Concurrency.InternStringsTestCase(name));
		}

		public virtual void Conc(IExtObjectContainer oc)
		{
			IQuery query = oc.Query();
			query.Constrain(typeof(Db4objects.Db4o.Tests.Common.Concurrency.InternStringsTestCase
				));
			IObjectSet result = query.Execute();
			Assert.AreEqual(2, result.Size());
			Db4objects.Db4o.Tests.Common.Concurrency.InternStringsTestCase first = (Db4objects.Db4o.Tests.Common.Concurrency.InternStringsTestCase
				)result.Next();
			Db4objects.Db4o.Tests.Common.Concurrency.InternStringsTestCase second = (Db4objects.Db4o.Tests.Common.Concurrency.InternStringsTestCase
				)result.Next();
			Assert.AreSame(first._name, second._name);
		}
	}
}

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
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4oUnit.Extensions.Fixtures;
using Db4oUnit.Extensions.Util;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Refactor;

namespace Db4objects.Db4o.Tests.Common.Refactor
{
	public class RemoveArrayFieldTestCase : AbstractDb4oTestCase, IOptOutDefragSolo
	{
		public class DataBefore
		{
			public object[] array;

			public string name;

			public bool status;

			public DataBefore(string name, bool status, object[] array)
			{
				this.name = name;
				this.status = status;
				this.array = array;
			}
		}

		public class DataAfter
		{
			public string name;

			public bool status;

			public DataAfter(string name, bool status)
			{
				this.name = name;
				this.status = status;
			}
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestRemoveArrayField()
		{
			RemoveArrayFieldTestCase.DataBefore dataA = new RemoveArrayFieldTestCase.DataBefore
				("a", true, new object[] { "X" });
			RemoveArrayFieldTestCase.DataBefore dataB = new RemoveArrayFieldTestCase.DataBefore
				("b", false, new object[0]);
			Store(dataA);
			Store(dataB);
			IObjectClass oc = Fixture().Config().ObjectClass(typeof(RemoveArrayFieldTestCase.DataBefore
				));
			// we must use ReflectPlatform here as the string must include
			// the assembly name in .net
			oc.Rename(CrossPlatformServices.FullyQualifiedName(typeof(RemoveArrayFieldTestCase.DataAfter
				)));
			Reopen();
			IQuery query = NewQuery(typeof(RemoveArrayFieldTestCase.DataAfter));
			query.Descend("name").Constrain("a");
			IObjectSet result = query.Execute();
			Assert.AreEqual(1, result.Size());
			RemoveArrayFieldTestCase.DataAfter data = (RemoveArrayFieldTestCase.DataAfter)result
				.Next();
			Assert.AreEqual(dataA.name, data.name);
			Assert.AreEqual(dataA.status, data.status);
		}
	}
}

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
using Db4objects.Db4o.Tests.Common.Assorted;
using Db4objects.Db4o.Tests.Common.Regression;

namespace Db4objects.Db4o.Tests.Common.Regression
{
	public class SetRollbackTestCase : Db4oClientServerTestCase
	{
		public static void Main(string[] args)
		{
			new SetRollbackTestCase().RunClientServer();
		}

		public virtual void TestSetRollback()
		{
			IExtObjectContainer oc1 = OpenNewClient();
			IExtObjectContainer oc2 = OpenNewClient();
			try
			{
				for (int i = 0; i < 1000; i++)
				{
					SimpleObject obj1 = new SimpleObject("oc " + i, i);
					SimpleObject obj2 = new SimpleObject("oc2 " + i, i);
					oc1.Store(obj1);
					oc2.Store(obj2);
					oc2.Rollback();
					obj2 = new SimpleObject("oc2.2 " + i, i);
					oc2.Store(obj2);
				}
				oc1.Commit();
				oc2.Rollback();
				Assert.AreEqual(1000, oc1.Query(typeof(SimpleObject)).Size());
				Assert.AreEqual(1000, oc2.Query(typeof(SimpleObject)).Size());
			}
			finally
			{
				oc1.Close();
				oc2.Close();
			}
		}
	}
}

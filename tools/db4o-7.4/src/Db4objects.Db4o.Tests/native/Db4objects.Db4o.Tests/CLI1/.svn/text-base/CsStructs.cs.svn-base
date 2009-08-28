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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Query;
using Db4oUnit;
using Db4oUnit.Extensions;

namespace Db4objects.Db4o.Tests.CLI1
{
	public class CsStructs : AbstractDb4oTestCase
	{
		public static string GUID = "6a0d8033-444e-4b44-b0df-bf33dfe050f9";

		SimpleStruct simpleStruct;
		RecursiveStruct recursiveStruct;
		Guid guid;

		public CsStructs()
		{
		}

		protected override void  Store()
	    {
			simpleStruct.foo = 100;
			simpleStruct.bar = "hi";

			RecursiveStruct r = new RecursiveStruct();
			r.child = new CsStructs();

			SimpleStruct s = new SimpleStruct();
			s.foo = 22;
			s.bar = "jo";
			r.child.simpleStruct = s;

			recursiveStruct = r;

			guid = new Guid(GUID);
		    
		    Store(this);
		}

		public void Test()
		{
			IExtObjectContainer oc = Db();
            IQuery q = NewQuery(GetType());
			IQuery qd = q.Descend("simpleStruct");
			qd = qd.Descend("foo");
			qd.Constrain(100);

			IObjectSet objectSet = q.Execute();

			Assert.AreEqual(1, objectSet.Size());
			CsStructs csStructs = (CsStructs)objectSet.Next();

			Assert.AreEqual(GUID, csStructs.guid.ToString());
            Assert.AreEqual(100, csStructs.simpleStruct.foo);
            Assert.AreEqual("hi", csStructs.simpleStruct.bar);
            Assert.AreEqual(22, csStructs.recursiveStruct.child.simpleStruct.foo);
            Assert.AreEqual("jo", csStructs.recursiveStruct.child.simpleStruct.bar);
		}

	}

	public struct SimpleStruct
	{
		public int foo;
		public string bar;
	}

	public struct RecursiveStruct
	{
		public CsStructs child;
	}
}

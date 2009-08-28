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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Tests.Common.Set;

namespace Db4objects.Db4o.Tests.Common.Set
{
	public class DeepSetClientServerTestCase : Db4oClientServerTestCase
	{
		public static void Main(string[] args)
		{
			new DeepSetClientServerTestCase().RunAll();
		}

		public class Item
		{
			public DeepSetClientServerTestCase.Item child;

			public string name;
		}

		protected override void Store()
		{
			DeepSetClientServerTestCase.Item item = new DeepSetClientServerTestCase.Item();
			item.name = "1";
			item.child = new DeepSetClientServerTestCase.Item();
			item.child.name = "2";
			item.child.child = new DeepSetClientServerTestCase.Item();
			item.child.child.name = "3";
			Store(item);
		}

		/// <exception cref="Exception"></exception>
		public virtual void Test()
		{
			IExtObjectContainer oc1 = OpenNewClient();
			IExtObjectContainer oc2 = OpenNewClient();
			IExtObjectContainer oc3 = OpenNewClient();
			DeepSetClientServerTestCase.Item example = new DeepSetClientServerTestCase.Item();
			example.name = "1";
			try
			{
				DeepSetClientServerTestCase.Item item1 = (DeepSetClientServerTestCase.Item)oc1.QueryByExample
					(example).Next();
				Assert.AreEqual("1", item1.name);
				Assert.AreEqual("2", item1.child.name);
				Assert.AreEqual("3", item1.child.child.name);
				DeepSetClientServerTestCase.Item item2 = (DeepSetClientServerTestCase.Item)oc2.QueryByExample
					(example).Next();
				Assert.AreEqual("1", item2.name);
				Assert.AreEqual("2", item2.child.name);
				Assert.AreEqual("3", item2.child.child.name);
				item1.child.name = "12";
				item1.child.child.name = "13";
				oc1.Store(item1, 2);
				oc1.Commit();
				// check result
				DeepSetClientServerTestCase.Item item = (DeepSetClientServerTestCase.Item)oc1.QueryByExample
					(example).Next();
				Assert.AreEqual("1", item.name);
				Assert.AreEqual("12", item.child.name);
				Assert.AreEqual("13", item.child.child.name);
				item = (DeepSetClientServerTestCase.Item)oc2.QueryByExample(example).Next();
				oc2.Refresh(item, 3);
				Assert.AreEqual("1", item.name);
				Assert.AreEqual("12", item.child.name);
				Assert.AreEqual("3", item.child.child.name);
				item = (DeepSetClientServerTestCase.Item)oc3.QueryByExample(example).Next();
				Assert.AreEqual("1", item.name);
				Assert.AreEqual("12", item.child.name);
				Assert.AreEqual("3", item.child.child.name);
			}
			finally
			{
				oc1.Close();
				oc2.Close();
				oc3.Close();
			}
		}
	}
}

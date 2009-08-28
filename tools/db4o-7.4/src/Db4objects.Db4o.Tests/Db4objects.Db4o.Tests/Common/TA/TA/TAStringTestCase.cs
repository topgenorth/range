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
using Db4objects.Db4o.Tests.Common.TA.TA;

namespace Db4objects.Db4o.Tests.Common.TA.TA
{
	/// <exclude></exclude>
	public class TAStringTestCase : TAItemTestCaseBase
	{
		public static void Main(string[] args)
		{
			new TAStringTestCase().RunAll();
		}

		/// <exception cref="Exception"></exception>
		protected override object CreateItem()
		{
			TAStringItem item = new TAStringItem();
			item.value = "42";
			item.obj = "hello";
			return item;
		}

		/// <exception cref="Exception"></exception>
		protected override void AssertItemValue(object obj)
		{
			TAStringItem item = (TAStringItem)obj;
			Assert.AreEqual("42", item.Value());
			Assert.AreEqual("hello", item.Object());
		}

		protected override void AssertRetrievedItem(object obj)
		{
			TAStringItem item = (TAStringItem)obj;
			Assert.IsNull(item.value);
			Assert.IsNull(item.obj);
		}
	}
}

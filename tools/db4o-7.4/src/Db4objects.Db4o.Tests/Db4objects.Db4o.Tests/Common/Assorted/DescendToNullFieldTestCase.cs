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
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Assorted;

namespace Db4objects.Db4o.Tests.Common.Assorted
{
	public class DescendToNullFieldTestCase : AbstractDb4oTestCase
	{
		private static int Count = 2;

		public class ParentItem
		{
			public string _name;

			public DescendToNullFieldTestCase.ChildItem one;

			public DescendToNullFieldTestCase.ChildItem two;

			public ParentItem(string name, DescendToNullFieldTestCase.ChildItem child1, DescendToNullFieldTestCase.ChildItem
				 child2)
			{
				_name = name;
				one = child1;
				two = child2;
			}
		}

		public class ChildItem
		{
			public string _name;

			public ChildItem(string name)
			{
				_name = name;
			}
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			for (int i = 0; i < Count; i++)
			{
				Store(new DescendToNullFieldTestCase.ParentItem("one", new DescendToNullFieldTestCase.ChildItem
					("one"), null));
			}
			for (int i = 0; i < Count; i++)
			{
				Store(new DescendToNullFieldTestCase.ParentItem("two", null, new DescendToNullFieldTestCase.ChildItem
					("two")));
			}
		}

		public virtual void Test()
		{
			AssertResults("one");
			AssertResults("two");
		}

		private void AssertResults(string name)
		{
			IQuery query = NewQuery(typeof(DescendToNullFieldTestCase.ParentItem));
			query.Descend(name).Descend("_name").Constrain(name);
			IObjectSet objectSet = query.Execute();
			Assert.AreEqual(Count, objectSet.Size());
			while (objectSet.HasNext())
			{
				DescendToNullFieldTestCase.ParentItem parentItem = (DescendToNullFieldTestCase.ParentItem
					)objectSet.Next();
				Assert.AreEqual(name, parentItem._name);
			}
		}
	}
}

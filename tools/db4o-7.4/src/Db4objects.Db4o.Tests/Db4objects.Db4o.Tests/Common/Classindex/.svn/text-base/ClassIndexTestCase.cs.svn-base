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
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o.Internal.Classindex;
using Db4objects.Db4o.Tests.Common.Btree;
using Db4objects.Db4o.Tests.Common.Classindex;

namespace Db4objects.Db4o.Tests.Common.Classindex
{
	public class ClassIndexTestCase : AbstractDb4oTestCase, IOptOutCS
	{
		public class Item
		{
			public string name;

			public Item(string _name)
			{
				this.name = _name;
			}
		}

		public static void Main(string[] args)
		{
			new ClassIndexTestCase().RunSolo();
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestDelete()
		{
			ClassIndexTestCase.Item item = new ClassIndexTestCase.Item("test");
			Store(item);
			int id = (int)Db().GetID(item);
			AssertID(id);
			Reopen();
			item = (ClassIndexTestCase.Item)Db().QueryByExample(item).Next();
			id = (int)Db().GetID(item);
			AssertID(id);
			Db().Delete(item);
			Db().Commit();
			AssertEmpty();
			Reopen();
			AssertEmpty();
		}

		private void AssertID(int id)
		{
			AssertIndex(new object[] { id });
		}

		private void AssertEmpty()
		{
			AssertIndex(new object[] {  });
		}

		private void AssertIndex(object[] expected)
		{
			ExpectingVisitor visitor = new ExpectingVisitor(expected);
			IClassIndexStrategy index = ClassMetadataFor(typeof(ClassIndexTestCase.Item)).Index
				();
			index.TraverseAll(Trans(), visitor);
			visitor.AssertExpectations();
		}
	}
}

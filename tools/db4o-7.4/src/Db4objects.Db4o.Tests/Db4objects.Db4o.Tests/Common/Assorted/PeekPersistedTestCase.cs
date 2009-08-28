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
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Assorted;

namespace Db4objects.Db4o.Tests.Common.Assorted
{
	public class PeekPersistedTestCase : AbstractDb4oTestCase
	{
		public sealed class Item
		{
			public string name;

			public PeekPersistedTestCase.Item child;

			public Item()
			{
			}

			public Item(string name, PeekPersistedTestCase.Item child)
			{
				this.name = name;
				this.child = child;
			}

			public override string ToString()
			{
				return "Item(" + name + ", " + child + ")";
			}
		}

		protected override void Store()
		{
			PeekPersistedTestCase.Item root = new PeekPersistedTestCase.Item("1", null);
			PeekPersistedTestCase.Item current = root;
			for (int i = 2; i < 11; i++)
			{
				current.child = new PeekPersistedTestCase.Item(string.Empty + i, null);
				current = current.child;
			}
			Store(root);
		}

		public virtual void Test()
		{
			PeekPersistedTestCase.Item root = QueryRoot();
			for (int i = 0; i < 10; i++)
			{
				Peek(root, i);
			}
		}

		private PeekPersistedTestCase.Item QueryRoot()
		{
			IQuery q = NewQuery(typeof(PeekPersistedTestCase.Item));
			q.Descend("name").Constrain("1");
			IObjectSet objectSet = q.Execute();
			return (PeekPersistedTestCase.Item)objectSet.Next();
		}

		private void Peek(PeekPersistedTestCase.Item original, int depth)
		{
			PeekPersistedTestCase.Item peeked = (PeekPersistedTestCase.Item)Db().PeekPersisted
				(original, depth, true);
			for (int i = 0; i <= depth; i++)
			{
				Assert.IsNotNull(peeked, "Failed to peek at child " + i + " at depth " + depth);
				Assert.IsFalse(Db().IsStored(peeked));
				peeked = peeked.child;
			}
			Assert.IsNull(peeked);
		}
	}
}

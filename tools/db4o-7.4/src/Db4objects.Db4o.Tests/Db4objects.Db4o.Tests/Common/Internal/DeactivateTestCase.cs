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
using Db4objects.Db4o.Tests.Common.Internal;

namespace Db4objects.Db4o.Tests.Common.Internal
{
	public class DeactivateTestCase : AbstractDb4oTestCase
	{
		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			Db().Set(new DeactivateTestCase.Item(this, "foo", new DeactivateTestCase.Item(this
				, "bar", null)));
		}

		public virtual void Test()
		{
			IQuery query = NewQuery();
			query.Descend("_name").Constrain("foo");
			IObjectSet results = query.Execute();
			Assert.AreEqual(1, results.Size());
			DeactivateTestCase.Item item1 = (DeactivateTestCase.Item)results.Next();
			DeactivateTestCase.Item item2 = item1._child;
			Assert.IsTrue(Db().IsActive(item1));
			Assert.IsTrue(Db().IsActive(item2));
			Db().Deactivate(item1);
			Assert.IsFalse(Db().IsActive(item1));
			Assert.IsTrue(Db().IsActive(item2));
		}

		public static void Main(string[] args)
		{
			new DeactivateTestCase().RunAll();
		}

		public class Item
		{
			public DeactivateTestCase.Item _child;

			public string _name;

			public Item(DeactivateTestCase _enclosing, string name, DeactivateTestCase.Item child
				)
			{
				this._enclosing = _enclosing;
				this._name = name;
				this._child = child;
			}

			private readonly DeactivateTestCase _enclosing;
		}
	}
}

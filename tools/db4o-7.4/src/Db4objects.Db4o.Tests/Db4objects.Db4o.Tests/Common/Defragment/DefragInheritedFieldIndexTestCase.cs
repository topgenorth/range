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
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Defragment;

namespace Db4objects.Db4o.Tests.Common.Defragment
{
	public class DefragInheritedFieldIndexTestCase : AbstractDb4oTestCase, IOptOutCS
	{
		private static readonly string FieldName = "_name";

		private static readonly string[] Names = new string[] { "Foo", "Bar", "Baz" };

		/// <exception cref="Exception"></exception>
		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(typeof(DefragInheritedFieldIndexTestCase.ParentItem)).ObjectField
				(FieldName).Indexed(true);
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			for (int nameIdx = 0; nameIdx < Names.Length; nameIdx++)
			{
				Store(new DefragInheritedFieldIndexTestCase.ChildItem(Names[nameIdx]));
			}
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestDefragInheritedFieldIndex()
		{
			AssertQueryByIndex();
			Defragment();
			AssertQueryByIndex();
		}

		private void AssertQueryByIndex()
		{
			IQuery query = NewQuery(typeof(DefragInheritedFieldIndexTestCase.ChildItem));
			query.Descend(FieldName).Constrain(Names[0]);
			IObjectSet result = query.Execute();
			Assert.AreEqual(1, result.Size());
			Assert.AreEqual(Names[0], ((DefragInheritedFieldIndexTestCase.ChildItem)result.Next
				())._name);
		}

		public class ParentItem
		{
			public string _name;

			public ParentItem(string name)
			{
				_name = name;
			}
		}

		public class ChildItem : DefragInheritedFieldIndexTestCase.ParentItem
		{
			public ChildItem(string name) : base(name)
			{
			}
		}
	}
}

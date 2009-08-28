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
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Constraints;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Constraints;

namespace Db4objects.Db4o.Tests.Common.Constraints
{
	public class UniqueFieldIndexTestCase : AbstractDb4oTestCase
	{
		public static void Main(string[] arguments)
		{
			new UniqueFieldIndexTestCase().RunAll();
		}

		public class Item
		{
			public string _str;

			public Item()
			{
			}

			public Item(string str)
			{
				_str = str;
			}
		}

		/// <exception cref="Exception"></exception>
		protected override void Configure(IConfiguration config)
		{
			base.Configure(config);
			IndexField(config, typeof(UniqueFieldIndexTestCase.Item), "_str");
			config.Add(new UniqueFieldValueConstraint(typeof(UniqueFieldIndexTestCase.Item), 
				"_str"));
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			AddItem("1");
			AddItem("2");
			AddItem("3");
		}

		public virtual void TestNewViolates()
		{
			AddItem("2");
			CommitExpectingViolation();
		}

		public virtual void TestUpdateViolates()
		{
			UpdateItem("2", "3");
			CommitExpectingViolation();
		}

		public virtual void TestUpdateDoesNotViolate()
		{
			UpdateItem("2", "4");
			Db().Commit();
		}

		public virtual void TestUpdatingSameObjectDoesNotViolate()
		{
			UpdateItem("2", "2");
			Db().Commit();
		}

		public virtual void TestNewAfterDeleteDoesNotViolate()
		{
			DeleteItem("2");
			AddItem("2");
			Db().Commit();
		}

		public virtual void TestDeleteAfterNewDoesNotViolate()
		{
			UniqueFieldIndexTestCase.Item existing = QueryItem("2");
			AddItem("2");
			Db().Delete(existing);
			Db().Commit();
		}

		private void DeleteItem(string value)
		{
			Db().Delete(QueryItem(value));
		}

		private void CommitExpectingViolation()
		{
			Assert.Expect(typeof(UniqueFieldValueConstraintViolationException), new _ICodeBlock_81
				(this));
			Db().Rollback();
		}

		private sealed class _ICodeBlock_81 : ICodeBlock
		{
			public _ICodeBlock_81(UniqueFieldIndexTestCase _enclosing)
			{
				this._enclosing = _enclosing;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				this._enclosing.Db().Commit();
			}

			private readonly UniqueFieldIndexTestCase _enclosing;
		}

		private UniqueFieldIndexTestCase.Item QueryItem(string str)
		{
			IQuery q = NewQuery(typeof(UniqueFieldIndexTestCase.Item));
			q.Descend("_str").Constrain(str);
			return (UniqueFieldIndexTestCase.Item)q.Execute().Next();
		}

		private void AddItem(string value)
		{
			Store(new UniqueFieldIndexTestCase.Item(value));
		}

		private void UpdateItem(string existing, string newValue)
		{
			UniqueFieldIndexTestCase.Item item = QueryItem(existing);
			item._str = newValue;
			Store(item);
		}
	}
}

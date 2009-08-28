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
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Reflect;
using Db4objects.Db4o.Tests.Common.TA;

namespace Db4objects.Db4o.Tests.Common.TA
{
	public abstract class ItemTestCaseBase : TransparentActivationTestCaseBase, IOptOutDefragSolo
	{
		private Type _clazz;

		protected long id;

		protected Db4oUUID uuid;

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			object value = CreateItem();
			_clazz = value.GetType();
			Store(value);
			id = Db().Ext().GetID(value);
			uuid = Db().Ext().GetObjectInfo(value).GetUUID();
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestQuery()
		{
			object item = RetrieveOnlyInstance();
			AssertRetrievedItem(item);
			AssertItemValue(item);
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestDeactivate()
		{
			object item = RetrieveOnlyInstance();
			Db().Deactivate(item, 1);
			AssertNullItem(item);
			Db().Activate(item, 42);
			Db().Deactivate(item, 1);
			AssertNullItem(item);
		}

		protected virtual object RetrieveOnlyInstance()
		{
			return RetrieveOnlyInstance(_clazz);
		}

		/// <exception cref="Exception"></exception>
		protected virtual void AssertNullItem(object obj)
		{
			IReflectClass claxx = Reflector().ForObject(obj);
			IReflectField[] fields = claxx.GetDeclaredFields();
			for (int i = 0; i < fields.Length; ++i)
			{
				IReflectField field = fields[i];
				if (field.IsStatic() || field.IsTransient())
				{
					continue;
				}
				IReflectClass type = field.GetFieldType();
				if (Container().ClassMetadataForReflectClass(type).IsSecondClass())
				{
					continue;
				}
				object value = field.Get(obj);
				Assert.IsNull(value);
			}
		}

		/// <exception cref="Exception"></exception>
		protected abstract void AssertItemValue(object obj);

		/// <exception cref="Exception"></exception>
		protected abstract object CreateItem();

		/// <exception cref="Exception"></exception>
		protected abstract void AssertRetrievedItem(object obj);
	}
}

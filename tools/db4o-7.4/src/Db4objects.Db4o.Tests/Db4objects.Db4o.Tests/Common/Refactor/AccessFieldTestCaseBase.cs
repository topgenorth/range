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
using System.IO;
using Db4oUnit;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation.IO;
using Db4objects.Db4o.Tests.Common.Refactor;

namespace Db4objects.Db4o.Tests.Common.Refactor
{
	public abstract class AccessFieldTestCaseBase
	{
		private readonly string DatabasePath = Path.GetTempFileName();

		/// <exception cref="Exception"></exception>
		public virtual void SetUp()
		{
			DeleteFile();
			WithDatabase(new _IDatabaseAction_16(this));
		}

		private sealed class _IDatabaseAction_16 : AccessFieldTestCaseBase.IDatabaseAction
		{
			public _IDatabaseAction_16(AccessFieldTestCaseBase _enclosing)
			{
				this._enclosing = _enclosing;
			}

			public void RunWith(IObjectContainer db)
			{
				db.Store(this._enclosing.NewOriginalData());
			}

			private readonly AccessFieldTestCaseBase _enclosing;
		}

		/// <exception cref="Exception"></exception>
		public virtual void TearDown()
		{
			DeleteFile();
		}

		protected virtual void RenameClass(Type origClazz, string targetName)
		{
			IConfiguration config = Db4oFactory.NewConfiguration();
			config.ObjectClass(origClazz).Rename(targetName);
			WithDatabase(config, new _IDatabaseAction_30());
		}

		private sealed class _IDatabaseAction_30 : AccessFieldTestCaseBase.IDatabaseAction
		{
			public _IDatabaseAction_30()
			{
			}

			public void RunWith(IObjectContainer db)
			{
			}
		}

		// do nothing
		protected abstract object NewOriginalData();

		protected virtual void AssertField(Type targetClazz, string fieldName, Type fieldType
			, object fieldValue)
		{
			WithDatabase(new _IDatabaseAction_40(targetClazz, fieldName, fieldType, fieldValue
				));
		}

		private sealed class _IDatabaseAction_40 : AccessFieldTestCaseBase.IDatabaseAction
		{
			public _IDatabaseAction_40(Type targetClazz, string fieldName, Type fieldType, object
				 fieldValue)
			{
				this.targetClazz = targetClazz;
				this.fieldName = fieldName;
				this.fieldType = fieldType;
				this.fieldValue = fieldValue;
			}

			public void RunWith(IObjectContainer db)
			{
				IStoredClass storedClass = db.Ext().StoredClass(targetClazz);
				IStoredField storedField = storedClass.StoredField(fieldName, fieldType);
				IObjectSet result = db.Query(targetClazz);
				Assert.AreEqual(1, result.Size());
				object obj = result.Next();
				object value = storedField.Get(obj);
				Assert.AreEqual(fieldValue, value);
			}

			private readonly Type targetClazz;

			private readonly string fieldName;

			private readonly Type fieldType;

			private readonly object fieldValue;
		}

		private void DeleteFile()
		{
			File4.Delete(DatabasePath);
		}

		private interface IDatabaseAction
		{
			void RunWith(IObjectContainer db);
		}

		private void WithDatabase(AccessFieldTestCaseBase.IDatabaseAction action)
		{
			WithDatabase(Db4oFactory.NewConfiguration(), action);
		}

		private void WithDatabase(IConfiguration config, AccessFieldTestCaseBase.IDatabaseAction
			 action)
		{
			IObjectContainer db = Db4oFactory.OpenFile(config, DatabasePath);
			try
			{
				action.RunWith(db);
			}
			finally
			{
				db.Close();
			}
		}
	}
}

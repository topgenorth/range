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
using System.IO;
using Db4oUnit;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Defragment;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Defragment;

namespace Db4objects.Db4o.Tests.Common.Defragment
{
	public class SlotDefragmentFixture
	{
		public static readonly string PrimitiveFieldname = "_id";

		public static readonly string WrapperFieldname = "_wrapper";

		public static readonly string TypedobjectFieldname = "_next";

		public class Data
		{
			public int _id;

			public int _wrapper;

			public SlotDefragmentFixture.Data _next;

			public Data(int id, SlotDefragmentFixture.Data next)
			{
				_id = id;
				_wrapper = id;
				_next = next;
			}
		}

		public const int Value = 42;

		public static DefragmentConfig DefragConfig(bool forceBackupDelete)
		{
			DefragmentConfig defragConfig = new DefragmentConfig(SlotDefragmentTestConstants.
				Filename, SlotDefragmentTestConstants.Backupfilename);
			defragConfig.ForceBackupDelete(forceBackupDelete);
			defragConfig.Db4oConfig(Db4oConfig());
			return defragConfig;
		}

		public static IConfiguration Db4oConfig()
		{
			IConfiguration db4oConfig = Db4oFactory.NewConfiguration();
			db4oConfig.ReflectWith(Platform4.ReflectorForType(typeof(SlotDefragmentFixture.Data
				)));
			return db4oConfig;
		}

		public static void CreateFile(string fileName)
		{
			IObjectContainer db = Db4oFactory.OpenFile(Db4oConfig(), fileName);
			SlotDefragmentFixture.Data data = null;
			for (int value = Value - 1; value <= Value + 1; value++)
			{
				data = new SlotDefragmentFixture.Data(value, data);
				db.Store(data);
			}
			db.Close();
		}

		public static void ForceIndex()
		{
			IConfiguration config = Db4oConfig();
			config.ObjectClass(typeof(SlotDefragmentFixture.Data)).ObjectField(PrimitiveFieldname
				).Indexed(true);
			config.ObjectClass(typeof(SlotDefragmentFixture.Data)).ObjectField(WrapperFieldname
				).Indexed(true);
			config.ObjectClass(typeof(SlotDefragmentFixture.Data)).ObjectField(TypedobjectFieldname
				).Indexed(true);
			IObjectContainer db = Db4oFactory.OpenFile(config, SlotDefragmentTestConstants.Filename
				);
			Assert.IsTrue(db.Ext().StoredClass(typeof(SlotDefragmentFixture.Data)).StoredField
				(PrimitiveFieldname, typeof(int)).HasIndex());
			Assert.IsTrue(db.Ext().StoredClass(typeof(SlotDefragmentFixture.Data)).StoredField
				(WrapperFieldname, typeof(int)).HasIndex());
			Assert.IsTrue(db.Ext().StoredClass(typeof(SlotDefragmentFixture.Data)).StoredField
				(TypedobjectFieldname, typeof(SlotDefragmentFixture.Data)).HasIndex());
			db.Close();
		}

		/// <exception cref="IOException"></exception>
		public static void AssertIndex(string fieldName)
		{
			ForceIndex();
			DefragmentConfig defragConfig = new DefragmentConfig(SlotDefragmentTestConstants.
				Filename, SlotDefragmentTestConstants.Backupfilename);
			defragConfig.Db4oConfig(Db4oConfig());
			Db4objects.Db4o.Defragment.Defragment.Defrag(defragConfig);
			IObjectContainer db = Db4oFactory.OpenFile(Db4oConfig(), SlotDefragmentTestConstants
				.Filename);
			IQuery query = db.Query();
			query.Constrain(typeof(SlotDefragmentFixture.Data));
			query.Descend(fieldName).Constrain(Value);
			IObjectSet result = query.Execute();
			Assert.AreEqual(1, result.Size());
			db.Close();
		}

		public static void AssertDataClassKnown(bool expected)
		{
			IObjectContainer db = Db4oFactory.OpenFile(Db4oConfig(), SlotDefragmentTestConstants
				.Filename);
			try
			{
				IStoredClass storedClass = db.Ext().StoredClass(typeof(SlotDefragmentFixture.Data
					));
				if (expected)
				{
					Assert.IsNotNull(storedClass);
				}
				else
				{
					Assert.IsNull(storedClass);
				}
			}
			finally
			{
				db.Close();
			}
		}
	}
}

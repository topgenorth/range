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
using System.Collections;
using Db4oUnit;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Tests.Common.Freespace;
using Db4objects.Db4o.Tests.Common.Handlers;

namespace Db4objects.Db4o.Tests.Common.Freespace
{
	/// <exclude></exclude>
	public class FreespaceManagerMigrationTestCase : FormatMigrationTestCaseBase
	{
		internal int[][] IntArrayData = new int[][] { new int[] { 1, 2 }, new int[] { 3, 
			4 } };

		internal string[][] StringArrayData = new string[][] { new string[] { "a", "b" }, 
			new string[] { "c", "d" } };

		public class StClass
		{
			public int id;

			public ArrayList vect;

			public virtual ArrayList GetVect()
			{
				return vect;
			}

			public virtual void SetVect(ArrayList vect)
			{
				this.vect = vect;
			}

			public virtual int GetId()
			{
				return id;
			}

			public virtual void SetId(int id)
			{
				this.id = id;
			}
		}

		protected override void ConfigureForStore(IConfiguration config)
		{
			if (NotApplicableForDb4oVersion())
			{
				return;
			}
			CommonConfigure(config);
			config.Freespace().UseIndexSystem();
		}

		private bool NotApplicableForDb4oVersion()
		{
			return Db4oMajorVersion() < 5;
		}

		protected override void ConfigureForTest(IConfiguration config)
		{
			if (NotApplicableForDb4oVersion())
			{
				return;
			}
			CommonConfigure(config);
			config.Freespace().UseBTreeSystem();
		}

		private void CommonConfigure(IConfiguration config)
		{
			// config.blockSize(8);
			config.ObjectClass(typeof(FreespaceManagerMigrationTestCase.StClass)).CascadeOnActivate
				(true);
			config.ObjectClass(typeof(FreespaceManagerMigrationTestCase.StClass)).CascadeOnUpdate
				(true);
			config.ObjectClass(typeof(FreespaceManagerMigrationTestCase.StClass)).CascadeOnDelete
				(true);
			config.ObjectClass(typeof(FreespaceManagerMigrationTestCase.StClass)).MinimumActivationDepth
				(5);
			config.ObjectClass(typeof(FreespaceManagerMigrationTestCase.StClass)).UpdateDepth
				(10);
		}

		protected override void AssertObjectsAreReadable(IExtObjectContainer objectContainer
			)
		{
			if (NotApplicableForDb4oVersion())
			{
				return;
			}
			IObjectSet objectSet = objectContainer.Query(typeof(FreespaceManagerMigrationTestCase.StClass
				));
			for (int i = 0; i < 2; i++)
			{
				FreespaceManagerMigrationTestCase.StClass cls = (FreespaceManagerMigrationTestCase.StClass
					)objectSet.Next();
				ArrayList v = cls.GetVect();
				int[][] intArray = (int[][])v[0];
				ArrayAssert.AreEqual(IntArrayData[0], intArray[0]);
				ArrayAssert.AreEqual(IntArrayData[1], intArray[1]);
				string[][] stringArray = (string[][])v[1];
				ArrayAssert.AreEqual(StringArrayData[0], stringArray[0]);
				ArrayAssert.AreEqual(StringArrayData[1], stringArray[1]);
				objectContainer.Delete(cls);
			}
		}

		protected override string FileNamePrefix()
		{
			return "freespace";
		}

		protected override void Store(IExtObjectContainer objectContainer)
		{
			if (NotApplicableForDb4oVersion())
			{
				return;
			}
			for (int i = 0; i < 10; i++)
			{
				FreespaceManagerMigrationTestCase.StClass cls = new FreespaceManagerMigrationTestCase.StClass
					();
				ArrayList v = new ArrayList(10);
				v.Add(IntArrayData);
				v.Add(StringArrayData);
				cls.SetId(i);
				cls.SetVect(v);
				objectContainer.Set(cls);
			}
		}
	}
}

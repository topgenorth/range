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
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Defragment;

namespace Db4objects.Db4o.Tests.Common.Defragment
{
	public class SlotDefragmentTestCase : ITestLifeCycle
	{
		/// <exception cref="Exception"></exception>
		public virtual void TestPrimitiveIndex()
		{
			SlotDefragmentFixture.AssertIndex(SlotDefragmentFixture.PrimitiveFieldname);
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestWrapperIndex()
		{
			SlotDefragmentFixture.AssertIndex(SlotDefragmentFixture.WrapperFieldname);
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestTypedObjectIndex()
		{
			SlotDefragmentFixture.ForceIndex();
			Db4objects.Db4o.Defragment.Defragment.Defrag(SlotDefragmentTestConstants.Filename
				, SlotDefragmentTestConstants.Backupfilename);
			IObjectContainer db = Db4oFactory.OpenFile(Db4oFactory.NewConfiguration(), SlotDefragmentTestConstants
				.Filename);
			IQuery query = db.Query();
			query.Constrain(typeof(SlotDefragmentFixture.Data));
			query.Descend(SlotDefragmentFixture.TypedobjectFieldname).Descend(SlotDefragmentFixture
				.PrimitiveFieldname).Constrain(SlotDefragmentFixture.Value);
			IObjectSet result = query.Execute();
			Assert.AreEqual(1, result.Size());
			db.Close();
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestNoForceDelete()
		{
			Db4objects.Db4o.Defragment.Defragment.Defrag(SlotDefragmentTestConstants.Filename
				, SlotDefragmentTestConstants.Backupfilename);
			Assert.Expect(typeof(IOException), new _ICodeBlock_37());
		}

		private sealed class _ICodeBlock_37 : ICodeBlock
		{
			public _ICodeBlock_37()
			{
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				Db4objects.Db4o.Defragment.Defragment.Defrag(SlotDefragmentTestConstants.Filename
					, SlotDefragmentTestConstants.Backupfilename);
			}
		}

		/// <exception cref="Exception"></exception>
		public virtual void SetUp()
		{
			new Sharpen.IO.File(SlotDefragmentTestConstants.Filename).Delete();
			new Sharpen.IO.File(SlotDefragmentTestConstants.Backupfilename).Delete();
			SlotDefragmentFixture.CreateFile(SlotDefragmentTestConstants.Filename);
		}

		/// <exception cref="Exception"></exception>
		public virtual void TearDown()
		{
		}
	}
}

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
using System.Collections;
using System.IO;
using System.Text;
using Db4oUnit;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Defragment;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Tests.Common.Defragment;

namespace Db4objects.Db4o.Tests.Common.Defragment
{
	public class BlockSizeDefragTestCase : ITestCase
	{
		private static readonly string FileName = Path.GetTempPath() + "/blocksizedefrag.db4o";

		public class ItemA
		{
			public int _id;

			public ItemA(int id)
			{
				_id = id;
			}

			public override string ToString()
			{
				return "A" + _id;
			}
		}

		public class ItemB
		{
			public string _name;

			public ItemB(string name)
			{
				_name = name;
			}

			public override string ToString()
			{
				return "A" + _name;
			}
		}

		private static readonly int[] BlockSizes = new int[] { 1, 2, 3, 4, 7, 8, 13, 16 };

		private const int NumItemsPerClass = 10;

		private const int DeleteRatio = 3;

		/// <exception cref="IOException"></exception>
		public virtual void Test()
		{
			for (int idx = 0; idx < BlockSizes.Length; idx++)
			{
				AssertBlockSizeDefrag(BlockSizes[idx]);
			}
		}

		/// <exception cref="IOException"></exception>
		private void AssertBlockSizeDefrag(int blockSize)
		{
			string fileName = FileName;
			new Sharpen.IO.File(fileName).Delete();
			CreateDatabase(fileName, blockSize);
			Defrag(fileName, blockSize);
			AssertCanRead(fileName, blockSize);
			new Sharpen.IO.File(fileName).Delete();
		}

		private void CreateDatabase(string fileName, int blockSize)
		{
			IObjectContainer db = Db4oFactory.OpenFile(Config(blockSize), fileName);
			Collection4 removed = new Collection4();
			for (int idx = 0; idx < NumItemsPerClass; idx++)
			{
				BlockSizeDefragTestCase.ItemA itemA = new BlockSizeDefragTestCase.ItemA(idx);
				BlockSizeDefragTestCase.ItemB itemB = new BlockSizeDefragTestCase.ItemB(FillStr('x'
					, idx));
				db.Store(itemA);
				db.Store(itemB);
				if ((idx % DeleteRatio) == 0)
				{
					removed.Add(itemA);
					removed.Add(itemB);
				}
			}
			db.Commit();
			DeleteAndReadd(db, removed);
			db.Close();
		}

		private void DeleteAndReadd(IObjectContainer db, Collection4 removed)
		{
			IEnumerator removeIter = removed.GetEnumerator();
			while (removeIter.MoveNext())
			{
				db.Delete(removeIter.Current);
			}
			db.Commit();
			IEnumerator readdIter = removed.GetEnumerator();
			while (readdIter.MoveNext())
			{
				db.Store(readdIter.Current);
			}
			db.Commit();
		}

		/// <exception cref="IOException"></exception>
		private void Defrag(string fileName, int blockSize)
		{
			DefragmentConfig config = new DefragmentConfig(fileName);
			config.Db4oConfig(Config(blockSize));
			config.ForceBackupDelete(true);
			Db4objects.Db4o.Defragment.Defragment.Defrag(config);
		}

		private void AssertCanRead(string fileName, int blockSize)
		{
			IObjectContainer db = Db4oFactory.OpenFile(Config(blockSize), fileName);
			AssertResult(db, typeof(BlockSizeDefragTestCase.ItemA));
			AssertResult(db, typeof(BlockSizeDefragTestCase.ItemB));
			db.Close();
		}

		private void AssertResult(IObjectContainer db, Type clazz)
		{
			IObjectSet result = db.Query(clazz);
			Assert.AreEqual(NumItemsPerClass, result.Size());
			while (result.HasNext())
			{
				Assert.IsInstanceOf(clazz, result.Next());
			}
		}

		private IConfiguration Config(int blockSize)
		{
			IConfiguration config = Db4oFactory.NewConfiguration();
			config.BlockSize(blockSize);
			config.ReflectWith(Platform4.ReflectorForType(typeof(BlockSizeDefragTestCase.ItemA
				)));
			return config;
		}

		private string FillStr(char ch, int len)
		{
			StringBuilder buf = new StringBuilder();
			for (int idx = 0; idx < len; idx++)
			{
				buf.Append(ch);
			}
			return buf.ToString();
		}
	}
}

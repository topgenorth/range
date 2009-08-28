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
using Db4objects.Db4o.Defragment;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation.IO;
using Db4objects.Db4o.IO;
using Db4objects.Db4o.Tests.Common.Defragment;

namespace Db4objects.Db4o.Tests.Common.Defragment
{
	public class COR775TestCase : ITestLifeCycle
	{
		private static readonly string Original = Path.GetTempFileName();

		private static readonly string Defgared = Original + ".bk";

		internal IConfiguration db4oConfig;

		/// <exception cref="Exception"></exception>
		public virtual void SetUp()
		{
			Cleanup();
		}

		/// <exception cref="Exception"></exception>
		public virtual void TearDown()
		{
			Cleanup();
		}

		private void Cleanup()
		{
			File4.Delete(Original);
			File4.Delete(Defgared);
		}

		public static void Main(string[] args)
		{
			new ConsoleTestRunner(typeof(COR775TestCase)).Run();
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestCOR775()
		{
			Prepare();
			VerifyDB();
			DefragmentConfig config = new DefragmentConfig(Original, Defgared);
			config.ForceBackupDelete(true);
			//config.storedClassFilter(new AvailableClassFilter());
			config.Db4oConfig(GetConfiguration());
			Db4objects.Db4o.Defragment.Defragment.Defrag(config);
			VerifyDB();
		}

		private void Prepare()
		{
			Sharpen.IO.File file = new Sharpen.IO.File(Original);
			if (file.Exists())
			{
				file.Delete();
			}
			IObjectContainer testDB = OpenDB();
			COR775TestCase.Item item = new COR775TestCase.Item("richard", 100);
			testDB.Store(item);
			testDB.Close();
		}

		private void VerifyDB()
		{
			IObjectContainer testDB = OpenDB();
			IObjectSet result = testDB.QueryByExample(typeof(COR775TestCase.Item));
			if (result.HasNext())
			{
				COR775TestCase.Item retrievedItem = (COR775TestCase.Item)result.Next();
				Assert.AreEqual("richard", retrievedItem.name);
				Assert.AreEqual(100, retrievedItem.value);
			}
			else
			{
				Assert.Fail("Cannot retrieve the expected object.");
			}
			testDB.Close();
		}

		private IObjectContainer OpenDB()
		{
			IConfiguration db4oConfig = GetConfiguration();
			IObjectContainer testDB = Db4oFactory.OpenFile(db4oConfig, Original);
			return testDB;
		}

		private IConfiguration GetConfiguration()
		{
			if (db4oConfig == null)
			{
				db4oConfig = Db4oFactory.NewConfiguration();
				db4oConfig.ActivationDepth(int.MaxValue);
				db4oConfig.CallConstructors(true);
				IoAdapter ioAdapter = new COR775TestCase.MockIOAdapter(new RandomAccessFileAdapter
					(), "db4o");
				db4oConfig.Io(ioAdapter);
			}
			return db4oConfig;
		}

		public class Item
		{
			public string name;

			public int value;

			public Item(string name, int value)
			{
				this.name = name;
				this.value = value;
			}
		}

		public class MockIOAdapter : IoAdapter
		{
			private IoAdapter ioAdapter;

			private string password;

			private long pos;

			public MockIOAdapter(IoAdapter ioAdapter, string password)
			{
				this.ioAdapter = ioAdapter;
				this.password = password;
			}

			/// <exception cref="Db4oIOException"></exception>
			public override void Close()
			{
				ioAdapter.Close();
			}

			public override void Delete(string path)
			{
				ioAdapter.Delete(path);
			}

			public override bool Exists(string path)
			{
				return ioAdapter.Exists(path);
			}

			/// <exception cref="Db4oIOException"></exception>
			public override long GetLength()
			{
				return ioAdapter.GetLength();
			}

			/// <exception cref="Db4oIOException"></exception>
			public override IoAdapter Open(string path, bool lockFile, long initialLength, bool
				 readOnly)
			{
				return new COR775TestCase.MockIOAdapter(ioAdapter.Open(path, lockFile, initialLength
					, readOnly), password);
			}

			/// <exception cref="Db4oIOException"></exception>
			public override int Read(byte[] bytes, int length)
			{
				ioAdapter.Read(bytes);
				for (int i = 0; i < length; i++)
				{
					bytes[i] = (byte)(bytes[i] - password.GetHashCode());
				}
				ioAdapter.Seek(pos + length);
				return length;
			}

			/// <exception cref="Db4oIOException"></exception>
			public override void Seek(long pos)
			{
				this.pos = pos;
				ioAdapter.Seek(pos);
			}

			/// <exception cref="Db4oIOException"></exception>
			public override void Sync()
			{
				ioAdapter.Sync();
			}

			/// <exception cref="Db4oIOException"></exception>
			public override void Write(byte[] buffer, int length)
			{
				for (int i = 0; i < length; i++)
				{
					buffer[i] = (byte)(buffer[i] + password.GetHashCode());
				}
				ioAdapter.Write(buffer, length);
				Seek(pos + length);
			}
		}
	}
}

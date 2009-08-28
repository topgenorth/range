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
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Types.Arrays;

namespace Db4objects.Db4o.Tests.Common.Types.Arrays
{
	public class ByteArrayTestCase : AbstractDb4oTestCase
	{
		public static void Main(string[] args)
		{
			new ByteArrayTestCase().RunAll();
		}

		public interface IIByteArrayHolder
		{
			byte[] GetBytes();
		}

		[System.Serializable]
		public class SerializableByteArrayHolder : ByteArrayTestCase.IIByteArrayHolder
		{
			private const long serialVersionUID = 1L;

			internal byte[] _bytes;

			public SerializableByteArrayHolder(byte[] bytes)
			{
				this._bytes = bytes;
			}

			public virtual byte[] GetBytes()
			{
				return _bytes;
			}
		}

		public class ByteArrayHolder : ByteArrayTestCase.IIByteArrayHolder
		{
			public byte[] _bytes;

			public ByteArrayHolder(byte[] bytes)
			{
				this._bytes = bytes;
			}

			public virtual byte[] GetBytes()
			{
				return _bytes;
			}
		}

		internal const int Instances = 2;

		internal const int ArrayLength = 1024;

		#if !CF
		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(typeof(ByteArrayTestCase.SerializableByteArrayHolder)).Translate
				(new TSerializable());
		}
		#endif // !CF

		protected override void Store()
		{
			for (int i = 0; i < Instances; ++i)
			{
				Db().Store(new ByteArrayTestCase.ByteArrayHolder(CreateByteArray()));
				Db().Store(new ByteArrayTestCase.SerializableByteArrayHolder(CreateByteArray()));
			}
		}

		#if !CF
		/// <exception cref="Exception"></exception>
		public virtual void TestByteArrayHolder()
		{
			TimeQueryLoop("raw byte array", typeof(ByteArrayTestCase.ByteArrayHolder));
		}
		#endif // !CF

		#if !CF
		/// <exception cref="Exception"></exception>
		public virtual void TestSerializableByteArrayHolder()
		{
			TimeQueryLoop("TSerializable", typeof(ByteArrayTestCase.SerializableByteArrayHolder
				));
		}
		#endif // !CF

		/// <exception cref="Exception"></exception>
		private void TimeQueryLoop(string label, Type clazz)
		{
			IQuery query = NewQuery(clazz);
			IObjectSet os = query.Execute();
			Assert.AreEqual(Instances, os.Size());
			while (os.HasNext())
			{
				Assert.AreEqual(ArrayLength, ((ByteArrayTestCase.IIByteArrayHolder)os.Next()).GetBytes
					().Length, label);
			}
		}

		internal virtual byte[] CreateByteArray()
		{
			byte[] bytes = new byte[ArrayLength];
			for (int i = 0; i < bytes.Length; ++i)
			{
				bytes[i] = (byte)(i % 256);
			}
			return bytes;
		}
	}
}

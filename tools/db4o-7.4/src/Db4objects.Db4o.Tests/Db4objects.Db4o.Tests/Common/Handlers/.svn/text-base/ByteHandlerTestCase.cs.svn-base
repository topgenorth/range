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
using Db4objects.Db4o.Internal.Handlers;
using Db4objects.Db4o.Tests.Common.Handlers;

namespace Db4objects.Db4o.Tests.Common.Handlers
{
	public class ByteHandlerTestCase : TypeHandlerTestCaseBase
	{
		public static void Main(string[] args)
		{
			new ByteHandlerTestCase().RunSolo();
		}

		public class Item
		{
			public byte _byte;

			public byte _byteWrapper;

			public Item(byte b, byte wrapper)
			{
				_byte = b;
				_byteWrapper = wrapper;
			}

			public override bool Equals(object obj)
			{
				if (obj == this)
				{
					return true;
				}
				if (!(obj is ByteHandlerTestCase.Item))
				{
					return false;
				}
				ByteHandlerTestCase.Item other = (ByteHandlerTestCase.Item)obj;
				return (other._byte == this._byte) && this._byteWrapper.Equals(other._byteWrapper
					);
			}

			public override string ToString()
			{
				return "[" + _byte + "," + _byteWrapper + "]";
			}
		}

		private ByteHandler ByteHandler()
		{
			return new ByteHandler();
		}

		public virtual void TestReadWrite()
		{
			MockWriteContext writeContext = new MockWriteContext(Db());
			byte expected = (byte)unchecked((int)(0x61));
			ByteHandler().Write(writeContext, expected);
			MockReadContext readContext = new MockReadContext(writeContext);
			byte byteValue = (byte)ByteHandler().Read(readContext);
			Assert.AreEqual(expected, byteValue);
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestStoreObject()
		{
			ByteHandlerTestCase.Item storedItem = new ByteHandlerTestCase.Item((byte)5, (byte
				)6);
			DoTestStoreObject(storedItem);
		}
	}
}

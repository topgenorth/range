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
	public class ShortHandlerTestCase : TypeHandlerTestCaseBase
	{
		public static void Main(string[] args)
		{
			new ShortHandlerTestCase().RunSolo();
		}

		public class Item
		{
			public short _short;

			public short _shortWrapper;

			public Item(short s, short wrapper)
			{
				_short = s;
				_shortWrapper = wrapper;
			}

			public override bool Equals(object obj)
			{
				if (obj == this)
				{
					return true;
				}
				if (!(obj is ShortHandlerTestCase.Item))
				{
					return false;
				}
				ShortHandlerTestCase.Item other = (ShortHandlerTestCase.Item)obj;
				return (other._short == this._short) && this._shortWrapper.Equals(other._shortWrapper
					);
			}

			public override string ToString()
			{
				return "[" + _short + "," + _shortWrapper + "]";
			}
		}

		private ShortHandler ShortHandler()
		{
			return new ShortHandler();
		}

		public virtual void TestReadWrite()
		{
			MockWriteContext writeContext = new MockWriteContext(Db());
			short expected = (short)unchecked((int)(0x1020));
			ShortHandler().Write(writeContext, expected);
			MockReadContext readContext = new MockReadContext(writeContext);
			short shortValue = (short)ShortHandler().Read(readContext);
			Assert.AreEqual(expected, shortValue);
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestStoreObject()
		{
			ShortHandlerTestCase.Item storedItem = new ShortHandlerTestCase.Item((short)unchecked(
				(int)(0x1020)), (short)unchecked((int)(0x1122)));
			DoTestStoreObject(storedItem);
		}
	}
}

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
	public class CharHandlerTestCase : TypeHandlerTestCaseBase
	{
		public static void Main(string[] args)
		{
			new CharHandlerTestCase().RunSolo();
		}

		public class Item
		{
			public char _char;

			public char _charWrapper;

			public Item(char c, char wrapper)
			{
				_char = c;
				_charWrapper = wrapper;
			}

			public override bool Equals(object obj)
			{
				if (obj == this)
				{
					return true;
				}
				if (!(obj is CharHandlerTestCase.Item))
				{
					return false;
				}
				CharHandlerTestCase.Item other = (CharHandlerTestCase.Item)obj;
				return (other._char == this._char) && this._charWrapper.Equals(other._charWrapper
					);
			}

			public override string ToString()
			{
				return "[" + _char + "," + _charWrapper + "]";
			}
		}

		private CharHandler CharHandler()
		{
			return new CharHandler();
		}

		public virtual void TestReadWrite()
		{
			MockWriteContext writeContext = new MockWriteContext(Db());
			char expected = (char)unchecked((int)(0x4e2d));
			CharHandler().Write(writeContext, expected);
			MockReadContext readContext = new MockReadContext(writeContext);
			char charValue = (char)CharHandler().Read(readContext);
			Assert.AreEqual(expected, charValue);
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestStoreObject()
		{
			CharHandlerTestCase.Item storedItem = new CharHandlerTestCase.Item((char)unchecked(
				(int)(0x4e2f)), (char)unchecked((int)(0x4e2d)));
			DoTestStoreObject(storedItem);
		}
	}
}

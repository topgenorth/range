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
using Db4oUnit;
using Db4objects.Db4o.Tests.Common.Handlers;

namespace Db4objects.Db4o.Tests.Common.Handlers
{
	public class FloatHandlerTestCase : TypeHandlerTestCaseBase
	{
		public static void Main(string[] args)
		{
			new FloatHandlerTestCase().RunSolo();
		}

		private Db4objects.Db4o.Internal.Handlers.FloatHandler FloatHandler()
		{
			return new Db4objects.Db4o.Internal.Handlers.FloatHandler();
		}

		public virtual void TestReadWrite()
		{
			MockWriteContext writeContext = new MockWriteContext(Db());
			float expected = float.MaxValue;
			FloatHandler().Write(writeContext, expected);
			MockReadContext readContext = new MockReadContext(writeContext);
			float f = (float)FloatHandler().Read(readContext);
			Assert.AreEqual(expected, f);
		}

		public virtual void TestStoreObject()
		{
			FloatHandlerTestCase.Item storedItem = new FloatHandlerTestCase.Item(1.23456789f, 
				1.23456789f);
			DoTestStoreObject(storedItem);
		}

		public class Item
		{
			public float _float;

			public float _floatWrapper;

			public Item(float f, float wrapper)
			{
				_float = f;
				_floatWrapper = wrapper;
			}

			public override bool Equals(object obj)
			{
				if (obj == this)
				{
					return true;
				}
				if (!(obj is FloatHandlerTestCase.Item))
				{
					return false;
				}
				FloatHandlerTestCase.Item other = (FloatHandlerTestCase.Item)obj;
				return (other._float == this._float) && this._floatWrapper.Equals(other._floatWrapper
					);
			}

			public override string ToString()
			{
				return "[" + _float + "," + _floatWrapper + "]";
			}
		}
	}
}

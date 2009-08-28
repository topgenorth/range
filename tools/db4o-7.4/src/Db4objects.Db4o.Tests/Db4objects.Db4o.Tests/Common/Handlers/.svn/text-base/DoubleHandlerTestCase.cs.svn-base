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
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Handlers;
using Db4objects.Db4o.Tests.Common.Handlers;

namespace Db4objects.Db4o.Tests.Common.Handlers
{
	/// <exclude></exclude>
	public class DoubleHandlerTestCase : TypeHandlerTestCaseBase
	{
		private IIndexable4 _handler;

		public static void Main(string[] args)
		{
			new DoubleHandlerTestCase().RunSolo();
		}

		/// <exception cref="Exception"></exception>
		protected override void Db4oSetupBeforeStore()
		{
			_handler = new DoubleHandler();
		}

		public virtual void TestMarshalling()
		{
			double expected = 1.1;
			ByteArrayBuffer buffer = new ByteArrayBuffer(_handler.LinkLength());
			_handler.WriteIndexEntry(buffer, expected);
			buffer.Seek(0);
			object actual = _handler.ReadIndexEntry(buffer);
			Assert.AreEqual(expected, actual);
		}

		public virtual void TestComparison()
		{
			AssertComparison(0, 1.1, 1.1);
			AssertComparison(-1, 1.0, 1.1);
			AssertComparison(1, 1.1, 0.5);
		}

		private void AssertComparison(int expected, double prepareWith, double compareTo)
		{
			IPreparedComparison preparedComparison = _handler.PrepareComparison(Stream().Transaction
				().Context(), prepareWith);
			double doubleCompareTo = compareTo;
			Assert.AreEqual(expected, preparedComparison.CompareTo(doubleCompareTo));
		}

		public virtual void TestReadWrite()
		{
			MockWriteContext writeContext = new MockWriteContext(Db());
			DoubleHandler doubleHandler = (DoubleHandler)_handler;
			double expected = 1.23456789;
			doubleHandler.Write(writeContext, expected);
			MockReadContext readContext = new MockReadContext(writeContext);
			double d = (double)doubleHandler.Read(readContext);
			Assert.AreEqual(expected, d);
		}

		public virtual void TestStoreObject()
		{
			DoubleHandlerTestCase.Item storedItem = new DoubleHandlerTestCase.Item(1.023456789
				, 1.023456789);
			DoTestStoreObject(storedItem);
		}

		public class Item
		{
			public double _double;

			public double _doubleWrapper;

			public Item(double d, double wrapper)
			{
				_double = d;
				_doubleWrapper = wrapper;
			}

			public override bool Equals(object obj)
			{
				if (obj == this)
				{
					return true;
				}
				if (!(obj is DoubleHandlerTestCase.Item))
				{
					return false;
				}
				DoubleHandlerTestCase.Item other = (DoubleHandlerTestCase.Item)obj;
				return (other._double == this._double) && this._doubleWrapper.Equals(other._doubleWrapper
					);
			}

			public override string ToString()
			{
				return "[" + _double + "," + _doubleWrapper + "]";
			}
		}
	}
}

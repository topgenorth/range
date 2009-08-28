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
using Db4oUnit;
using Db4objects.Db4o.Internal.Handlers.Array;
using Db4objects.Db4o.Tests.Common.Handlers;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Tests.Common.Handlers
{
	public class MultiDimensionalArrayHandlerTestCase : TypeHandlerTestCaseBase
	{
		public static void Main(string[] args)
		{
			new MultiDimensionalArrayHandlerTestCase().RunSolo();
		}

		internal static readonly int[][] ArrayData = new int[][] { new int[] { 1, 2, 3 }, 
			new int[] { 6, 5, 4 } };

		internal static readonly int[] Data = new int[] { 1, 2, 3, 6, 5, 4 };

		public class Item
		{
			public int[][] _int;

			public Item(int[][] int_)
			{
				_int = int_;
			}

			public override bool Equals(object obj)
			{
				if (obj == this)
				{
					return true;
				}
				if (!(obj is MultiDimensionalArrayHandlerTestCase.Item))
				{
					return false;
				}
				MultiDimensionalArrayHandlerTestCase.Item other = (MultiDimensionalArrayHandlerTestCase.Item
					)obj;
				if (_int.Length != other._int.Length)
				{
					return false;
				}
				for (int i = 0; i < _int.Length; i++)
				{
					if (_int[i].Length != other._int[i].Length)
					{
						return false;
					}
					for (int j = 0; j < _int[i].Length; j++)
					{
						if (_int[i][j] != other._int[i][j])
						{
							return false;
						}
					}
				}
				return true;
			}
		}

		private ArrayHandler IntArrayHandler()
		{
			return ArrayHandler(typeof(int), true);
		}

		private ArrayHandler ArrayHandler(Type clazz, bool isPrimitive)
		{
			ITypeHandler4 typeHandler = (ITypeHandler4)Container().FieldHandlerForClass(Reflector
				().ForClass(clazz));
			return new MultidimensionalArrayHandler(typeHandler, isPrimitive);
		}

		public virtual void TestReadWrite()
		{
			MockWriteContext writeContext = new MockWriteContext(Db());
			MultiDimensionalArrayHandlerTestCase.Item expected = new MultiDimensionalArrayHandlerTestCase.Item
				(ArrayData);
			IntArrayHandler().Write(writeContext, expected._int);
			MockReadContext readContext = new MockReadContext(writeContext);
			int[][] arr = (int[][])IntArrayHandler().Read(readContext);
			MultiDimensionalArrayHandlerTestCase.Item actualValue = new MultiDimensionalArrayHandlerTestCase.Item
				(arr);
			Assert.AreEqual(expected, actualValue);
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestStoreObject()
		{
			MultiDimensionalArrayHandlerTestCase.Item storedItem = new MultiDimensionalArrayHandlerTestCase.Item
				(new int[][] { new int[] { 1, 2, 3 }, new int[] { 6, 5, 4 } });
			DoTestStoreObject(storedItem);
		}

		public virtual void TestAllElements()
		{
			int pos = 0;
			IEnumerator allElements = IntArrayHandler().AllElements(Container(), ArrayData);
			while (allElements.MoveNext())
			{
				Assert.AreEqual(Data[pos++], allElements.Current);
			}
			Assert.AreEqual(pos, Data.Length);
		}
	}
}

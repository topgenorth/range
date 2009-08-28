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
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Handlers;
using Db4objects.Db4o.Marshall;
using Db4objects.Db4o.Reflect;
using Db4objects.Db4o.Tests.Common.Internal;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Tests.Common.Internal
{
	public partial class Comparable4TestCase : AbstractDb4oTestCase, IOptOutCS
	{
		public static void Main(string[] args)
		{
			new Comparable4TestCase().RunAll();
		}

		public class Item
		{
		}

		public virtual void TestHandlers()
		{
			AssertHandlerComparison(typeof(BooleanHandler), false, true);
			AssertHandlerComparison(typeof(ByteHandler), (byte)1, (byte)2);
			AssertHandlerComparison(typeof(ByteHandler), byte.MinValue, byte.MaxValue);
			AssertHandlerComparison(typeof(CharHandler), (char)1, (char)2);
			AssertHandlerComparison(typeof(CharHandler), char.MinValue, char.MaxValue);
			AssertHandlerComparison(typeof(DoubleHandler), System.Convert.ToDouble(1), System.Convert.ToDouble
				(2));
			AssertHandlerComparison(typeof(DoubleHandler), 0.1, 0.2);
			AssertHandlerComparison(typeof(DoubleHandler), double.MinValue, double.MaxValue);
			AssertHandlerComparison(typeof(FloatHandler), System.Convert.ToSingle(1), System.Convert.ToSingle
				(2));
			AssertHandlerComparison(typeof(FloatHandler), System.Convert.ToSingle(0.1), System.Convert.ToSingle
				(0.2));
			AssertHandlerComparison(typeof(FloatHandler), float.MinValue, float.MaxValue);
			AssertHandlerComparison(typeof(IntHandler), 2, 4);
			AssertHandlerComparison(typeof(IntHandler), int.MinValue, int.MaxValue);
			AssertHandlerComparison(typeof(LongHandler), System.Convert.ToInt64(2), System.Convert.ToInt64
				(4));
			AssertHandlerComparison(typeof(LongHandler), long.MinValue, long.MaxValue);
			AssertHandlerComparison(typeof(ShortHandler), (short)2, (short)4);
			AssertHandlerComparison(typeof(ShortHandler), short.MinValue, short.MaxValue);
			AssertHandlerComparison(typeof(StringHandler), "a", "b");
			AssertHandlerComparison(typeof(StringHandler), "Hello", "Hello_");
			AssertClassHandler();
		}

		private void AssertClassHandler()
		{
			int id1 = StoreItem();
			int id2 = StoreItem();
			int smallerID = Math.Min(id1, id2);
			int biggerID = Math.Max(id1, id2);
			ClassMetadata classMetadata = new ClassMetadata(Container(), Reflector().ForClass
				(typeof(Comparable4TestCase.Item)));
			AssertHandlerComparison(classMetadata, smallerID, biggerID);
		}

		private int StoreItem()
		{
			Comparable4TestCase.Item item = new Comparable4TestCase.Item();
			Db().Store(item);
			return (int)Db().GetID(item);
		}

		private void AssertHandlerComparison(Type handlerClass, object smaller, object greater
			)
		{
			ITypeHandler4 handler = (ITypeHandler4)NewInstance(handlerClass);
			AssertHandlerComparison(handler, smaller, greater);
		}

		private void AssertHandlerComparison(ITypeHandler4 handler, object smaller, object
			 greater)
		{
			IPreparedComparison comparable = handler.PrepareComparison(Context(), smaller);
			Assert.IsNotNull(comparable);
			Assert.AreEqual(0, comparable.CompareTo(smaller));
			Assert.IsSmaller(0, comparable.CompareTo(greater));
			comparable = handler.PrepareComparison(Context(), greater);
			Assert.IsNotNull(comparable);
			Assert.AreEqual(0, comparable.CompareTo(greater));
			Assert.IsGreater(0, comparable.CompareTo(smaller));
			comparable = handler.PrepareComparison(Context(), null);
			Assert.IsNotNull(comparable);
			Assert.AreEqual(0, comparable.CompareTo(null));
			Assert.IsSmaller(0, comparable.CompareTo(smaller));
		}

		private IContext Context()
		{
			return Stream().Transaction().Context();
		}

		private object NewInstance(Type clazz)
		{
			IReflectClass classReflector = Reflector().ForClass(clazz);
			object obj = classReflector.NewInstance();
			if (obj == null)
			{
				throw new ArgumentException("No usable constructor for Class " + clazz);
			}
			return obj;
		}
	}
}

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
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Tests.Common.Foundation;

namespace Db4objects.Db4o.Tests.Common.Foundation
{
	/// <exclude></exclude>
	public class Iterable4AdaptorTestCase : ITestCase
	{
		public virtual void TestEmptyIterator()
		{
			Iterable4Adaptor adaptor = NewAdaptor(new int[] {  });
			Assert.IsFalse(adaptor.HasNext());
			Assert.IsFalse(adaptor.HasNext());
			Assert.Expect(typeof(InvalidOperationException), new _ICodeBlock_20(adaptor));
		}

		private sealed class _ICodeBlock_20 : ICodeBlock
		{
			public _ICodeBlock_20(Iterable4Adaptor adaptor)
			{
				this.adaptor = adaptor;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				adaptor.Next();
			}

			private readonly Iterable4Adaptor adaptor;
		}

		public virtual void TestHasNext()
		{
			int[] expected = new int[] { 1, 2, 3 };
			Iterable4Adaptor adaptor = NewAdaptor(expected);
			for (int i = 0; i < expected.Length; i++)
			{
				AssertHasNext(adaptor);
				Assert.AreEqual(expected[i], adaptor.Next());
			}
			Assert.IsFalse(adaptor.HasNext());
		}

		public virtual void TestNext()
		{
			int[] expected = new int[] { 1, 2, 3 };
			Iterable4Adaptor adaptor = NewAdaptor(expected);
			for (int i = 0; i < expected.Length; i++)
			{
				Assert.AreEqual(expected[i], adaptor.Next());
			}
			Assert.IsFalse(adaptor.HasNext());
		}

		private Iterable4Adaptor NewAdaptor(int[] expected)
		{
			return new Iterable4Adaptor(NewIterable(expected));
		}

		private void AssertHasNext(Iterable4Adaptor adaptor)
		{
			for (int i = 0; i < 10; ++i)
			{
				Assert.IsTrue(adaptor.HasNext());
			}
		}

		private IEnumerable NewIterable(int[] values)
		{
			Collection4 collection = new Collection4();
			collection.AddAll(IntArrays4.ToObjectArray(values));
			return collection;
		}
	}
}

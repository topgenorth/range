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
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Tests.Common.Foundation;

namespace Db4objects.Db4o.Tests.Common.Foundation
{
	public class CompositeIterator4TestCase : ITestCase
	{
		public virtual void TestWithEmptyIterators()
		{
			AssertIterator(NewIterator());
		}

		public virtual void TestReset()
		{
			CompositeIterator4 iterator = NewIterator();
			AssertIterator(iterator);
			iterator.Reset();
			AssertIterator(iterator);
		}

		private void AssertIterator(CompositeIterator4 iterator)
		{
			Iterator4Assert.AreEqual(IntArrays4.NewIterator(new int[] { 1, 2, 3, 4, 5, 6 }), 
				iterator);
		}

		private CompositeIterator4 NewIterator()
		{
			Collection4 iterators = new Collection4();
			iterators.Add(IntArrays4.NewIterator(new int[] { 1, 2, 3 }));
			iterators.Add(IntArrays4.NewIterator(new int[] {  }));
			iterators.Add(IntArrays4.NewIterator(new int[] { 4 }));
			iterators.Add(IntArrays4.NewIterator(new int[] { 5, 6 }));
			CompositeIterator4 iterator = new CompositeIterator4(iterators.GetEnumerator());
			return iterator;
		}
	}
}

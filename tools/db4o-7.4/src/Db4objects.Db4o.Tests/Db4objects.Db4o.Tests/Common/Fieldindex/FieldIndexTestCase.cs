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
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Btree;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Btree;
using Db4objects.Db4o.Tests.Common.Fieldindex;
using Db4objects.Db4o.Tests.Common.Foundation;

namespace Db4objects.Db4o.Tests.Common.Fieldindex
{
	public class FieldIndexTestCase : FieldIndexTestCaseBase
	{
		private static readonly int[] Foos = new int[] { 3, 7, 9, 4 };

		public static void Main(string[] arguments)
		{
			new FieldIndexTestCase().RunSolo();
		}

		protected override void Configure(IConfiguration config)
		{
			base.Configure(config);
		}

		protected override void Store()
		{
			StoreItems(Foos);
		}

		public virtual void TestTraverseValues()
		{
			IStoredField field = YapField();
			ExpectingVisitor expectingVisitor = new ExpectingVisitor(IntArrays4.ToObjectArray
				(Foos));
			field.TraverseValues(expectingVisitor);
			expectingVisitor.AssertExpectations();
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestAllThere()
		{
			for (int i = 0; i < Foos.Length; i++)
			{
				IQuery q = CreateQuery(Foos[i]);
				IObjectSet objectSet = q.Execute();
				Assert.AreEqual(1, objectSet.Size());
				FieldIndexItem fii = (FieldIndexItem)objectSet.Next();
				Assert.AreEqual(Foos[i], fii.foo);
			}
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestAccessingBTree()
		{
			BTree bTree = YapField().GetIndex(Trans());
			Assert.IsNotNull(bTree);
			ExpectKeysSearch(bTree, Foos);
		}

		private void ExpectKeysSearch(BTree btree, int[] values)
		{
			int lastValue = int.MinValue;
			for (int i = 0; i < values.Length; i++)
			{
				if (values[i] != lastValue)
				{
					ExpectingVisitor expectingVisitor = BTreeAssert.CreateExpectingVisitor(values[i], 
						IntArrays4.Occurences(values, values[i]));
					IBTreeRange range = FieldIndexKeySearch(Trans(), btree, values[i]);
					BTreeAssert.TraverseKeys(range, new _IVisitor4_63(expectingVisitor));
					expectingVisitor.AssertExpectations();
					lastValue = values[i];
				}
			}
		}

		private sealed class _IVisitor4_63 : IVisitor4
		{
			public _IVisitor4_63(ExpectingVisitor expectingVisitor)
			{
				this.expectingVisitor = expectingVisitor;
			}

			public void Visit(object obj)
			{
				Db4objects.Db4o.Internal.Btree.FieldIndexKey fik = (Db4objects.Db4o.Internal.Btree.FieldIndexKey
					)obj;
				expectingVisitor.Visit(fik.Value());
			}

			private readonly ExpectingVisitor expectingVisitor;
		}

		private Db4objects.Db4o.Internal.Btree.FieldIndexKey FieldIndexKey(int integerPart
			, object composite)
		{
			return new Db4objects.Db4o.Internal.Btree.FieldIndexKey(integerPart, composite);
		}

		private IBTreeRange FieldIndexKeySearch(Transaction trans, BTree btree, object key
			)
		{
			// SearchTarget should not make a difference, HIGHEST is faster
			BTreeNodeSearchResult start = btree.SearchLeaf(trans, FieldIndexKey(0, key), SearchTarget
				.Lowest);
			BTreeNodeSearchResult end = btree.SearchLeaf(trans, FieldIndexKey(int.MaxValue, key
				), SearchTarget.Lowest);
			return start.CreateIncludingRange(end);
		}

		private FieldMetadata YapField()
		{
			return ClassMetadataFor(typeof(FieldIndexItem)).FieldMetadataForName("foo");
		}
	}
}

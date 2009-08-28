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
using Db4objects.Db4o.Config;
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o.TA;

namespace Db4objects.Db4o.Tests.CLI2.TA
{
	using StringIntP = Pair<string, int>;
	using IntC = NullableContainer<int>;
	using IntCStringIntP = Pair<NullableContainer<int>, Pair<string, int>>;
	using IntCStringIntC = NullableContainer<Pair<NullableContainer<int>, Pair<string, int>>>;
	using IntCStringIntIntP = Pair<NullableContainer<Pair<NullableContainer<int>, Pair<string, int>>>, int>;
	using IntCStringIntIntPC = NullableContainer<Pair<NullableContainer<Pair<NullableContainer<int>, Pair<string, int>>>, int>>;

    class NullableTypeActivationTestCase : AbstractDb4oTestCase, IOptOutCS
	{
		protected override void Configure(IConfiguration config)
		{
			config.Add(new TransparentActivationSupport());
		}

		// the object graph goes like this:
		// container
		//	pair
		//		first: container
		//			pair:
		//				first: container(null)
		//				second: pair
		//					first: "foo"
		//					second: 11
		//		second: 42

		protected override void Store()
		{
			Store(
				new IntCStringIntIntPC(
					"root",
					new IntCStringIntIntP(
						new IntCStringIntC(
							"child",
							new IntCStringIntP(
								new IntC("grandchild", null),
								new StringIntP("foo", 11)
							)
						),
						42
					)
				)
			);
		}

		public void TestGetunderlyingType()
		{
			Assert.AreSame(typeof(int), Nullable.GetUnderlyingType(typeof(int?)));
			Assert.AreSame(typeof(StringIntP), Nullable.GetUnderlyingType(typeof(StringIntP?)));
		}

		public void TestDepth0()
		{
			IntCStringIntIntPC root = GetRoot();
			NotActivated(root);
		}

		public void TestDepth1()
		{
			IntCStringIntIntPC root = GetRoot();
			Assert.AreEqual("root", root.Name, "ta");
			Assert.IsNotNull(root.Value.Value.First, "ta");
			Assert.AreEqual(42, root.Value.Value.Second, "ta");

			NotActivated(root.Value.Value.First);
		}

		public void TestDepthN()
		{
			IntCStringIntIntPC root = GetRoot();
			NotActivated(root.Value.Value.First.Value.Value.First);
			Assert.IsFalse(root.Value.Value.First.Value.Value.First.Value.HasValue);
			Assert.AreEqual(new StringIntP("foo", 11), root.Value.Value.First.Value.Value.Second);
		}

		private IntCStringIntIntPC GetRoot()
		{
			return (IntCStringIntIntPC)NewQuery(typeof(IntCStringIntIntPC)).Execute().Next();
		}

		private static void NotActivated<T>(NullableContainer<T> container) where T : struct
		{
			Assert.IsNull(container.PassThroughName, "depth(0) shouldn't activate ref member");
			Assert.IsFalse(container.PassThroughValue.HasValue, "depth(0) shouldn't activate nested value types");
		}
	}
}

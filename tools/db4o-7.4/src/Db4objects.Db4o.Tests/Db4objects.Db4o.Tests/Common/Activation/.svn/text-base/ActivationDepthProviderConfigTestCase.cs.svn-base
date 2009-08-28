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
using Db4oUnit.Extensions;
using Db4oUnit.Mocking;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Activation;
using Db4objects.Db4o.Tests.Common.Activation;

namespace Db4objects.Db4o.Tests.Common.Activation
{
	/// <summary>
	/// Ensures the container uses the provided ActivationDepthProvider instance
	/// whenever necessary.
	/// </summary>
	/// <remarks>
	/// Ensures the container uses the provided ActivationDepthProvider instance
	/// whenever necessary.
	/// </remarks>
	public class ActivationDepthProviderConfigTestCase : AbstractDb4oTestCase, IOptOutTA
	{
		public sealed class ItemRoot
		{
			public ActivationDepthProviderConfigTestCase.Item root;

			public ItemRoot(ActivationDepthProviderConfigTestCase.Item root_)
			{
				this.root = root_;
			}

			public ItemRoot()
			{
			}
		}

		public sealed class Item
		{
			public ActivationDepthProviderConfigTestCase.Item child;

			public Item()
			{
			}

			public Item(ActivationDepthProviderConfigTestCase.Item child_)
			{
				this.child = child_;
			}
		}

		private readonly MockActivationDepthProvider _dummyProvider = new MockActivationDepthProvider
			();

		/// <exception cref="Exception"></exception>
		protected override void Configure(IConfiguration config)
		{
			((Config4Impl)config).ActivationDepthProvider(_dummyProvider);
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			Store(new ActivationDepthProviderConfigTestCase.ItemRoot(new ActivationDepthProviderConfigTestCase.Item
				(new ActivationDepthProviderConfigTestCase.Item(new ActivationDepthProviderConfigTestCase.Item
				()))));
		}

		public virtual void TestCSActivationDepthFor()
		{
			if (!IsNetworkCS())
			{
				return;
			}
			ResetProvider();
			QueryItem();
			AssertProviderCalled(new MethodCall[] { new MethodCall("activationDepthFor", ItemRootMetadata
				(), ActivationMode.Prefetch), new MethodCall("activationDepthFor", ItemRootMetadata
				(), ActivationMode.Activate) });
		}

		public virtual void TestSoloActivationDepthFor()
		{
			if (IsNetworkCS())
			{
				return;
			}
			ResetProvider();
			QueryItem();
			AssertProviderCalled("activationDepthFor", ItemRootMetadata(), ActivationMode.Activate
				);
		}

		public virtual void TestSpecificActivationDepth()
		{
			ActivationDepthProviderConfigTestCase.Item item = QueryItem();
			ResetProvider();
			Db().Activate(item, 3);
			AssertProviderCalled("activationDepth", 3, ActivationMode.Activate);
		}

		private bool IsNetworkCS()
		{
			return IsClientServer() && !IsMTOC();
		}

		private ClassMetadata ItemRootMetadata()
		{
			return ClassMetadataFor(typeof(ActivationDepthProviderConfigTestCase.ItemRoot));
		}

		private void AssertProviderCalled(MethodCall[] expectedCalls)
		{
			_dummyProvider.Verify(expectedCalls);
		}

		private void AssertProviderCalled(string methodName, object arg1, object arg2)
		{
			_dummyProvider.Verify(new MethodCall[] { new MethodCall(methodName, arg1, arg2) }
				);
		}

		private void ResetProvider()
		{
			_dummyProvider.Reset();
		}

		private ActivationDepthProviderConfigTestCase.Item QueryItem()
		{
			return ((ActivationDepthProviderConfigTestCase.ItemRoot)RetrieveOnlyInstance(typeof(
				ActivationDepthProviderConfigTestCase.ItemRoot))).root;
		}
	}
}

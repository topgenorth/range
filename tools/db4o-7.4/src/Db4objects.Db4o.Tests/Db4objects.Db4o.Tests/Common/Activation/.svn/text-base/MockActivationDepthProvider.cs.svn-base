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
using Db4oUnit.Mocking;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Activation;

namespace Db4objects.Db4o.Tests.Common.Activation
{
	/// <summary>
	/// An ActivationDepthProvider that records ActivationDepthProvider calls and
	/// delegates to another provider.
	/// </summary>
	/// <remarks>
	/// An ActivationDepthProvider that records ActivationDepthProvider calls and
	/// delegates to another provider.
	/// </remarks>
	public class MockActivationDepthProvider : MethodCallRecorder, IActivationDepthProvider
	{
		private readonly IActivationDepthProvider _delegate;

		public MockActivationDepthProvider()
		{
			_delegate = LegacyActivationDepthProvider.Instance;
		}

		public virtual IActivationDepth ActivationDepthFor(ClassMetadata classMetadata, ActivationMode
			 mode)
		{
			Record(new MethodCall("activationDepthFor", classMetadata, mode));
			return _delegate.ActivationDepthFor(classMetadata, mode);
		}

		public virtual IActivationDepth ActivationDepth(int depth, ActivationMode mode)
		{
			Record(new MethodCall("activationDepth", depth, mode));
			return _delegate.ActivationDepth(depth, mode);
		}
	}
}

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
using System.Reflection;
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4objects.Db4o.Foundation;

namespace Db4oUnit.Extensions
{
	public class Db4oTestSuiteBuilder : ReflectionTestSuiteBuilder
	{
		private IDb4oFixture _fixture;

		public Db4oTestSuiteBuilder(IDb4oFixture fixture, Type clazz) : this(fixture, new 
			Type[] { clazz })
		{
		}

		public Db4oTestSuiteBuilder(IDb4oFixture fixture, Type[] classes) : base(classes)
		{
			Fixture(fixture);
		}

		private void Fixture(IDb4oFixture fixture)
		{
			if (null == fixture)
			{
				throw new ArgumentNullException("fixture");
			}
			_fixture = fixture;
		}

		protected override bool IsApplicable(Type clazz)
		{
			return _fixture.Accept(clazz);
		}

		protected override ITest CreateTest(object instance, MethodInfo method)
		{
			ITest test = base.CreateTest(instance, method);
			return new _TestDecorationAdapter_39(test, test);
		}

		private sealed class _TestDecorationAdapter_39 : TestDecorationAdapter
		{
			public _TestDecorationAdapter_39(ITest test, ITest baseArg1) : base(baseArg1)
			{
				this.test = test;
			}

			public override string Label()
			{
				return "(" + Db4oFixtureVariable.Fixture().Label() + ") " + test.Label();
			}

			private readonly ITest test;
		}

		protected override object WithContext(IClosure4 closure)
		{
			return Db4oFixtureVariable.FixtureVariable.With(_fixture, closure);
		}
	}
}

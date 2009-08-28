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
using Db4oUnit.Extensions.Concurrency;
using Db4objects.Db4o.Ext;

namespace Db4oUnit.Extensions.Concurrency
{
	public class Db4oConcurrencyTestSuiteBuilder : Db4oUnit.Extensions.Db4oTestSuiteBuilder
	{
		public Db4oConcurrencyTestSuiteBuilder(IDb4oFixture fixture, Type clazz) : base(fixture
			, clazz)
		{
		}

		public Db4oConcurrencyTestSuiteBuilder(IDb4oFixture fixture, Type[] classes) : base
			(fixture, classes)
		{
		}

		protected override ITest CreateTest(object instance, MethodInfo method)
		{
			return new ConcurrencyTestMethod(instance, method);
		}

		protected override bool IsTestMethod(MethodInfo method)
		{
			string name = method.Name;
			return StartsWithIgnoreCase(name, ConcurrencyConventions.TestPrefix()) && TestPlatform
				.IsPublic(method) && !TestPlatform.IsStatic(method) && HasValidParameter(method);
		}

		internal static bool HasValidParameter(MethodInfo method)
		{
			Type[] parameters = Sharpen.Runtime.GetParameterTypes(method);
			if (parameters.Length == 1 && parameters[0] == typeof(IExtObjectContainer))
			{
				return true;
			}
			if (parameters.Length == 2 && parameters[0] == typeof(IExtObjectContainer) && parameters
				[1] == typeof(int))
			{
				return true;
			}
			return false;
		}
	}
}

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

namespace Db4objects.Db4o.Tests.CLI1
{
	public class AllTests : Db4oUnit.Extensions.Db4oTestSuite
	{
		protected override Type[] TestCases()
		{
			return new System.Type[]
				{
                    typeof(Aliases.AllTests),
					typeof(CrossPlatform.AllTests),
#if !CF
					typeof(CsAppDomains),
					typeof(CsAssemblyVersionChange),
					typeof(CsImage),
					typeof(ShutdownMultipleContainer),
#endif
					typeof(Events.EventRegistryTestCase),
                    typeof(Handlers.AllTests),
					typeof(Inside.AllTests),
					typeof(NativeQueries.AllTests),
					typeof(Reflect.Net.AllTests),
					typeof(CsCascadeDeleteToStructs),
					typeof(CsCollections),
					typeof(CsCustomTransientAttribute),
					typeof(CsDate),
					typeof(CsDelegate),
					typeof(CsDisposableTestCase),
					typeof(CsEnum),
					typeof(CsEvaluationDelegate),
					typeof(CsMarshalByRef),
					typeof(CsType),
					typeof(CsStructs),
					typeof(CsStructsRegression),
					typeof(CsValueTypesTestCase),
					typeof(CultureInfoTestCase),
					typeof(ImageTestCase),
					typeof(JavaDateCompatibilityTestCase),
					typeof(JavaUUIDCompatibilityTestCase),
					typeof(MDArrayTestCase),
					typeof(NonSerializedAttributeTestCase),
					typeof(ObjectInfoMigration52TestCase),
                    typeof(ObjectInfoMigration57TestCase),
					typeof(ObjectSetAsListTestCase),
				};
		}
	}
}

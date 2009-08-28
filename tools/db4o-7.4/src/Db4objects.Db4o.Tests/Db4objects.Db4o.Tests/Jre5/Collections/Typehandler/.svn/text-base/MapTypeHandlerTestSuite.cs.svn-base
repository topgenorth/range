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
using Db4oUnit.Extensions;
using Db4oUnit.Extensions.Fixtures;
using Db4oUnit.Fixtures;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Jre5.Collections.Typehandler;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Tests.Jre5.Collections.Typehandler
{
	public class MapTypeHandlerTestSuite : FixtureBasedTestSuite, IDb4oTestCase
	{
		public override IFixtureProvider[] FixtureProviders()
		{
			return new IFixtureProvider[] { new Db4oFixtureProvider(), MapTypeHandlerTestVariables
				.MapFixtureProvider, MapTypeHandlerTestVariables.MapKeysProvider, MapTypeHandlerTestVariables
				.MapValuesProvider, MapTypeHandlerTestVariables.TypehandlerFixtureProvider };
		}

		public override Type[] TestUnits()
		{
			return new Type[] { typeof(MapTypeHandlerTestSuite.MapTypeHandlerUnitTestCase) };
		}

		public class MapTypeHandlerUnitTestCase : TypeHandlerUnitTest
		{
			protected override void FillItem(object item)
			{
				FillMapItem(item);
			}

			protected override void AssertContent(object item)
			{
				AssertMapContent(item);
			}

			protected override AbstractItemFactory ItemFactory()
			{
				return (AbstractItemFactory)MapTypeHandlerTestVariables.MapImplementation.Value;
			}

			protected override ITypeHandler4 TypeHandler()
			{
				return (ITypeHandler4)MapTypeHandlerTestVariables.MapTypehander.Value;
			}

			protected override ListTypeHandlerTestElementsSpec ElementsSpec()
			{
				return (ListTypeHandlerTestElementsSpec)MapTypeHandlerTestVariables.MapKeysSpec.Value;
			}

			protected override void AssertCompareItems(object element, bool successful)
			{
				IQuery q = NewQuery();
				object item = ItemFactory().NewItem();
				IDictionary map = MapFromItem(item);
				map.Add(element, Values()[0]);
				q.Constrain(item);
				AssertQueryResult(q, successful);
			}

			//TODO: remove when COR-1311 solved 
			/// <exception cref="Exception"></exception>
			public override void TestSuccessfulQuery()
			{
				if (Elements()[0] is ListTypeHandlerTestVariables.FirstClassElement)
				{
					return;
				}
				base.TestSuccessfulQuery();
			}

			//TODO: remove when COR-1311 solved 
			/// <exception cref="Exception"></exception>
			public override void TestFailingQuery()
			{
				if (Elements()[0] is ListTypeHandlerTestVariables.FirstClassElement)
				{
					return;
				}
				base.TestFailingQuery();
			}

			//TODO: remove when COR-1311 solved 
			/// <exception cref="Exception"></exception>
			public override void TestFailingContainsQuery()
			{
				if (Elements()[0] is ListTypeHandlerTestVariables.FirstClassElement)
				{
					return;
				}
				base.TestFailingContainsQuery();
			}

			//TODO: remove when COR-1311 solved 
			/// <exception cref="Exception"></exception>
			public override void TestFailingCompareItems()
			{
				if (Elements()[0] is ListTypeHandlerTestVariables.FirstClassElement)
				{
					return;
				}
				base.TestFailingCompareItems();
			}

			//TODO: remove when COR-1311 solved 
			/// <exception cref="Exception"></exception>
			public override void TestCompareItems()
			{
				if (Elements()[0] is ListTypeHandlerTestVariables.FirstClassElement)
				{
					return;
				}
				base.TestCompareItems();
			}

			//TODO: remove when COR-1311 solved 
			/// <exception cref="Exception"></exception>
			public override void TestSuccessfulContainsQuery()
			{
				if (Elements()[0] is ListTypeHandlerTestVariables.FirstClassElement)
				{
					return;
				}
				base.TestSuccessfulContainsQuery();
			}
		}
	}
}

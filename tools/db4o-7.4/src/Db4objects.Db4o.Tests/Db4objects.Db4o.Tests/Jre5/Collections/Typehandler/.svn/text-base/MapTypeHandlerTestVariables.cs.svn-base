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
using Db4oUnit.Fixtures;
using Db4objects.Db4o.Tests.Jre5.Collections.Typehandler;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Tests.Jre5.Collections.Typehandler
{
	public class MapTypeHandlerTestVariables
	{
		public static readonly FixtureVariable MapImplementation = new FixtureVariable("map"
			);

		public static readonly FixtureVariable MapTypehander = new FixtureVariable("typehandler"
			);

		public static readonly IFixtureProvider MapFixtureProvider = new SimpleFixtureProvider
			(MapImplementation, new object[] { new MapTypeHandlerTestVariables.UntypedHashMapItemFactory
			(), new MapTypeHandlerTestVariables.TypedHashMapItemFactory(), new MapTypeHandlerTestVariables.HashtableItemFactory
			(), new MapTypeHandlerTestVariables.NamedHashMapItemFactory() });

		public static readonly IFixtureProvider TypehandlerFixtureProvider = new SimpleFixtureProvider
			(MapTypehander, new object[] { null, new MapTypeHandler() });

		public static readonly FixtureVariable MapKeysSpec = new FixtureVariable("keys");

		public static readonly IFixtureProvider MapKeysProvider = new SimpleFixtureProvider
			(MapKeysSpec, new object[] { ListTypeHandlerTestVariables.StringElementsSpec, ListTypeHandlerTestVariables
			.IntElementsSpec, ListTypeHandlerTestVariables.ObjectElementsSpec });

		public static readonly FixtureVariable MapValuesSpec = new FixtureVariable("values"
			);

		public static readonly IFixtureProvider MapValuesProvider = new SimpleFixtureProvider
			(MapValuesSpec, new object[] { ListTypeHandlerTestVariables.StringElementsSpec, 
			ListTypeHandlerTestVariables.IntElementsSpec, ListTypeHandlerTestVariables.ObjectElementsSpec
			 });

		private class HashtableItemFactory : AbstractMapItemFactory, ILabeled
		{
			private class Item
			{
				public IDictionary _map = new Hashtable();
			}

			public override Type ContainerClass()
			{
				return typeof(Hashtable);
			}

			public override Type ItemClass()
			{
				return typeof(MapTypeHandlerTestVariables.HashtableItemFactory.Item);
			}

			public override object NewItem()
			{
				return new MapTypeHandlerTestVariables.HashtableItemFactory.Item();
			}

			public virtual string Label()
			{
				return "Hashtable";
			}
		}

		private class TypedHashMapItemFactory : AbstractMapItemFactory, ILabeled
		{
			private class Item
			{
				public Hashtable _map = new Hashtable();
			}

			public override Type ContainerClass()
			{
				return typeof(Hashtable);
			}

			public override Type ItemClass()
			{
				return typeof(MapTypeHandlerTestVariables.TypedHashMapItemFactory.Item);
			}

			public override object NewItem()
			{
				return new MapTypeHandlerTestVariables.TypedHashMapItemFactory.Item();
			}

			public virtual string Label()
			{
				return "HashMap Typed";
			}
		}

		private class NamedHashMapItemFactory : AbstractMapItemFactory, ILabeled
		{
			private class Item
			{
				public IDictionary _map = new NamedHashMap();
			}

			public override Type ContainerClass()
			{
				return typeof(NamedHashMap);
			}

			public override Type ItemClass()
			{
				return typeof(MapTypeHandlerTestVariables.NamedHashMapItemFactory.Item);
			}

			public override object NewItem()
			{
				return new MapTypeHandlerTestVariables.NamedHashMapItemFactory.Item();
			}

			public virtual string Label()
			{
				return "NamedHashMap";
			}
		}

		private class UntypedHashMapItemFactory : AbstractMapItemFactory, ILabeled
		{
			private class Item
			{
				public IDictionary _map = new Hashtable();
			}

			public override Type ContainerClass()
			{
				return typeof(Hashtable);
			}

			public override Type ItemClass()
			{
				return typeof(MapTypeHandlerTestVariables.UntypedHashMapItemFactory.Item);
			}

			public override object NewItem()
			{
				return new MapTypeHandlerTestVariables.UntypedHashMapItemFactory.Item();
			}

			public virtual string Label()
			{
				return "HashMap Untyped";
			}
		}
	}
}

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
	public sealed class ListTypeHandlerTestVariables
	{
		public static readonly FixtureVariable ListImplementation = new FixtureVariable("list"
			);

		public static readonly FixtureVariable ElementsSpec = new FixtureVariable("elements"
			);

		public static readonly FixtureVariable ListTypehander = new FixtureVariable("typehandler"
			);

		public static readonly IFixtureProvider ListFixtureProvider = new SimpleFixtureProvider
			(ListImplementation, new object[] { new ListTypeHandlerTestVariables.ArrayListItemFactory
			(), new ListTypeHandlerTestVariables.LinkedListItemFactory(), new ListTypeHandlerTestVariables.ListItemFactory
			(), new ListTypeHandlerTestVariables.NamedArrayListItemFactory() });

		public static readonly IFixtureProvider TypehandlerFixtureProvider = new SimpleFixtureProvider
			(ListTypehander, new object[] { new ListTypeHandler(), new EmbeddedListTypeHandler
			() });

		public static readonly ListTypeHandlerTestElementsSpec StringElementsSpec = new ListTypeHandlerTestElementsSpec
			(new object[] { "zero", "one" }, "two", "zzz");

		public static readonly ListTypeHandlerTestElementsSpec IntElementsSpec = new ListTypeHandlerTestElementsSpec
			(new object[] { 0, 1 }, 2, int.MaxValue);

		public static readonly ListTypeHandlerTestElementsSpec ObjectElementsSpec = new ListTypeHandlerTestElementsSpec
			(new object[] { new ListTypeHandlerTestVariables.FirstClassElement(0), new ListTypeHandlerTestVariables.FirstClassElement
			(1) }, new ListTypeHandlerTestVariables.FirstClassElement(2), null);

		private ListTypeHandlerTestVariables()
		{
		}

		public class FirstClassElement
		{
			public int _id;

			public FirstClassElement(int id)
			{
				_id = id;
			}

			public override bool Equals(object obj)
			{
				if (this == obj)
				{
					return true;
				}
				if (obj == null || GetType() != obj.GetType())
				{
					return false;
				}
				ListTypeHandlerTestVariables.FirstClassElement other = (ListTypeHandlerTestVariables.FirstClassElement
					)obj;
				return _id == other._id;
			}

			public override int GetHashCode()
			{
				return _id;
			}

			public override string ToString()
			{
				return "FCE#" + _id;
			}
		}

		private class ArrayListItemFactory : AbstractListItemFactory, ILabeled
		{
			private class Item
			{
				public ArrayList _list = new ArrayList();
			}

			public override object NewItem()
			{
				return new ListTypeHandlerTestVariables.ArrayListItemFactory.Item();
			}

			public override Type ItemClass()
			{
				return typeof(ListTypeHandlerTestVariables.ArrayListItemFactory.Item);
			}

			public override Type ContainerClass()
			{
				return typeof(ArrayList);
			}

			public virtual string Label()
			{
				return "ArrayList";
			}
		}

		private class LinkedListItemFactory : AbstractListItemFactory, ILabeled
		{
			private class Item
			{
				public ArrayList _list = new ArrayList();
			}

			public override object NewItem()
			{
				return new ListTypeHandlerTestVariables.LinkedListItemFactory.Item();
			}

			public override Type ItemClass()
			{
				return typeof(ListTypeHandlerTestVariables.LinkedListItemFactory.Item);
			}

			public override Type ContainerClass()
			{
				return typeof(ArrayList);
			}

			public virtual string Label()
			{
				return "LinkedList";
			}
		}

		private class ListItemFactory : AbstractListItemFactory, ILabeled
		{
			private class Item
			{
				public IList _list = new ArrayList();
			}

			public override object NewItem()
			{
				return new ListTypeHandlerTestVariables.ListItemFactory.Item();
			}

			public override Type ItemClass()
			{
				return typeof(ListTypeHandlerTestVariables.ListItemFactory.Item);
			}

			public override Type ContainerClass()
			{
				return typeof(ArrayList);
			}

			public virtual string Label()
			{
				return "[Linked]List";
			}
		}

		private class NamedArrayListItemFactory : AbstractListItemFactory, ILabeled
		{
			private class Item
			{
				public IList _list = new NamedArrayList();
			}

			public override object NewItem()
			{
				return new ListTypeHandlerTestVariables.NamedArrayListItemFactory.Item();
			}

			public override Type ItemClass()
			{
				return typeof(ListTypeHandlerTestVariables.NamedArrayListItemFactory.Item);
			}

			public override Type ContainerClass()
			{
				return typeof(NamedArrayList);
			}

			public virtual string Label()
			{
				return "NamedArrayList";
			}
		}
	}
}

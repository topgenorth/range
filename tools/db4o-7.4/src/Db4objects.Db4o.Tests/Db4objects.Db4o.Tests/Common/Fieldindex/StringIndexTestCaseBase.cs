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
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Freespace;
using Db4objects.Db4o.Internal.Slots;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Btree;
using Db4objects.Db4o.Tests.Common.Fieldindex;
using Sharpen;

namespace Db4objects.Db4o.Tests.Common.Fieldindex
{
	/// <exclude></exclude>
	public abstract class StringIndexTestCaseBase : AbstractDb4oTestCase
	{
		public class Item
		{
			public string name;

			public Item()
			{
			}

			public Item(string name_)
			{
				name = name_;
			}
		}

		public StringIndexTestCaseBase() : base()
		{
		}

		protected override void Configure(IConfiguration config)
		{
			IndexField(config, typeof(StringIndexTestCaseBase.Item), "name");
		}

		protected virtual void AssertItems(string[] expected, IObjectSet result)
		{
			ExpectingVisitor expectingVisitor = new ExpectingVisitor(ToObjectArray(expected));
			while (result.HasNext())
			{
				expectingVisitor.Visit(((StringIndexTestCaseBase.Item)result.Next()).name);
			}
			expectingVisitor.AssertExpectations();
		}

		protected virtual object[] ToObjectArray(string[] source)
		{
			object[] array = new object[source.Length];
			System.Array.Copy(source, 0, array, 0, source.Length);
			return array;
		}

		protected virtual void GrafittiFreeSpace()
		{
			if (!(Db() is IoAdaptedObjectContainer))
			{
				return;
			}
			IoAdaptedObjectContainer file = ((IoAdaptedObjectContainer)Db());
			IFreespaceManager fm = file.FreespaceManager();
			fm.Traverse(new _IVisitor4_61(file));
		}

		private sealed class _IVisitor4_61 : IVisitor4
		{
			public _IVisitor4_61(IoAdaptedObjectContainer file)
			{
				this.file = file;
			}

			public void Visit(object obj)
			{
				Slot slot = (Slot)obj;
				file.OverwriteDeletedBlockedSlot(slot);
			}

			private readonly IoAdaptedObjectContainer file;
		}

		protected virtual void AssertExists(string itemName)
		{
			AssertExists(Trans(), itemName);
		}

		protected virtual void Add(string itemName)
		{
			Add(Trans(), itemName);
		}

		protected virtual void Add(Transaction transaction, string itemName)
		{
			Container().Store(transaction, new StringIndexTestCaseBase.Item(itemName));
		}

		protected virtual void AssertExists(Transaction transaction, string itemName)
		{
			Assert.IsNotNull(Query(transaction, itemName));
		}

		protected virtual void Rename(Transaction transaction, string from, string to)
		{
			StringIndexTestCaseBase.Item item = Query(transaction, from);
			Assert.IsNotNull(item);
			item.name = to;
			Container().Store(transaction, item);
		}

		protected virtual void Rename(string from, string to)
		{
			Rename(Trans(), from, to);
		}

		protected virtual StringIndexTestCaseBase.Item Query(string name)
		{
			return Query(Trans(), name);
		}

		protected virtual StringIndexTestCaseBase.Item Query(Transaction transaction, string
			 name)
		{
			IObjectSet objectSet = NewQuery(transaction, name).Execute();
			if (!objectSet.HasNext())
			{
				return null;
			}
			return (StringIndexTestCaseBase.Item)objectSet.Next();
		}

		protected virtual IQuery NewQuery(Transaction transaction, string itemName)
		{
			IQuery query = Container().Query(transaction);
			query.Constrain(typeof(StringIndexTestCaseBase.Item));
			query.Descend("name").Constrain(itemName);
			return query;
		}
	}
}

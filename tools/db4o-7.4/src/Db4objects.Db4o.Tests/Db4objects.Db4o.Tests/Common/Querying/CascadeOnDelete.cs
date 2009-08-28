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
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Tests.Common.Querying;

namespace Db4objects.Db4o.Tests.Common.Querying
{
	public class CascadeOnDelete : AbstractDb4oTestCase
	{
		public class Item
		{
			public string item;
		}

		public CascadeOnDelete.Item[] items;

		/// <exception cref="Exception"></exception>
		public virtual void Test()
		{
			NoAccidentalDeletes();
		}

		/// <exception cref="Exception"></exception>
		private void NoAccidentalDeletes()
		{
			NoAccidentalDeletes1(true, true);
			NoAccidentalDeletes1(true, false);
			NoAccidentalDeletes1(false, true);
			NoAccidentalDeletes1(false, false);
		}

		/// <exception cref="Exception"></exception>
		private void NoAccidentalDeletes1(bool cascadeOnUpdate, bool cascadeOnDelete)
		{
			DeleteAll(GetType());
			DeleteAll(typeof(CascadeOnDelete.Item));
			IObjectClass oc = Db4oFactory.Configure().ObjectClass(typeof(CascadeOnDelete));
			oc.CascadeOnDelete(cascadeOnDelete);
			oc.CascadeOnUpdate(cascadeOnUpdate);
			Reopen();
			CascadeOnDelete.Item i = new CascadeOnDelete.Item();
			CascadeOnDelete cod = new CascadeOnDelete();
			cod.items = new CascadeOnDelete.Item[] { i };
			Db().Store(cod);
			Db().Commit();
			cod.items[0].item = "abrakadabra";
			Db().Store(cod);
			if (!cascadeOnDelete && !cascadeOnUpdate)
			{
				// the only case, where we don't cascade
				Db().Store(cod.items[0]);
			}
			Assert.AreEqual(1, CountOccurences(typeof(CascadeOnDelete.Item)));
			Db().Commit();
			Assert.AreEqual(1, CountOccurences(typeof(CascadeOnDelete.Item)));
		}
	}
}

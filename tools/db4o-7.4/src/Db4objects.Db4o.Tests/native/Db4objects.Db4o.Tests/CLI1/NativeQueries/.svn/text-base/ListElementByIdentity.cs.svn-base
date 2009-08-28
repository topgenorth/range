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
using Db4objects.Db4o.Ext;
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4oUnit.Extensions.Fixtures;
using System.Collections.Generic;

namespace Db4objects.Db4o.Tests.CLI1.NativeQueries
{
	public class ListElementByIdentity : AbstractDb4oTestCase, IOptOutCS
	{
		IList<LebiElement> _list;

		override protected void Store()
		{
			StoreElement("1");
			StoreElement("2");
			StoreElement("3");
			StoreElement("4");
		}

		public void Test()
		{
			LebiElement elem = (LebiElement)Db().Get(new LebiElement("23"))[0];

			IList<ListElementByIdentity> res = Db().Query<ListElementByIdentity>(delegate(ListElementByIdentity lebi)
			{
				return lebi._list.Contains(elem);
			});

			Assert.AreEqual(1, res.Count);
			Assert.AreEqual("23", res[0]._list[3]._name);

		}

		private void StoreElement(string prefix)
		{
			ListElementByIdentity lebi = new ListElementByIdentity();
			lebi.CreateListElements(prefix);
			Store(lebi);
		}

		private void CreateListElements(string prefix)
		{
			_list = new List<LebiElement>();
			_list.Add(new LebiElement(prefix + "0"));
			_list.Add(new LebiElement(prefix + "1"));
			_list.Add(new LebiElement(prefix + "2"));
			_list.Add(new LebiElement(prefix + "3"));
		}
	}

	public class LebiElement
	{
		public string _name;

		public LebiElement(string name)
		{
			_name = name;
		}
	}
}

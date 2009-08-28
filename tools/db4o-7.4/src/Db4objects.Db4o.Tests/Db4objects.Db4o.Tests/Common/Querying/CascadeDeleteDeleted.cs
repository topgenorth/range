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
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Querying;

namespace Db4objects.Db4o.Tests.Common.Querying
{
	public class CascadeDeleteDeleted : AbstractDb4oTestCase
	{
		public class CddMember
		{
			public string name;
		}

		public string name;

		public object untypedMember;

		public CascadeDeleteDeleted.CddMember typedMember;

		public CascadeDeleteDeleted()
		{
		}

		public CascadeDeleteDeleted(string name)
		{
			this.name = name;
		}

		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(this).CascadeOnDelete(true);
		}

		protected override void Store()
		{
			MembersFirst("membersFirst commit");
			MembersFirst("membersFirst");
			TwoRef("twoRef");
			TwoRef("twoRef commit");
			TwoRef("twoRef delete");
			TwoRef("twoRef delete commit");
		}

		private void MembersFirst(string name)
		{
			CascadeDeleteDeleted cdd = new CascadeDeleteDeleted(name);
			cdd.untypedMember = new CascadeDeleteDeleted.CddMember();
			cdd.typedMember = new CascadeDeleteDeleted.CddMember();
			Db().Store(cdd);
		}

		private void TwoRef(string name)
		{
			CascadeDeleteDeleted cdd = new CascadeDeleteDeleted(name);
			cdd.untypedMember = new CascadeDeleteDeleted.CddMember();
			cdd.typedMember = new CascadeDeleteDeleted.CddMember();
			CascadeDeleteDeleted cdd2 = new CascadeDeleteDeleted(name);
			cdd2.untypedMember = cdd.untypedMember;
			cdd2.typedMember = cdd.typedMember;
			Db().Store(cdd);
			Db().Store(cdd2);
		}

		public virtual void Test()
		{
			TMembersFirst("membersFirst commit");
			TMembersFirst("membersFirst");
			TTwoRef("twoRef");
			TTwoRef("twoRef commit");
			TTwoRef("twoRef delete");
			TTwoRef("twoRef delete commit");
			Assert.AreEqual(0, CountOccurences(typeof(CascadeDeleteDeleted.CddMember)));
		}

		private void TMembersFirst(string name)
		{
			bool commit = name.IndexOf("commit") > 1;
			IQuery q = NewQuery(this.GetType());
			q.Descend("name").Constrain(name);
			IObjectSet objectSet = q.Execute();
			CascadeDeleteDeleted cdd = (CascadeDeleteDeleted)objectSet.Next();
			Db().Delete(cdd.untypedMember);
			Db().Delete(cdd.typedMember);
			if (commit)
			{
				Db().Commit();
			}
			Db().Delete(cdd);
			if (!commit)
			{
				Db().Commit();
			}
		}

		private void TTwoRef(string name)
		{
			bool commit = name.IndexOf("commit") > 1;
			bool delete = name.IndexOf("delete") > 1;
			IQuery q = NewQuery(this.GetType());
			q.Descend("name").Constrain(name);
			IObjectSet objectSet = q.Execute();
			CascadeDeleteDeleted cdd = (CascadeDeleteDeleted)objectSet.Next();
			CascadeDeleteDeleted cdd2 = (CascadeDeleteDeleted)objectSet.Next();
			if (delete)
			{
				Db().Delete(cdd.untypedMember);
				Db().Delete(cdd.typedMember);
			}
			Db().Delete(cdd);
			if (commit)
			{
				Db().Commit();
			}
			Db().Delete(cdd2);
			if (!commit)
			{
				Db().Commit();
			}
		}
	}
}

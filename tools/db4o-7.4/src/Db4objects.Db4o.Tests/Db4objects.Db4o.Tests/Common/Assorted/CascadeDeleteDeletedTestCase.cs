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
using Db4oUnit.Extensions;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Tests.Common.Assorted;

namespace Db4objects.Db4o.Tests.Common.Assorted
{
	public class CascadeDeleteDeletedTestCase : Db4oClientServerTestCase
	{
		public string name;

		public object untypedMember;

		public CascadeDeleteDeletedTestCase.CddMember typedMember;

		public static void Main(string[] args)
		{
			new Db4objects.Db4o.Tests.Common.Assorted.CascadeDeleteDeletedTestCase().RunClientServer
				();
		}

		public CascadeDeleteDeletedTestCase()
		{
		}

		public CascadeDeleteDeletedTestCase(string name)
		{
			this.name = name;
		}

		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(this).CascadeOnDelete(true);
		}

		protected override void Store()
		{
			IExtObjectContainer oc = Db();
			MembersFirst(oc, "membersFirst commit");
			MembersFirst(oc, "membersFirst");
			TwoRef(oc, "twoRef");
			TwoRef(oc, "twoRef commit");
			TwoRef(oc, "twoRef delete");
			TwoRef(oc, "twoRef delete commit");
		}

		private void MembersFirst(IExtObjectContainer oc, string name)
		{
			Db4objects.Db4o.Tests.Common.Assorted.CascadeDeleteDeletedTestCase cdd = new Db4objects.Db4o.Tests.Common.Assorted.CascadeDeleteDeletedTestCase
				(name);
			cdd.untypedMember = new CascadeDeleteDeletedTestCase.CddMember();
			cdd.typedMember = new CascadeDeleteDeletedTestCase.CddMember();
			oc.Store(cdd);
		}

		private void TwoRef(IExtObjectContainer oc, string name)
		{
			Db4objects.Db4o.Tests.Common.Assorted.CascadeDeleteDeletedTestCase cdd = new Db4objects.Db4o.Tests.Common.Assorted.CascadeDeleteDeletedTestCase
				(name);
			cdd.untypedMember = new CascadeDeleteDeletedTestCase.CddMember();
			cdd.typedMember = new CascadeDeleteDeletedTestCase.CddMember();
			Db4objects.Db4o.Tests.Common.Assorted.CascadeDeleteDeletedTestCase cdd2 = new Db4objects.Db4o.Tests.Common.Assorted.CascadeDeleteDeletedTestCase
				(name);
			cdd2.untypedMember = cdd.untypedMember;
			cdd2.typedMember = cdd.typedMember;
			oc.Store(cdd);
			oc.Store(cdd2);
		}

		/// <exception cref="Exception"></exception>
		public virtual void _testDeleteDeleted()
		{
			int total = 10;
			int CddMemberCount = 12;
			IExtObjectContainer[] containers = new IExtObjectContainer[total];
			IExtObjectContainer oc = null;
			try
			{
				for (int i = 0; i < total; i++)
				{
					containers[i] = OpenNewClient();
					AssertOccurrences(containers[i], typeof(CascadeDeleteDeletedTestCase.CddMember), 
						CddMemberCount);
				}
				for (int i = 0; i < total; i++)
				{
					DeleteAll(containers[i], typeof(CascadeDeleteDeletedTestCase.CddMember));
				}
				oc = OpenNewClient();
				AssertOccurrences(oc, typeof(CascadeDeleteDeletedTestCase.CddMember), CddMemberCount
					);
				// ocs[0] deleted all CddMember objects, and committed the change
				containers[0].Commit();
				containers[0].Close();
				// FIXME: following assertion fails
				AssertOccurrences(oc, typeof(CascadeDeleteDeletedTestCase.CddMember), 0);
				for (int i = 1; i < total; i++)
				{
					containers[i].Close();
				}
				AssertOccurrences(oc, typeof(CascadeDeleteDeletedTestCase.CddMember), 0);
			}
			finally
			{
				if (oc != null)
				{
					oc.Close();
				}
				for (int i = 0; i < total; i++)
				{
					if (containers[i] != null)
					{
						containers[i].Close();
					}
				}
			}
		}

		public class CddMember
		{
			public string name;
		}
	}
}

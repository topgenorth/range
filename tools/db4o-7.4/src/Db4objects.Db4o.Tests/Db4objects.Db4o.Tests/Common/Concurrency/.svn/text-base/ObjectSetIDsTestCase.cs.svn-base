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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Concurrency;

namespace Db4objects.Db4o.Tests.Common.Concurrency
{
	public class ObjectSetIDsTestCase : Db4oClientServerTestCase
	{
		public static void Main(string[] args)
		{
			new ObjectSetIDsTestCase().RunConcurrency();
		}

		internal const int Count = 11;

		protected override void Store()
		{
			for (int i = 0; i < Count; i++)
			{
				Store(new ObjectSetIDsTestCase());
			}
		}

		public virtual void Conc(IExtObjectContainer oc)
		{
			IQuery q = oc.Query();
			q.Constrain(this.GetType());
			IObjectSet res = q.Execute();
			Assert.AreEqual(Count, res.Size());
			long[] ids1 = new long[res.Size()];
			int i = 0;
			while (res.HasNext())
			{
				ids1[i++] = oc.GetID(res.Next());
			}
			res.Reset();
			long[] ids2 = res.Ext().GetIDs();
			Assert.AreEqual(Count, ids1.Length);
			Assert.AreEqual(Count, ids2.Length);
			for (int j = 0; j < ids1.Length; j++)
			{
				bool found = false;
				for (int k = 0; k < ids2.Length; k++)
				{
					if (ids1[j] == ids2[k])
					{
						found = true;
						break;
					}
				}
				Assert.IsTrue(found);
			}
		}
	}
}

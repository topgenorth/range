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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Concurrency;
using Db4objects.Db4o.Tests.Common.Persistent;

namespace Db4objects.Db4o.Tests.Common.Concurrency
{
	public class ReadObjectSODATestCase : Db4oClientServerTestCase
	{
		public static void Main(string[] args)
		{
			new ReadObjectSODATestCase().RunConcurrency();
		}

		private static string testString = "simple test string";

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			for (int i = 0; i < ThreadCount(); i++)
			{
				Store(new SimpleObject(testString + i, i));
			}
		}

		/// <exception cref="Exception"></exception>
		public virtual void ConcReadSameObject(IExtObjectContainer oc)
		{
			int mid = ThreadCount() / 2;
			IQuery query = oc.Query();
			query.Descend("_s").Constrain(testString + mid).And(query.Descend("_i").Constrain
				(mid));
			IObjectSet result = query.Execute();
			Assert.AreEqual(1, result.Size());
			SimpleObject expected = new SimpleObject(testString + mid, mid);
			Assert.AreEqual(expected, result.Next());
		}

		/// <exception cref="Exception"></exception>
		public virtual void ConcReadDifferentObject(IExtObjectContainer oc, int seq)
		{
			IQuery query = oc.Query();
			query.Descend("_s").Constrain(testString + seq).And(query.Descend("_i").Constrain
				(seq));
			IObjectSet result = query.Execute();
			Assert.AreEqual(1, result.Size());
			SimpleObject expected = new SimpleObject(testString + seq, seq);
			Assert.AreEqual(expected, result.Next());
		}
	}
}

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
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Soda.Util;

namespace Db4objects.Db4o.Tests.Common.Soda.Util
{
	public class SodaTestUtil
	{
		public static void ExpectOne(IQuery query, object @object)
		{
			Expect(query, new object[] { @object });
		}

		public static void ExpectNone(IQuery query)
		{
			Expect(query, null);
		}

		public static void Expect(IQuery query, object[] results)
		{
			Expect(query, results, false);
		}

		public static void ExpectOrdered(IQuery query, object[] results)
		{
			Expect(query, results, true);
		}

		public static void Expect(IQuery query, object[] results, bool ordered)
		{
			IObjectSet set = query.Execute();
			if (results == null || results.Length == 0)
			{
				if (set.Size() > 0)
				{
					Assert.Fail("No content expected.");
				}
				return;
			}
			int j = 0;
			Assert.AreEqual(results.Length, set.Size());
			while (set.HasNext())
			{
				object obj = set.Next();
				bool found = false;
				if (ordered)
				{
					if (TCompare.IsEqual(results[j], obj))
					{
						results[j] = null;
						found = true;
					}
					j++;
				}
				else
				{
					for (int i = 0; i < results.Length; i++)
					{
						if (results[i] != null)
						{
							if (TCompare.IsEqual(results[i], obj))
							{
								results[i] = null;
								found = true;
								break;
							}
						}
					}
				}
				if (ordered)
				{
					Assert.IsTrue(found, "Expected '" + results[j - 1] + "' but got '" + obj + "' at index "
						 + (j - 1));
				}
				else
				{
					Assert.IsTrue(found, "Object not expected: " + obj);
				}
			}
			for (int i = 0; i < results.Length; i++)
			{
				if (results[i] != null)
				{
					Assert.Fail("Expected object not returned: " + results[i]);
				}
			}
		}

		private SodaTestUtil()
		{
		}
	}
}

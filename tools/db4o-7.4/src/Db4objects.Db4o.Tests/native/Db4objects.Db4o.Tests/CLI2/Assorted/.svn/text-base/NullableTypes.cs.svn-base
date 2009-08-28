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
namespace Db4objects.Db4o.Tests.CLI2.Assorted
{
	using System;

	using Db4objects.Db4o;
	using Db4objects.Db4o.Query;

	using Db4oUnit;
	using Db4oUnit.Extensions;

	class NullableContainer
	{
		public int? intValue = null;
		public DateTime? dateValue = null;

		public NullableContainer(int value)
		{
			intValue = value;
		}

		public NullableContainer(DateTime value)
		{
			dateValue = value;
		}
	}

	class NullableTypes : AbstractDb4oTestCase
	{
		static readonly DateTime TheDate = new DateTime(1983, 3, 7);

		protected override void Store()
		{
			Db().Store(new NullableContainer(42));
			Db().Store(new NullableContainer(TheDate));
		}

		public void TestGlobalQuery()
		{
			IQuery query = NewQuery();
			query.Constrain(typeof(NullableContainer));

			IObjectSet os = query.Execute();
			Assert.AreEqual(2, os.Size());

			bool foundInt = false;
			bool foundDate = false;
			while (os.HasNext())
			{
				NullableContainer item = (NullableContainer)os.Next();
				if (item.intValue.HasValue)
				{
					Assert.AreEqual(42, item.intValue.Value);
					Assert.IsFalse(item.dateValue.HasValue);
					foundInt = true;
				}
				else if (item.dateValue.HasValue)
				{
					Assert.AreEqual(TheDate, item.dateValue.Value);
					Assert.IsFalse(item.intValue.HasValue);
					foundDate = true;
				}
			}

			Assert.IsTrue(foundInt);
			Assert.IsTrue(foundDate);
		}

		public void TestDateQuery()
		{
			IObjectSet os = Db().Get(new NullableContainer(TheDate));
			CheckDateValueQueryResult(os);
		}

		private static void CheckDateValueQueryResult(IObjectSet os)
		{
			Assert.AreEqual(1, os.Size());
			NullableContainer found = (NullableContainer)os.Next();
			Assert.AreEqual(TheDate, found.dateValue.Value);
			EnsureIsNull(found.intValue);
		}

		public void TestIntQuery()
		{
			IObjectSet os = Db().Get(new NullableContainer(42));
			CheckIntValueQueryResult(os);
		}

		public void TestSodaQuery()
		{
			IQuery q = NewQuery(typeof(NullableContainer));
			q.Descend("intValue").Constrain(42);
			CheckIntValueQueryResult(q.Execute());
		}

		public void TestSodaQueryWithNullConstrain()
		{
			IQuery q = NewQuery(typeof(NullableContainer));
			q.Descend("intValue").Constrain(null);
			CheckDateValueQueryResult(q.Execute());
		}

		private static void CheckIntValueQueryResult(IObjectSet os)
		{
			Assert.AreEqual(1, os.Size());
			NullableContainer found = (NullableContainer)os.Next();
			Assert.AreEqual(42, found.intValue.Value);
			EnsureIsNull(found.dateValue);
		}

		private static void EnsureIsNull<T>(Nullable<T> value) where T : struct
		{
			Assert.IsFalse(value.HasValue, "!nullable.HasValue");
		}
	}
}

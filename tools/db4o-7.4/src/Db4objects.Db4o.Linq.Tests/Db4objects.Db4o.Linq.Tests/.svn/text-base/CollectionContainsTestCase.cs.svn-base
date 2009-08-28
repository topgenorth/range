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
using System.Collections.Generic;
using System.Linq;

using Db4objects.Db4o;
using Db4objects.Db4o.Linq;
using Db4objects.Db4o.Linq.Internals;

using Db4oUnit;
using Db4oUnit.Extensions;

namespace Db4objects.Db4o.Linq.Tests
{
	public class CollectionContainsTestCase : AbstractDb4oLinqTestCase
	{
		public abstract class Person
		{
			protected abstract IEnumerable<string> GetNames();

			public override bool Equals(object obj)
			{
				Person p = obj as Person;
				if (p == null) return false;

				var anames = GetNames();
				var bnames = p.GetNames();

				if (anames == null)
					return bnames == null;

				return anames.SequenceEqual(bnames);
			}

			public override int GetHashCode()
			{
				var names = GetNames();
				if (names == null) return 0;

				var list = names.ToList();

				if (list.Count == 0) return 0;

				int hash = list[0].GetHashCode();
				for (int i = 1; i < list.Count; i++)
					hash ^= list[i].GetHashCode();

				return hash;
			}
		}

		public class ArrayListPerson : Person
		{
			public IList Names = new ArrayList();

			protected override IEnumerable<string> GetNames()
			{
				return Names.Cast<string>();
			}
		}

		public class ArrayPerson : Person
		{
			public string[] Names;

			protected override IEnumerable<string> GetNames()
			{
				return this.Names;
			}
		}

		protected override void Store()
		{
			Store(new ArrayListPerson { Names = { "Biro", "Biro" } });
			Store(new ArrayListPerson { Names = { "Luna" } });
			Store(new ArrayListPerson { Names = { "Loustic" } });
			Store(new ArrayListPerson { Names = { "Loupiot" } });
			Store(new ArrayListPerson { Names = { "Biro", "Miro" } });
			Store(new ArrayListPerson { Names = { "Tounage" } });

			Store(new ArrayPerson { Names = new [] { "Biro", "Biro" } });
			Store(new ArrayPerson { Names = new [] { "Luna" } });
			Store(new ArrayPerson { Names = new [] { "Loustic" } });
			Store(new ArrayPerson { Names = new [] { "Loupiot" } });
			Store(new ArrayPerson { Names = new [] { "Biro", "Miro" } });
			Store(new ArrayPerson { Names = new [] { "Tounage" } });
		}

		public void TestQueryOnArrayListContains()
		{
			var q = NewQuery(typeof(ArrayListPerson));
			q.Descend("Names").Constrain("Biro").Contains();

			var persons = new ObjectSetWrapper<Person>(q.Execute());

			AssertSet(new[]
				{
					new ArrayListPerson { Names = { "Biro", "Biro" } },
					new ArrayListPerson { Names = { "Biro", "Miro" } },
				}, persons);
		}

		public void TestLinqQueryOnArrayListContains()
		{
			AssertQuery("(ArrayListPerson(Names contains 'Biro'))",
				delegate
				{
					var biros = from ArrayListPerson p in Db()
								where p.Names.Contains("Biro")
								select p;

					AssertSet(new[]
						{
							new ArrayListPerson { Names = { "Biro", "Biro" } },
							new ArrayListPerson { Names = { "Biro", "Miro" } },
						}, biros);
				});
		}

		public void _TestQueryOnArrayContains()
		{
			var q = NewQuery(typeof(ArrayPerson));
			q.Descend("Names").Constrain("Biro").Contains();

			var persons = new ObjectSetWrapper<Person>(q.Execute());

			AssertSet(new[]
				{
					new ArrayPerson { Names = new [] { "Biro", "Biro" } },
					new ArrayPerson { Names = new [] { "Biro", "Miro" } },
				}, persons);
		}

		public void _TestLinqQueryOnArrayContains()
		{
			AssertQuery("(ArrayPerson(Names contains 'Biro'))",
				delegate
				{
					var biros = from ArrayPerson p in Db()
								where p.Names.Contains("Biro")
								select p;

					AssertSet(new[]
						{
							new ArrayPerson { Names = new [] { "Biro", "Biro" } },
							new ArrayPerson { Names = new [] { "Biro", "Miro" } },
						}, biros);
				});
		}
	}
}

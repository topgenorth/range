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
using System.Collections.Generic;

namespace Db4objects.Db4o.Tests.CLI1.CrossPlatform
{
	public class Person
	{
		private static IComparer<Person> _sortByYear = new SortByYearImpl();

		private string _name;
		private int _year;
		private DateTime _localReleaseDate;

		public Person(string name, int year, DateTime localReleaseDate)
		{
			_name = name;
			_year = year;
			_localReleaseDate = localReleaseDate;
		}

		public string Name
		{
			get { return _name; }
		}

		public int Year
		{
			get { return _year; }
		}

		public DateTime LocalReleaseDate
		{
			get { return _localReleaseDate; }
		}

		public override bool Equals(object obj)
		{
			Person candidate = obj as Person;
			if (candidate == null) return false;

			if (candidate.GetType() != GetType()) return false;

			// FIXME: Dates are not working correctly yet.
			//return _name == candidate.Name && _year == candidate.Year && _localReleaseDate == candidate.LocalReleaseDate;
			return _name == candidate.Name && _year == candidate.Year;
		}

		public static IComparer<Person> SortByYear
		{
			get
			{
				return _sortByYear;
			}
		}

		public override string ToString()
		{
			//FIXME: Dates not working
			return _name + "|" + _year; // +"|" + _localReleaseDate;
		}

		private sealed class SortByYearImpl : IComparer<Person>
		{
			#region IComparer<Person> Members

			public int Compare(Person lhs, Person rhs)
			{
				return lhs._year - rhs._year;
			}

			#endregion
		}
	}
}

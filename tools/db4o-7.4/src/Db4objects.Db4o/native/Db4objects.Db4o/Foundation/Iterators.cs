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
using System.Collections;
using System.Collections.Generic;

namespace Db4objects.Db4o.Foundation
{
	public delegate B Function<A, B>(A a);

	public struct Tuple<A, B>
	{
		public A a;
		public B b;

		public Tuple(A a, B b)
		{
			this.a = a;
			this.b = b;
		}
	}

	public partial class Iterators
	{
		public static IEnumerable<T> Cast<T>(IEnumerable source)
		{
			foreach (object o in source) yield return (T) o;
		}

		public static IEnumerable<Tuple<object, object>> Zip(IEnumerable @as, IEnumerable bs)
		{
			return Zip(Cast<object>(@as), Cast<object>(bs));
		}

		public static IEnumerable<Tuple<A, B>> Zip<A, B>(IEnumerable<A> @as, IEnumerable<B> bs)
		{
			IEnumerator<B> bsEnumerator = bs.GetEnumerator();
			foreach (A a in @as)
			{
				if (!bsEnumerator.MoveNext())
				{
					yield break;
				}

				yield return new Tuple<A, B>(a, bsEnumerator.Current);
			}
		}

		public static IEnumerable Unique(IEnumerable enumerable)
		{
			Hashtable seen = new Hashtable();
			foreach (object item in enumerable)
			{
				if (seen.ContainsKey(item)) continue;
				seen.Add(item, item);
				yield return item;
			}
		}
	}
}

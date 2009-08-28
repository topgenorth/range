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
#if CF_3_5

using System.Collections.Generic;
using Db4objects.Db4o;

namespace Db4objects.Db4o.Linq
{
	public static class ObjectContainerExtensions
	{
		/// <summary>
		/// This is the entry point of Linq to db4o.
		/// It allows the compiler to call the standard query operators.
		/// 
		/// As Compact Framework doesn't suport Expression Trees, Linq queries
		/// over an ObjectContainer will run unoptimized.
		/// </summary>
		/// <typeparam name="T">The type to query the ObjectContainer</typeparam>
		/// <param name="self">An ObjectContainer</param>
		/// <returns>A <see cref="Db4objects.Db4o.Linq.IDb4oLinqQuery">IDb4oLinqQuery</see> marker interface</returns>
		public static IEnumerable<T> Cast<T>(this IObjectContainer container)
		{
			return container.Query<T>();
		}
	}
}
#endif
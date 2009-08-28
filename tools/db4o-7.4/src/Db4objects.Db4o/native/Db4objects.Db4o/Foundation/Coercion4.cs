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

namespace Db4objects.Db4o.Foundation
{
	public class Coercion4
	{
		public static object ToSByte(object obj)
		{
			if (obj is byte) return obj;

			IConvertible convertible = obj as IConvertible;
			if (null != convertible) return convertible.ToSByte(null);
			return Db4objects.Db4o.Foundation.No4.Instance;
		}

		public static object ToShort(object obj)
		{
			if (obj is short) return obj;

			IConvertible convertible = obj as IConvertible;
			if (null != convertible) return convertible.ToInt16(null);
			return Db4objects.Db4o.Foundation.No4.Instance;
		}

		public static object ToInt(object obj)
		{
			if (obj is int) return obj;

			IConvertible convertible = obj as IConvertible;
			if (null != convertible) return convertible.ToInt32(null);
			return Db4objects.Db4o.Foundation.No4.Instance;
		}

		public static object ToLong(object obj)
		{
			if (obj is long) return obj;

			IConvertible convertible = obj as IConvertible;
			if (null != convertible) return convertible.ToInt64(null);
			return Db4objects.Db4o.Foundation.No4.Instance;
		}

		public static object ToFloat(object obj)
		{
			if (obj is float) return obj;

			IConvertible convertible = obj as IConvertible;
			if (null != convertible) return convertible.ToSingle(null);
			return Db4objects.Db4o.Foundation.No4.Instance;
		}

		public static object ToDouble(object obj)
		{
			if (obj is double) return obj;

			IConvertible convertible = obj as IConvertible;
			if (null != convertible) return convertible.ToDouble(null);
			return Db4objects.Db4o.Foundation.No4.Instance;
		}
	}
}


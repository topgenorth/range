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
using Db4objects.Db4o.Reflect.Generic;

namespace Db4objects.Db4o.Reflect.Generic
{
	/// <exclude></exclude>
	public class GenericObject
	{
		internal readonly GenericClass _class;

		private object[] _values;

		public GenericObject(GenericClass clazz)
		{
			_class = clazz;
		}

		private void EnsureValuesInitialized()
		{
			if (_values == null)
			{
				_values = new object[_class.GetFieldCount()];
			}
		}

		public virtual void Set(int index, object value)
		{
			EnsureValuesInitialized();
			_values[index] = value;
		}

		/// <param name="index"></param>
		/// <returns>the value of the field at index, based on the fields obtained GenericClass.getDeclaredFields
		/// 	</returns>
		public virtual object Get(int index)
		{
			EnsureValuesInitialized();
			return _values[index];
		}

		public override string ToString()
		{
			if (_class == null)
			{
				return base.ToString();
			}
			return _class.ToString(this);
		}

		public virtual GenericClass GetGenericClass()
		{
			return _class;
		}
	}
}

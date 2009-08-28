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
namespace Db4objects.Db4o.Internal.Btree
{
	/// <summary>
	/// Composite key for field indexes, first compares on the actual
	/// indexed field _value and then on the _parentID (which is a
	/// reference to the containing object).
	/// </summary>
	/// <remarks>
	/// Composite key for field indexes, first compares on the actual
	/// indexed field _value and then on the _parentID (which is a
	/// reference to the containing object).
	/// </remarks>
	/// <exclude></exclude>
	public class FieldIndexKey
	{
		private readonly object _value;

		private readonly int _parentID;

		public FieldIndexKey(int parentID, object value)
		{
			_parentID = parentID;
			_value = value;
		}

		public virtual int ParentID()
		{
			return _parentID;
		}

		public virtual object Value()
		{
			return _value;
		}

		public override string ToString()
		{
			return "FieldIndexKey(" + _parentID + ", " + SafeString(_value) + ")";
		}

		private string SafeString(object value)
		{
			if (null == value)
			{
				return "null";
			}
			return value.ToString();
		}
	}
}

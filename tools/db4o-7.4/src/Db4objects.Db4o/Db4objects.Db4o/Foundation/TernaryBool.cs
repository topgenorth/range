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
namespace Db4objects.Db4o.Foundation
{
	/// <summary>yes/no/dontknow data type</summary>
	/// <exclude></exclude>
	[System.Serializable]
	public sealed class TernaryBool
	{
		private const int NoId = -1;

		private const int YesId = 1;

		private const int UnspecifiedId = 0;

		public static readonly Db4objects.Db4o.Foundation.TernaryBool No = new Db4objects.Db4o.Foundation.TernaryBool
			(NoId);

		public static readonly Db4objects.Db4o.Foundation.TernaryBool Yes = new Db4objects.Db4o.Foundation.TernaryBool
			(YesId);

		public static readonly Db4objects.Db4o.Foundation.TernaryBool Unspecified = new Db4objects.Db4o.Foundation.TernaryBool
			(UnspecifiedId);

		private readonly int _value;

		private TernaryBool(int value)
		{
			_value = value;
		}

		public bool BooleanValue(bool defaultValue)
		{
			switch (_value)
			{
				case NoId:
				{
					return false;
				}

				case YesId:
				{
					return true;
				}

				default:
				{
					return defaultValue;
					break;
				}
			}
		}

		public bool IsUnspecified()
		{
			return this == Unspecified;
		}

		public bool DefiniteYes()
		{
			return this == Yes;
		}

		public bool DefiniteNo()
		{
			return this == No;
		}

		public static Db4objects.Db4o.Foundation.TernaryBool ForBoolean(bool value)
		{
			return (value ? Yes : No);
		}

		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			Db4objects.Db4o.Foundation.TernaryBool tb = (Db4objects.Db4o.Foundation.TernaryBool
				)obj;
			return _value == tb._value;
		}

		public override int GetHashCode()
		{
			return _value;
		}

		private object ReadResolve()
		{
			switch (_value)
			{
				case NoId:
				{
					return No;
				}

				case YesId:
				{
					return Yes;
				}

				default:
				{
					return Unspecified;
					break;
				}
			}
		}

		public override string ToString()
		{
			switch (_value)
			{
				case NoId:
				{
					return "NO";
				}

				case YesId:
				{
					return "YES";
				}

				default:
				{
					return "UNSPECIFIED";
					break;
				}
			}
		}
	}
}

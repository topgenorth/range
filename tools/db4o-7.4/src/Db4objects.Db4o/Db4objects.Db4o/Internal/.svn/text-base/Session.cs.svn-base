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
using Db4objects.Db4o.Internal;

namespace Db4objects.Db4o.Internal
{
	internal sealed class Session
	{
		internal readonly string i_fileName;

		private int i_openCount;

		internal Session(string a_fileName)
		{
			i_fileName = a_fileName;
		}

		/// <summary>returns true, if session is to be closed completely</summary>
		internal bool CloseInstance()
		{
			i_openCount--;
			return i_openCount < 0;
		}

		/// <summary>Will raise an exception if argument class doesn't match this class - violates equals() contract in favor of failing fast.
		/// 	</summary>
		/// <remarks>Will raise an exception if argument class doesn't match this class - violates equals() contract in favor of failing fast.
		/// 	</remarks>
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			if (null == obj)
			{
				return false;
			}
			if (GetType() != obj.GetType())
			{
				Exceptions4.ShouldNeverHappen();
			}
			return i_fileName.Equals(((Db4objects.Db4o.Internal.Session)obj).i_fileName);
		}

		public override int GetHashCode()
		{
			return i_fileName.GetHashCode();
		}

		internal string FileName()
		{
			return i_fileName;
		}
	}
}

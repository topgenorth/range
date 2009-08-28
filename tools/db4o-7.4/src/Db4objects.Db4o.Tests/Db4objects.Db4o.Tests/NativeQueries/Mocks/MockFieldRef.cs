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
using Db4objects.Db4o.Instrumentation.Api;
using Db4objects.Db4o.Tests.NativeQueries.Mocks;

namespace Db4objects.Db4o.Tests.NativeQueries.Mocks
{
	public class MockFieldRef : IFieldRef
	{
		private readonly string _name;

		private readonly ITypeRef _type;

		public MockFieldRef(string name) : this(name, new MockTypeRef(typeof(object)))
		{
		}

		public MockFieldRef(string name, ITypeRef typeRef)
		{
			if (null == name)
			{
				throw new ArgumentNullException();
			}
			if (null == typeRef)
			{
				throw new ArgumentNullException();
			}
			_name = name;
			_type = typeRef;
		}

		public virtual string Name
		{
			get
			{
				return _name;
			}
		}

		public virtual ITypeRef Type
		{
			get
			{
				return _type;
			}
		}

		public override string ToString()
		{
			return Name;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is IFieldRef))
			{
				return false;
			}
			IFieldRef other = (IFieldRef)obj;
			return _name.Equals(other.Name) && _type.Equals(other.Type);
		}

		public override int GetHashCode()
		{
			return _name.GetHashCode() + 29 * _type.GetHashCode();
		}
	}
}

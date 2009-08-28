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
using Db4objects.Db4o.Reflect;

namespace Db4objects.Db4o.Reflect
{
	/// <exclude></exclude>
	public class ArrayInfo
	{
		private int _elementCount;

		private bool _primitive;

		private bool _nullable;

		private IReflectClass _reflectClass;

		public virtual int ElementCount()
		{
			return _elementCount;
		}

		public virtual void ElementCount(int count)
		{
			_elementCount = count;
		}

		public virtual bool Primitive()
		{
			return _primitive;
		}

		public virtual void Primitive(bool flag)
		{
			_primitive = flag;
		}

		public virtual bool Nullable()
		{
			return _nullable;
		}

		public virtual void Nullable(bool flag)
		{
			_nullable = flag;
		}

		public virtual IReflectClass ReflectClass()
		{
			return _reflectClass;
		}

		public virtual void ReflectClass(IReflectClass claxx)
		{
			_reflectClass = claxx;
		}
	}
}

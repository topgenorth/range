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
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal.Marshall;

namespace Db4objects.Db4o.Internal.Marshall
{
	public class RawFieldSpec
	{
		private readonly AspectType _type;

		private readonly string _name;

		private readonly int _handlerID;

		private readonly bool _isPrimitive;

		private readonly bool _isArray;

		private readonly bool _isNArray;

		private readonly bool _isVirtual;

		private int _indexID;

		public RawFieldSpec(AspectType aspectType, string name, int handlerID, byte attribs
			)
		{
			_type = aspectType;
			_name = name;
			_handlerID = handlerID;
			BitMap4 bitmap = new BitMap4(attribs);
			_isPrimitive = bitmap.IsTrue(0);
			_isArray = bitmap.IsTrue(1);
			_isNArray = bitmap.IsTrue(2);
			_isVirtual = false;
			_indexID = 0;
		}

		public RawFieldSpec(AspectType aspectType, string name)
		{
			_type = aspectType;
			_name = name;
			_handlerID = 0;
			_isPrimitive = false;
			_isArray = false;
			_isNArray = false;
			_isVirtual = true;
			_indexID = 0;
		}

		public virtual string Name()
		{
			return _name;
		}

		public virtual int HandlerID()
		{
			return _handlerID;
		}

		public virtual bool IsPrimitive()
		{
			return _isPrimitive;
		}

		public virtual bool IsArray()
		{
			return _isArray;
		}

		public virtual bool IsNArray()
		{
			return _isNArray;
		}

		public virtual bool IsVirtual()
		{
			return _isVirtual;
		}

		public virtual int IndexID()
		{
			return _indexID;
		}

		internal virtual void IndexID(int indexID)
		{
			_indexID = indexID;
		}

		public override string ToString()
		{
			return "RawFieldSpec(" + Name() + ")";
		}

		public virtual bool IsFieldMetadata()
		{
			return _type.IsFieldMetadata();
		}
	}
}

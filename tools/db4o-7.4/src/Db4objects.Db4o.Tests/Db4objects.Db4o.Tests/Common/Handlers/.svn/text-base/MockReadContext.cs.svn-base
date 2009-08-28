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
using Db4objects.Db4o;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Marshall;
using Db4objects.Db4o.Tests.Common.Handlers;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Tests.Common.Handlers
{
	public class MockReadContext : Db4objects.Db4o.Tests.Common.Handlers.MockMarshallingContext
		, IReadContext
	{
		public MockReadContext(IObjectContainer objectContainer) : base(objectContainer)
		{
		}

		public MockReadContext(MockWriteContext writeContext) : this(writeContext.ObjectContainer
			())
		{
			writeContext._header.CopyTo(_header, 0, 0, writeContext._header.Length());
			writeContext._payLoad.CopyTo(_payLoad, 0, 0, writeContext._payLoad.Length());
		}

		public virtual object ReadObject(ITypeHandler4 handler)
		{
			return handler.Read(this);
		}

		public virtual BitMap4 ReadBitMap(int bitCount)
		{
			BitMap4 map = new BitMap4(_current._buffer, _current._offset, bitCount);
			_current.Seek(_current.Offset() + map.MarshalledLength());
			return map;
		}
	}
}

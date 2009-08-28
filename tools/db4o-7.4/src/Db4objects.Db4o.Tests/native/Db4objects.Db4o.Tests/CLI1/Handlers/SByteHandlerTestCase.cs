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
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4objects.Db4o.Tests.Common.Handlers;

namespace Db4objects.Db4o.Tests.CLI1.Handlers
{
    class SByteHandlerTestCase : TypeHandlerTestCaseBase
    {
        public virtual void TestReadWrite()
        {
            MockWriteContext writeContext = new MockWriteContext(Db());
            sbyte expected = 0x11;
            SByteHandler().Write(writeContext, expected);
            MockReadContext readContext = new MockReadContext(writeContext);
            sbyte sbyteValue = (sbyte)SByteHandler().Read(readContext);
            Assert.AreEqual(expected, sbyteValue);
        }

        public virtual void TestStoreObject()
        {
            SByteHandlerTestCase.Item storedItem = new SByteHandlerTestCase.Item(0x11, 0x22);
            DoTestStoreObject(storedItem);
        }

        private Db4objects.Db4o.Internal.Handlers.SByteHandler SByteHandler()
        {
            return new Db4objects.Db4o.Internal.Handlers.SByteHandler();
        }

        public class Item
        {
            public sbyte _sbyte;

            public SByte _sbyteWrapper;

            public Item(sbyte s, SByte wrapper)
            {
                _sbyte = s;
                _sbyteWrapper = wrapper;
            }

            public override bool Equals(object obj)
            {
                if (obj == this)
                {
                    return true;
                }
                if (!(obj is SByteHandlerTestCase.Item))
                {
                    return false;
                }
                SByteHandlerTestCase.Item other = (SByteHandlerTestCase.Item)obj;
                return (other._sbyte == this._sbyte) && this._sbyteWrapper.Equals(other._sbyteWrapper
                    );
            }

            public override string ToString()
            {
                return "[" + _sbyte + "," + _sbyteWrapper + "]";
            }
        }

    }
}

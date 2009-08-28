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
    class UShortHandlerTestCase : TypeHandlerTestCaseBase
    {
        public virtual void TestReadWrite()
        {
            MockWriteContext writeContext = new MockWriteContext(Db());
            ushort expected = 0x1122;
            UShortHandler().Write(writeContext, expected);
            MockReadContext readContext = new MockReadContext(writeContext);
            ushort ushortValue = (ushort)UShortHandler().Read(readContext);
            Assert.AreEqual(expected, ushortValue);
        }

        public virtual void TestStoreObject()
        {
            ULongHandlerTestCase.Item storedItem = new ULongHandlerTestCase.Item(0x1122, 0x8877);
            DoTestStoreObject(storedItem);
        }

        private Db4objects.Db4o.Internal.Handlers.UShortHandler UShortHandler()
        {
            return new Db4objects.Db4o.Internal.Handlers.UShortHandler();
        }

        public class Item
        {
            public ushort _ushort;

            public UInt16 _ushortWrapper;

            public Item(ushort u, UInt16 wrapper)
            {
                _ushort = u;
                _ushortWrapper = wrapper;
            }

            public override bool Equals(object obj)
            {
                if (obj == this)
                {
                    return true;
                }
                if (!(obj is UShortHandlerTestCase.Item))
                {
                    return false;
                }
                UShortHandlerTestCase.Item other = (UShortHandlerTestCase.Item)obj;
                return (other._ushort == this._ushort) && this._ushortWrapper.Equals(other._ushortWrapper
                    );
            }

            public override string ToString()
            {
                return "[" + _ushort + "," + _ushortWrapper + "]";
            }
        }

    }
}
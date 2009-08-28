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
    class UIntHandlerTestCase : TypeHandlerTestCaseBase
    {
        public virtual void TestReadWrite()
        {
            MockWriteContext writeContext = new MockWriteContext(Db());
            uint expected = 0x11223344;
            UIntHandler().Write(writeContext, expected);
            MockReadContext readContext = new MockReadContext(writeContext);
            uint uintValue = (uint)UIntHandler().Read(readContext);
            Assert.AreEqual(expected, uintValue);
        }

        public virtual void TestStoreObject()
        {
            UIntHandlerTestCase.Item storedItem = new UIntHandlerTestCase.Item((uint)0x11223344, (uint)0x55667788);
            DoTestStoreObject(storedItem);
        }

        private Db4objects.Db4o.Internal.Handlers.UIntHandler UIntHandler()
        {
            return new Db4objects.Db4o.Internal.Handlers.UIntHandler();
        }

        public class Item
        {
            public uint _uint;

            public UInt32 _uintWrapper;

            public Item(uint u, UInt32 wrapper)
            {
                _uint = u;
                _uintWrapper = wrapper;
            }

            public override bool Equals(object obj)
            {
                if (obj == this)
                {
                    return true;
                }
                if (!(obj is UIntHandlerTestCase.Item))
                {
                    return false;
                }
                UIntHandlerTestCase.Item other = (UIntHandlerTestCase.Item)obj;
                return (other._uint == this._uint) && this._uintWrapper.Equals(other._uintWrapper
                    );
            }

            public override string ToString()
            {
                return "[" + _uint + "," + _uintWrapper + "]";
            }
        }

    }
}

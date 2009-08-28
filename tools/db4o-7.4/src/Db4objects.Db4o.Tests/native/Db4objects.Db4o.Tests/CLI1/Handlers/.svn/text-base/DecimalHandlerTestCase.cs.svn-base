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
    class DecimalHandlerTestCase : TypeHandlerTestCaseBase
    {
        public virtual void TestReadWrite()
        {
            MockWriteContext writeContext = new MockWriteContext(Db());
            decimal expected = Decimal.MaxValue;
            DecimalHandler().Write(writeContext, expected);
            MockReadContext readContext = new MockReadContext(writeContext);
            decimal decimalValue = (decimal)DecimalHandler().Read(readContext);
            Assert.AreEqual(expected, decimalValue);
        }

        public virtual void TestStoreObject()
        {
            DecimalHandlerTestCase.Item storedItem = new DecimalHandlerTestCase.Item(Decimal.MaxValue, Decimal.MinValue);
            DoTestStoreObject(storedItem);
        }

        private Db4objects.Db4o.Internal.Handlers.DecimalHandler DecimalHandler()
        {
            return new Db4objects.Db4o.Internal.Handlers.DecimalHandler();
        }

        public class Item
        {
            public decimal _decimal;

            public Decimal _decimalWrapper;

            public Item(decimal d, Decimal wrapper)
            {
                _decimal = d;
                _decimalWrapper = wrapper;
            }

            public override bool Equals(object obj)
            {
                if (obj == this)
                {
                    return true;
                }
                if (!(obj is DecimalHandlerTestCase.Item))
                {
                    return false;
                }
                DecimalHandlerTestCase.Item other = (DecimalHandlerTestCase.Item)obj;
                return (other._decimal == this._decimal) && this._decimalWrapper.Equals(other._decimalWrapper
                    );
            }

            public override string ToString()
            {
                return "[" + _decimal + "," + _decimalWrapper + "]";
            }
        }

    }
}

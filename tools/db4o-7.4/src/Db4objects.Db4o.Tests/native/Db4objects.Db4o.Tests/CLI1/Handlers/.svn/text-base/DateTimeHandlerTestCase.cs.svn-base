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
    class DateTimeHandlerTestCase : TypeHandlerTestCaseBase
    {
        public virtual void TestReadWrite()
        {
            MockWriteContext writeContext = new MockWriteContext(Db());
            DateTime expected = new DateTime();
            DateTimeHandler().Write(writeContext, expected);
            MockReadContext readContext = new MockReadContext(writeContext);
            DateTime actual = (DateTime)DateTimeHandler().Read(readContext);
            Assert.AreEqual(expected, actual);
        }

        public virtual void TestStoreObject()
        {
            DateTimeHandlerTestCase.Item storedItem = new DateTimeHandlerTestCase.Item(new DateTime());
            DoTestStoreObject(storedItem);
        }

        private Db4objects.Db4o.Internal.Handlers.DateTimeHandler DateTimeHandler()
        {
            return new Db4objects.Db4o.Internal.Handlers.DateTimeHandler();
        }

        public class Item
        {
            public DateTime _dateTime;

            public Item(DateTime wrapper)
            {
                _dateTime = wrapper;
            }

            public override bool Equals(object obj)
            {
                if (obj == this)
                {
                    return true;
                }
                if (!(obj is DateTimeHandlerTestCase.Item))
                {
                    return false;
                }
                DateTimeHandlerTestCase.Item other = (DateTimeHandlerTestCase.Item)obj;
                return (other._dateTime == _dateTime);
            }

            public override string ToString()
            {
                return "[" + _dateTime + "]";
            }
        }

    }
}

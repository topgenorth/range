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
using Db4objects.Db4o.Internal.Handlers;

namespace Db4objects.Db4o.Tests.Common.Internal
{
    public partial class Comparable4TestCase
    {
        public void TestDecimalHandler()
        {
            AssertHandlerComparison(typeof(DecimalHandler), ((decimal)0), ((decimal) 1));
            AssertHandlerComparison(typeof(DecimalHandler), ((decimal)-1), ((decimal) 0));
            AssertHandlerComparison(typeof(DecimalHandler), Decimal.MinValue, Decimal.MaxValue);
        }

        public void TestSByteHandler()
        {
            AssertHandlerComparison(typeof(SByteHandler), ((sbyte)0), ((sbyte)1));
            AssertHandlerComparison(typeof(SByteHandler), ((sbyte)-1), ((sbyte)0));
            AssertHandlerComparison(typeof(SByteHandler), SByte.MinValue, SByte.MaxValue);
        }
        
        public void TestULongHandler()
        {
            AssertHandlerComparison(typeof(ULongHandler), ((ulong)0), ((ulong)1));
            AssertHandlerComparison(typeof(ULongHandler), UInt64.MinValue, UInt64.MaxValue);
        }

        public void TestUIntHandler()
        {
            AssertHandlerComparison(typeof(UIntHandler), ((uint)0), ((uint)1));
            AssertHandlerComparison(typeof(UIntHandler), UInt32.MinValue, UInt32.MaxValue);
        }
        
        public void TestUShortHandler()
        {
            AssertHandlerComparison(typeof(UShortHandler), ((ushort)0), ((ushort)1));
            AssertHandlerComparison(typeof(UShortHandler), UInt16.MinValue, UInt16.MaxValue);
        }

        public void TestDateTimeHandler()
        {
            AssertHandlerComparison(typeof(DateTimeHandler), new DateTime(2007, 12, 18), new DateTime(2007, 12, 19));
            AssertHandlerComparison(typeof(DateTimeHandler), DateTime.MinValue, DateTime.MaxValue);
        }
    }
}

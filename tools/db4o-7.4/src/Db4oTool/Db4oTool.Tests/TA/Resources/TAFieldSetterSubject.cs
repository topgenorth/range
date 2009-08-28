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
using System.Collections.Generic;
using Db4oTool.Tests.TA; // MockActivator
using Db4oUnit;

class Tagged
{
    public string tags;

    public Tagged(string tags_)
    {
        tags = tags_;
    }
}

struct ValueTypeSubject
{
    public ValueTypeSubject(int i)
    {
        intValue = i;
    }

    public int intValue;
}

class FieldSetterTestSubject
{
    public int intValue = 0;
    public volatile byte volatileByte = 0;
    public Tagged refValue = null;
    public ValueTypeSubject valueType;
    public List<int> intList = null;
}

class TAFieldSetterSubject : ITestCase
{
    public void TestFieldSetterActivatesObject()
    {
        FieldSetterTestSubject obj = new FieldSetterTestSubject();
        MockActivator a = ActivatorFor(obj);
        Assert.AreEqual(0, a.ReadCount);
     
        obj.intValue = 10;
        int writeCount = 1;
		Assert.AreEqual(writeCount++, a.WriteCount);

        obj.refValue = null;
		Assert.AreEqual(writeCount++, a.WriteCount);

        obj.volatileByte = 3;
		Assert.AreEqual(writeCount++, a.WriteCount);
		
		Assert.AreEqual(0, a.ReadCount);
        obj.valueType.intValue = 4;
		Assert.AreEqual(1, a.ReadCount);

        obj.valueType = new ValueTypeSubject(5);
		Assert.AreEqual(writeCount++, a.WriteCount);

        obj.intList = new List<int>(6);
		Assert.AreEqual(writeCount++, a.WriteCount);
		Assert.AreEqual(1, a.ReadCount);
    }

    private static MockActivator ActivatorFor(object p)
    {
        return MockActivator.ActivatorFor(p);
    }
}
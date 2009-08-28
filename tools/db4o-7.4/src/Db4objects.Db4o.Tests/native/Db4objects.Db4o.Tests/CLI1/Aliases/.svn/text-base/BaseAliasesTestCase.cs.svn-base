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
using Db4objects.Db4o.Query;
using Db4oUnit;
using Db4oUnit.Extensions;

namespace Db4objects.Db4o.Tests.CLI1.Aliases
{
    public class BaseAliasesTestCase : AbstractDb4oTestCase
    {
        protected void AssertAliasedData(IObjectContainer container)
        {
            AssertAliasedData(container.Get(GetAliasedDataType()));

            AssertAliasedData(QueryAliasedData(container));
        }

        protected IObjectSet QueryAliasedData(IObjectContainer container)
        {
            IQuery query = container.Query();
            query.Constrain(GetAliasedDataType());
            return query.Execute();
        }

        protected void AssertAliasedData(IObjectSet os)
        {
            AssertAliasedData(os, "Homer Simpson", "John Cleese");
        }

        protected void AssertAliasedData(IObjectSet os, params string[] expectedNames)
        {
            Assert.AreEqual(expectedNames.Length, os.Size());
            foreach (string name in expectedNames)
            {
                AssertContains(os, CreateAliasedData(name));
            }
        }

        protected virtual Type GetAliasedDataType()
        {
            return typeof(Person2);
        }

        protected object CreateAliasedData(string name)
        {
            return GetAliasedDataType()
                .GetConstructor(new Type[] { typeof(string) })
                    .Invoke(new object[] { name });
        }

        public static void AssertContains(IObjectSet actual, object expected)
        {
            actual.Reset();
            while (actual.HasNext())
            {
                object next = actual.Next();
                if (CFHelper.AreEqual(next, expected)) return;
            }
            Assert.Fail("Expected item: " + expected);
        }

    }
}

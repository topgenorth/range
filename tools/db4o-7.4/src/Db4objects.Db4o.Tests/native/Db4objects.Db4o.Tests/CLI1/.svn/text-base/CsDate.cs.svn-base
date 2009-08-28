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

namespace Db4objects.Db4o.Tests.CLI1
{
    /// <summary>
    /// testing deactivation and zero date.
    /// </summary>
    public class CsDate : AbstractDb4oTestCase
    {
        public class Item
        {
            public DateTime dateTime;

            public bool flag = true;

            public Item()
            {
            }

            public Item(DateTime value)
            {
                dateTime = value;
            }
        }

        override protected void Store()
        {
            Store(new Item(DateTime.Now));
        }

        public void TestTrivialQuery()
        {
            IQuery q = NewQuery(typeof(Item));
            IObjectSet os = q.Execute();
            Assert.AreEqual(1, os.Size());
        }

        public void TestQueryByExample()
        {
            Item template = new Item();
            GetOne(template);

            template.dateTime = new DateTime(0);
            GetOne(template);

            template.dateTime = new DateTime(100);
            Assert.AreEqual(0, Db().Get(template).Size());
        }

        private void GetOne(object template)
        {
            Assert.AreEqual(1, Db().Get(template).Count);
        }

        public void TestDeactivation()
        {
            Item template = new Item(new DateTime(100));
            Db().Deactivate(template, int.MaxValue);
            Assert.AreEqual(new DateTime(0), template.dateTime);
        }

        public void TestSODA()
        {
            DateTime before = DateTime.Now.AddDays(-1);
            DateTime after = DateTime.Now.AddDays(1);

            IQuery q = NewQuery(typeof(Item));
            q.Descend("dateTime").Constrain(before).Smaller();
            Assert.AreEqual(0, q.Execute().Size());

            q = NewQuery(typeof(Item));
            q.Descend("dateTime").Constrain(after).Greater();
            Assert.AreEqual(0, q.Execute().Size());

            q = NewQuery(typeof(Item));
            q.Descend("dateTime").Constrain(before).Greater();
            Assert.AreEqual(1, q.Execute().Size());

            q = NewQuery(typeof(Item));
            q.Descend("dateTime").Constrain(after).Smaller();
            Assert.AreEqual(1, q.Execute().Size());

            q = NewQuery(typeof(Item));
            q.Descend("flag").Constrain(true);
            q.Descend("dateTime").Constrain(before).Greater();
            q.Descend("dateTime").Constrain(after).Smaller();
            Assert.AreEqual(1, q.Execute().Size());

            q = NewQuery(typeof(Item));
            q.Descend("flag").Constrain(false);
            q.Descend("dateTime").Constrain(before).Greater();
            q.Descend("dateTime").Constrain(after).Smaller();
            Assert.AreEqual(0, q.Execute().Size());
        }
    }
}

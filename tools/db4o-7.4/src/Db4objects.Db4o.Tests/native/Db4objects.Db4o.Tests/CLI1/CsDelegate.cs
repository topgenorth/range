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

namespace Db4objects.Db4o.Tests.CLI1
{
    public class OnActivateEventStrategy
    {
        public event EventHandler Crash;

        public void ObjectOnActivate(IObjectContainer container)
        {
            Assert.IsNull(Crash);
            Crash += new EventHandler(Boom);
        }

        public void RaiseCrash()
        {
            if (null != Crash)
            {
                Crash(this, EventArgs.Empty);
            }
        }

        static string Message = null;

        public static void Prepare()
        {
            Message = null;
        }

        public static void Check()
        {
            Assert.AreEqual("Boom!!!!", OnActivateEventStrategy.Message);
        }

        static void Boom(object sender, EventArgs args)
        {
            Message = "Boom!!!!";
        }
    }

    public class CsDelegate : AbstractDb4oTestCase
    {
        public event EventHandler Bang;

        object UntypedDelegate;

        static string Message = null;

        public void RaiseBang()
        {
            Bang(this, EventArgs.Empty);
        }

        override protected void Store()
        {
            CsDelegate item = new CsDelegate();
            item.Bang += new EventHandler(OnBang);
            item.UntypedDelegate = new EventHandler(OnBang);
            Store(item);
        }

        public void TestFieldsAreNotStored()
        {
            CsDelegate instance = (CsDelegate)RetrieveOnlyInstance(GetType());
            // delegate fields are simply not stored
            Assert.AreEqual(null, instance.Bang);
            Assert.AreEqual(null, instance.UntypedDelegate);
        }

        public void TestOnActivateEventStrategy()
        {
            DeleteAllInstances(typeof(OnActivateEventStrategy));
            Store(new OnActivateEventStrategy());
            Fixture().Reopen(GetType());

            OnActivateEventStrategy.Prepare();
            OnActivateEventStrategy obj = (OnActivateEventStrategy)Db().Get(typeof(OnActivateEventStrategy)).Next();
            obj.RaiseCrash();
            OnActivateEventStrategy.Check();
        }

        private void DeleteAllInstances(Type type)
        {
            foreach (object item in Db().Query(type))
            {
                Db().Delete(item);
            }
        }

        static void OnBang(object sender, EventArgs args)
        {
            CsDelegate.Message = "Bang!!!!";
        }
    }
}

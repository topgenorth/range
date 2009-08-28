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
using Db4objects.Db4o;

namespace Db4objects.Db4o.Tests.CLI1
{
    /// <summary>
    /// A facade to an ObjectContainer executing in a different AppDomain.
    /// </summary>
    public class MarshalByRefDatabase : MarshalByRefObject, IDisposable
    {
        protected IObjectServer _server;
        protected IObjectContainer _container;

        public void Open(string fname, bool clientServer)
        {
            if (clientServer)
            {
                _server = Db4oFactory.OpenServer(fname, 0);
                _container = _server.OpenClient();
            }
            else
            {
                _container = Db4oFactory.OpenFile(fname);
            }
        }

        public void Dispose()
        {
            if (null != _container)
            {
                _container.Close();
                _container = null;
            }
            if (null != _server)
            {
                _server.Close();
                _server = null;
            }
            // MAGIC: give some time for the db4o background threads to exit
            System.Threading.Thread.Sleep(1000);
        }
    }
}

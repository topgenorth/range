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
using Db4objects.Db4o;
using Db4objects.Db4o.Bench.Crud;
using Db4objects.Db4o.Bench.Logging;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.IO;

namespace Db4objects.Db4o.Tutorial.F1.Chapter10
{
    /// <summary>
    /// Very simple CRUD (Create, Read, Update, Delete) application to
    /// produce log files as an input for I/O-benchmarking.
    /// </summary>
    /// <remarks>
    /// Very simple CRUD (Create, Read, Update, Delete) application to
    /// produce log files as an input for I/O-benchmarking.
    /// </remarks>
    public class CrudApplication
    {
        private static readonly string DatabaseFile = "simplecrud.db4o";

        public CrudApplication()
        { }

        public virtual void Run(int itemCount)
        {
            IConfiguration config = Prepare(itemCount);
            Create(itemCount, config);
            Read(config);
            Update(config);
            Delete(config);
            DeleteDbFile();
        }

        private IConfiguration Prepare(int itemCount)
        {
            DeleteDbFile();
            RandomAccessFileAdapter rafAdapter = new RandomAccessFileAdapter();
            IoAdapter ioAdapter = new LoggingIoAdapter(rafAdapter, LogFileName(itemCount));
            IConfiguration config = Db4oFactory.CloneConfiguration();
            config.Io(ioAdapter);
            ioAdapter.Close();
            return config;
        }

        private void Create(int itemCount, IConfiguration config)
        {
            IObjectContainer oc = Db4oFactory.OpenFile(config, DatabaseFile);
            for (int i = 0; i < itemCount; i++)
            {
                oc.Store(Item.NewItem(i));
                // preventing heap space problems by committing from time to time
                if (i % 100000 == 0)
                {
                    oc.Commit();
                }
            }
            oc.Commit();
            oc.Close();
        }

        private void Read(IConfiguration config)
        {
            IObjectContainer oc = Db4oFactory.OpenFile(config, DatabaseFile);
            IObjectSet objectSet = oc.Query(typeof(Item));
            while (objectSet.HasNext())
            {
                Item item = (Item)objectSet.Next();
            }
            oc.Close();
        }

        private void Update(IConfiguration config)
        {
            IObjectContainer oc = Db4oFactory.OpenFile(config, DatabaseFile);
            IObjectSet objectSet = oc.Query(typeof(Item));
            while (objectSet.HasNext())
            {
                Item item = (Item)objectSet.Next();
                item.Change();
                oc.Store(item);
            }
            oc.Close();
        }

        private void Delete(IConfiguration config)
        {
            IObjectContainer oc = Db4oFactory.OpenFile(config, DatabaseFile);
            IObjectSet objectSet = oc.Query(typeof(Item));
            while (objectSet.HasNext())
            {
                oc.Delete(objectSet.Next());
                // adding commit results in more syncs in the log, 
                // which is necessary for meaningful statistics!
                oc.Commit();
            }
            oc.Close();
        }

        private void DeleteDbFile()
        {
            new Sharpen.IO.File(DatabaseFile).Delete();
        }

        public static string LogFileName(int itemCount)
        {
            return "simplecrud_" + itemCount + ".log";
        }
    }
}

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
using System.IO;
using Db4objects.Db4o;

namespace Db4objects.Db4o.Tutorial.F1.Chapter5
{
    public class TransactionExample : Util
    {
        public static void Main(string[] args)
        {
            File.Delete(Util.YapFileName);
            IObjectContainer db=Db4oFactory.OpenFile(Util.YapFileName);
            try
            {
                StoreCarCommit(db);
                db.Close();
                db = Db4oFactory.OpenFile(Util.YapFileName);
                ListAllCars(db);
                StoreCarRollback(db);
                db.Close();
                db = Db4oFactory.OpenFile(Util.YapFileName);
                ListAllCars(db);
                CarSnapshotRollback(db);
                CarSnapshotRollbackRefresh(db);
            }
            finally
            {
                db.Close();
            }
        }
        
        public static void StoreCarCommit(IObjectContainer db)
        {
            Pilot pilot = new Pilot("Rubens Barrichello", 99);
            Car car = new Car("BMW");
            car.Pilot = pilot;
            db.Store(car);
            db.Commit();
        }
    
        public static void ListAllCars(IObjectContainer db)
        {
            IObjectSet result = db.QueryByExample(typeof(Car));
            ListResult(result);
        }
        
        public static void StoreCarRollback(IObjectContainer db)
        {
            Pilot pilot = new Pilot("Michael Schumacher", 100);
            Car car = new Car("Ferrari");
            car.Pilot = pilot;
            db.Store(car);
            db.Rollback();
        }
    
        public static void CarSnapshotRollback(IObjectContainer db)
        {
            IObjectSet result = db.QueryByExample(new Car("BMW"));
            Car car = (Car)result.Next();
            car.Snapshot();
            db.Store(car);
            db.Rollback();
            Console.WriteLine(car);
        }
    
        public static void CarSnapshotRollbackRefresh(IObjectContainer db)
        {
            IObjectSet result=db.QueryByExample(new Car("BMW"));
            Car car=(Car)result.Next();
            car.Snapshot();
            db.Store(car);
            db.Rollback();
            db.Ext().Refresh(car, int.MaxValue);
            Console.WriteLine(car);
        }
    }
}

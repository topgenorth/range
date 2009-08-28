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
using Db4objects.Db4o.TA;
using Db4objects.Db4o.Tutorial.F1;

namespace Db4objects.Db4o.Tutorial.F1.Chapter8
{
    public class TransparentPersistenceExample : Util
    {
        public static void Main(String[] args)
        {
            File.Delete(YapFileName);
            ConfigureTransparentPersistence();
            IObjectContainer db = Db4oFactory.OpenFile(YapFileName);
            try
            {
                StoreCarAndSnapshots(db);
                db.Close();
                db = Db4oFactory.OpenFile(YapFileName);
                ModifySnapshotHistory(db);
                db.Close();
                db = Db4oFactory.OpenFile(YapFileName);
                ReadSnapshotHistory(db);
            }
            finally
            {
                db.Close();
            }
        }

        public static void ConfigureTransparentPersistence()
        {
            Db4oFactory.Configure().Add(new TransparentPersistenceSupport());
        }

        public static void StoreCarAndSnapshots(IObjectContainer db)
        {
            Car car = new Car("Ferrari");
            for (int i = 0; i < 3; i++)
            {
                car.snapshot();
            }
            db.Store(car);
        }

    public static void ModifySnapshotHistory(IObjectContainer db) {
    	System.Console.WriteLine("Read all sensors and modify the description:");
        IObjectSet result=db.QueryByExample(typeof(Car));
        Car car=(Car)result.Next();
        SensorReadout readout=car.History;
        while(readout!=null) {
            System.Console.WriteLine(readout);
        	readout.Description = "Modified: " + readout.Description;
            readout = readout.Next;
        }
        db.Commit();
    }

    public static void ReadSnapshotHistory(IObjectContainer db) {
    	System.Console.WriteLine("Read all modified sensors:");
        IObjectSet result=db.QueryByExample(typeof(Car));
        Car car=(Car)result.Next();
        SensorReadout readout=car.History;
        while(readout!=null) {
            System.Console.WriteLine(readout);
            readout=readout.Next;
        }
    }
}
}
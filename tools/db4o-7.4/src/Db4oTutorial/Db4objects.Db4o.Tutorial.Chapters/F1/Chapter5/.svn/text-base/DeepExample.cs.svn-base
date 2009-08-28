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
	public class DeepExample : Util
    {
        public static void Main(string[] args)
        {
            File.Delete(Util.YapFileName);
            IObjectContainer db = Db4oFactory.OpenFile(Util.YapFileName);
            try
            {
                StoreCar(db);
                db.Close();
                SetCascadeOnUpdate();
                db = Db4oFactory.OpenFile(Util.YapFileName);
                TakeManySnapshots(db);
                db.Close();
                db = Db4oFactory.OpenFile(Util.YapFileName);
                RetrieveAllSnapshots(db);
                db.Close();
                db = Db4oFactory.OpenFile(Util.YapFileName);
                RetrieveSnapshotsSequentially(db);
                RetrieveSnapshotsSequentiallyImproved(db);
                db.Close();
                SetActivationDepth();
                db = Db4oFactory.OpenFile(Util.YapFileName);
                RetrieveSnapshotsSequentially(db);
            }
            finally
            {
                db.Close();
            }
        }
        
        public static void StoreCar(IObjectContainer db)
        {
            Pilot pilot = new Pilot("Rubens Barrichello", 99);
            Car car = new Car("BMW");
            car.Pilot = pilot;
            db.Store(car);
        }
        
        public static void SetCascadeOnUpdate()
        {
            Db4oFactory.Configure().ObjectClass(typeof(Car)).CascadeOnUpdate(true);
        }
        
        public static void TakeManySnapshots(IObjectContainer db)
        {
            IObjectSet result = db.QueryByExample(typeof(Car));
            Car car = (Car)result.Next();
            for (int i=0; i<5; i++)
            {
                car.Snapshot();
            }
            db.Store(car);
        }
        
        public static void RetrieveAllSnapshots(IObjectContainer db)
        {
            IObjectSet result = db.QueryByExample(typeof(SensorReadout));
            while (result.HasNext())
            {
                Console.WriteLine(result.Next());
            }
        }
        
        public static void RetrieveSnapshotsSequentially(IObjectContainer db)
        {
            IObjectSet result = db.QueryByExample(typeof(Car));
            Car car = (Car)result.Next();
            SensorReadout readout = car.GetHistory();
            while (readout != null)
            {
                Console.WriteLine(readout);
                readout = readout.Next;
            }
        }
        
        public static void RetrieveSnapshotsSequentiallyImproved(IObjectContainer db)
        {
            IObjectSet result = db.QueryByExample(typeof(Car));
            Car car = (Car)result.Next();
            SensorReadout readout = car.GetHistory();
            while (readout != null)
            {
                db.Activate(readout, 1);
                Console.WriteLine(readout);
                readout = readout.Next;
            }
        }
        
        public static void SetActivationDepth()
        {
            Db4oFactory.Configure().ObjectClass(typeof(TemperatureSensorReadout))
                .CascadeOnActivate(true);
        }
        
    }
}

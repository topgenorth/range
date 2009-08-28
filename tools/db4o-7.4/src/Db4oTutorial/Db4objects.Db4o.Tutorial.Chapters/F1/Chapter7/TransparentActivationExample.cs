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

namespace Db4objects.Db4o.Tutorial.F1.Chapter7
{
    public class TransparentActivationExample : Util
    {
        public static void Main(String[] args)
        {
            File.Delete(YapFileName);
            IObjectContainer db = Db4oFactory.OpenFile(YapFileName);
            try
            {
                StoreCarAndSnapshots(db);
                db.Close();

                db = Db4oFactory.OpenFile(YapFileName);
                RetrieveSnapshotsSequentially(db);
                db.Close();

                ConfigureTransparentActivation();
                db = Db4oFactory.OpenFile(YapFileName);
                RetrieveSnapshotsSequentially(db);
                db.Close();

                db = Db4oFactory.OpenFile(YapFileName);
                DemonstrateTransparentActivation(db);
                db.Close();
            }
            finally
            {
                db.Close();
            }
        }

        public static void ConfigureTransparentActivation()
        {
            Db4oFactory.Configure().Add(new TransparentActivationSupport());
        }

        public static void SetCascadeOnUpdate()
        {
            Db4oFactory.Configure().ObjectClass(typeof (Car)).CascadeOnUpdate(true);
        }

        public static void StoreCarAndSnapshots(IObjectContainer db)
        {
            Pilot pilot = new Pilot("Kimi Raikkonen", 110);
            Car car = new Car("Ferrari");
            car.Pilot = pilot;
            for (int i = 0; i < 5; i++)
            {
                car.snapshot();
            }
            db.Store(car);
        }

        public static void RetrieveSnapshotsSequentially(IObjectContainer db)
        {
            IObjectSet result = db.QueryByExample(typeof (Car));
            Car car = (Car) result.Next();
            SensorReadout readout = car.History;
            while (readout != null)
            {
                Console.WriteLine(readout);
                readout = readout.Next;
            }
        }

        public static void DemonstrateTransparentActivation(IObjectContainer db)
        {
            IObjectSet result = db.QueryByExample(typeof (Car));
            Car car = (Car) result.Next();

            Console.WriteLine("#PilotWithoutActivation before the car is activated");
            Console.WriteLine(car.PilotWithoutActivation);

            Console.WriteLine("accessing 'Pilot' property activates the car object");
            Console.WriteLine(car.Pilot);

            Console.WriteLine("Accessing PilotWithoutActivation property after the car is activated");
            Console.WriteLine(car.PilotWithoutActivation);
        }
    }
}
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
using System.Collections;
using System.IO;

using Db4objects.Db4o;
using Db4objects.Db4o.Query;

namespace Db4objects.Db4o.Tutorial.F1.Chapter3
{	
	public class CollectionsExample : Util
	{
		public static void Main(string[] args)
		{
			File.Delete(Util.YapFileName);            
			IObjectContainer db = Db4oFactory.OpenFile(Util.YapFileName);
			try
			{
				StoreFirstCar(db);
				StoreSecondCar(db);
				RetrieveAllSensorReadout(db);
				RetrieveSensorReadoutQBE(db);
				RetrieveCarQBE(db);
				RetrieveCollections(db);
				RetrieveArrays(db);
				RetrieveSensorReadoutQuery(db);
				RetrieveCarQuery(db);
				db.Close();
				UpdateCarPart1();
				db = Db4oFactory.OpenFile(Util.YapFileName);
				UpdateCarPart2(db);
				UpdateCollection(db);
				db.Close();
				DeleteAllPart1();
				db=Db4oFactory.OpenFile(Util.YapFileName);
				DeleteAllPart2(db);
				RetrieveAllSensorReadout(db);
			}
			finally
			{
				db.Close();
			}
		}
        
		public static void StoreFirstCar(IObjectContainer db)
		{
			Car car1 = new Car("Ferrari");
			Pilot pilot1 = new Pilot("Michael Schumacher", 100);
			car1.Pilot = pilot1;
			db.Store(car1);
		}
        
		public static void StoreSecondCar(IObjectContainer db)
		{
			Pilot pilot2 = new Pilot("Rubens Barrichello", 99);
			Car car2 = new Car("BMW");
			car2.Pilot = pilot2;
			car2.Snapshot();
			car2.Snapshot();
			db.Store(car2);       
		}
        
		public static void RetrieveAllSensorReadout(IObjectContainer db)
		{
			IObjectSet result = db.QueryByExample(typeof(SensorReadout));
			ListResult(result);
		}
        
		public static void RetrieveSensorReadoutQBE(IObjectContainer db)
		{
			SensorReadout proto = new SensorReadout(new double[] { 0.3, 0.1 }, DateTime.MinValue, null);
			IObjectSet result = db.QueryByExample(proto);
			ListResult(result);
		}
        
		public static void RetrieveCarQBE(IObjectContainer db)
		{
			SensorReadout protoReadout = new SensorReadout(new double[] { 0.6, 0.2 }, DateTime.MinValue, null);
			IList protoHistory = new ArrayList();
			protoHistory.Add(protoReadout);
			Car protoCar = new Car(null, protoHistory);
			IObjectSet result = db.QueryByExample(protoCar);
			ListResult(result);
		}
        
		public static void RetrieveCollections(IObjectContainer db)
		{
			IObjectSet result = db.QueryByExample(new ArrayList());
			ListResult(result);
		}
        
		public static void RetrieveArrays(IObjectContainer db)
		{
			IObjectSet result = db.QueryByExample(new double[] { 0.6, 0.4 });
			ListResult(result);
		}
        
		public static void RetrieveSensorReadoutQuery(IObjectContainer db)
		{
			IQuery query = db.Query();
			query.Constrain(typeof(SensorReadout));
			IQuery valueQuery = query.Descend("_values");
			valueQuery.Constrain(0.3);
			valueQuery.Constrain(0.1);
			IObjectSet results = query.Execute();
			ListResult(results);
		}
        
		public static void RetrieveCarQuery(IObjectContainer db)
		{
			IQuery query = db.Query();
			query.Constrain(typeof(Car));
			IQuery historyQuery = query.Descend("_history");
			historyQuery.Constrain(typeof(SensorReadout));
			IQuery valueQuery = historyQuery.Descend("_values");
			valueQuery.Constrain(0.3);
			valueQuery.Constrain(0.1);
			IObjectSet results = query.Execute();
			ListResult(results);
		}

		public class RetrieveSensorReadoutPredicate : Predicate
		{
			public bool Match(SensorReadout candidate)
			{
				return Array.IndexOf(candidate.Values, 0.3) > -1
					&& Array.IndexOf(candidate.Values, 0.1) > -1;
			}
		}
        
		public static void RetrieveSensorReadoutNative(IObjectContainer db) 
		{
			IObjectSet results = db.Query(new RetrieveSensorReadoutPredicate());
			ListResult(results);
		}

		public class RetrieveCarPredicate : Predicate
		{
			public bool Match(Car car)
			{
				foreach (SensorReadout sensor in car.History)
				{
					if (Array.IndexOf(sensor.Values, 0.3) > -1
						&& Array.IndexOf(sensor.Values, 0.1) > -1)
					{
						return true; 
					}
				}
				return false;
			}
		}

		public static void RetrieveCarNative(IObjectContainer db)
		{
			IObjectSet results = db.Query(new RetrieveCarPredicate());
			ListResult(results);
		}

		public static void UpdateCarPart1()
		{
			Db4oFactory.Configure().ObjectClass(typeof(Car)).CascadeOnUpdate(true);
		}
        
		public static void UpdateCarPart2(IObjectContainer db)
		{
			IObjectSet result = db.QueryByExample(new Car("BMW", null));
			Car car = (Car)result.Next();
			car.Snapshot();
			db.Store(car);
			RetrieveAllSensorReadout(db);
		}
        
		public static void UpdateCollection(IObjectContainer db)
		{
			IQuery query = db.Query();
			query.Constrain(typeof(Car));
			IObjectSet result = query.Descend("_history").Execute();
			IList history = (IList)result.Next();
			history.RemoveAt(0);
			db.Store(history);
			Car proto = new Car(null, null);
			result = db.QueryByExample(proto);
			foreach (Car car in result)
			{	
				foreach (object readout in car.History)
				{
					Console.WriteLine(readout);
				}
			}
		}
        
		public static void DeleteAllPart1()
		{
			Db4oFactory.Configure().ObjectClass(typeof(Car)).CascadeOnDelete(true);
		}

		public static void DeleteAllPart2(IObjectContainer db)
		{
			IObjectSet result = db.QueryByExample(new Car(null, null));
			foreach (object car in result)
			{
				db.Delete(car);
			}
			IObjectSet readouts = db.QueryByExample(new SensorReadout(null, DateTime.MinValue, null));
			foreach (object readout in readouts)
			{
				db.Delete(readout);
			}
		}
	}
}

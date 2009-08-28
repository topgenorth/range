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
using Db4objects.Db4o.Query;

using Db4objects.Db4o.Tutorial;

namespace Db4objects.Db4o.Tutorial.F1.Chapter1
{
    public class QueryExample : Util
    {
        public static void Main(string[] args)
        {
            IObjectContainer db = Db4oFactory.OpenFile(Util.YapFileName);
            try
            {
                StoreFirstPilot(db);
                StoreSecondPilot(db);
                RetrieveAllPilots(db);
                RetrievePilotByName(db);
                RetrievePilotByExactPoints(db);
                RetrieveByNegation(db);
                RetrieveByConjunction(db);
                RetrieveByDisjunction(db);
                RetrieveByComparison(db);
                RetrieveByDefaultFieldValue(db);
                RetrieveSorted(db); 
                ClearDatabase(db);
            }
            finally
            {
                db.Close();
            }
        }
    
        public static void StoreFirstPilot(IObjectContainer db)
        {
            Pilot pilot1 = new Pilot("Michael Schumacher", 100);
            db.Store(pilot1);
            Console.WriteLine("Stored {0}", pilot1);
        }
    
        public static void StoreSecondPilot(IObjectContainer db)
        {
            Pilot pilot2 = new Pilot("Rubens Barrichello", 99);
            db.Store(pilot2);
            Console.WriteLine("Stored {0}", pilot2);
        }
    
        public static void RetrieveAllPilots(IObjectContainer db)
        {
            IQuery query = db.Query();
            query.Constrain(typeof(Pilot));
            IObjectSet result = query.Execute();
            ListResult(result);
        }
    
        public static void RetrievePilotByName(IObjectContainer db)
        {
            IQuery query = db.Query();
            query.Constrain(typeof(Pilot));
            query.Descend("_name").Constrain("Michael Schumacher");
            IObjectSet result = query.Execute();
            ListResult(result);
        }
        
        public static void RetrievePilotByExactPoints(IObjectContainer db)
        {
            IQuery query = db.Query();
            query.Constrain(typeof(Pilot));
            query.Descend("_points").Constrain(100);
            IObjectSet result = query.Execute();
            ListResult(result);
        }
    
        public static void RetrieveByNegation(IObjectContainer db)
        {
            IQuery query = db.Query();
            query.Constrain(typeof(Pilot));
            query.Descend("_name").Constrain("Michael Schumacher").Not();
            IObjectSet result = query.Execute();
            ListResult(result);
        }
    
        public static void RetrieveByConjunction(IObjectContainer db)
        {
            IQuery query = db.Query();
            query.Constrain(typeof(Pilot));
            IConstraint constr = query.Descend("_name")
                    .Constrain("Michael Schumacher");
            query.Descend("_points")
                    .Constrain(99).And(constr);
            IObjectSet result = query.Execute();
            ListResult(result);
        }
    
        public static void RetrieveByDisjunction(IObjectContainer db)
        {
            IQuery query = db.Query();
            query.Constrain(typeof(Pilot));
            IConstraint constr = query.Descend("_name")
                    .Constrain("Michael Schumacher");
            query.Descend("_points")
                    .Constrain(99).Or(constr);
            IObjectSet result = query.Execute();
            ListResult(result);
        }
    
        public static void RetrieveByComparison(IObjectContainer db)
        {
            IQuery query = db.Query();
            query.Constrain(typeof(Pilot));
            query.Descend("_points")
                    .Constrain(99).Greater();
            IObjectSet result = query.Execute();
            ListResult(result);
        }
    
        public static void RetrieveByDefaultFieldValue(IObjectContainer db)
        {
            Pilot somebody = new Pilot("Somebody else", 0);
            db.Store(somebody);
            IQuery query = db.Query();
            query.Constrain(typeof(Pilot));
            query.Descend("_points").Constrain(0);
            IObjectSet result = query.Execute();
            ListResult(result);
            db.Delete(somebody);
        }
        
        public static void RetrieveSorted(IObjectContainer db)
        {
            IQuery query = db.Query();
            query.Constrain(typeof(Pilot));
            query.Descend("_name").OrderAscending();
            IObjectSet result = query.Execute();
            ListResult(result);
            query.Descend("_name").OrderDescending();
            result = query.Execute();
            ListResult(result);
        }
    
        public static void ClearDatabase(IObjectContainer db)
        {
            IObjectSet result = db.QueryByExample(typeof(Pilot));
            foreach (object item in result)
            {
                db.Delete(item);
            }
        }
    }
}

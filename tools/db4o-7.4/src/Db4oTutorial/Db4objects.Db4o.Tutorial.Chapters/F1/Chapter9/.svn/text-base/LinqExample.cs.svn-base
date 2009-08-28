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
using System.Collections.Generic;
using System.Linq;

using Db4objects.Db4o;
using Db4objects.Db4o.Linq;
using Db4objects.Db4o.Query;

using Db4objects.Db4o.Tutorial;

namespace Db4objects.Db4o.Tutorial.F1.Chapter9
{
	public class NQExample : Util
	{
		public static void Main(string[] args)
		{
			IObjectContainer db = Db4oFactory.OpenFile(Util.YapFileName);
			try
			{
				StoreObjects(db);
                RetrievePilot(db);
                RetrievePilotByCars(db);
                RetrievePilotUnoptimized(db);
				ClearDatabase(db);
			}
			finally
			{
				db.Close();
			}
		}
    
		public static void StoreObjects(IObjectContainer db)
		{
			db.Store(new Car("Ferrari", (new Pilot("Michael Schumacher", 100))));
			db.Store(new Car("BMW", (new Pilot("Rubens Barrichello", 99))));
		}
    
		public static void RetrievePilot(IObjectContainer db)
		{
            IEnumerable<Pilot> result = from Pilot p in db
                                        where p.Name.StartsWith("Michael")
                                        select p;
			ListResult(result);
		}

        public static void RetrievePilotByCars(IObjectContainer db)
        {
            IEnumerable<Pilot> result = from Car c in db
                                        where c.Model.StartsWith("F")
                                        && (c.Pilot.Points > 99 && c.Pilot.Points <150)
                                        select c.Pilot;
            ListResult(result);
        }

        public static void RetrievePilotUnoptimized(IObjectContainer db)
        {
            IEnumerable<Pilot> result = from Pilot p in db
                                        where (p.Points - 81) == p.Name.Length
                                        select p;
            ListResult(result);
        }

		public static void ClearDatabase(IObjectContainer db)
		{
			IObjectSet result = db.QueryByExample(null);
			while (result.HasNext())
			{
				db.Delete(result.Next());
			}
		}

        private static void ListResult<T>(IEnumerable<T> result)
        {
            foreach (T t in result)
            {
                System.Console.WriteLine(t);
            }
        }
	}
}

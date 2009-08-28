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
using System.IO;

using Db4objects.Db4o.Query;

using Db4objects.Db4o.Tutorial.F1.Chapter3;

namespace Db4objects.Db4o.Tutorial.F1.Chapter6
{
	public class EvaluationExample : Util
	{
		public static void Main(string[] args)
		{
			File.Delete(Util.YapFileName);
			IObjectContainer db = Db4oFactory.OpenFile(Util.YapFileName);
			try
			{
				StoreCars(db);
				QueryWithEvaluation(db);
			}
			finally
			{
				db.Close();
			}
		}

		public static void StoreCars(IObjectContainer db)
		{
			Pilot pilot1 = new Pilot("Michael Schumacher", 100);
			Car car1 = new Car("Ferrari");
			car1.Pilot = pilot1;
			car1.Snapshot();
			db.Store(car1);
			Pilot pilot2 = new Pilot("Rubens Barrichello", 99);
			Car car2 = new Car("BMW");
			car2.Pilot = pilot2;
			car2.Snapshot();
			car2.Snapshot();
			db.Store(car2);
		}

		public static void QueryWithEvaluation(IObjectContainer db)
		{
			IQuery query = db.Query();
			query.Constrain(typeof (Car));
			query.Constrain(new EvenHistoryEvaluation());
			IObjectSet result = query.Execute();
			Util.ListResult(result);
		}
	}
}
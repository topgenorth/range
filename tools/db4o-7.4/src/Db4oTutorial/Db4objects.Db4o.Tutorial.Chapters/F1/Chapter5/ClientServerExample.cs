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
using Db4objects.Db4o;

namespace Db4objects.Db4o.Tutorial.F1.Chapter5
{
    public class ClientServerExample : Util
    {
        public static void Main(string[] args)
        {
            File.Delete(Util.YapFileName);
            AccessLocalServer();
            File.Delete(Util.YapFileName);
            IObjectContainer db = Db4oFactory.OpenFile(Util.YapFileName);
            try
            {
                SetFirstCar(db);
                SetSecondCar(db);
            }
            finally
            {
                db.Close();
            }
            
            ConfigureDb4o();
            IObjectServer server = Db4oFactory.OpenServer(Util.YapFileName, 0);
            try
            {
                QueryLocalServer(server);
                DemonstrateLocalReadCommitted(server);
                DemonstrateLocalRollback(server);
            }
            finally
            {
                server.Close();
            }
            
            AccessRemoteServer();
            server = Db4oFactory.OpenServer(Util.YapFileName, ServerPort);
            server.GrantAccess(ServerUser, ServerPassword);
            try
            {
                QueryRemoteServer(ServerPort, ServerUser, ServerPassword);
                DemonstrateRemoteReadCommitted(ServerPort, ServerUser, ServerPassword);
                DemonstrateRemoteRollback(ServerPort, ServerUser, ServerPassword);
            }
            finally
            {
                server.Close();
            }
        }
            
        public static void SetFirstCar(IObjectContainer db)
        {
            Pilot pilot = new Pilot("Rubens Barrichello", 99);
            Car car = new Car("BMW");
            car.Pilot = pilot;
            db.Store(car);
        }
    
        public static void SetSecondCar(IObjectContainer db)
        {
            Pilot pilot = new Pilot("Michael Schumacher", 100);
            Car car = new Car("Ferrari");
            car.Pilot = pilot;
            db.Store(car);
        }
    
        public static void AccessLocalServer()
        {
            IObjectServer server = Db4oFactory.OpenServer(Util.YapFileName, 0);
            try
            {
                IObjectContainer client = server.OpenClient();
                // Do something with this client, or open more clients
                client.Close();
            }
            finally
            {
                server.Close();
            }
        }
    
        public static void QueryLocalServer(IObjectServer server)
        {
            IObjectContainer client = server.OpenClient();
            ListResult(client.QueryByExample(new Car(null)));
            client.Close();
        }
        
        public static void ConfigureDb4o()
        {
        	Db4oFactory.Configure().ObjectClass(typeof(Car)).UpdateDepth(3);
        }
    
        public static void DemonstrateLocalReadCommitted(IObjectServer server)
        {
            IObjectContainer client1 =server.OpenClient();
            IObjectContainer client2 =server.OpenClient();
            Pilot pilot = new Pilot("David Coulthard", 98);
            IObjectSet result = client1.QueryByExample(new Car("BMW"));
            Car car = (Car)result.Next();
            car.Pilot = pilot;
            client1.Store(car);
            ListResult(client1.QueryByExample(new Car(null)));
            ListResult(client2.QueryByExample(new Car(null)));
            client1.Commit();
            ListResult(client1.QueryByExample(typeof(Car)));			
            ListRefreshedResult(client2, client2.QueryByExample(typeof(Car)), 2);
            client1.Close();
            client2.Close();
        }
    
        public static void DemonstrateLocalRollback(IObjectServer server)
        {
            IObjectContainer client1 = server.OpenClient();
            IObjectContainer client2 = server.OpenClient();
            IObjectSet result = client1.QueryByExample(new Car("BMW"));
            Car car = (Car)result.Next();
            car.Pilot = new Pilot("Someone else", 0);
            client1.Store(car);
            ListResult(client1.QueryByExample(new Car(null)));
            ListResult(client2.QueryByExample(new Car(null)));
            client1.Rollback();
            client1.Ext().Refresh(car, 2);
            ListResult(client1.QueryByExample(new Car(null)));
            ListResult(client2.QueryByExample(new Car(null)));
            client1.Close();
            client2.Close();
        }
    
        public static void AccessRemoteServer()
        {
            IObjectServer server = Db4oFactory.OpenServer(Util.YapFileName, ServerPort);
            server.GrantAccess(ServerUser, ServerPassword);
            try
            {
                IObjectContainer client = Db4oFactory.OpenClient("localhost", ServerPort, ServerUser, ServerPassword);
                // Do something with this client, or open more clients
                client.Close();
            }
            finally
            {
                server.Close();
            }
        }
    
        public static void QueryRemoteServer(int port, string user, string password)
        {
            IObjectContainer client = Db4oFactory.OpenClient("localhost", port, user, password);
            ListResult(client.QueryByExample(new Car(null)));
            client.Close();
        }
    
        public static void DemonstrateRemoteReadCommitted(int port, string user, string password)
        {
            IObjectContainer client1 = Db4oFactory.OpenClient("localhost", port, user, password);
            IObjectContainer client2 = Db4oFactory.OpenClient("localhost", port, user, password);
            Pilot pilot = new Pilot("Jenson Button", 97);
            IObjectSet result = client1.QueryByExample(new Car(null));
            Car car = (Car)result.Next();
            car.Pilot = pilot;
            client1.Store(car);
            ListResult(client1.QueryByExample(new Car(null)));
            ListResult(client2.QueryByExample(new Car(null)));
            client1.Commit();
            ListResult(client1.QueryByExample(new Car(null)));
            ListResult(client2.QueryByExample(new Car(null)));
            client1.Close();
            client2.Close();
        }
    
        public static void DemonstrateRemoteRollback(int port, string user, string password)
        {
            IObjectContainer client1 = Db4oFactory.OpenClient("localhost", port, user, password);
            IObjectContainer client2 = Db4oFactory.OpenClient("localhost", port, user, password);
            IObjectSet result = client1.QueryByExample(new Car(null));
            Car car = (Car)result.Next();
            car.Pilot = new Pilot("Someone else", 0);
            client1.Store(car);
            ListResult(client1.QueryByExample(new Car(null)));
            ListResult(client2.QueryByExample(new Car(null)));
            client1.Rollback();
            client1.Ext().Refresh(car,2);
            ListResult(client1.QueryByExample(new Car(null)));
            ListResult(client2.QueryByExample(new Car(null)));
            client1.Close();
            client2.Close();
        }
    }
}

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
using Db4objects.Db4o.Query;
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4oUnit.Extensions.Fixtures;

namespace Db4objects.Db4o.Tests.CLI1
{
#if !CF
	public class ADTask : MarshalByRefObject
	{
		private String _name;
		
		public ADTask(string name)
		{
			_name = name;
		}
		
		public string Name
		{
			get
			{
				return _name;
			}
		}
	}

    class TaskDatabase : MarshalByRefDatabase, IOptOutCS
    {
        public string[] QueryTaskNames()
        {
            ArrayList names = new ArrayList();
            IObjectSet os = InternalQueryTasks();
            while (os.HasNext())
            {
                names.Add(((ADTask)os.Next()).Name);
            }
            return (string[])names.ToArray(typeof(string));
        }

        public ADTask[] QueryTasks()
        {
            ArrayList tasks = new ArrayList();
            IObjectSet os = InternalQueryTasks();
            while (os.HasNext())
            {
                tasks.Add(os.Next());
            }
            return (ADTask[])tasks.ToArray(typeof(ADTask));
        }

        private IObjectSet InternalQueryTasks()
        {
            IQuery query = _container.Query();
            query.Constrain(typeof(ADTask));
            query.Descend("_name").OrderAscending();
            return query.Execute();
        }
    }
	
	/// <summary>
	/// Tests the interaction of db4o with multiple AppDomains
	/// </summary>
	public class CsAppDomains : AbstractDb4oTestCase, Db4oUnit.Extensions.Fixtures.IOptOutCS
	{
		// keep task objects alive to check for any identity problems
		ArrayList _tasks = new ArrayList();

		override protected void Store()
		{
			ADTask task = null;
			Store(task = new ADTask("task 1"));
			_tasks.Add(task);

			Store(task = new ADTask("task 2"));
			_tasks.Add(task);
		}
		
		public void TestRemoteDomain()
		{
			Fixture().Close();

			AppDomain domain = AppDomain.CreateDomain("db4o-remote-domain");
			try
			{
				using (TaskDatabase db = (TaskDatabase)domain.CreateInstanceAndUnwrap(typeof(TaskDatabase).Assembly.GetName().ToString(), typeof(TaskDatabase).FullName))
				{
					db.Open(CurrentFileName(), false);
				
					string[] taskNames = db.QueryTaskNames();
					Assert.AreEqual(2, taskNames.Length);
                    Assert.AreEqual("task 1", taskNames[0]);
                    Assert.AreEqual("task 2", taskNames[1]);

					ADTask[] tasks = db.QueryTasks();
                    Assert.AreEqual(2, tasks.Length);
                    Assert.AreEqual("task 1", tasks[0].Name);
                    Assert.AreEqual("task 2", tasks[1].Name);
				}
			}
			finally
			{
				AppDomain.Unload(domain);
				Fixture().Open(GetType());
			}
		}
	    
	    public string CurrentFileName()
		{
            return ((Db4oSolo) Fixture()).GetAbsolutePath();
		}
	}
#endif
}

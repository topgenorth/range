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
using Db4oUnit;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Tests.Common.TA;
using Db4objects.Db4o.Tests.Common.TA.Collections;
using Db4objects.Db4o.Tests.Common.TA.Hierarchy;

namespace Db4objects.Db4o.Tests.Common.TA.Hierarchy
{
	public class TransparentActivationTestCase : TransparentActivationTestCaseBase
	{
		public static void Main(string[] args)
		{
			new TransparentActivationTestCase().RunAll();
		}

		private const int Priority = 42;

		protected override void Configure(IConfiguration config)
		{
			base.Configure(config);
			config.Add(new PagedListSupport());
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			Project project = new PrioritizedProject("db4o", Priority);
			project.LogWorkDone(new UnitOfWork("ta kick-off", new DateTime(1000), new DateTime
				(2000)));
			Store(project);
		}

		public virtual void Test()
		{
			PrioritizedProject project = (PrioritizedProject)RetrieveOnlyInstance(typeof(Project
				));
			Assert.AreEqual(Priority, project.GetPriority());
			// Project.totalTimeSpent needs the UnitOfWork objects to be activated
			Assert.AreEqual(1000, project.TotalTimeSpent());
		}
	}
}

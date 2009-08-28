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
using Db4oUnit.Extensions;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Concurrency;

namespace Db4objects.Db4o.Tests.Common.Concurrency
{
	public class DeleteDeepTestCase : Db4oClientServerTestCase
	{
		public static void Main(string[] args)
		{
			new DeleteDeepTestCase().RunConcurrency();
		}

		public string name;

		public DeleteDeepTestCase child;

		protected override void Store()
		{
			AddNodes(10);
			name = "root";
			Store(this);
		}

		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(typeof(DeleteDeepTestCase)).CascadeOnDelete(true);
		}

		// config.objectClass(DeleteDeepTestCase.class).cascadeOnActivate(true);
		private void AddNodes(int count)
		{
			if (count > 0)
			{
				child = new DeleteDeepTestCase();
				child.name = string.Empty + count;
				child.AddNodes(count - 1);
			}
		}

		/// <exception cref="Exception"></exception>
		public virtual void Conc(IExtObjectContainer oc)
		{
			IQuery q = oc.Query();
			q.Constrain(typeof(DeleteDeepTestCase));
			q.Descend("name").Constrain("root");
			IObjectSet os = q.Execute();
			if (os.Size() == 0)
			{
				// already deleted
				return;
			}
			Assert.AreEqual(1, os.Size());
			if (!os.HasNext())
			{
				return;
			}
			DeleteDeepTestCase root = (DeleteDeepTestCase)os.Next();
			// wait for other threads
			// Thread.sleep(500);
			oc.Delete(root);
			oc.Commit();
			AssertOccurrences(oc, typeof(DeleteDeepTestCase), 0);
		}

		public virtual void Check(IExtObjectContainer oc)
		{
			AssertOccurrences(oc, typeof(DeleteDeepTestCase), 0);
		}
	}
}

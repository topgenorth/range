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
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Tests.Common.Assorted;

namespace Db4objects.Db4o.Tests.Common.Assorted
{
	public class SystemInfoTestCase : AbstractDb4oTestCase
	{
		public class Item
		{
		}

		public static void Main(string[] arguments)
		{
			new SystemInfoTestCase().RunSolo();
		}

		public virtual void TestDefaultFreespaceInfo()
		{
			AssertFreespaceInfo(FileSession().SystemInfo());
		}

		private void AssertFreespaceInfo(ISystemInfo info)
		{
			Assert.IsNotNull(info);
			SystemInfoTestCase.Item item = new SystemInfoTestCase.Item();
			Db().Store(item);
			Db().Commit();
			Db().Delete(item);
			Db().Commit();
			Assert.IsTrue(info.FreespaceEntryCount() > 0);
			Assert.IsTrue(info.FreespaceSize() > 0);
		}

		public virtual void TestTotalSize()
		{
			if (Fixture() is AbstractFileBasedDb4oFixture)
			{
				// assuming YapFile only
				AbstractFileBasedDb4oFixture fixture = (AbstractFileBasedDb4oFixture)Fixture();
				Sharpen.IO.File f = new Sharpen.IO.File(fixture.GetAbsolutePath());
				long expectedSize = f.Length();
				long actual = Db().SystemInfo().TotalSize();
				Assert.AreEqual(expectedSize, actual);
			}
		}
	}
}

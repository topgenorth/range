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
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Freespace;
using Db4objects.Db4o.Internal.Slots;
using Db4objects.Db4o.Tests.Common.Freespace;

namespace Db4objects.Db4o.Tests.Common.Freespace
{
	public class FreespaceManagerDiscardLimitTestCase : FreespaceManagerTestCaseBase
	{
		public static void Main(string[] args)
		{
			new FreespaceManagerDiscardLimitTestCase().RunSolo();
		}

		protected override void Configure(IConfiguration config)
		{
			config.Freespace().DiscardSmallerThan(10 * ((Config4Impl)config).BlockSize());
		}

		public virtual void TestGetSlot()
		{
			for (int i = 0; i < fm.Length; i++)
			{
				if (fm[i].SystemType() == AbstractFreespaceManager.FmIx)
				{
					continue;
				}
				fm[i].Free(new Slot(20, 15));
				Slot slot = fm[i].GetSlot(5);
				AssertSlot(new Slot(20, 5), slot);
				Assert.AreEqual(1, fm[i].SlotCount());
				fm[i].Free(slot);
				Assert.AreEqual(1, fm[i].SlotCount());
				slot = fm[i].GetSlot(6);
				AssertSlot(new Slot(20, 15), slot);
				Assert.AreEqual(0, fm[i].SlotCount());
				fm[i].Free(slot);
				Assert.AreEqual(1, fm[i].SlotCount());
				slot = fm[i].GetSlot(10);
				AssertSlot(new Slot(20, 15), slot);
				Assert.AreEqual(0, fm[i].SlotCount());
			}
		}
	}
}

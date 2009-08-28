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
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Freespace;
using Db4objects.Db4o.Internal.Slots;
using Db4objects.Db4o.Tests.Common.Freespace;

namespace Db4objects.Db4o.Tests.Common.Freespace
{
	public abstract class FreespaceManagerTestCaseBase : FileSizeTestCaseBase, IOptOutCS
	{
		protected IFreespaceManager[] fm;

		/// <exception cref="Exception"></exception>
		protected override void Db4oSetupAfterStore()
		{
			LocalObjectContainer container = (LocalObjectContainer)Db();
			BTreeFreespaceManager btreeFm = new BTreeFreespaceManager(container);
			btreeFm.Start(0);
			fm = new IFreespaceManager[] { new RamFreespaceManager(container), btreeFm };
		}

		protected virtual void Clear(IFreespaceManager freespaceManager)
		{
			Slot slot = null;
			do
			{
				slot = freespaceManager.GetSlot(1);
			}
			while (slot != null);
			Assert.AreEqual(0, freespaceManager.SlotCount());
			Assert.AreEqual(0, freespaceManager.TotalFreespace());
		}

		protected virtual void AssertSame(IFreespaceManager fm1, IFreespaceManager fm2)
		{
			Assert.AreEqual(fm1.SlotCount(), fm2.SlotCount());
			Assert.AreEqual(fm1.TotalFreespace(), fm2.TotalFreespace());
		}

		protected virtual void AssertSlot(Slot expected, Slot actual)
		{
			Assert.AreEqual(expected, actual);
		}

		protected virtual void ProduceSomeFreeSpace()
		{
			IFreespaceManager fm = CurrentFreespaceManager();
			int length = 300;
			Slot slot = LocalContainer().GetSlot(length);
			ByteArrayBuffer buffer = new ByteArrayBuffer(length);
			LocalContainer().WriteBytes(buffer, slot.Address(), 0);
			fm.Free(slot);
		}

		protected virtual IFreespaceManager CurrentFreespaceManager()
		{
			return LocalContainer().FreespaceManager();
		}

		public class Item
		{
			public int _int;
		}

		protected virtual void StoreSomeItems()
		{
			for (int i = 0; i < 3; i++)
			{
				Store(new FreespaceManagerTestCaseBase.Item());
			}
			Db().Commit();
		}

		protected virtual LocalObjectContainer LocalContainer()
		{
			return Fixture().FileSession();
		}
	}
}

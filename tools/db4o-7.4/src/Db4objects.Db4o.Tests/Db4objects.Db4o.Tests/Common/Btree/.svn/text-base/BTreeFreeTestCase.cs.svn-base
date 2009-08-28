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
using System.Collections;
using Db4oUnit;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Slots;
using Db4objects.Db4o.Tests.Common.Btree;

namespace Db4objects.Db4o.Tests.Common.Btree
{
	public class BTreeFreeTestCase : BTreeTestCaseBase
	{
		private static readonly int[] Values = new int[] { 1, 2, 5, 7, 8, 9, 12 };

		public static void Main(string[] args)
		{
			new BTreeFreeTestCase().RunSolo();
		}

		public virtual void Test()
		{
			Add(Values);
			IEnumerator allSlotIDs = _btree.AllNodeIds(SystemTrans());
			Collection4 allSlots = new Collection4();
			while (allSlotIDs.MoveNext())
			{
				int slotID = ((int)allSlotIDs.Current);
				Slot slot = GetSlotForID(slotID);
				allSlots.Add(slot);
			}
			Slot bTreeSlot = GetSlotForID(_btree.GetID());
			allSlots.Add(bTreeSlot);
			LocalObjectContainer container = (LocalObjectContainer)Stream();
			Collection4 freedSlots = new Collection4();
			container.InstallDebugFreespaceManager(new FreespaceManagerForDebug(container, new 
				_ISlotListener_43(freedSlots, container)));
			_btree.Free(SystemTrans());
			SystemTrans().Commit();
			Assert.IsTrue(freedSlots.ContainsAll(allSlots.GetEnumerator()));
		}

		private sealed class _ISlotListener_43 : ISlotListener
		{
			public _ISlotListener_43(Collection4 freedSlots, LocalObjectContainer container)
			{
				this.freedSlots = freedSlots;
				this.container = container;
			}

			public void OnFree(Slot slot)
			{
				freedSlots.Add(container.ToNonBlockedLength(slot));
			}

			private readonly Collection4 freedSlots;

			private readonly LocalObjectContainer container;
		}

		private Slot GetSlotForID(int slotID)
		{
			return FileTransaction().GetCurrentSlotOfID(slotID);
		}

		private LocalTransaction FileTransaction()
		{
			return ((LocalTransaction)Trans());
		}
	}
}

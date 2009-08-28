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
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Slots;

namespace Db4objects.Db4o.Internal
{
	/// <summary>TODO: Check if all time-consuming stuff is overridden!</summary>
	internal class TransactionObjectCarrier : LocalTransaction
	{
		internal TransactionObjectCarrier(ObjectContainerBase container, Db4objects.Db4o.Internal.Transaction
			 parentTransaction, TransactionalReferenceSystem referenceSystem) : base(container
			, parentTransaction, referenceSystem)
		{
		}

		public override void Commit()
		{
		}

		// do nothing
		public override void SlotFreeOnCommit(int id, Slot slot)
		{
		}

		//      do nothing
		public override void SlotFreeOnRollback(int id, Slot slot)
		{
		}

		//      do nothing
		internal override void ProduceUpdateSlotChange(int id, Slot slot)
		{
			SetPointer(id, slot);
		}

		internal override void SlotFreeOnRollbackCommitSetPointer(int id, Slot slot, bool
			 forFreespace)
		{
			SetPointer(id, slot);
		}

		internal override void SlotFreePointerOnCommit(int a_id, Slot slot)
		{
		}

		//      do nothing
		public override void SlotFreePointerOnCommit(int a_id)
		{
		}

		// do nothing
		public override void SetPointer(int a_id, Slot slot)
		{
			WritePointer(a_id, slot);
		}

		internal override bool SupportsVirtualFields()
		{
			return false;
		}
	}
}

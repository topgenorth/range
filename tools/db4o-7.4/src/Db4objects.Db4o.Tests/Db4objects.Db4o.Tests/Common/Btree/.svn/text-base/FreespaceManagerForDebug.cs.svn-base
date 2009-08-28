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
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Freespace;
using Db4objects.Db4o.Internal.Slots;
using Db4objects.Db4o.Tests.Common.Btree;

namespace Db4objects.Db4o.Tests.Common.Btree
{
	public class FreespaceManagerForDebug : AbstractFreespaceManager
	{
		private readonly ISlotListener _listener;

		public FreespaceManagerForDebug(LocalObjectContainer file, ISlotListener listener
			) : base(file)
		{
			_listener = listener;
		}

		public override Slot AllocateTransactionLogSlot(int length)
		{
			return null;
		}

		public override void FreeTransactionLogSlot(Slot slot)
		{
		}

		public override void BeginCommit()
		{
		}

		public override void Commit()
		{
		}

		public override void EndCommit()
		{
		}

		public override int SlotCount()
		{
			return 0;
		}

		public override void Free(Slot slot)
		{
			_listener.OnFree(slot);
		}

		public override void FreeSelf()
		{
		}

		public override Slot GetSlot(int length)
		{
			return null;
		}

		public override void Read(int freeSlotsID)
		{
		}

		public override void Start(int slotAddress)
		{
		}

		public override byte SystemType()
		{
			return FmDebug;
		}

		public override void Traverse(IVisitor4 visitor)
		{
		}

		public override int Write()
		{
			return 0;
		}
	}
}

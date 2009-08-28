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
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Freespace;
using Db4objects.Db4o.Internal.Slots;

namespace Db4objects.Db4o.Internal.Freespace
{
	/// <summary>Old freespacemanager, before version 7.0.</summary>
	/// <remarks>
	/// Old freespacemanager, before version 7.0.
	/// If it is still in use freespace is dropped.
	/// <see cref="BTreeFreespaceManager">BTreeFreespaceManager</see>
	/// should be used instead.
	/// </remarks>
	public class FreespaceManagerIx : Db4objects.Db4o.Internal.Freespace.AbstractFreespaceManager
	{
		public FreespaceManagerIx(LocalObjectContainer file) : base(file)
		{
		}

		public override Slot AllocateTransactionLogSlot(int length)
		{
			throw new InvalidOperationException();
		}

		public override void FreeTransactionLogSlot(Slot slot)
		{
			throw new InvalidOperationException();
		}

		public override void BeginCommit()
		{
		}

		public override void EndCommit()
		{
		}

		public override int SlotCount()
		{
			throw new InvalidOperationException();
		}

		public override void Free(Slot slot)
		{
		}

		// Should no longer be used: Should not happen.
		public override void FreeSelf()
		{
		}

		// do nothing, freespace is dropped.
		public override Slot GetSlot(int length)
		{
			// implementation is no longer present, no freespace returned.
			return null;
		}

		public override void MigrateTo(IFreespaceManager fm)
		{
		}

		// do nothing, freespace is dropped.
		public override void Traverse(IVisitor4 visitor)
		{
			throw new InvalidOperationException();
		}

		public override void Read(int freespaceID)
		{
		}

		public override void Start(int slotAddress)
		{
		}

		public override byte SystemType()
		{
			return FmIx;
		}

		public override int Write()
		{
			return 0;
		}

		public override void Commit()
		{
		}
	}
}

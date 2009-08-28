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
using System.IO;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Btree;
using Db4objects.Db4o.Internal.Mapping;
using Db4objects.Db4o.Internal.Slots;

namespace Db4objects.Db4o.Internal.Mapping
{
	/// <summary>Encapsulates services involving source and target database files during defragmenting.
	/// 	</summary>
	/// <remarks>Encapsulates services involving source and target database files during defragmenting.
	/// 	</remarks>
	/// <exclude></exclude>
	public interface IDefragmentServices : IIDMapping
	{
		/// <exception cref="IOException"></exception>
		ByteArrayBuffer SourceBufferByAddress(int address, int length);

		/// <exception cref="IOException"></exception>
		ByteArrayBuffer TargetBufferByAddress(int address, int length);

		ByteArrayBuffer SourceBufferByID(int sourceID);

		Slot AllocateTargetSlot(int targetLength);

		void TargetWriteBytes(ByteArrayBuffer targetPointerReader, int targetAddress);

		Transaction SystemTrans();

		void TargetWriteBytes(DefragmentContextImpl context, int targetAddress);

		void TraverseAllIndexSlots(BTree tree, IVisitor4 visitor4);

		ClassMetadata ClassMetadataForId(int id);

		int MappedID(int id, bool lenient);

		void RegisterUnindexed(int id);

		IdSource UnindexedIDs();

		int SourceAddressByID(int sourceID);
	}
}

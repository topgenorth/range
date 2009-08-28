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
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Marshall;
using Db4objects.Db4o.Internal.Slots;
using Db4objects.Db4o.Marshall;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Internal
{
	public interface IDefragmentContext : IBufferContext, IMarshallingInfo, IHandlerVersionContext
	{
		ITypeHandler4 TypeHandlerForId(int id);

		int CopyID();

		int CopyIDReturnOriginalID();

		int CopySlotlessID();

		int CopyUnindexedID();

		void Defragment(ITypeHandler4 handler);

		int HandlerVersion();

		void IncrementOffset(int length);

		bool IsLegacyHandlerVersion();

		int MappedID(int origID);

		ByteArrayBuffer SourceBuffer();

		ByteArrayBuffer TargetBuffer();

		Slot AllocateTargetSlot(int length);

		Slot AllocateMappedTargetSlot(int sourceAddress, int length);

		/// <exception cref="IOException"></exception>
		int CopySlotToNewMapped(int sourceAddress, int length);

		/// <exception cref="IOException"></exception>
		ByteArrayBuffer SourceBufferByAddress(int sourceAddress, int length);

		/// <exception cref="IOException"></exception>
		ByteArrayBuffer SourceBufferById(int sourceId);

		void TargetWriteBytes(int address, ByteArrayBuffer buffer);
	}
}

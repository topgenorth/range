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
using Db4objects.Db4o;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Delete;
using Db4objects.Db4o.Internal.Handlers;
using Db4objects.Db4o.Internal.Marshall;
using Db4objects.Db4o.Marshall;

namespace Db4objects.Db4o.Internal.Handlers
{
	/// <exclude></exclude>
	public class StringHandler0 : StringHandler
	{
		public override object Read(IReadContext context)
		{
			ByteArrayBuffer buffer = (ByteArrayBuffer)((IInternalReadContext)context).ReadIndirectedBuffer
				();
			if (buffer == null)
			{
				return null;
			}
			return ReadString(context, buffer);
		}

		public override void Delete(IDeleteContext context)
		{
			context.DefragmentRecommended();
		}

		public override void Defragment(IDefragmentContext context)
		{
			int sourceAddress = context.SourceBuffer().ReadInt();
			int length = context.SourceBuffer().ReadInt();
			if (sourceAddress == 0 && length == 0)
			{
				context.TargetBuffer().WriteInt(0);
				context.TargetBuffer().WriteInt(0);
				return;
			}
			int targetAddress = 0;
			try
			{
				targetAddress = context.CopySlotToNewMapped(sourceAddress, length);
			}
			catch (IOException exc)
			{
				throw new Db4oIOException(exc);
			}
			context.TargetBuffer().WriteInt(targetAddress);
			context.TargetBuffer().WriteInt(length);
		}

		/// <exception cref="CorruptionException"></exception>
		/// <exception cref="Db4oIOException"></exception>
		public override object ReadIndexEntryFromObjectSlot(MarshallerFamily mf, StatefulBuffer
			 buffer)
		{
			return buffer.Container().ReadWriterByAddress(buffer.Transaction(), buffer.ReadInt
				(), buffer.ReadInt());
		}

		/// <exception cref="CorruptionException"></exception>
		/// <exception cref="Db4oIOException"></exception>
		public override object ReadIndexEntry(IObjectIdContext context)
		{
			return context.Transaction().Container().ReadWriterByAddress(context.Transaction(
				), context.ReadInt(), context.ReadInt());
		}
	}
}

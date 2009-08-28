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
using Db4objects.Db4o.Internal.Slots;

namespace Db4objects.Db4o.Internal.Classindex
{
	/// <summary>representation to collect and hold all IDs of one class</summary>
	public class ClassIndex : PersistentBase, IReadWriteable
	{
		private readonly ClassMetadata _clazz;

		/// <summary>contains TreeInt with object IDs</summary>
		private TreeInt i_root;

		internal ClassIndex(ClassMetadata yapClass)
		{
			_clazz = yapClass;
		}

		public virtual void Add(int a_id)
		{
			i_root = TreeInt.Add(i_root, a_id);
		}

		public int MarshalledLength()
		{
			return Const4.IntLength * (Tree.Size(i_root) + 1);
		}

		public void Clear()
		{
			i_root = null;
		}

		internal virtual void EnsureActive(Transaction trans)
		{
			if (!IsActive())
			{
				SetStateDirty();
				Read(trans);
			}
		}

		internal virtual int EntryCount(Transaction ta)
		{
			if (IsActive() || IsNew())
			{
				return Tree.Size(i_root);
			}
			Slot slot = ((LocalTransaction)ta).GetCurrentSlotOfID(GetID());
			int length = Const4.IntLength;
			ByteArrayBuffer reader = new ByteArrayBuffer(length);
			reader.ReadEncrypt(ta.Container(), slot.Address());
			return reader.ReadInt();
		}

		public sealed override byte GetIdentifier()
		{
			return Const4.Yapindex;
		}

		internal virtual TreeInt GetRoot()
		{
			return i_root;
		}

		public sealed override int OwnLength()
		{
			return Const4.ObjectLength + MarshalledLength();
		}

		public object Read(ByteArrayBuffer a_reader)
		{
			throw Exceptions4.VirtualException();
		}

		public sealed override void ReadThis(Transaction a_trans, ByteArrayBuffer a_reader
			)
		{
			i_root = (TreeInt)new TreeReader(a_reader, new TreeInt(0)).Read();
		}

		public virtual void Remove(int a_id)
		{
			i_root = TreeInt.RemoveLike(i_root, a_id);
		}

		internal virtual void SetDirty(ObjectContainerBase a_stream)
		{
			// TODO: get rid of the setDirty call
			a_stream.SetDirtyInSystemTransaction(this);
		}

		public virtual void Write(ByteArrayBuffer a_writer)
		{
			WriteThis(null, a_writer);
		}

		public sealed override void WriteThis(Transaction trans, ByteArrayBuffer a_writer
			)
		{
			TreeInt.Write(a_writer, i_root);
		}

		public override string ToString()
		{
			return _clazz + " index";
		}
	}
}

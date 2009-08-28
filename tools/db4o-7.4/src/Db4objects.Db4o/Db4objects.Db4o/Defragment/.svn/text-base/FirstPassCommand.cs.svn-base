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
using Db4objects.Db4o;
using Db4objects.Db4o.Defragment;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Btree;
using Db4objects.Db4o.Internal.Slots;

namespace Db4objects.Db4o.Defragment
{
	/// <summary>
	/// First step in the defragmenting process: Allocates pointer slots in the target file for
	/// each ID (but doesn't fill them in, yet) and registers the mapping from source pointer address
	/// to target pointer address.
	/// </summary>
	/// <remarks>
	/// First step in the defragmenting process: Allocates pointer slots in the target file for
	/// each ID (but doesn't fill them in, yet) and registers the mapping from source pointer address
	/// to target pointer address.
	/// </remarks>
	/// <exclude></exclude>
	internal sealed class FirstPassCommand : IPassCommand
	{
		private const int IdBatchSize = 4096;

		private TreeInt _ids;

		internal void Process(DefragmentServicesImpl context, int objectID, bool isClassID
			)
		{
			if (BatchFull())
			{
				Flush(context);
			}
			_ids = TreeInt.Add(_ids, (isClassID ? -objectID : objectID));
		}

		private bool BatchFull()
		{
			return _ids != null && _ids.Size() == IdBatchSize;
		}

		public void ProcessClass(DefragmentServicesImpl context, ClassMetadata classMetadata
			, int id, int classIndexID)
		{
			Process(context, id, true);
			classMetadata.ForEachField(new _IProcedure4_36(this, context));
		}

		private sealed class _IProcedure4_36 : IProcedure4
		{
			public _IProcedure4_36(FirstPassCommand _enclosing, DefragmentServicesImpl context
				)
			{
				this._enclosing = _enclosing;
				this.context = context;
			}

			public void Apply(object arg)
			{
				FieldMetadata field = (FieldMetadata)arg;
				if (!field.IsVirtual() && field.HasIndex())
				{
					this._enclosing.ProcessBTree(context, field.GetIndex(context.SystemTrans()));
				}
			}

			private readonly FirstPassCommand _enclosing;

			private readonly DefragmentServicesImpl context;
		}

		public void ProcessObjectSlot(DefragmentServicesImpl context, ClassMetadata yapClass
			, int sourceID)
		{
			Process(context, sourceID, false);
		}

		/// <exception cref="CorruptionException"></exception>
		public void ProcessClassCollection(DefragmentServicesImpl context)
		{
			Process(context, context.SourceClassCollectionID(), false);
		}

		public void ProcessBTree(DefragmentServicesImpl context, BTree btree)
		{
			Process(context, btree.GetID(), false);
			context.TraverseAllIndexSlots(btree, new _IVisitor4_56(this, context));
		}

		private sealed class _IVisitor4_56 : IVisitor4
		{
			public _IVisitor4_56(FirstPassCommand _enclosing, DefragmentServicesImpl context)
			{
				this._enclosing = _enclosing;
				this.context = context;
			}

			public void Visit(object obj)
			{
				int id = ((int)obj);
				this._enclosing.Process(context, id, false);
			}

			private readonly FirstPassCommand _enclosing;

			private readonly DefragmentServicesImpl context;
		}

		public void Flush(DefragmentServicesImpl context)
		{
			if (_ids == null)
			{
				return;
			}
			int blockSize = context.BlockSize();
			bool overlapping = (Const4.PointerLength % blockSize > 0);
			int blocksPerPointer = Const4.PointerLength / blockSize;
			if (overlapping)
			{
				blocksPerPointer++;
			}
			int bytesPerPointer = blocksPerPointer * blockSize;
			int batchSize = _ids.Size() * bytesPerPointer;
			Slot pointerSlot = context.AllocateTargetSlot(batchSize);
			int pointerAddress = pointerSlot.Address();
			IEnumerator idIter = new TreeKeyIterator(_ids);
			while (idIter.MoveNext())
			{
				int objectID = ((int)idIter.Current);
				bool isClassID = false;
				if (objectID < 0)
				{
					objectID = -objectID;
					isClassID = true;
				}
				// seen object ids don't come by here anymore - any other candidates?
				context.MapIDs(objectID, pointerAddress, isClassID);
				pointerAddress += blocksPerPointer;
			}
			_ids = null;
		}
	}
}

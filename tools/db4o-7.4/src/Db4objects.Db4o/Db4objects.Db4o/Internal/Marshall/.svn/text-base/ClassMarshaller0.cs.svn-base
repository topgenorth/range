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
using Db4objects.Db4o.Internal.Btree;
using Db4objects.Db4o.Internal.Classindex;
using Db4objects.Db4o.Internal.Convert.Conversions;
using Db4objects.Db4o.Internal.Marshall;

namespace Db4objects.Db4o.Internal.Marshall
{
	/// <exclude></exclude>
	public class ClassMarshaller0 : ClassMarshaller
	{
		protected override void ReadIndex(ObjectContainerBase stream, ClassMetadata clazz
			, ByteArrayBuffer reader)
		{
			int indexID = reader.ReadInt();
			if (!stream.MaintainsIndices() || !(stream is LocalObjectContainer))
			{
				return;
			}
			if (Btree(clazz) != null)
			{
				return;
			}
			clazz.Index().Read(stream, ValidIndexId(indexID));
			if (IsOldClassIndex(indexID))
			{
				new ClassIndexesToBTrees_5_5().Convert((LocalObjectContainer)stream, indexID, Btree
					(clazz));
				stream.SetDirtyInSystemTransaction(clazz);
			}
		}

		private BTree Btree(ClassMetadata clazz)
		{
			return BTreeClassIndexStrategy.Btree(clazz);
		}

		private int ValidIndexId(int indexID)
		{
			return IsOldClassIndex(indexID) ? 0 : -indexID;
		}

		private bool IsOldClassIndex(int indexID)
		{
			return indexID > 0;
		}

		protected override int IndexIDForWriting(int indexID)
		{
			return indexID;
		}
	}
}

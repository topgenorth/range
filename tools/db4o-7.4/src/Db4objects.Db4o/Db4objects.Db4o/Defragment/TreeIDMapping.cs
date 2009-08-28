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
using Db4objects.Db4o.Defragment;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;

namespace Db4objects.Db4o.Defragment
{
	/// <summary>In-memory mapping for IDs during a defragmentation run.</summary>
	/// <remarks>In-memory mapping for IDs during a defragmentation run.</remarks>
	/// <seealso cref="Db4objects.Db4o.Defragment.Defragment">Db4objects.Db4o.Defragment.Defragment
	/// 	</seealso>
	public class TreeIDMapping : AbstractContextIDMapping
	{
		private Tree _tree;

		public override int MappedID(int oldID, bool lenient)
		{
			int classID = MappedClassID(oldID);
			if (classID != 0)
			{
				return classID;
			}
			TreeIntObject res = (TreeIntObject)TreeInt.Find(_tree, oldID);
			if (res != null)
			{
				return ((int)res._object);
			}
			if (lenient)
			{
				TreeIntObject nextSmaller = (TreeIntObject)Tree.FindSmaller(_tree, new TreeInt(oldID
					));
				if (nextSmaller != null)
				{
					int baseOldID = nextSmaller._key;
					int baseNewID = ((int)nextSmaller._object);
					return baseNewID + oldID - baseOldID;
				}
			}
			return 0;
		}

		public override void Open()
		{
		}

		public override void Close()
		{
		}

		protected override void MapNonClassIDs(int origID, int mappedID)
		{
			_tree = Tree.Add(_tree, new TreeIntObject(origID, mappedID));
		}
	}
}

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
namespace Db4objects.Db4o.Defragment
{
	/// <summary>The ID mapping used internally during a defragmentation run.</summary>
	/// <remarks>The ID mapping used internally during a defragmentation run.</remarks>
	/// <seealso cref="Db4objects.Db4o.Defragment.Defragment">Db4objects.Db4o.Defragment.Defragment
	/// 	</seealso>
	public interface IContextIDMapping
	{
		/// <summary>Returns a previously registered mapping ID for the given ID if it exists.
		/// 	</summary>
		/// <remarks>
		/// Returns a previously registered mapping ID for the given ID if it exists.
		/// If lenient mode is set to true, will provide the mapping ID for the next
		/// smaller original ID a mapping exists for. Otherwise returns 0.
		/// </remarks>
		/// <param name="origID">The original ID</param>
		/// <param name="lenient">If true, lenient mode will be used for lookup, strict mode otherwise.
		/// 	</param>
		/// <returns>The mapping ID for the given original ID or 0, if none has been registered.
		/// 	</returns>
		int MappedID(int origID, bool lenient);

		/// <summary>Registers a mapping for the given IDs.</summary>
		/// <remarks>Registers a mapping for the given IDs.</remarks>
		/// <param name="origID">The original ID</param>
		/// <param name="mappedID">The ID to be mapped to the original ID.</param>
		/// <param name="isClassID">true if the given original ID specifies a class slot, false otherwise.
		/// 	</param>
		void MapIDs(int origID, int mappedID, bool isClassID);

		/// <summary>Prepares the mapping for use.</summary>
		/// <remarks>Prepares the mapping for use.</remarks>
		void Open();

		/// <summary>Shuts down the mapping after use.</summary>
		/// <remarks>Shuts down the mapping after use.</remarks>
		void Close();
	}
}

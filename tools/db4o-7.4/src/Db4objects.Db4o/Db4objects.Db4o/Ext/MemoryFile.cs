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
using Db4objects.Db4o.Ext;

namespace Db4objects.Db4o.Ext
{
	/// <summary>carries in-memory data for db4o in-memory operation.</summary>
	/// <remarks>
	/// carries in-memory data for db4o in-memory operation.
	/// <br/><br/>In-memory ObjectContainers are useful for maximum performance
	/// on small databases, for swapping objects or for storing db4o format data
	/// to other media or other databases.<br/><br/>Be aware of the danger of running
	/// into OutOfMemory problems or complete loss of all data, in case of hardware
	/// or runtime failures.
	/// <br/><br/>
	/// 
	/// </remarks>
	/// <seealso cref="ExtDb4oFactory.OpenMemoryFile">ExtDb4oFactory.OpenMemoryFile</seealso>
	public class MemoryFile
	{
		private byte[] i_bytes;

		private const int InitialSizeAndInc = 1024 * 64;

		private int i_initialSize = InitialSizeAndInc;

		private int i_incrementSizeBy = InitialSizeAndInc;

		/// <summary>constructs a new MemoryFile without any data.</summary>
		/// <remarks>constructs a new MemoryFile without any data.</remarks>
		/// <seealso cref="ExtDb4oFactory.OpenMemoryFile">ExtDb4oFactory.OpenMemoryFile</seealso>
		public MemoryFile()
		{
		}

		/// <summary>
		/// constructs a MemoryFile to use the byte data from a previous
		/// MemoryFile.
		/// </summary>
		/// <remarks>
		/// constructs a MemoryFile to use the byte data from a previous
		/// MemoryFile.
		/// </remarks>
		/// <param name="bytes">the raw byte data.</param>
		/// <seealso cref="ExtDb4oFactory.OpenMemoryFile">ExtDb4oFactory.OpenMemoryFile</seealso>
		public MemoryFile(byte[] bytes)
		{
			i_bytes = bytes;
		}

		/// <summary>returns the raw byte data.</summary>
		/// <remarks>
		/// returns the raw byte data.
		/// <br /><br />Use this method to get the byte data from the MemoryFile
		/// to store it to other media or databases, for backup purposes or
		/// to create other MemoryFile sessions.
		/// <br /><br />The byte data from a MemoryFile should only be used
		/// after it is closed.<br /><br />
		/// </remarks>
		/// <returns>bytes the raw byte data.</returns>
		public virtual byte[] GetBytes()
		{
			if (i_bytes == null)
			{
				return new byte[0];
			}
			return i_bytes;
		}

		/// <summary>
		/// returns the size the MemoryFile is to be enlarged, if it grows beyond
		/// the current size.
		/// </summary>
		/// <remarks>
		/// returns the size the MemoryFile is to be enlarged, if it grows beyond
		/// the current size.
		/// </remarks>
		/// <returns>size in bytes</returns>
		public virtual int GetIncrementSizeBy()
		{
			return i_incrementSizeBy;
		}

		/// <summary>returns the initial size of the MemoryFile.</summary>
		/// <remarks>returns the initial size of the MemoryFile.</remarks>
		/// <returns>size in bytes</returns>
		public virtual int GetInitialSize()
		{
			return i_initialSize;
		}

		/// <summary>sets the raw byte data.</summary>
		/// <remarks>
		/// sets the raw byte data.
		/// <br /><br /><b>Caution!</b><br />Calling this method during a running
		/// Memory File session may produce unpreditable results.
		/// </remarks>
		/// <param name="bytes">the raw byte data.</param>
		public virtual void SetBytes(byte[] bytes)
		{
			i_bytes = bytes;
		}

		/// <summary>
		/// configures the size the MemoryFile is to be enlarged by, if it grows
		/// beyond the current size.
		/// </summary>
		/// <remarks>
		/// configures the size the MemoryFile is to be enlarged by, if it grows
		/// beyond the current size.
		/// <br /><br />Call this method before passing the MemoryFile to
		/// <see cref="ExtDb4oFactory.OpenMemoryFile">ExtDb4oFactory.OpenMemoryFile</see>
		/// .
		/// <br /><br />
		/// This parameter can be modified to tune the maximum performance of
		/// a MemoryFile for a specific usecase. To produce the best results,
		/// test the speed of your application with real data.<br /><br />
		/// </remarks>
		/// <param name="byteCount">the desired size in bytes</param>
		public virtual void SetIncrementSizeBy(int byteCount)
		{
			i_incrementSizeBy = byteCount;
		}

		/// <summary>configures the initial size of the MemoryFile.</summary>
		/// <remarks>
		/// configures the initial size of the MemoryFile.
		/// <br /><br />Call this method before passing the MemoryFile to
		/// <see cref="ExtDb4oFactory.OpenMemoryFile">ExtDb4oFactory.OpenMemoryFile</see>
		/// .
		/// <br /><br />
		/// This parameter can be modified to tune the maximum performance of
		/// a MemoryFile for a specific usecase. To produce the best results,
		/// test speed and memory consumption of your application with
		/// real data.<br /><br />
		/// </remarks>
		/// <param name="byteCount">the desired size in bytes</param>
		public virtual void SetInitialSize(int byteCount)
		{
			i_initialSize = byteCount;
		}
	}
}

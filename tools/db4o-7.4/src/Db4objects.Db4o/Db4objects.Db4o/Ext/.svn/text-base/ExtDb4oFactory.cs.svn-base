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
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;

namespace Db4objects.Db4o.Ext
{
	/// <summary>extended factory class with static methods to open special db4o sessions.
	/// 	</summary>
	/// <remarks>extended factory class with static methods to open special db4o sessions.
	/// 	</remarks>
	public class ExtDb4oFactory : Db4oFactory
	{
		/// <summary>
		/// Operates just like
		/// <see cref="ExtDb4oFactory.OpenMemoryFile">ExtDb4oFactory.OpenMemoryFile</see>
		/// , but uses
		/// the global db4o
		/// <see cref="IConfiguration">IConfiguration</see>
		/// context.
		/// opens an
		/// <see cref="IObjectContainer">IObjectContainer</see>
		/// for in-memory use .
		/// <br/><br/>In-memory ObjectContainers are useful for maximum performance
		/// on small databases, for swapping objects or for storing db4o format data
		/// to other media or other databases.<br/><br/>Be aware of the danger of running
		/// into OutOfMemory problems or complete loss of all data, in case of hardware
		/// or software failures.<br/><br/>
		/// 
		/// </summary>
		/// <param name="memoryFile">
		/// a
		/// <see cref="MemoryFile">MemoryFile</see>
		/// 
		/// to store the raw byte data.
		/// 
		/// </param>
		/// <returns>
		/// an open
		/// <see cref="IObjectContainer">IObjectContainer</see>
		/// 
		/// </returns>
		/// <seealso cref="MemoryFile">MemoryFile</seealso>
		public static IObjectContainer OpenMemoryFile(MemoryFile memoryFile)
		{
			return OpenMemoryFile1(Db4oFactory.NewConfiguration(), memoryFile);
		}

		/// <summary>
		/// opens an
		/// <see cref="IObjectContainer">IObjectContainer</see>
		/// for in-memory use .
		/// <br/><br/>In-memory ObjectContainers are useful for maximum performance
		/// on small databases, for swapping objects or for storing db4o format data
		/// to other media or other databases.<br/><br/>Be aware of the danger of running
		/// into OutOfMemory problems or complete loss of all data, in case of hardware
		/// or software failures.<br/><br/>
		/// 
		/// </summary>
		/// <param name="config">
		/// a custom
		/// <see cref="IConfiguration">IConfiguration</see>
		/// instance to be obtained via
		/// <see cref="Db4oFactory.NewConfiguration">Db4oFactory.NewConfiguration</see>
		/// 
		/// </param>
		/// <param name="memoryFile">
		/// a
		/// <see cref="MemoryFile">MemoryFile</see>
		/// 
		/// to store the raw byte data.
		/// 
		/// </param>
		/// <returns>
		/// an open
		/// <see cref="IObjectContainer">IObjectContainer</see>
		/// 
		/// </returns>
		/// <seealso cref="MemoryFile">MemoryFile</seealso>
		public static IObjectContainer OpenMemoryFile(IConfiguration config, MemoryFile memoryFile
			)
		{
			return OpenMemoryFile1(config, memoryFile);
		}
	}
}

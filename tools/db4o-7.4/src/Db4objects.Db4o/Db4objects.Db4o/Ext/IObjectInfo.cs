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
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;

namespace Db4objects.Db4o.Ext
{
	/// <summary>
	/// interface to the internal reference that an ObjectContainer
	/// holds for a stored object.
	/// </summary>
	/// <remarks>
	/// interface to the internal reference that an ObjectContainer
	/// holds for a stored object.
	/// </remarks>
	public interface IObjectInfo
	{
		/// <summary>returns the internal db4o ID.</summary>
		/// <remarks>returns the internal db4o ID.</remarks>
		long GetInternalID();

		/// <summary>returns the object that is referenced.</summary>
		/// <remarks>
		/// returns the object that is referenced.
		/// <br /><br />This method may return null, if the object has
		/// been garbage collected.
		/// </remarks>
		/// <returns>
		/// the referenced object or null, if the object has
		/// been garbage collected.
		/// </returns>
		object GetObject();

		/// <summary>returns a UUID representation of the referenced object.</summary>
		/// <remarks>
		/// returns a UUID representation of the referenced object.
		/// UUID generation has to be turned on, in order to be able
		/// to use this feature:
		/// <see cref="IConfiguration.GenerateUUIDs">IConfiguration.GenerateUUIDs</see>
		/// </remarks>
		/// <returns>the UUID of the referenced object.</returns>
		Db4oUUID GetUUID();

		/// <summary>
		/// returns the transaction serial number ("version") the
		/// referenced object was stored with last.
		/// </summary>
		/// <remarks>
		/// returns the transaction serial number ("version") the
		/// referenced object was stored with last.
		/// Version number generation has to be turned on, in order to
		/// be able to use this feature:
		/// <see cref="IConfiguration.GenerateVersionNumbers">IConfiguration.GenerateVersionNumbers
		/// 	</see>
		/// </remarks>
		/// <returns>the version number.</returns>
		long GetVersion();
	}
}

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
using System;
using Db4objects.Db4o.Config;

namespace Db4objects.Db4o.Config
{
	/// <summary>
	/// a simple Alias for a single Class or Type, using #equals() on
	/// the names in the resolve method.
	/// </summary>
	/// <remarks>
	/// a simple Alias for a single Class or Type, using #equals() on
	/// the names in the resolve method.
	/// <br /><br />See
	/// <see cref="IAlias">IAlias</see>
	/// for concrete examples.
	/// </remarks>
	public class TypeAlias : IAlias
	{
		private readonly string _storedType;

		private readonly string _runtimeType;

		/// <summary>
		/// pass the stored name as the first
		/// parameter and the desired runtime name as the second parameter.
		/// </summary>
		/// <remarks>
		/// pass the stored name as the first
		/// parameter and the desired runtime name as the second parameter.
		/// </remarks>
		public TypeAlias(string storedType, string runtimeType)
		{
			if (null == storedType || null == runtimeType)
			{
				throw new ArgumentException();
			}
			_storedType = storedType;
			_runtimeType = runtimeType;
		}

		/// <summary>returns the stored type name if the alias was written for the passed runtime type name
		/// 	</summary>
		public virtual string ResolveRuntimeName(string runtimeTypeName)
		{
			return _runtimeType.Equals(runtimeTypeName) ? _storedType : null;
		}

		/// <summary>returns the runtime type name if the alias was written for the passed stored type name
		/// 	</summary>
		public virtual string ResolveStoredName(string storedTypeName)
		{
			return _storedType.Equals(storedTypeName) ? _runtimeType : null;
		}
	}
}

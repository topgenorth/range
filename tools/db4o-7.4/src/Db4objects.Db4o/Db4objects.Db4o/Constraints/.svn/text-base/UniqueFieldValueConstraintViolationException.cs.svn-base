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
using Db4objects.Db4o.Constraints;

namespace Db4objects.Db4o.Constraints
{
	/// <summary>
	/// db4o-specific exception.<br /><br />
	/// This exception can be thrown by a
	/// <see cref="UniqueFieldValueConstraint">UniqueFieldValueConstraint</see>
	/// on commit.
	/// </summary>
	/// <seealso cref="IObjectField.Indexed">IObjectField.Indexed</seealso>
	/// <seealso cref="IConfiguration.Add">IConfiguration.Add</seealso>
	[System.Serializable]
	public class UniqueFieldValueConstraintViolationException : ConstraintViolationException
	{
		/// <summary>
		/// Constructor with a message composed from the class and field
		/// name of the entity causing the exception.
		/// </summary>
		/// <remarks>
		/// Constructor with a message composed from the class and field
		/// name of the entity causing the exception.
		/// </remarks>
		/// <param name="className">class, which caused the exception</param>
		/// <param name="fieldName">field, which caused the exception</param>
		public UniqueFieldValueConstraintViolationException(string className, string fieldName
			) : base("class: " + className + " field: " + fieldName)
		{
		}
	}
}

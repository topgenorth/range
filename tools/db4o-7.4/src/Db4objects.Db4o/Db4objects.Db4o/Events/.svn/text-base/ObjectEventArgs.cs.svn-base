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
using Db4objects.Db4o.Events;
using Db4objects.Db4o.Internal;

namespace Db4objects.Db4o.Events
{
	/// <summary>Arguments for object related events.</summary>
	/// <remarks>Arguments for object related events.</remarks>
	/// <seealso cref="IEventRegistry">IEventRegistry</seealso>
	public class ObjectEventArgs : TransactionalEventArgs
	{
		private object _obj;

		/// <summary>Creates a new instance for the specified object.</summary>
		/// <remarks>Creates a new instance for the specified object.</remarks>
		public ObjectEventArgs(Transaction transaction, object obj) : base(transaction)
		{
			_obj = obj;
		}

		/// <summary>The object that triggered this event.</summary>
		/// <remarks>The object that triggered this event.</remarks>
		public virtual object Object
		{
			get
			{
				return _obj;
			}
		}
	}
}

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
using Db4objects.Db4o;
using Db4objects.Db4o.Ext;

namespace Db4objects.Db4o.Internal
{
	/// <exclude></exclude>
	public class Exceptions4
	{
		public static void ThrowRuntimeException(int code)
		{
			ThrowRuntimeException(code, null, null);
		}

		public static void ThrowRuntimeException(int code, Exception cause)
		{
			ThrowRuntimeException(code, null, cause);
		}

		public static void ThrowRuntimeException(int code, string msg)
		{
			ThrowRuntimeException(code, msg, null);
		}

		public static void ThrowRuntimeException(int code, string msg, Exception cause)
		{
			ThrowRuntimeException(code, msg, cause, true);
		}

		[System.ObsoleteAttribute]
		public static void ThrowRuntimeException(int code, string msg, Exception cause, bool
			 doLog)
		{
			if (doLog)
			{
				Db4objects.Db4o.Internal.Messages.LogErr(Db4oFactory.Configure(), code, msg, cause
					);
			}
			throw new Db4oException(Db4objects.Db4o.Internal.Messages.Get(code, msg));
		}

		[System.ObsoleteAttribute(@"Use com.db4o.foundation.NotSupportedException instead"
			)]
		public static void NotSupported()
		{
			ThrowRuntimeException(53);
		}

		/// <exception cref="Db4oException"></exception>
		public static void CatchAllExceptDb4oException(Exception exc)
		{
			if (exc is Db4oException)
			{
				throw (Db4oException)exc;
			}
		}

		public static Exception ShouldNeverBeCalled()
		{
			throw new Exception();
		}

		public static void ShouldNeverHappen()
		{
			throw new Exception();
		}

		public static Exception VirtualException()
		{
			throw new Exception();
		}
	}
}

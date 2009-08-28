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
using System.Reflection;

namespace Sharpen.Lang
{
	public class IdentityHashCodeProvider
	{
#if !CF
		public static int IdentityHashCode(object obj)
		{
			return System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
		}
#else
		public static int IdentityHashCode(object obj)
		{
			if (obj == null) return 0;
			return (int) _hashMethod.Invoke(null, new object[] { obj });
		}

		private static MethodInfo _hashMethod = GetIdentityHashCodeMethod();

		private static MethodInfo GetIdentityHashCodeMethod()
		{
			Assembly assembly = typeof(object).Assembly;
			try
			{
				Type t = assembly.GetType("System.PInvoke.EE");
				return t.GetMethod(
					"Object_GetHashCode",
					BindingFlags.Public |
					BindingFlags.NonPublic |
					BindingFlags.Static);
			}
			catch (Exception e)
			{
			}
			// We may be running the CF app on .NET Framework 1.1
			// for profiling, let's give that a chance
			try
			{
				Type t = assembly.GetType(
					"System.Runtime.CompilerServices.RuntimeHelpers");
				return t.GetMethod(
					"GetHashCode",
					BindingFlags.Public |
					BindingFlags.Static);
			}
			catch
			{
			}
			return null;
		}
#endif
	}
}
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
using Db4oUnit;
using Db4oUnit.Mocking;
using Db4objects.Db4o.Foundation;

namespace Db4oUnit.Mocking
{
	public class MethodCall
	{
		private sealed class _object_9 : object
		{
			public _object_9()
			{
			}

			public override string ToString()
			{
				return "...";
			}
		}

		public static readonly object IgnoredArgument = new _object_9();

		public interface IArgumentCondition
		{
			void Verify(object argument);
		}

		public readonly string methodName;

		public readonly object[] args;

		public MethodCall(string methodName) : this(methodName, new object[0])
		{
		}

		public MethodCall(string methodName, object[] args)
		{
			this.methodName = methodName;
			this.args = args;
		}

		public MethodCall(string methodName, object singleArg) : this(methodName, new object
			[] { singleArg })
		{
		}

		public MethodCall(string methodName, object arg1, object arg2) : this(methodName, 
			new object[] { arg1, arg2 })
		{
		}

		public override string ToString()
		{
			return methodName + "(" + Iterators.Join(Iterators.Iterate(args), ", ") + ")";
		}

		public override bool Equals(object obj)
		{
			if (null == obj)
			{
				return false;
			}
			if (GetType() != obj.GetType())
			{
				return false;
			}
			MethodCall other = (MethodCall)obj;
			if (!methodName.Equals(other.methodName))
			{
				return false;
			}
			if (args.Length != other.args.Length)
			{
				return false;
			}
			for (int i = 0; i < args.Length; ++i)
			{
				object expectedArg = args[i];
				if (expectedArg == IgnoredArgument)
				{
					continue;
				}
				object actualArg = other.args[i];
				if (expectedArg is MethodCall.IArgumentCondition)
				{
					((MethodCall.IArgumentCondition)expectedArg).Verify(actualArg);
					continue;
				}
				if (!Check.ObjectsAreEqual(expectedArg, actualArg))
				{
					return false;
				}
			}
			return true;
		}
	}
}

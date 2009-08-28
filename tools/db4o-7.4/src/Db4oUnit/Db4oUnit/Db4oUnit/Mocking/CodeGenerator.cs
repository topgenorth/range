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
using System.Collections;
using Db4oUnit;
using Db4oUnit.Mocking;
using Db4objects.Db4o.Foundation;

namespace Db4oUnit.Mocking
{
	public class CodeGenerator
	{
		/// <summary>
		/// Generates an array that can be used with
		/// <see cref="MethodCallRecorder.Verify">MethodCallRecorder.Verify</see>
		/// .
		/// Example:
		/// MethodCallRecorder recorder = new MethodCallRecorder();
		/// runTest(recorder);
		/// System.out.println(CodeGenerator.generateMethodCallArray(recorder))
		/// </summary>
		/// <param name="calls">MethodCall generator</param>
		/// <returns>array string</returns>
		public static string GenerateMethodCallArray(IEnumerable calls)
		{
			IEnumerable callStrings = Iterators.Map(calls, new _IFunction4_23());
			return Iterators.Join(callStrings.GetEnumerator(), "," + TestPlatform.NewLine);
		}

		private sealed class _IFunction4_23 : IFunction4
		{
			public _IFunction4_23()
			{
			}

			public object Apply(object arg)
			{
				return CodeGenerator.GenerateMethodCall((MethodCall)arg);
			}
		}

		public static string GenerateValue(object value)
		{
			if (value == null)
			{
				return "null";
			}
			if (value is string)
			{
				return "\"" + value + "\"";
			}
			if (value is object[])
			{
				return GenerateArray((object[])value);
			}
			return value.ToString();
		}

		public static string GenerateArray(object[] array)
		{
			IEnumerator values = Iterators.Map(Iterators.Iterate(array), new _IFunction4_45()
				);
			return "new Object[] " + Iterators.Join(values, "{", "}", ", ");
		}

		private sealed class _IFunction4_45 : IFunction4
		{
			public _IFunction4_45()
			{
			}

			public object Apply(object arg)
			{
				return CodeGenerator.GenerateValue(arg);
			}
		}

		public static string GenerateMethodCall(MethodCall call)
		{
			return "new MethodCall(\"" + call.methodName + "\", " + GenerateArray(call.args) 
				+ ")";
		}
	}
}

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
using System.IO;
using System.Reflection;
using System.Threading;
using Sharpen.Lang;

namespace Sharpen
{
	public class Runtime 
	{
		private static long DIFFERENCE_IN_TICKS = 62135604000000;
		private static long RATIO = 10000;

		public static TextWriter Out
		{
			get
			{
				return Console.Out;
			}
		}

		public static TextWriter Err
		{
			get
			{
				return Console.Error;
			}
		}

		public static object GetArrayValue(object array, int i)
	    {
	        return ((Array)array).GetValue(i);
	    }
	    
	    public static int GetArrayLength(object array)
	    {
            return ((Array) array).Length;
	    }

	    public static void SetArrayValue(object array, int index, object value)
	    {
	        ((Array)array).SetValue(value, index);
	    }

        private const BindingFlags AllMembers = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        private const BindingFlags DeclaredMembers = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        private const BindingFlags DeclaredMembersIncludingStatic = DeclaredMembers | BindingFlags.Static;
		
		public static FieldInfo GetDeclaredField(Type type, string name)
		{
            return type.GetField(name, DeclaredMembersIncludingStatic);
		}

		public static FieldInfo[] GetDeclaredFields(Type type)
		{
            return type.GetFields(DeclaredMembersIncludingStatic);
		}
		
		public static MethodInfo GetDeclaredMethod(Type type, string name, Type[] parameterTypes)
		{
			return type.GetMethod(name, DeclaredMembers, null, parameterTypes, null);
		}

        public static MethodInfo GetMethod(Type type, string name, Type[] parameterTypes)
        {
            return type.GetMethod(name, AllMembers, null, parameterTypes, null);
        }

		public static Type[] GetParameterTypes(MethodBase method)
		{
			ParameterInfo[] parameters = method.GetParameters();
			Type[] types = new Type[parameters.Length];
			for (int i=0; i<types.Length; ++i)
			{
				types[i] = parameters[i].ParameterType;
			}
			return types;
		}

		public static long CurrentTimeMillis() 
		{
			return Runtime.ToJavaMilliseconds(DateTime.Now.ToUniversalTime());
		}

		public static int FloatToIntBits(float value) 
		{
			return BitConverter.ToInt32(BitConverter.GetBytes(value), 0);
		}

		public static void Gc() 
		{
			System.GC.Collect();
		}
		
		public static bool EqualsIgnoreCase(string lhs, string rhs) 
		{
			return 0 == string.Compare(lhs, rhs, true);
		}

		public static string Substring(String s, int startIndex)
		{
			return s.Substring(startIndex);
		}

		public static string Substring(String s, int startIndex, int endIndex)
		{
			return s.Substring(startIndex, endIndex-startIndex);
		}

		public static void GetCharsForString(string str, int start, int end, char[] destination, int destinationStart) 
		{
			str.CopyTo(start, destination, 0, end-start);
		}

		public static byte[] GetBytesForString(string str)
		{
			return System.Text.Encoding.Default.GetBytes(str);
		}

		public static string GetStringForBytes(byte[] bytes, int index, int length)
		{
			return System.Text.Encoding.Default.GetString(bytes, index, length);
		}

		public static string GetStringValueOf(object value) 
		{
			return null == value
				? "null"
				: value.ToString();
		}

		public static String GetProperty(String key) 
		{
			return GetProperty(key, null);
		}

		public static String GetProperty(String key, String defaultValue)
		{
#if CF
			return key.Equals("line.separator") ? "\n" : defaultValue;
#else
			return key.Equals("line.separator")
				? Environment.NewLine
				: GetEnvironmentVariable(key, defaultValue);
#endif
		}

		public static string GetEnvironmentVariable(string variableName, string defaultValue)
		{
#if CF
			return defaultValue;
#else
			string value = Environment.GetEnvironmentVariable(variableName);
			if (value == null || value.Length == 0) return defaultValue;
			return value;
#endif
		}

		public static object GetReferenceTarget(WeakReference reference) 
		{
			return reference.Target;
		}

		public static long GetTimeForDate(DateTime dateTime) 
		{
			return Runtime.ToJavaMilliseconds(dateTime);
		}

		public static int IdentityHashCode(object obj) 
		{
			return IdentityHashCodeProvider.IdentityHashCode(obj);
		}

		public static float IntBitsToFloat(int value) 
		{
			return BitConverter.ToSingle(BitConverter.GetBytes(value), 0);
		}

		public static void Wait(object obj, long timeout) 
		{
#if !CF
			Monitor.Wait(obj, (int) timeout);
#endif
		}

		public static void Notify(object obj) 
		{
#if !CF
			Monitor.Pulse(obj);
#endif
		}

		public static void NotifyAll(object obj) 
		{
#if !CF
			Monitor.PulseAll(obj);
#endif
		}

		public static void PrintStackTrace(Exception exception) 
		{
			PrintStackTrace(exception, Sharpen.Runtime.Err);
		}

		public static void PrintStackTrace(Exception exception, System.IO.TextWriter writer) 
		{
			writer.WriteLine(exception);
		}

		public static void RunFinalization() 
		{
			System.GC.WaitForPendingFinalizers();
		}

		public static void RunFinalizersOnExit(bool flag) 
		{
			// do nothing
		}

        public static System.Type GetType(string typeName)
        {
            return TypeReference.FromString(typeName).Resolve();
        }

		public static long ToJavaMilliseconds(DateTime dateTimeNet)
		{
			return ToJavaMilliseconds(dateTimeNet.Ticks);
		}

		public static long ToJavaMilliseconds(long ticks)
		{
			return ticks / RATIO - DIFFERENCE_IN_TICKS;
		}

		public static long ToNetTicks(long javaMilliseconds)
		{
			return (javaMilliseconds + Runtime.DIFFERENCE_IN_TICKS) * Runtime.RATIO;
		}
	}
}
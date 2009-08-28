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
namespace Db4oUnit
{
	using System;
	using System.IO;
	using System.Reflection;

	public class TestPlatform
	{
#if CF
        public static string NewLine = "\n";
#else
	    public static string NewLine = Environment.NewLine;
#endif

		// will be assigned from the outside on CF
		public static TextWriter Out;

        public static TextWriter Error;
        
		static TestPlatform()
		{
			Out = Console.Out;
            Error = Console.Error;
		}
		
		public static void PrintStackTrace(TextWriter writer, Exception e)
		{	
			writer.Write(e);
		}

        public static TextWriter GetNullWriter()
        {
            return new NullTextWriter();
        }
        
        public static TextWriter GetStdErr()
		{
			return Error;
		}
		
		public static void EmitWarning(string warning)
		{
			Out.WriteLine(warning);
		}		

		public static bool IsStatic(MethodInfo method)
		{
			return method.IsStatic;
		}

		public static bool IsPublic(MethodInfo method)
		{
			return method.IsPublic;
		}

		public static bool HasParameters(MethodInfo method)
		{
			return method.GetParameters().Length > 0;
		}

        public static TextWriter OpenTextFile(string fname)
        {
            return new StreamWriter(fname);
        }
	}
}

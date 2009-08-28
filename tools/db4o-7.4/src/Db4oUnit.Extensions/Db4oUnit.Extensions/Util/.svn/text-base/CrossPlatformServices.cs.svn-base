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
using Db4objects.Db4o.Foundation.IO;
using Db4objects.Db4o.Internal;
using Sharpen;

namespace Db4oUnit.Extensions.Util
{
	public class CrossPlatformServices
	{
		public static string SimpleName(string typeName)
		{
			int index = typeName.IndexOf(',');
			if (index < 0)
			{
				return typeName;
			}
			return Sharpen.Runtime.Substring(typeName, 0, index);
		}

		public static string FullyQualifiedName(Type klass)
		{
			return ReflectPlatform.FullyQualifiedName(klass);
		}

		public static string DatabasePath(string fileName)
		{
			string path = Runtime.GetProperty("db4ounit.file.path");
			if (path == null || path.Length == 0)
			{
				path = ".";
			}
			else
			{
				System.IO.Directory.CreateDirectory(path);
			}
			return Path.Combine(path, fileName);
		}
	}
}

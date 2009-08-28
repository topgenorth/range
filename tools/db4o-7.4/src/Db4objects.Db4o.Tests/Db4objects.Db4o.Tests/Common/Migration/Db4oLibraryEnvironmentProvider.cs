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
using System.IO;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Tests.Common.Migration;

namespace Db4objects.Db4o.Tests.Common.Migration
{
	public class Db4oLibraryEnvironmentProvider
	{
		private readonly Hashtable4 _environments = new Hashtable4();

		private readonly Sharpen.IO.File _classPath;

		public Db4oLibraryEnvironmentProvider(Sharpen.IO.File classPath)
		{
			_classPath = classPath;
		}

		/// <exception cref="IOException"></exception>
		public virtual Db4oLibraryEnvironment EnvironmentFor(string path)
		{
			Db4oLibraryEnvironment existing = ExistingEnvironment(path);
			if (existing != null)
			{
				return existing;
			}
			return NewEnvironment(path);
		}

		private Db4oLibraryEnvironment ExistingEnvironment(string path)
		{
			return (Db4oLibraryEnvironment)_environments.Get(path);
		}

		/// <exception cref="IOException"></exception>
		private Db4oLibraryEnvironment NewEnvironment(string path)
		{
			Db4oLibraryEnvironment env = new Db4oLibraryEnvironment(new Sharpen.IO.File(path)
				, _classPath);
			_environments.Put(path, env);
			return env;
		}
	}
}

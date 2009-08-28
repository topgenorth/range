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
using Db4oUnit;
using Db4oUnit.Extensions.Util;
using Db4objects.Db4o;
using Db4objects.Db4o.Defragment;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation.IO;
using Db4objects.Db4o.Reflect;
using Db4objects.Db4o.Tests.Common.Defragment;
using Db4objects.Db4o.Tests.Util;

namespace Db4objects.Db4o.Tests.Common.Defragment
{
	public class StoredClassFilterTestCase : ITestCase
	{
		private static readonly string Db4oBackup = BuildTempPath("defrag.db4o.backup");

		private static readonly string Db4oFile = BuildTempPath("defrag.db4o");

		public class SimpleClass
		{
			public string _simpleField;

			public SimpleClass(string simple)
			{
				_simpleField = simple;
			}
		}

		public static void Main(string[] args)
		{
			new ConsoleTestRunner(typeof(StoredClassFilterTestCase)).Run();
		}

		private static string BuildTempPath(string fname)
		{
			return IOServices.BuildTempPath(fname);
		}

		/// <exception cref="Exception"></exception>
		public virtual void Test()
		{
			DeleteAllFiles();
			string fname = CreateDatabase();
			Defrag(fname);
			AssertStoredClasses(fname);
		}

		private void DeleteAllFiles()
		{
			File4.Delete(Db4oFile);
			File4.Delete(Db4oBackup);
		}

		private void AssertStoredClasses(string fname)
		{
			IObjectContainer db = Db4oFactory.OpenFile(fname);
			try
			{
				IReflectClass[] knownClasses = db.Ext().KnownClasses();
				AssertKnownClasses(knownClasses);
			}
			finally
			{
				db.Close();
			}
		}

		private void AssertKnownClasses(IReflectClass[] knownClasses)
		{
			for (int i = 0; i < knownClasses.Length; i++)
			{
				Assert.AreNotEqual(FullyQualifiedName(typeof(StoredClassFilterTestCase.SimpleClass
					)), knownClasses[i].GetName());
			}
		}

		private string FullyQualifiedName(Type klass)
		{
			return CrossPlatformServices.FullyQualifiedName(klass);
		}

		/// <exception cref="IOException"></exception>
		private void Defrag(string fname)
		{
			DefragmentConfig config = new DefragmentConfig(fname);
			config.StoredClassFilter(IgnoreClassFilter(typeof(StoredClassFilterTestCase.SimpleClass
				)));
			Db4objects.Db4o.Defragment.Defragment.Defrag(config);
		}

		private IStoredClassFilter IgnoreClassFilter(Type klass)
		{
			return new _IStoredClassFilter_73(this, klass);
		}

		private sealed class _IStoredClassFilter_73 : IStoredClassFilter
		{
			public _IStoredClassFilter_73(StoredClassFilterTestCase _enclosing, Type klass)
			{
				this._enclosing = _enclosing;
				this.klass = klass;
			}

			public bool Accept(IStoredClass storedClass)
			{
				return !storedClass.GetName().Equals(this._enclosing.FullyQualifiedName(klass));
			}

			private readonly StoredClassFilterTestCase _enclosing;

			private readonly Type klass;
		}

		private string CreateDatabase()
		{
			string fname = Db4oFile;
			IObjectContainer db = Db4oFactory.OpenFile(fname);
			try
			{
				db.Store(new StoredClassFilterTestCase.SimpleClass("verySimple"));
				db.Commit();
			}
			finally
			{
				db.Close();
			}
			return fname;
		}
	}
}

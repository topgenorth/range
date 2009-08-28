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
using System.Collections;
using System.IO;
using Db4oUnit;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Tests.Common.Handlers;
using Db4objects.Db4o.Tests.Common.Migration;

namespace Db4objects.Db4o.Tests.Common.Migration
{
	public class Db4oMigrationSuiteBuilder : ReflectionTestSuiteBuilder
	{
		/// <summary>Runs the tests against all archived libraries + the current one</summary>
		public static readonly string[] All = null;

		/// <summary>Runs the tests against the current version only.</summary>
		/// <remarks>Runs the tests against the current version only.</remarks>
		public static readonly string[] Current = new string[0];

		private readonly Db4oLibraryEnvironmentProvider _environmentProvider = new Db4oLibraryEnvironmentProvider
			(PathProvider.TestCasePath());

		private readonly string[] _specificLibraries;

		/// <summary>
		/// Creates a suite builder for the specific FormatMigrationTestCaseBase derived classes
		/// and specific db4o libraries.
		/// </summary>
		/// <remarks>
		/// Creates a suite builder for the specific FormatMigrationTestCaseBase derived classes
		/// and specific db4o libraries. If no libraries are specified (either null or empty array)
		/// <see cref="Db4oLibrarian.Libraries">Db4oLibrarian.Libraries</see>
		/// is used to find archived libraries.
		/// </remarks>
		/// <param name="classes"></param>
		/// <param name="specificLibraries"></param>
		public Db4oMigrationSuiteBuilder(Type[] classes, string[] specificLibraries) : base
			(classes)
		{
			_specificLibraries = specificLibraries;
		}

		protected override IEnumerator FromClass(Type clazz)
		{
			AssertMigrationTestCase(clazz);
			IEnumerator defaultTestSuite = base.FromClass(clazz);
			try
			{
				IEnumerator migrationTestSuite = MigrationTestSuite(clazz, Db4oLibraries());
				return Iterators.Concat(migrationTestSuite, defaultTestSuite);
			}
			catch (Exception e)
			{
				return Iterators.Concat(Iterators.IterateSingle(new FailingTest(clazz.FullName, e
					)), defaultTestSuite);
			}
		}

		/// <exception cref="Exception"></exception>
		private IEnumerator MigrationTestSuite(Type clazz, Db4oLibrary[] libraries)
		{
			return Iterators.Map(libraries, new _IFunction4_53(this, clazz));
		}

		private sealed class _IFunction4_53 : IFunction4
		{
			public _IFunction4_53(Db4oMigrationSuiteBuilder _enclosing, Type clazz)
			{
				this._enclosing = _enclosing;
				this.clazz = clazz;
			}

			public object Apply(object library)
			{
				try
				{
					return this._enclosing.MigrationTest((Db4oLibrary)library, clazz);
				}
				catch (Exception e)
				{
					throw new Db4oException(e);
				}
			}

			private readonly Db4oMigrationSuiteBuilder _enclosing;

			private readonly Type clazz;
		}

		/// <exception cref="Exception"></exception>
		private Db4oMigrationSuiteBuilder.Db4oMigrationTest MigrationTest(Db4oLibrary library
			, Type clazz)
		{
			FormatMigrationTestCaseBase instance = (FormatMigrationTestCaseBase)NewInstance(clazz
				);
			return new Db4oMigrationSuiteBuilder.Db4oMigrationTest(instance, library);
		}

		/// <exception cref="Exception"></exception>
		private Db4oLibrary[] Db4oLibraries()
		{
			if (HasSpecificLibraries())
			{
				return SpecificLibraries();
			}
			return Librarian().Libraries();
		}

		/// <exception cref="Exception"></exception>
		private Db4oLibrary[] SpecificLibraries()
		{
			Db4oLibrary[] libraries = new Db4oLibrary[_specificLibraries.Length];
			for (int i = 0; i < libraries.Length; i++)
			{
				libraries[i] = Librarian().ForFile(_specificLibraries[i]);
			}
			return libraries;
		}

		private bool HasSpecificLibraries()
		{
			return null != _specificLibraries;
		}

		private Db4oLibrarian Librarian()
		{
			return new Db4oLibrarian(_environmentProvider);
		}

		private void AssertMigrationTestCase(Type clazz)
		{
			if (!typeof(FormatMigrationTestCaseBase).IsAssignableFrom(clazz))
			{
				throw new ArgumentException();
			}
		}

		private sealed class Db4oMigrationTest : ITest
		{
			private readonly FormatMigrationTestCaseBase _test;

			private readonly Db4oLibrary _library;

			private readonly string _version;

			/// <exception cref="Exception"></exception>
			public Db4oMigrationTest(FormatMigrationTestCaseBase test, Db4oLibrary library)
			{
				_library = library;
				_test = test;
				_version = Environment().Version();
			}

			public string Label()
			{
				return "[" + _version + "] " + _test.GetType().FullName;
			}

			public void Run()
			{
				try
				{
					CreateDatabase();
					Test();
				}
				catch (TestException e)
				{
					throw;
				}
				catch (Exception e)
				{
					throw new TestException(e);
				}
			}

			/// <exception cref="IOException"></exception>
			private void Test()
			{
				_test.Test(_version);
			}

			/// <exception cref="Exception"></exception>
			private void CreateDatabase()
			{
				Environment().InvokeInstanceMethod(_test.GetType(), "createDatabaseFor", new object
					[] { _version });
			}

			private Db4oLibraryEnvironment Environment()
			{
				return _library.environment;
			}
		}
	}
}

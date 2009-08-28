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
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Db4objects.Db4o.Tests.Util;

namespace Db4objects.Db4o.Tests.Common.Migration
{
	public class Db4oLibrarian
	{
		private const double MinimumVersionToTest = 6.0;
		private readonly Db4oLibraryEnvironmentProvider _provider;

		public Db4oLibrarian(Db4oLibraryEnvironmentProvider provider)
		{
			_provider = provider;
		}

		public Db4oLibrary[] Libraries()
		{
            List <Db4oLibrary> libraries = new List<Db4oLibrary>();
			foreach (string directory in Directory.GetDirectories(LibraryPath()))
			{
				// comment out the next line to run against legacy versions
				if (!IsVersionOrGreater(directory, MinimumVersionToTest)) continue;

				string db4oLib = FindLibraryFile(directory);
				if (null == db4oLib) continue;
				libraries.Add(ForFile(db4oLib));
			}
			return libraries.ToArray();
		}

		private static bool IsVersionOrGreater(string versionName, double minimumVersion)
		{
#if !CF
			double currentVersion;
			if (!Double.TryParse(Path.GetFileName(versionName), NumberStyles.AllowDecimalPoint | NumberStyles.Float, NumberFormatInfo.InvariantInfo, out currentVersion))
			{
				return false;
			}

			return currentVersion >= minimumVersion;
#else
			return false;
#endif
		}

		public static bool IsLegacyVersion(string versionName)
		{
			return VersionFromVersionName(versionName) < MinimumVersionToTest;
		}

		private static double VersionFromVersionName(string versionName)
		{
			Version version = new Version(versionName);
			return version.Major + version.Minor / 10.0;
		}

		public static string LibraryPath()
		{
			return WorkspaceServices.WorkspacePath("db4o.archives/net-2.0");
		}

		public Db4oLibrary ForVersion(string version)
		{
			return ForFile(FindLibraryFile(Path.Combine(LibraryPath(), version)));
		}

		public Db4oLibrary ForFile(string db4oLib)
		{
			return new Db4oLibrary(db4oLib, EnvironmentFor(db4oLib));
		}

		private static string FindLibraryFile(string directory)
		{
			string[] found = Directory.GetFiles(directory, "*.dll");
			return found.Length == 1 ? found[0] : null;
		}

		private Db4oLibraryEnvironment EnvironmentFor(string db4oLib)
		{
			return _provider.EnvironmentFor(db4oLib);
		}

    }

}
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
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4oUnit.Extensions.Fixtures;
using Db4oUnit.Extensions.Util;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Tests.Common.Refactor;

namespace Db4objects.Db4o.Tests.Common.Refactor
{
	public class ClassRenameByStoredClassTestCase : AbstractDb4oTestCase, IOptOutNetworkingCS
	{
		private static string Name = "test";

		public static void Main(string[] args)
		{
			new ClassRenameByStoredClassTestCase().RunAll();
		}

		public class Original
		{
			public string _name;

			public Original(string name)
			{
				this._name = name;
			}
		}

		public class Changed
		{
			public string _name;

			public string _otherName;

			public Changed(string name)
			{
				_name = name;
				_otherName = name;
			}
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			Store(new ClassRenameByStoredClassTestCase.Original(Name));
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestWithReopen()
		{
			AssertRenamed(true);
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestWithoutReopen()
		{
			AssertRenamed(false);
		}

		/// <exception cref="Exception"></exception>
		private void AssertRenamed(bool doReopen)
		{
			IStoredClass originalClazz = Db().Ext().StoredClass(typeof(ClassRenameByStoredClassTestCase.Original
				));
			originalClazz.Rename(CrossPlatformServices.FullyQualifiedName(typeof(ClassRenameByStoredClassTestCase.Changed
				)));
			if (doReopen)
			{
				Reopen();
			}
			ClassRenameByStoredClassTestCase.Changed changedObject = (ClassRenameByStoredClassTestCase.Changed
				)RetrieveOnlyInstance(typeof(ClassRenameByStoredClassTestCase.Changed));
			Assert.AreEqual(Name, changedObject._name);
			Assert.IsNull(changedObject._otherName);
		}
	}
}

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
using Db4oUnit.Extensions;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Tests.Common.Assorted;

namespace Db4objects.Db4o.Tests.Common.Assorted
{
	public class PersistStaticFieldValuesTestCase : AbstractDb4oTestCase
	{
		public class Data
		{
			public static readonly PersistStaticFieldValuesTestCase.PsfvHelper One = new PersistStaticFieldValuesTestCase.PsfvHelper
				();

			public static readonly PersistStaticFieldValuesTestCase.PsfvHelper Two = new PersistStaticFieldValuesTestCase.PsfvHelper
				();

			public static readonly PersistStaticFieldValuesTestCase.PsfvHelper Three = new PersistStaticFieldValuesTestCase.PsfvHelper
				();

			public PersistStaticFieldValuesTestCase.PsfvHelper one;

			public PersistStaticFieldValuesTestCase.PsfvHelper two;

			public PersistStaticFieldValuesTestCase.PsfvHelper three;
		}

		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(typeof(PersistStaticFieldValuesTestCase.Data)).PersistStaticFieldValues
				();
		}

		protected override void Store()
		{
			PersistStaticFieldValuesTestCase.Data psfv = new PersistStaticFieldValuesTestCase.Data
				();
			psfv.one = PersistStaticFieldValuesTestCase.Data.One;
			psfv.two = PersistStaticFieldValuesTestCase.Data.Two;
			psfv.three = PersistStaticFieldValuesTestCase.Data.Three;
			Store(psfv);
		}

		public virtual void Test()
		{
			PersistStaticFieldValuesTestCase.Data psfv = (PersistStaticFieldValuesTestCase.Data
				)RetrieveOnlyInstance(typeof(PersistStaticFieldValuesTestCase.Data));
			Assert.AreSame(PersistStaticFieldValuesTestCase.Data.One, psfv.one);
			Assert.AreSame(PersistStaticFieldValuesTestCase.Data.Two, psfv.two);
			Assert.AreSame(PersistStaticFieldValuesTestCase.Data.Three, psfv.three);
		}

		public class PsfvHelper
		{
		}
	}
}

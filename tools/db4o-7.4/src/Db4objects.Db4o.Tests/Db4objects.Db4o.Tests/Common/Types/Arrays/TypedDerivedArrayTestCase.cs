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
using Db4objects.Db4o.Tests.Common.Sampledata;
using Db4objects.Db4o.Tests.Common.Types.Arrays;

namespace Db4objects.Db4o.Tests.Common.Types.Arrays
{
	public class TypedDerivedArrayTestCase : AbstractDb4oTestCase
	{
		private static readonly MoleculeData[] Array = new MoleculeData[] { new MoleculeData
			("TypedDerivedArray") };

		public class Data
		{
			public AtomData[] _array;

			public Data(AtomData[] AtomDatas)
			{
				this._array = AtomDatas;
			}
		}

		protected override void Store()
		{
			Db().Store(new TypedDerivedArrayTestCase.Data(Array));
		}

		public virtual void Test()
		{
			TypedDerivedArrayTestCase.Data data = (TypedDerivedArrayTestCase.Data)RetrieveOnlyInstance
				(typeof(TypedDerivedArrayTestCase.Data));
			Assert.IsTrue(data._array is MoleculeData[], "Expected instance of " + typeof(MoleculeData
				[]) + ", but got " + data._array);
			ArrayAssert.AreEqual(Array, data._array);
		}
	}
}

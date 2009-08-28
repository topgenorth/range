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
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Tests.Common.Assorted;

namespace Db4objects.Db4o.Tests.Common.Assorted
{
	public class ClassMetadataTestCase : AbstractDb4oTestCase
	{
		public class SuperClazz
		{
			public int _id;

			public string _name;
		}

		public class SubClazz : ClassMetadataTestCase.SuperClazz
		{
			public int _age;
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			Store(new ClassMetadataTestCase.SubClazz());
		}

		public virtual void TestForEachField()
		{
			Collection4 expectedNames = new Collection4(new ArrayIterator4(new string[] { "_id"
				, "_name", "_age" }));
			ClassMetadata classMetadata = ClassMetadataFor(typeof(ClassMetadataTestCase.SubClazz
				));
			classMetadata.ForEachField(new _IProcedure4_29(expectedNames));
			Assert.IsTrue(expectedNames.IsEmpty());
		}

		private sealed class _IProcedure4_29 : IProcedure4
		{
			public _IProcedure4_29(Collection4 expectedNames)
			{
				this.expectedNames = expectedNames;
			}

			public void Apply(object arg)
			{
				FieldMetadata curField = (FieldMetadata)arg;
				Assert.IsNotNull(expectedNames.Remove(curField.GetName()));
			}

			private readonly Collection4 expectedNames;
		}
	}
}

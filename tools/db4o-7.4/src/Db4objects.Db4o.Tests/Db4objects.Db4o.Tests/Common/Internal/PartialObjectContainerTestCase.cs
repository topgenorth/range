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
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Tests.Common.Internal;

namespace Db4objects.Db4o.Tests.Common.Internal
{
	/// <exclude></exclude>
	public class PartialObjectContainerTestCase : AbstractDb4oTestCase, IOptOutTA
	{
		public static void Main(string[] arguments)
		{
			new PartialObjectContainerTestCase().RunSolo();
		}

		/// <exception cref="Exception"></exception>
		protected override void Configure(IConfiguration config)
		{
			config.BlockSize(8);
		}

		public virtual void TestBlocksToBytes()
		{
			int[] blocks = new int[] { 0, 1, 8, 9 };
			int[] bytes = new int[] { 0, 8, 64, 72 };
			for (int i = 0; i < blocks.Length; i++)
			{
				Assert.AreEqual(bytes[i], LocalContainer().BlocksToBytes(blocks[i]));
			}
		}

		public virtual void TestBytesToBlocks()
		{
			int[] bytes = new int[] { 0, 1, 2, 7, 8, 9, 16, 17, 799, 800, 801 };
			int[] blocks = new int[] { 0, 1, 1, 1, 1, 2, 2, 3, 100, 100, 101 };
			for (int i = 0; i < blocks.Length; i++)
			{
				Assert.AreEqual(blocks[i], LocalContainer().BytesToBlocks(bytes[i]));
			}
		}

		private ObjectContainerBase LocalContainer()
		{
			return Stream().Container();
		}
	}
}

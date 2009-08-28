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
	public class LockedTreeTestCase : AbstractDb4oTestCase
	{
		public static void Main(string[] args)
		{
			new LockedTreeTestCase().RunSolo();
		}

		public virtual void TestAdd()
		{
			LockedTree lockedTree = new LockedTree();
			lockedTree.Add(new TreeInt(1));
			Assert.Expect(typeof(InvalidOperationException), new _ICodeBlock_21(lockedTree));
		}

		private sealed class _ICodeBlock_21 : ICodeBlock
		{
			public _ICodeBlock_21(LockedTree lockedTree)
			{
				this.lockedTree = lockedTree;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				lockedTree.TraverseLocked(new _IVisitor4_23(lockedTree));
			}

			private sealed class _IVisitor4_23 : IVisitor4
			{
				public _IVisitor4_23(LockedTree lockedTree)
				{
					this.lockedTree = lockedTree;
				}

				public void Visit(object obj)
				{
					TreeInt treeInt = (TreeInt)obj;
					if (treeInt._key == 1)
					{
						lockedTree.Add(new TreeInt(2));
					}
				}

				private readonly LockedTree lockedTree;
			}

			private readonly LockedTree lockedTree;
		}

		public virtual void TestClear()
		{
			LockedTree lockedTree = new LockedTree();
			lockedTree.Add(new TreeInt(1));
			Assert.Expect(typeof(InvalidOperationException), new _ICodeBlock_38(lockedTree));
		}

		private sealed class _ICodeBlock_38 : ICodeBlock
		{
			public _ICodeBlock_38(LockedTree lockedTree)
			{
				this.lockedTree = lockedTree;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				lockedTree.TraverseLocked(new _IVisitor4_40(lockedTree));
			}

			private sealed class _IVisitor4_40 : IVisitor4
			{
				public _IVisitor4_40(LockedTree lockedTree)
				{
					this.lockedTree = lockedTree;
				}

				public void Visit(object obj)
				{
					lockedTree.Clear();
				}

				private readonly LockedTree lockedTree;
			}

			private readonly LockedTree lockedTree;
		}
	}
}

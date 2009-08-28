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

namespace Db4oUnit.Tests
{
	public class AssertTestCase : ITestCase
	{
		public virtual void TestAreEqual()
		{
			Assert.AreEqual(true, true);
			Assert.AreEqual(42, 42);
			Assert.AreEqual(42, 42);
			Assert.AreEqual(null, null);
			ExpectFailure(new _ICodeBlock_14());
			ExpectFailure(new _ICodeBlock_19());
			ExpectFailure(new _ICodeBlock_24());
			ExpectFailure(new _ICodeBlock_29());
		}

		private sealed class _ICodeBlock_14 : ICodeBlock
		{
			public _ICodeBlock_14()
			{
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				Assert.AreEqual(true, false);
			}
		}

		private sealed class _ICodeBlock_19 : ICodeBlock
		{
			public _ICodeBlock_19()
			{
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				Assert.AreEqual(42, 43);
			}
		}

		private sealed class _ICodeBlock_24 : ICodeBlock
		{
			public _ICodeBlock_24()
			{
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				Assert.AreEqual(new object(), new object());
			}
		}

		private sealed class _ICodeBlock_29 : ICodeBlock
		{
			public _ICodeBlock_29()
			{
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				Assert.AreEqual(null, new object());
			}
		}

		public virtual void TestAreSame()
		{
			ExpectFailure(new _ICodeBlock_37());
			Assert.AreSame(this, this);
		}

		private sealed class _ICodeBlock_37 : ICodeBlock
		{
			public _ICodeBlock_37()
			{
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				Assert.AreSame(new object(), new object());
			}
		}

		private void ExpectFailure(ICodeBlock block)
		{
			Assert.Expect(typeof(AssertionException), block);
		}
	}
}

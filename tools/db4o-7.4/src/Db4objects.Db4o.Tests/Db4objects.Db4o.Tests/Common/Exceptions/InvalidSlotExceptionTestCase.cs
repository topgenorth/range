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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Tests.Common.Exceptions;

namespace Db4objects.Db4o.Tests.Common.Exceptions
{
	public class InvalidSlotExceptionTestCase : AbstractDb4oTestCase
	{
		private const int Max = 100000;

		public static void Main(string[] args)
		{
			new InvalidSlotExceptionTestCase().RunAll();
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestInvalidSlotException()
		{
			Assert.Expect(typeof(InvalidIDException), typeof(InvalidSlotException), new _ICodeBlock_19
				(this));
		}

		private sealed class _ICodeBlock_19 : ICodeBlock
		{
			public _ICodeBlock_19(InvalidSlotExceptionTestCase _enclosing)
			{
				this._enclosing = _enclosing;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				this._enclosing.Db().GetByID(1);
			}

			private readonly InvalidSlotExceptionTestCase _enclosing;
		}

		/// <exception cref="Exception"></exception>
		public virtual void _testTimes()
		{
			long[] ids = new long[Max];
			for (int i = 0; i < Max; i++)
			{
				object o = ComplexObject();
				Db().Store(o);
				ids[i] = Db().Ext().GetID(o);
			}
			Reopen();
			for (int i = 0; i < Max; i++)
			{
				Db().Ext().GetByID(ids[i]);
			}
		}

		public class A
		{
			internal InvalidSlotExceptionTestCase.A _a;

			public A(InvalidSlotExceptionTestCase.A a)
			{
				this._a = a;
			}
		}

		private object ComplexObject()
		{
			return new InvalidSlotExceptionTestCase.A(new InvalidSlotExceptionTestCase.A(new 
				InvalidSlotExceptionTestCase.A(null)));
		}
	}
}

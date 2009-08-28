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
	public class DatabaseClosedExceptionTestCase : AbstractDb4oTestCase
	{
		public static void Main(string[] args)
		{
			new DatabaseClosedExceptionTestCase().RunAll();
		}

		public virtual void TestRollback()
		{
			Db().Close();
			Assert.Expect(typeof(DatabaseClosedException), new _ICodeBlock_17(this));
		}

		private sealed class _ICodeBlock_17 : ICodeBlock
		{
			public _ICodeBlock_17(DatabaseClosedExceptionTestCase _enclosing)
			{
				this._enclosing = _enclosing;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				this._enclosing.Db().Rollback();
			}

			private readonly DatabaseClosedExceptionTestCase _enclosing;
		}

		public virtual void TestCommit()
		{
			Db().Close();
			Assert.Expect(typeof(DatabaseClosedException), new _ICodeBlock_26(this));
		}

		private sealed class _ICodeBlock_26 : ICodeBlock
		{
			public _ICodeBlock_26(DatabaseClosedExceptionTestCase _enclosing)
			{
				this._enclosing = _enclosing;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				this._enclosing.Db().Commit();
			}

			private readonly DatabaseClosedExceptionTestCase _enclosing;
		}

		public virtual void TestSet()
		{
			Db().Close();
			Assert.Expect(typeof(DatabaseClosedException), new _ICodeBlock_35(this));
		}

		private sealed class _ICodeBlock_35 : ICodeBlock
		{
			public _ICodeBlock_35(DatabaseClosedExceptionTestCase _enclosing)
			{
				this._enclosing = _enclosing;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				this._enclosing.Db().Store(new Item());
			}

			private readonly DatabaseClosedExceptionTestCase _enclosing;
		}

		public virtual void TestDelete()
		{
			Db().Close();
			Assert.Expect(typeof(DatabaseClosedException), new _ICodeBlock_44(this));
		}

		private sealed class _ICodeBlock_44 : ICodeBlock
		{
			public _ICodeBlock_44(DatabaseClosedExceptionTestCase _enclosing)
			{
				this._enclosing = _enclosing;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				this._enclosing.Db().Delete(null);
			}

			private readonly DatabaseClosedExceptionTestCase _enclosing;
		}

		public virtual void TestQueryClass()
		{
			Db().Close();
			Assert.Expect(typeof(DatabaseClosedException), new _ICodeBlock_53(this));
		}

		private sealed class _ICodeBlock_53 : ICodeBlock
		{
			public _ICodeBlock_53(DatabaseClosedExceptionTestCase _enclosing)
			{
				this._enclosing = _enclosing;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				this._enclosing.Db().Query(this.GetType());
			}

			private readonly DatabaseClosedExceptionTestCase _enclosing;
		}

		public virtual void TestQuery()
		{
			Db().Close();
			Assert.Expect(typeof(DatabaseClosedException), new _ICodeBlock_62(this));
		}

		private sealed class _ICodeBlock_62 : ICodeBlock
		{
			public _ICodeBlock_62(DatabaseClosedExceptionTestCase _enclosing)
			{
				this._enclosing = _enclosing;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				this._enclosing.Db().Query();
			}

			private readonly DatabaseClosedExceptionTestCase _enclosing;
		}

		public virtual void TestDeactivate()
		{
			Db().Close();
			Assert.Expect(typeof(DatabaseClosedException), new _ICodeBlock_71(this));
		}

		private sealed class _ICodeBlock_71 : ICodeBlock
		{
			public _ICodeBlock_71(DatabaseClosedExceptionTestCase _enclosing)
			{
				this._enclosing = _enclosing;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				this._enclosing.Db().Deactivate(null, 1);
			}

			private readonly DatabaseClosedExceptionTestCase _enclosing;
		}

		public virtual void TestActivate()
		{
			Db().Close();
			Assert.Expect(typeof(DatabaseClosedException), new _ICodeBlock_80(this));
		}

		private sealed class _ICodeBlock_80 : ICodeBlock
		{
			public _ICodeBlock_80(DatabaseClosedExceptionTestCase _enclosing)
			{
				this._enclosing = _enclosing;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				this._enclosing.Db().Activate(null, 1);
			}

			private readonly DatabaseClosedExceptionTestCase _enclosing;
		}

		public virtual void TestGet()
		{
			Db().Close();
			Assert.Expect(typeof(DatabaseClosedException), new _ICodeBlock_89(this));
		}

		private sealed class _ICodeBlock_89 : ICodeBlock
		{
			public _ICodeBlock_89(DatabaseClosedExceptionTestCase _enclosing)
			{
				this._enclosing = _enclosing;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				this._enclosing.Db().QueryByExample(null);
			}

			private readonly DatabaseClosedExceptionTestCase _enclosing;
		}
	}
}

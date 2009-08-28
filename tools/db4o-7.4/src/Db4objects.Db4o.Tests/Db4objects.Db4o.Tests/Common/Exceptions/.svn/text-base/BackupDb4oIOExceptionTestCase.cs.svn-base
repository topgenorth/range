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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation.IO;
using Db4objects.Db4o.Tests.Common.Exceptions;

namespace Db4objects.Db4o.Tests.Common.Exceptions
{
	public class BackupDb4oIOExceptionTestCase : Db4oIOExceptionTestCaseBase
	{
		public static void Main(string[] args)
		{
			new BackupDb4oIOExceptionTestCase().RunAll();
		}

		private static readonly string BackupFile = "backup.db4o";

		/// <exception cref="Exception"></exception>
		protected override void Db4oSetupBeforeStore()
		{
			base.Db4oSetupBeforeStore();
			File4.Delete(BackupFile);
		}

		/// <exception cref="Exception"></exception>
		protected override void Db4oTearDownBeforeClean()
		{
			base.Db4oTearDownBeforeClean();
			File4.Delete(BackupFile);
		}

		public virtual void TestBackup()
		{
			Assert.Expect(typeof(Db4oIOException), new _ICodeBlock_28(this));
		}

		private sealed class _ICodeBlock_28 : ICodeBlock
		{
			public _ICodeBlock_28(BackupDb4oIOExceptionTestCase _enclosing)
			{
				this._enclosing = _enclosing;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				ExceptionIOAdapter.exception = true;
				this._enclosing.Db().Backup(BackupDb4oIOExceptionTestCase.BackupFile);
			}

			private readonly BackupDb4oIOExceptionTestCase _enclosing;
		}
	}
}

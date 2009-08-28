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
using System.IO;
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4objects.Db4o;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation.IO;
using Db4objects.Db4o.IO;
using Db4objects.Db4o.Tests.Common.Exceptions;

namespace Db4objects.Db4o.Tests.Common.Exceptions
{
	public class IncompatibleFileFormatExceptionTestCase : IDb4oTestCase, ITestLifeCycle
	{
		/// <exception cref="Exception"></exception>
		public static void Main(string[] args)
		{
			new ConsoleTestRunner(typeof(IncompatibleFileFormatExceptionTestCase)).Run();
		}

		private static readonly string IncompatibleFileFormat = Path.GetTempFileName();

		/// <exception cref="Exception"></exception>
		public virtual void SetUp()
		{
			File4.Delete(IncompatibleFileFormat);
			IoAdapter adapter = new RandomAccessFileAdapter();
			adapter = adapter.Open(IncompatibleFileFormat, false, 0, false);
			adapter.Write(new byte[] { 1, 2, 3 }, 3);
			adapter.Close();
		}

		/// <exception cref="Exception"></exception>
		public virtual void TearDown()
		{
			File4.Delete(IncompatibleFileFormat);
		}

		public virtual void Test()
		{
			Assert.Expect(typeof(IncompatibleFileFormatException), new _ICodeBlock_33());
			File4.Delete(IncompatibleFileFormat);
			IoAdapter adapter = new RandomAccessFileAdapter();
			Assert.IsFalse(adapter.Exists(IncompatibleFileFormat));
		}

		private sealed class _ICodeBlock_33 : ICodeBlock
		{
			public _ICodeBlock_33()
			{
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				Db4oFactory.OpenFile(IncompatibleFileFormatExceptionTestCase.IncompatibleFileFormat
					);
			}
		}
	}
}

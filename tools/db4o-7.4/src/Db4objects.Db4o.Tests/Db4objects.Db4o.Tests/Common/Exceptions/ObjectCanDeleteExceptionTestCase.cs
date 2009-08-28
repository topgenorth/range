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
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Tests.Common.Exceptions;

namespace Db4objects.Db4o.Tests.Common.Exceptions
{
	public class ObjectCanDeleteExceptionTestCase : AbstractDb4oTestCase, IOptOutCS
	{
		public static void Main(string[] args)
		{
			new ObjectCanDeleteExceptionTestCase().RunSolo();
		}

		public class Item
		{
			public virtual bool ObjectCanDelete(IObjectContainer container)
			{
				throw new ItemException();
			}
		}

		public virtual void Test()
		{
			ObjectCanDeleteExceptionTestCase.Item item = new ObjectCanDeleteExceptionTestCase.Item
				();
			Store(item);
			Assert.Expect(typeof(ReflectException), typeof(ItemException), new _ICodeBlock_27
				(this, item));
		}

		private sealed class _ICodeBlock_27 : ICodeBlock
		{
			public _ICodeBlock_27(ObjectCanDeleteExceptionTestCase _enclosing, ObjectCanDeleteExceptionTestCase.Item
				 item)
			{
				this._enclosing = _enclosing;
				this.item = item;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				this._enclosing.Db().Delete(item);
				this._enclosing.Db().Commit();
			}

			private readonly ObjectCanDeleteExceptionTestCase _enclosing;

			private readonly ObjectCanDeleteExceptionTestCase.Item item;
		}
	}
}

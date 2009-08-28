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
using System.Text;
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Diagnostic;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal.Handlers;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Handlers;
using Db4objects.Db4o.Typehandlers;
using Db4objects.Db4o.Types;

namespace Db4objects.Db4o.Tests.Common.Handlers
{
	public class StringBufferHandlerTestCase : AbstractDb4oTestCase
	{
		public static void Main(string[] args)
		{
			new StringBufferHandlerTestCase().RunAll();
		}

		public class Item : ISecondClass
		{
			public StringBuilder buffer;

			public Item(StringBuilder contents)
			{
				buffer = contents;
			}
		}

		internal static string _bufferValue = "42";

		//$NON-NLS-1$
		/// <exception cref="Exception"></exception>
		protected override void Configure(IConfiguration config)
		{
			config.ExceptionsOnNotStorable(true);
			config.RegisterTypeHandler(new SingleClassTypeHandlerPredicate(typeof(StringBuilder
				)), new StringBufferHandler());
			config.Diagnostic().AddListener(new _IDiagnosticListener_37());
		}

		private sealed class _IDiagnosticListener_37 : IDiagnosticListener
		{
			public _IDiagnosticListener_37()
			{
			}

			public void OnDiagnostic(IDiagnostic d)
			{
				if (d is DeletionFailed)
				{
					throw new Db4oException();
				}
			}
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			Store(new StringBufferHandlerTestCase.Item(new StringBuilder(_bufferValue)));
		}

		public virtual void TestRetrieve()
		{
			StringBufferHandlerTestCase.Item item = RetrieveItem();
			Assert.AreEqual(_bufferValue, item.buffer.ToString());
		}

		public virtual void TestTopLevelStore()
		{
			Assert.Expect(typeof(ObjectNotStorableException), new _ICodeBlock_56(this));
		}

		private sealed class _ICodeBlock_56 : ICodeBlock
		{
			public _ICodeBlock_56(StringBufferHandlerTestCase _enclosing)
			{
				this._enclosing = _enclosing;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				this._enclosing.Store(new StringBuilder("a"));
			}

			private readonly StringBufferHandlerTestCase _enclosing;
		}

		//$NON-NLS-1$
		public virtual void TestDelete()
		{
			StringBufferHandlerTestCase.Item item = RetrieveItem();
			Assert.AreEqual(_bufferValue, item.buffer.ToString());
			Db().Delete(item);
			IQuery query = NewQuery();
			query.Constrain(typeof(StringBufferHandlerTestCase.Item));
			Assert.AreEqual(0, query.Execute().Size());
		}

		public virtual void TestPrepareComparison()
		{
			StringBufferHandler handler = new StringBufferHandler();
			IPreparedComparison preparedComparison = handler.PrepareComparison(Trans().Context
				(), _bufferValue);
			Assert.IsGreater(preparedComparison.CompareTo("43"), 0);
		}

		//$NON-NLS-1$
		private StringBufferHandlerTestCase.Item RetrieveItem()
		{
			return (StringBufferHandlerTestCase.Item)RetrieveOnlyInstance(typeof(StringBufferHandlerTestCase.Item
				));
		}
	}
}

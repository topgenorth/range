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
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Exceptions;

namespace Db4objects.Db4o.Tests.Common.Exceptions
{
	public class ActivationExceptionBubblesUpTestCase : AbstractDb4oTestCase
	{
		public static void Main(string[] args)
		{
			new ActivationExceptionBubblesUpTestCase().RunAll();
		}

		[System.Serializable]
		public class AcceptAllEvaluation : IEvaluation
		{
			public virtual void Evaluate(ICandidate candidate)
			{
				candidate.Include(true);
			}
		}

		public sealed class ItemTranslator : IObjectTranslator
		{
			public void OnActivate(IObjectContainer container, object applicationObject, object
				 storedObject)
			{
				throw new ItemException();
			}

			public object OnStore(IObjectContainer container, object applicationObject)
			{
				return applicationObject;
			}

			public Type StoredClass()
			{
				return typeof(Item);
			}
		}

		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(typeof(Item)).Translate(new ActivationExceptionBubblesUpTestCase.ItemTranslator
				());
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			Store(new Item());
		}

		public virtual void Test()
		{
			Assert.Expect(typeof(ReflectException), typeof(ItemException), new _ICodeBlock_55
				(this));
		}

		private sealed class _ICodeBlock_55 : ICodeBlock
		{
			public _ICodeBlock_55(ActivationExceptionBubblesUpTestCase _enclosing)
			{
				this._enclosing = _enclosing;
			}

			/// <exception cref="Exception"></exception>
			public void Run()
			{
				IQuery q = this._enclosing.Db().Query();
				q.Constrain(typeof(Item));
				q.Constrain(new ActivationExceptionBubblesUpTestCase.AcceptAllEvaluation());
				q.Execute().Next();
			}

			private readonly ActivationExceptionBubblesUpTestCase _enclosing;
		}
	}
}

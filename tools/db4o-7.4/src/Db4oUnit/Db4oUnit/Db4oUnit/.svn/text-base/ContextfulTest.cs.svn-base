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
using Db4oUnit.Fixtures;
using Db4objects.Db4o.Foundation;

namespace Db4oUnit
{
	public class ContextfulTest : Contextful, ITestDecoration
	{
		private readonly ITestFactory _factory;

		private ITest _test;

		public ContextfulTest(ITestFactory factory)
		{
			_factory = factory;
		}

		public virtual string Label()
		{
			return (string)Run(new _IClosure4_19(this));
		}

		private sealed class _IClosure4_19 : IClosure4
		{
			public _IClosure4_19(ContextfulTest _enclosing)
			{
				this._enclosing = _enclosing;
			}

			public object Run()
			{
				return this._enclosing.TestInstance().Label();
			}

			private readonly ContextfulTest _enclosing;
		}

		public virtual void Run()
		{
			Run(TestInstance());
		}

		public virtual ITest Test()
		{
			if (null == _test)
			{
				throw new InvalidOperationException();
			}
			return _test;
		}

		private ITest TestInstance()
		{
			if (_test == null)
			{
				_test = _factory.NewInstance();
			}
			return _test;
		}
	}
}

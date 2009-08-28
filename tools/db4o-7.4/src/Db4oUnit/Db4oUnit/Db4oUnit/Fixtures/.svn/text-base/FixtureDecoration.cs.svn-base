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
using Db4oUnit;
using Db4oUnit.Fixtures;
using Db4objects.Db4o.Foundation;
using Sharpen.Lang;

namespace Db4oUnit.Fixtures
{
	public sealed class FixtureDecoration : ITestDecoration
	{
		private readonly ITest _test;

		private readonly FixtureVariable _variable;

		private readonly object _value;

		private readonly string _fixtureLabel;

		public FixtureDecoration(ITest test, FixtureVariable fixtureVariable, object fixtureValue
			) : this(test, null, fixtureVariable, fixtureValue)
		{
		}

		public FixtureDecoration(ITest test, string fixtureLabel, FixtureVariable fixtureVariable
			, object fixtureValue)
		{
			_test = test;
			_fixtureLabel = fixtureLabel;
			_variable = fixtureVariable;
			_value = fixtureValue;
		}

		public string Label()
		{
			ObjectByRef label = new ObjectByRef();
			RunDecorated(new _IRunnable_26(this, label));
			return (string)label.value;
		}

		private sealed class _IRunnable_26 : IRunnable
		{
			public _IRunnable_26(FixtureDecoration _enclosing, ObjectByRef label)
			{
				this._enclosing = _enclosing;
				this.label = label;
			}

			public void Run()
			{
				label.value = "(" + this._enclosing.FixtureLabel() + ") " + this._enclosing._test
					.Label();
			}

			private readonly FixtureDecoration _enclosing;

			private readonly ObjectByRef label;
		}

		public void Run()
		{
			RunDecorated(_test);
		}

		public ITest Test()
		{
			return _test;
		}

		private void RunDecorated(IRunnable block)
		{
			_variable.With(Value(), block);
		}

		private object Value()
		{
			return _value is IDeferred4 ? ((IDeferred4)_value).Value() : _value;
		}

		private object FixtureLabel()
		{
			return (_fixtureLabel == null ? _value : _fixtureLabel);
		}
	}
}

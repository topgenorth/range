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
using Db4oUnit.Tests.Fixtures;
using Sharpen.Lang;

namespace Db4oUnit.Tests.Fixtures
{
	public class FixtureContextTestCase : ITestCase
	{
		public sealed class ContextRef
		{
			public FixtureContext value;
		}

		public virtual void Test()
		{
			FixtureVariable f1 = new FixtureVariable();
			FixtureVariable f2 = new FixtureVariable();
			FixtureContextTestCase.ContextRef c1 = new FixtureContextTestCase.ContextRef();
			FixtureContextTestCase.ContextRef c2 = new FixtureContextTestCase.ContextRef();
			new FixtureContext().Run(new _IRunnable_19(this, f1, f2, c1, c2));
			AssertNoValue(f1);
			AssertNoValue(f2);
			c1.value.Run(new _IRunnable_41(this, f1, f2));
			c2.value.Run(new _IRunnable_48(this, f1, f2));
		}

		private sealed class _IRunnable_19 : IRunnable
		{
			public _IRunnable_19(FixtureContextTestCase _enclosing, FixtureVariable f1, FixtureVariable
				 f2, FixtureContextTestCase.ContextRef c1, FixtureContextTestCase.ContextRef c2)
			{
				this._enclosing = _enclosing;
				this.f1 = f1;
				this.f2 = f2;
				this.c1 = c1;
				this.c2 = c2;
			}

			public void Run()
			{
				f1.With("foo", new _IRunnable_21(this, f1, f2, c1, c2));
			}

			private sealed class _IRunnable_21 : IRunnable
			{
				public _IRunnable_21(_IRunnable_19 _enclosing, FixtureVariable f1, FixtureVariable
					 f2, FixtureContextTestCase.ContextRef c1, FixtureContextTestCase.ContextRef c2)
				{
					this._enclosing = _enclosing;
					this.f1 = f1;
					this.f2 = f2;
					this.c1 = c1;
					this.c2 = c2;
				}

				public void Run()
				{
					this._enclosing._enclosing.AssertValue("foo", f1);
					this._enclosing._enclosing.AssertNoValue(f2);
					c1.value = FixtureContext.Current;
					f2.With("bar", new _IRunnable_26(this, f1, f2, c2));
				}

				private sealed class _IRunnable_26 : IRunnable
				{
					public _IRunnable_26(_IRunnable_21 _enclosing, FixtureVariable f1, FixtureVariable
						 f2, FixtureContextTestCase.ContextRef c2)
					{
						this._enclosing = _enclosing;
						this.f1 = f1;
						this.f2 = f2;
						this.c2 = c2;
					}

					public void Run()
					{
						this._enclosing._enclosing._enclosing.AssertValue("foo", f1);
						this._enclosing._enclosing._enclosing.AssertValue("bar", f2);
						c2.value = FixtureContext.Current;
					}

					private readonly _IRunnable_21 _enclosing;

					private readonly FixtureVariable f1;

					private readonly FixtureVariable f2;

					private readonly FixtureContextTestCase.ContextRef c2;
				}

				private readonly _IRunnable_19 _enclosing;

				private readonly FixtureVariable f1;

				private readonly FixtureVariable f2;

				private readonly FixtureContextTestCase.ContextRef c1;

				private readonly FixtureContextTestCase.ContextRef c2;
			}

			private readonly FixtureContextTestCase _enclosing;

			private readonly FixtureVariable f1;

			private readonly FixtureVariable f2;

			private readonly FixtureContextTestCase.ContextRef c1;

			private readonly FixtureContextTestCase.ContextRef c2;
		}

		private sealed class _IRunnable_41 : IRunnable
		{
			public _IRunnable_41(FixtureContextTestCase _enclosing, FixtureVariable f1, FixtureVariable
				 f2)
			{
				this._enclosing = _enclosing;
				this.f1 = f1;
				this.f2 = f2;
			}

			public void Run()
			{
				this._enclosing.AssertValue("foo", f1);
				this._enclosing.AssertNoValue(f2);
			}

			private readonly FixtureContextTestCase _enclosing;

			private readonly FixtureVariable f1;

			private readonly FixtureVariable f2;
		}

		private sealed class _IRunnable_48 : IRunnable
		{
			public _IRunnable_48(FixtureContextTestCase _enclosing, FixtureVariable f1, FixtureVariable
				 f2)
			{
				this._enclosing = _enclosing;
				this.f1 = f1;
				this.f2 = f2;
			}

			public void Run()
			{
				this._enclosing.AssertValue("foo", f1);
				this._enclosing.AssertValue("bar", f2);
			}

			private readonly FixtureContextTestCase _enclosing;

			private readonly FixtureVariable f1;

			private readonly FixtureVariable f2;
		}

		private void AssertNoValue(FixtureVariable f1)
		{
			Assert.Expect(typeof(InvalidOperationException), new _ICodeBlock_57(f1));
		}

		private sealed class _ICodeBlock_57 : ICodeBlock
		{
			public _ICodeBlock_57(FixtureVariable f1)
			{
				this.f1 = f1;
			}

			public void Run()
			{
				this.Use(f1.Value);
			}

			private void Use(object value)
			{
			}

			private readonly FixtureVariable f1;
		}

		private void AssertValue(string expected, FixtureVariable fixture)
		{
			Assert.AreEqual(expected, fixture.Value);
		}
	}
}

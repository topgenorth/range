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
using Db4oUnit.Fixtures;
using Db4objects.Db4o.Foundation;
using Sharpen.Lang;

namespace Db4oUnit.Fixtures
{
	/// <summary>
	/// Set of live
	/// <see cref="FixtureVariable">FixtureVariable</see>
	/// /value pairs.
	/// </summary>
	public class FixtureContext
	{
		private sealed class _DynamicVariable_13 : DynamicVariable
		{
			public _DynamicVariable_13()
			{
				this.EmptyContext = new FixtureContext();
			}

			private readonly FixtureContext EmptyContext;

			protected override object DefaultValue()
			{
				return this.EmptyContext;
			}
		}

		private static readonly DynamicVariable _current = new _DynamicVariable_13();

		public static FixtureContext Current
		{
			get
			{
				return (FixtureContext)_current.Value;
			}
		}

		public virtual object Run(IClosure4 closure)
		{
			return _current.With(this, closure);
		}

		public virtual void Run(IRunnable block)
		{
			_current.With(this, block);
		}

		internal class Found
		{
			public readonly object value;

			public Found(object value_)
			{
				value = value_;
			}
		}

		internal virtual FixtureContext.Found Get(FixtureVariable fixture)
		{
			return null;
		}

		public virtual FixtureContext Combine(FixtureContext parent)
		{
			return new _FixtureContext_49(this, parent);
		}

		private sealed class _FixtureContext_49 : FixtureContext
		{
			public _FixtureContext_49(FixtureContext _enclosing, FixtureContext parent)
			{
				this._enclosing = _enclosing;
				this.parent = parent;
			}

			internal override FixtureContext.Found Get(FixtureVariable fixture)
			{
				FixtureContext.Found found = this._enclosing.Get(fixture);
				if (null != found)
				{
					return found;
				}
				return parent.Get(fixture);
			}

			private readonly FixtureContext _enclosing;

			private readonly FixtureContext parent;
		}

		internal virtual FixtureContext Add(FixtureVariable fixture, object value)
		{
			return new _FixtureContext_59(this, fixture, value);
		}

		private sealed class _FixtureContext_59 : FixtureContext
		{
			public _FixtureContext_59(FixtureContext _enclosing, FixtureVariable fixture, object
				 value)
			{
				this._enclosing = _enclosing;
				this.fixture = fixture;
				this.value = value;
			}

			internal override FixtureContext.Found Get(FixtureVariable key)
			{
				if (key == fixture)
				{
					return new FixtureContext.Found(value);
				}
				return this._enclosing.Get(key);
			}

			private readonly FixtureContext _enclosing;

			private readonly FixtureVariable fixture;

			private readonly object value;
		}
	}
}

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
using System.Collections;
using Db4oUnit.Fixtures;
using Db4objects.Db4o.Foundation;
using Sharpen.Lang;

namespace Db4oUnit.Fixtures
{
	public class ContextfulIterator : Contextful, IEnumerator
	{
		private readonly IEnumerator _delegate;

		public ContextfulIterator(IEnumerator delegate_)
		{
			_delegate = delegate_;
		}

		private sealed class _IClosure4_17 : IClosure4
		{
			public _IClosure4_17(ContextfulIterator _enclosing)
			{
				this._enclosing = _enclosing;
			}

			public object Run()
			{
				return this._enclosing._delegate.Current;
			}

			private readonly ContextfulIterator _enclosing;
		}

		public virtual object Current
		{
			get
			{
				return Run(new _IClosure4_17(this));
			}
		}

		public virtual bool MoveNext()
		{
			BooleanByRef result = new BooleanByRef();
			Run(new _IRunnable_26(this, result));
			return result.value;
		}

		private sealed class _IRunnable_26 : IRunnable
		{
			public _IRunnable_26(ContextfulIterator _enclosing, BooleanByRef result)
			{
				this._enclosing = _enclosing;
				this.result = result;
			}

			public void Run()
			{
				result.value = this._enclosing._delegate.MoveNext();
			}

			private readonly ContextfulIterator _enclosing;

			private readonly BooleanByRef result;
		}

		public virtual void Reset()
		{
			Run(new _IRunnable_35(this));
		}

		private sealed class _IRunnable_35 : IRunnable
		{
			public _IRunnable_35(ContextfulIterator _enclosing)
			{
				this._enclosing = _enclosing;
			}

			public void Run()
			{
				this._enclosing._delegate.Reset();
			}

			private readonly ContextfulIterator _enclosing;
		}
	}
}

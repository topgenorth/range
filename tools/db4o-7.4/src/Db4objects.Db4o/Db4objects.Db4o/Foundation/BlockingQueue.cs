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
using Db4objects.Db4o.Foundation;

namespace Db4objects.Db4o.Foundation
{
	/// <exclude></exclude>
	public class BlockingQueue : IQueue4
	{
		protected NonblockingQueue _queue = new NonblockingQueue();

		protected Lock4 _lock = new Lock4();

		protected bool _stopped;

		public virtual void Add(object obj)
		{
			_lock.Run(new _IClosure4_16(this, obj));
		}

		private sealed class _IClosure4_16 : IClosure4
		{
			public _IClosure4_16(BlockingQueue _enclosing, object obj)
			{
				this._enclosing = _enclosing;
				this.obj = obj;
			}

			public object Run()
			{
				this._enclosing._queue.Add(obj);
				this._enclosing._lock.Awake();
				return null;
			}

			private readonly BlockingQueue _enclosing;

			private readonly object obj;
		}

		public virtual bool HasNext()
		{
			bool hasNext = (bool)_lock.Run(new _IClosure4_26(this));
			return hasNext;
		}

		private sealed class _IClosure4_26 : IClosure4
		{
			public _IClosure4_26(BlockingQueue _enclosing)
			{
				this._enclosing = _enclosing;
			}

			public object Run()
			{
				return this._enclosing._queue.HasNext();
			}

			private readonly BlockingQueue _enclosing;
		}

		public virtual IEnumerator Iterator()
		{
			return (IEnumerator)_lock.Run(new _IClosure4_35(this));
		}

		private sealed class _IClosure4_35 : IClosure4
		{
			public _IClosure4_35(BlockingQueue _enclosing)
			{
				this._enclosing = _enclosing;
			}

			public object Run()
			{
				return this._enclosing._queue.Iterator();
			}

			private readonly BlockingQueue _enclosing;
		}

		/// <exception cref="BlockingQueueStoppedException"></exception>
		public virtual object Next()
		{
			return _lock.Run(new _IClosure4_43(this));
		}

		private sealed class _IClosure4_43 : IClosure4
		{
			public _IClosure4_43(BlockingQueue _enclosing)
			{
				this._enclosing = _enclosing;
			}

			public object Run()
			{
				if (this._enclosing._queue.HasNext())
				{
					return this._enclosing._queue.Next();
				}
				if (this._enclosing._stopped)
				{
					throw new BlockingQueueStoppedException();
				}
				this._enclosing._lock.Snooze(int.MaxValue);
				object obj = this._enclosing._queue.Next();
				if (obj == null)
				{
					throw new BlockingQueueStoppedException();
				}
				return obj;
			}

			private readonly BlockingQueue _enclosing;
		}

		public virtual void Stop()
		{
			_lock.Run(new _IClosure4_62(this));
		}

		private sealed class _IClosure4_62 : IClosure4
		{
			public _IClosure4_62(BlockingQueue _enclosing)
			{
				this._enclosing = _enclosing;
			}

			public object Run()
			{
				this._enclosing._stopped = true;
				this._enclosing._lock.Awake();
				return null;
			}

			private readonly BlockingQueue _enclosing;
		}

		public virtual object NextMatching(IPredicate4 condition)
		{
			return _lock.Run(new _IClosure4_72(this, condition));
		}

		private sealed class _IClosure4_72 : IClosure4
		{
			public _IClosure4_72(BlockingQueue _enclosing, IPredicate4 condition)
			{
				this._enclosing = _enclosing;
				this.condition = condition;
			}

			public object Run()
			{
				return this._enclosing._queue.NextMatching(condition);
			}

			private readonly BlockingQueue _enclosing;

			private readonly IPredicate4 condition;
		}
	}
}

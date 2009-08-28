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
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Sharpen.Lang;

namespace Db4objects.Db4o.Internal
{
	internal class WeakReferenceCollector : IRunnable
	{
		internal readonly object _queue;

		private readonly ObjectContainerBase _stream;

		private SimpleTimer _timer;

		public readonly bool _weak;

		internal WeakReferenceCollector(ObjectContainerBase a_stream)
		{
			_stream = a_stream;
			_weak = (!(a_stream is TransportObjectContainer) && Platform4.HasWeakReferences()
				 && a_stream.ConfigImpl().WeakReferences());
			_queue = _weak ? Platform4.CreateReferenceQueue() : null;
		}

		internal virtual object CreateYapRef(ObjectReference a_yo, object obj)
		{
			if (!_weak)
			{
				return obj;
			}
			return Platform4.CreateActiveObjectReference(_queue, a_yo, obj);
		}

		internal virtual void PollReferenceQueue()
		{
			if (!_weak)
			{
				return;
			}
			Platform4.PollReferenceQueue(_stream, _queue);
		}

		public virtual void Run()
		{
			try
			{
				PollReferenceQueue();
			}
			catch (Exception e)
			{
				// don't bring down the thread
				Sharpen.Runtime.PrintStackTrace(e);
			}
		}

		internal virtual void StartTimer()
		{
			if (!_weak)
			{
				return;
			}
			if (!_stream.ConfigImpl().WeakReferences())
			{
				return;
			}
			if (_stream.ConfigImpl().WeakReferenceCollectionInterval() <= 0)
			{
				return;
			}
			if (_timer != null)
			{
				return;
			}
			_timer = new SimpleTimer(this, _stream.ConfigImpl().WeakReferenceCollectionInterval
				(), "db4o WeakReference collector");
			_timer.Start();
		}

		internal virtual void StopTimer()
		{
			if (_timer == null)
			{
				return;
			}
			_timer.Stop();
			_timer = null;
		}
	}
}

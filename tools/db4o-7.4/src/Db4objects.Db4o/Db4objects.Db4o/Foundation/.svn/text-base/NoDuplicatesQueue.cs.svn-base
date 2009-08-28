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
	public class NoDuplicatesQueue : IQueue4
	{
		private IQueue4 _queue;

		private Hashtable4 _seen;

		public NoDuplicatesQueue(IQueue4 queue)
		{
			_queue = queue;
			_seen = new Hashtable4();
		}

		public virtual void Add(object obj)
		{
			if (_seen.ContainsKey(obj))
			{
				return;
			}
			_queue.Add(obj);
			_seen.Put(obj, obj);
		}

		public virtual bool HasNext()
		{
			return _queue.HasNext();
		}

		public virtual IEnumerator Iterator()
		{
			return _queue.Iterator();
		}

		public virtual object Next()
		{
			return _queue.Next();
		}

		public virtual object NextMatching(IPredicate4 condition)
		{
			return _queue.NextMatching(condition);
		}
	}
}

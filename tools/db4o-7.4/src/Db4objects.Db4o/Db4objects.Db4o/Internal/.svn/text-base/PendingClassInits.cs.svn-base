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
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;

namespace Db4objects.Db4o.Internal
{
	internal class PendingClassInits
	{
		private readonly Transaction _systemTransaction;

		private Collection4 _pending = new Collection4();

		private IQueue4 _members = new NonblockingQueue();

		private IQueue4 _statics = new NonblockingQueue();

		private IQueue4 _writes = new NonblockingQueue();

		private IQueue4 _inits = new NonblockingQueue();

		private bool _running = false;

		internal PendingClassInits(Transaction systemTransaction)
		{
			_systemTransaction = systemTransaction;
		}

		internal virtual void Process(ClassMetadata newYapClass)
		{
			if (_pending.Contains(newYapClass))
			{
				return;
			}
			ClassMetadata ancestor = newYapClass.GetAncestor();
			if (ancestor != null)
			{
				Process(ancestor);
			}
			_pending.Add(newYapClass);
			_members.Add(newYapClass);
			if (_running)
			{
				return;
			}
			_running = true;
			CheckInits();
			_pending = new Collection4();
			_running = false;
		}

		private void CheckMembers()
		{
			while (_members.HasNext())
			{
				ClassMetadata classMetadata = (ClassMetadata)_members.Next();
				classMetadata.AddMembers(Stream());
				_statics.Add(classMetadata);
			}
		}

		private ObjectContainerBase Stream()
		{
			return _systemTransaction.Container();
		}

		private void CheckStatics()
		{
			CheckMembers();
			while (_statics.HasNext())
			{
				ClassMetadata yc = (ClassMetadata)_statics.Next();
				yc.StoreStaticFieldValues(_systemTransaction, true);
				_writes.Add(yc);
				CheckMembers();
			}
		}

		private void CheckWrites()
		{
			CheckStatics();
			while (_writes.HasNext())
			{
				ClassMetadata yc = (ClassMetadata)_writes.Next();
				yc.SetStateDirty();
				yc.Write(_systemTransaction);
				_inits.Add(yc);
				CheckStatics();
			}
		}

		private void CheckInits()
		{
			CheckWrites();
			while (_inits.HasNext())
			{
				ClassMetadata yc = (ClassMetadata)_inits.Next();
				yc.InitConfigOnUp(_systemTransaction);
				CheckWrites();
			}
		}
	}
}

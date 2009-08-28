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
using System.Collections;
using Db4objects.Db4o;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.CS;

namespace Db4objects.Db4o.Internal.CS
{
	public class ClientTransactionPool
	{
		private readonly Hashtable4 _transaction2Container;

		private readonly Hashtable4 _fileName2Container;

		private readonly LocalObjectContainer _mainContainer;

		private bool _closed;

		public ClientTransactionPool(LocalObjectContainer mainContainer)
		{
			// Transaction -> ContainerCount
			// String -> ContainerCount
			ClientTransactionPool.ContainerCount mainEntry = new ClientTransactionPool.ContainerCount
				(mainContainer, 1);
			_transaction2Container = new Hashtable4();
			_fileName2Container = new Hashtable4();
			_fileName2Container.Put(mainContainer.FileName(), mainEntry);
			_mainContainer = mainContainer;
		}

		public virtual Transaction AcquireMain()
		{
			return Acquire(_mainContainer.FileName());
		}

		public virtual Transaction Acquire(string fileName)
		{
			lock (_mainContainer.Lock())
			{
				ClientTransactionPool.ContainerCount entry = (ClientTransactionPool.ContainerCount
					)_fileName2Container.Get(fileName);
				if (entry == null)
				{
					LocalObjectContainer container = (LocalObjectContainer)Db4oFactory.OpenFile(fileName
						);
					container.ConfigImpl().SetMessageRecipient(_mainContainer.ConfigImpl().MessageRecipient
						());
					entry = new ClientTransactionPool.ContainerCount(container);
					_fileName2Container.Put(fileName, entry);
				}
				Transaction transaction = entry.NewTransaction();
				_transaction2Container.Put(transaction, entry);
				return transaction;
			}
		}

		public virtual void Release(Transaction transaction, bool rollbackOnClose)
		{
			transaction.Close(rollbackOnClose);
			lock (_mainContainer.Lock())
			{
				ClientTransactionPool.ContainerCount entry = (ClientTransactionPool.ContainerCount
					)_transaction2Container.Get(transaction);
				_transaction2Container.Remove(transaction);
				entry.Release();
				if (entry.IsEmpty())
				{
					_fileName2Container.Remove(entry.FileName());
					entry.Close();
				}
			}
		}

		public virtual void Close()
		{
			lock (_mainContainer.Lock())
			{
				IEnumerator entryIter = _fileName2Container.Iterator();
				while (entryIter.MoveNext())
				{
					IEntry4 hashEntry = (IEntry4)entryIter.Current;
					((ClientTransactionPool.ContainerCount)hashEntry.Value()).Close();
				}
				_closed = true;
			}
		}

		public virtual int OpenTransactionCount()
		{
			return IsClosed() ? 0 : _transaction2Container.Size();
		}

		public virtual int OpenFileCount()
		{
			return IsClosed() ? 0 : _fileName2Container.Size();
		}

		public virtual bool IsClosed()
		{
			return _closed == true || _mainContainer.IsClosed();
		}

		private class ContainerCount
		{
			private LocalObjectContainer _container;

			private int _count;

			public ContainerCount(LocalObjectContainer container) : this(container, 0)
			{
			}

			public ContainerCount(LocalObjectContainer container, int count)
			{
				_container = container;
				_count = count;
			}

			public virtual bool IsEmpty()
			{
				return _count <= 0;
			}

			public virtual Transaction NewTransaction()
			{
				_count++;
				return _container.NewUserTransaction();
			}

			public virtual void Release()
			{
				if (_count == 0)
				{
					throw new InvalidOperationException();
				}
				_count--;
			}

			public virtual string FileName()
			{
				return _container.FileName();
			}

			public virtual void Close()
			{
				_container.Close();
				_container = null;
			}

			public override int GetHashCode()
			{
				return FileName().GetHashCode();
			}

			public override bool Equals(object obj)
			{
				if (this == obj)
				{
					return true;
				}
				if (obj == null || GetType() != obj.GetType())
				{
					return false;
				}
				ClientTransactionPool.ContainerCount other = (ClientTransactionPool.ContainerCount
					)obj;
				return FileName().Equals(other.FileName());
			}
		}
	}
}

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
using Db4objects.Db4o.Internal.CS;

namespace Db4objects.Db4o.Internal.CS
{
	public class ClientTransactionHandle
	{
		private readonly ClientTransactionPool _transactionPool;

		private Db4objects.Db4o.Internal.Transaction _mainTransaction;

		private Db4objects.Db4o.Internal.Transaction _transaction;

		private bool _rollbackOnClose;

		public ClientTransactionHandle(ClientTransactionPool transactionPool)
		{
			_transactionPool = transactionPool;
			_mainTransaction = _transactionPool.AcquireMain();
			_rollbackOnClose = true;
		}

		public virtual void AcquireTransactionForFile(string fileName)
		{
			_transaction = _transactionPool.Acquire(fileName);
		}

		public virtual void ReleaseTransaction()
		{
			if (_transaction != null)
			{
				_transactionPool.Release(_transaction, _rollbackOnClose);
				_transaction = null;
			}
		}

		public virtual bool IsClosed()
		{
			return _transactionPool.IsClosed();
		}

		public virtual void Close()
		{
			if ((!_transactionPool.IsClosed()) && (_mainTransaction != null))
			{
				_transactionPool.Release(_mainTransaction, _rollbackOnClose);
				_mainTransaction.Close(_rollbackOnClose);
			}
		}

		public virtual Db4objects.Db4o.Internal.Transaction Transaction()
		{
			if (_transaction != null)
			{
				return _transaction;
			}
			return _mainTransaction;
		}

		public virtual void Transaction(Db4objects.Db4o.Internal.Transaction transaction)
		{
			if (_transaction != null)
			{
				_transaction = transaction;
			}
			else
			{
				_mainTransaction = transaction;
			}
			_rollbackOnClose = false;
		}
	}
}

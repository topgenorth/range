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
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.CS;
using Db4objects.Db4o.Internal.CS.Messages;
using Db4objects.Db4o.Internal.Query.Result;

namespace Db4objects.Db4o.Internal.CS
{
	/// <exclude></exclude>
	public class LazyClientQueryResult : AbstractQueryResult
	{
		private const int SizeNotSet = -1;

		private readonly ClientObjectContainer _client;

		private readonly int _queryResultID;

		private int _size = SizeNotSet;

		private readonly LazyClientIdIterator _iterator;

		public LazyClientQueryResult(Transaction trans, ClientObjectContainer client, int
			 queryResultID) : base(trans)
		{
			_client = client;
			_queryResultID = queryResultID;
			_iterator = new LazyClientIdIterator(this);
		}

		public override object Get(int index)
		{
			lock (Lock())
			{
				return ActivatedObject(GetId(index));
			}
		}

		public override int GetId(int index)
		{
			return AskServer(Msg.ObjectsetGetId, index);
		}

		public override int IndexOf(int id)
		{
			return AskServer(Msg.ObjectsetIndexof, id);
		}

		private int AskServer(MsgD message, int param)
		{
			_client.Write(message.GetWriterForInts(_transaction, new int[] { _queryResultID, 
				param }));
			return ((MsgD)_client.ExpectedResponse(message)).ReadInt();
		}

		public override IIntIterator4 IterateIDs()
		{
			return _iterator;
		}

		public override IEnumerator GetEnumerator()
		{
			return ClientServerPlatform.CreateClientQueryResultIterator(this);
		}

		public override int Size()
		{
			if (_size == SizeNotSet)
			{
				_client.Write(Msg.ObjectsetSize.GetWriterForInt(_transaction, _queryResultID));
				_size = ((MsgD)_client.ExpectedResponse(Msg.ObjectsetSize)).ReadInt();
			}
			return _size;
		}

		~LazyClientQueryResult()
		{
			_client.Write(Msg.ObjectsetFinalized.GetWriterForInt(_transaction, _queryResultID
				));
		}

		public override void LoadFromIdReader(ByteArrayBuffer reader)
		{
			_iterator.LoadFromIdReader(reader, reader.ReadInt());
		}

		public virtual void Reset()
		{
			_client.Write(Msg.ObjectsetReset.GetWriterForInt(_transaction, _queryResultID));
		}

		public virtual void FetchIDs(int batchSize)
		{
			_client.Write(Msg.ObjectsetFetch.GetWriterForInts(_transaction, new int[] { _queryResultID
				, batchSize }));
			ByteArrayBuffer reader = _client.ExpectedByteResponse(Msg.IdList);
			LoadFromIdReader(reader);
		}
	}
}

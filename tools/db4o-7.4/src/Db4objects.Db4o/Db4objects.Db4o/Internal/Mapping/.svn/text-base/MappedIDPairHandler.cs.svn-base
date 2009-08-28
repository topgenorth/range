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
using Db4objects.Db4o.Internal.Handlers;
using Db4objects.Db4o.Internal.Mapping;
using Db4objects.Db4o.Marshall;

namespace Db4objects.Db4o.Internal.Mapping
{
	/// <exclude></exclude>
	public class MappedIDPairHandler : IIndexable4
	{
		private readonly IntHandler _origHandler;

		private readonly IntHandler _mappedHandler;

		public MappedIDPairHandler()
		{
			_origHandler = new IntHandler();
			_mappedHandler = new IntHandler();
		}

		public virtual void DefragIndexEntry(DefragmentContextImpl context)
		{
			throw new NotImplementedException();
		}

		public virtual int LinkLength()
		{
			return _origHandler.LinkLength() + _mappedHandler.LinkLength();
		}

		public virtual object ReadIndexEntry(ByteArrayBuffer reader)
		{
			int origID = ReadID(reader);
			int mappedID = ReadID(reader);
			return new MappedIDPair(origID, mappedID);
		}

		public virtual void WriteIndexEntry(ByteArrayBuffer reader, object obj)
		{
			MappedIDPair mappedIDs = (MappedIDPair)obj;
			_origHandler.WriteIndexEntry(reader, mappedIDs.Orig());
			_mappedHandler.WriteIndexEntry(reader, mappedIDs.Mapped());
		}

		private int ReadID(ByteArrayBuffer a_reader)
		{
			return ((int)_origHandler.ReadIndexEntry(a_reader));
		}

		public virtual IPreparedComparison PrepareComparison(IContext context, object source
			)
		{
			MappedIDPair sourceIDPair = (MappedIDPair)source;
			int sourceID = sourceIDPair.Orig();
			return new _IPreparedComparison_50(sourceID);
		}

		private sealed class _IPreparedComparison_50 : IPreparedComparison
		{
			public _IPreparedComparison_50(int sourceID)
			{
				this.sourceID = sourceID;
			}

			public int CompareTo(object target)
			{
				MappedIDPair targetIDPair = (MappedIDPair)target;
				int targetID = targetIDPair.Orig();
				return sourceID == targetID ? 0 : (sourceID < targetID ? -1 : 1);
			}

			private readonly int sourceID;
		}
	}
}

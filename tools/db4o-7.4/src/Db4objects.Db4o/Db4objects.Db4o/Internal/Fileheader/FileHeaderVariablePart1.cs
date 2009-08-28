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
using Db4objects.Db4o;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Activation;

namespace Db4objects.Db4o.Internal.Fileheader
{
	/// <exclude></exclude>
	public class FileHeaderVariablePart1 : PersistentBase
	{
		private const int Length = 1 + (Const4.IntLength * 4) + Const4.LongLength + Const4
			.AddedLength;

		private readonly Db4objects.Db4o.Internal.SystemData _systemData;

		public FileHeaderVariablePart1(int id, Db4objects.Db4o.Internal.SystemData systemData
			)
		{
			// The variable part format is:
			// (int) converter version
			// (byte) freespace system used
			// (int)  freespace address
			// (int) identity ID
			// (long) versionGenerator
			// (int) uuid index ID
			SetID(id);
			_systemData = systemData;
		}

		internal virtual Db4objects.Db4o.Internal.SystemData SystemData()
		{
			return _systemData;
		}

		public override byte GetIdentifier()
		{
			return Const4.Header;
		}

		public override int OwnLength()
		{
			return Length;
		}

		public override void ReadThis(Transaction trans, ByteArrayBuffer reader)
		{
			_systemData.ConverterVersion(reader.ReadInt());
			_systemData.FreespaceSystem(reader.ReadByte());
			_systemData.FreespaceAddress(reader.ReadInt());
			ReadIdentity((LocalTransaction)trans, reader.ReadInt());
			_systemData.LastTimeStampID(reader.ReadLong());
			_systemData.UuidIndexId(reader.ReadInt());
		}

		public override void WriteThis(Transaction trans, ByteArrayBuffer writer)
		{
			writer.WriteInt(_systemData.ConverterVersion());
			writer.WriteByte(_systemData.FreespaceSystem());
			writer.WriteInt(_systemData.FreespaceAddress());
			writer.WriteInt(_systemData.Identity().GetID(trans));
			writer.WriteLong(_systemData.LastTimeStampID());
			writer.WriteInt(_systemData.UuidIndexId());
		}

		private void ReadIdentity(LocalTransaction trans, int identityID)
		{
			LocalObjectContainer file = trans.File();
			Db4oDatabase identity = Debug.staticIdentity ? Db4oDatabase.StaticIdentity : (Db4oDatabase
				)file.GetByID(trans, identityID);
			file.Activate(trans, identity, new FixedActivationDepth(2));
			_systemData.Identity(identity);
		}
	}
}

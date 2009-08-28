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
using Db4objects.Db4o.Internal.Fileheader;

namespace Db4objects.Db4o.Internal.Fileheader
{
	/// <exclude></exclude>
	public class FileHeader0 : FileHeader
	{
		internal const int HeaderLength = 2 + (Const4.IntLength * 4);

		private ConfigBlock _configBlock;

		private PBootRecord _bootRecord;

		// The header format is:
		// Old format
		// -------------------------
		// {
		// Y
		// [Rest]
		// New format
		// -------------------------
		// (byte)4
		// block size in bytes 1 to 127
		// [Rest]
		// Rest (only ints)
		// -------------------
		// address of the extended configuration block, see YapConfigBlock
		// headerLock
		// YapClassCollection ID
		// FreeBySize ID
		/// <exception cref="Db4oIOException"></exception>
		public override void Close()
		{
			_configBlock.Close();
		}

		protected override FileHeader NewOnSignatureMatch(LocalObjectContainer file, ByteArrayBuffer
			 reader)
		{
			byte firstFileByte = reader.ReadByte();
			if (firstFileByte != Const4.Yapbegin)
			{
				if (firstFileByte != Const4.Yapfileversion)
				{
					return null;
				}
				file.BlockSizeReadFromFile(reader.ReadByte());
			}
			else
			{
				if (reader.ReadByte() != Const4.Yapfile)
				{
					return null;
				}
			}
			return new FileHeader0();
		}

		/// <exception cref="OldFormatException"></exception>
		protected override void ReadFixedPart(LocalObjectContainer file, ByteArrayBuffer 
			reader)
		{
			_configBlock = ConfigBlock.ForExistingFile(file, reader.ReadInt());
			SkipConfigurationLockTime(reader);
			ReadClassCollectionAndFreeSpace(file, reader);
		}

		private void SkipConfigurationLockTime(ByteArrayBuffer reader)
		{
			reader.IncrementOffset(Const4.IdLength);
		}

		public override void ReadVariablePart(LocalObjectContainer file)
		{
			if (_configBlock._bootRecordID <= 0)
			{
				return;
			}
			object bootRecord = Debug.readBootRecord ? GetBootRecord(file) : null;
			if (!(bootRecord is PBootRecord))
			{
				InitBootRecord(file);
				file.GenerateNewIdentity();
				return;
			}
			_bootRecord = (PBootRecord)bootRecord;
			file.Activate(bootRecord, int.MaxValue);
			file.SetNextTimeStampId(_bootRecord.i_versionGenerator);
			file.SystemData().Identity(_bootRecord.i_db);
		}

		private object GetBootRecord(LocalObjectContainer file)
		{
			file.ShowInternalClasses(true);
			try
			{
				return file.GetByID(file.SystemTransaction(), _configBlock._bootRecordID);
			}
			finally
			{
				file.ShowInternalClasses(false);
			}
		}

		/// <exception cref="Db4oIOException"></exception>
		public override void InitNew(LocalObjectContainer file)
		{
			_configBlock = ConfigBlock.ForNewFile(file);
			InitBootRecord(file);
		}

		private void InitBootRecord(LocalObjectContainer file)
		{
			file.ShowInternalClasses(true);
			try
			{
				_bootRecord = new PBootRecord();
				file.StoreInternal(file.SystemTransaction(), _bootRecord, false);
				_configBlock._bootRecordID = file.GetID(file.SystemTransaction(), _bootRecord);
				WriteVariablePart(file, 1);
			}
			finally
			{
				file.ShowInternalClasses(false);
			}
		}

		public override Transaction InterruptedTransaction()
		{
			return _configBlock.GetTransactionToCommit();
		}

		public override void WriteTransactionPointer(Transaction systemTransaction, int transactionAddress
			)
		{
			WriteTransactionPointer(systemTransaction, transactionAddress, _configBlock.Address
				(), ConfigBlock.TransactionOffset);
		}

		public virtual MetaIndex GetUUIDMetaIndex()
		{
			return _bootRecord.GetUUIDMetaIndex();
		}

		public override int Length()
		{
			return HeaderLength;
		}

		public override void WriteFixedPart(LocalObjectContainer file, bool startFileLockingThread
			, bool shuttingDown, StatefulBuffer writer, int blockSize_, int freespaceID)
		{
			writer.WriteByte(Const4.Yapfileversion);
			writer.WriteByte((byte)blockSize_);
			writer.WriteInt(_configBlock.Address());
			writer.WriteInt((int)TimeToWrite(_configBlock.OpenTime(), shuttingDown));
			writer.WriteInt(file.SystemData().ClassCollectionID());
			writer.WriteInt(freespaceID);
			if (Debug.xbytes && Deploy.overwrite)
			{
				writer.SetID(Const4.IgnoreId);
			}
			writer.Write();
			file.SyncFiles();
		}

		public override void WriteVariablePart(LocalObjectContainer file, int part)
		{
			if (part == 1)
			{
				_configBlock.Write();
			}
			else
			{
				if (part == 2)
				{
					_bootRecord.Write(file);
				}
			}
		}
	}
}

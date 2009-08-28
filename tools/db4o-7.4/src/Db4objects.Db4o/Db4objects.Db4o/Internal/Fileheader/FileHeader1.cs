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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Fileheader;
using Sharpen;

namespace Db4objects.Db4o.Internal.Fileheader
{
	/// <exclude></exclude>
	public class FileHeader1 : FileHeader
	{
		private static readonly byte[] Signature = new byte[] { (byte)'d', (byte)'b', (byte
			)'4', (byte)'o' };

		private static byte Version = 1;

		private static readonly int HeaderLockOffset = Signature.Length + 1;

		private static readonly int OpenTimeOffset = HeaderLockOffset + Const4.IntLength;

		private static readonly int AccessTimeOffset = OpenTimeOffset + Const4.LongLength;

		private static readonly int TransactionPointerOffset = AccessTimeOffset + Const4.
			LongLength;

		public static readonly int HeaderLength = TransactionPointerOffset + (Const4.IntLength
			 * 6);

		private TimerFileLock _timerFileLock;

		private Transaction _interruptedTransaction;

		private FileHeaderVariablePart1 _variablePart;

		// The header format is:
		// (byte) 'd'
		// (byte) 'b'
		// (byte) '4'
		// (byte) 'o'
		// (byte) headerVersion
		// (int) headerLock
		// (long) openTime
		// (long) accessTime
		// (int) Transaction pointer 1
		// (int) Transaction pointer 2
		// (int) blockSize
		// (int) classCollectionID
		// (int) freespaceID
		// (int) variablePartID
		/// <exception cref="Db4oIOException"></exception>
		public override void Close()
		{
			_timerFileLock.Close();
		}

		/// <exception cref="Db4oIOException"></exception>
		public override void InitNew(LocalObjectContainer file)
		{
			CommonTasksForNewAndRead(file);
			_variablePart = new FileHeaderVariablePart1(0, file.SystemData());
			WriteVariablePart(file, 0);
		}

		protected override FileHeader NewOnSignatureMatch(LocalObjectContainer file, ByteArrayBuffer
			 reader)
		{
			if (SignatureMatches(reader, Signature, Version))
			{
				return new FileHeader1();
			}
			return null;
		}

		private void NewTimerFileLock(LocalObjectContainer file)
		{
			_timerFileLock = TimerFileLock.ForFile(file);
			_timerFileLock.SetAddresses(0, OpenTimeOffset, AccessTimeOffset);
		}

		public override Transaction InterruptedTransaction()
		{
			return _interruptedTransaction;
		}

		public override int Length()
		{
			return HeaderLength;
		}

		protected override void ReadFixedPart(LocalObjectContainer file, ByteArrayBuffer 
			reader)
		{
			CommonTasksForNewAndRead(file);
			CheckThreadFileLock(file, reader);
			reader.Seek(TransactionPointerOffset);
			_interruptedTransaction = LocalTransaction.ReadInterruptedTransaction(file, reader
				);
			file.BlockSizeReadFromFile(reader.ReadInt());
			ReadClassCollectionAndFreeSpace(file, reader);
			_variablePart = new FileHeaderVariablePart1(reader.ReadInt(), file.SystemData());
		}

		private void CheckThreadFileLock(LocalObjectContainer container, ByteArrayBuffer 
			reader)
		{
			reader.Seek(AccessTimeOffset);
			long lastAccessTime = reader.ReadLong();
			if (FileHeader.LockedByOtherSession(container, lastAccessTime))
			{
				_timerFileLock.CheckIfOtherSessionAlive(container, 0, AccessTimeOffset, lastAccessTime
					);
			}
		}

		private void CommonTasksForNewAndRead(LocalObjectContainer file)
		{
			NewTimerFileLock(file);
			file._handlers.OldEncryptionOff();
		}

		public override void ReadVariablePart(LocalObjectContainer file)
		{
			_variablePart.Read(file.SystemTransaction());
		}

		public override void WriteFixedPart(LocalObjectContainer file, bool startFileLockingThread
			, bool shuttingDown, StatefulBuffer writer, int blockSize, int freespaceID)
		{
			writer.Append(Signature);
			writer.WriteByte(Version);
			writer.WriteInt((int)TimeToWrite(_timerFileLock.OpenTime(), shuttingDown));
			writer.WriteLong(TimeToWrite(_timerFileLock.OpenTime(), shuttingDown));
			writer.WriteLong(TimeToWrite(Runtime.CurrentTimeMillis(), shuttingDown));
			writer.WriteInt(0);
			// transaction pointer 1 for "in-commit-mode"
			writer.WriteInt(0);
			// transaction pointer 2
			writer.WriteInt(blockSize);
			writer.WriteInt(file.SystemData().ClassCollectionID());
			writer.WriteInt(freespaceID);
			writer.WriteInt(_variablePart.GetID());
			writer.NoXByteCheck();
			writer.Write();
			file.SyncFiles();
			if (startFileLockingThread)
			{
				_timerFileLock.Start();
			}
		}

		public override void WriteTransactionPointer(Transaction systemTransaction, int transactionAddress
			)
		{
			WriteTransactionPointer(systemTransaction, transactionAddress, 0, TransactionPointerOffset
				);
		}

		public override void WriteVariablePart(LocalObjectContainer file, int part)
		{
			_variablePart.SetStateDirty();
			_variablePart.Write(file.SystemTransaction());
		}
	}
}

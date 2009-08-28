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
using Db4objects.Db4o.IO;
using Db4objects.Db4o.Tests.Common.Acid;
using Sharpen;

namespace Db4objects.Db4o.Tests.Common.Acid
{
	public class CrashSimulatingIoAdapter : Db4objects.Db4o.IO.VanillaIoAdapter
	{
		internal CrashSimulatingBatch batch;

		internal long curPos;

		public CrashSimulatingIoAdapter(IoAdapter delegateAdapter) : base(delegateAdapter
			)
		{
			batch = new CrashSimulatingBatch();
		}

		/// <exception cref="Db4oIOException"></exception>
		private CrashSimulatingIoAdapter(IoAdapter delegateAdapter, string path, bool lockFile
			, long initialLength, bool readOnly, CrashSimulatingBatch batch) : base(delegateAdapter
			.Open(path, lockFile, initialLength, readOnly))
		{
			this.batch = batch;
		}

		/// <exception cref="Db4oIOException"></exception>
		public override IoAdapter Open(string path, bool lockFile, long initialLength, bool
			 readOnly)
		{
			return new Db4objects.Db4o.Tests.Common.Acid.CrashSimulatingIoAdapter(_delegate, 
				path, lockFile, initialLength, readOnly, batch);
		}

		/// <exception cref="Db4oIOException"></exception>
		public override int Read(byte[] bytes, int length)
		{
			int readBytes = base.Read(bytes, length);
			if (readBytes > 0)
			{
				curPos += readBytes;
			}
			return readBytes;
		}

		/// <exception cref="Db4oIOException"></exception>
		public override void Seek(long pos)
		{
			curPos = pos;
			base.Seek(pos);
		}

		/// <exception cref="Db4oIOException"></exception>
		public override void Write(byte[] buffer, int length)
		{
			base.Write(buffer, length);
			byte[] copy = new byte[buffer.Length];
			System.Array.Copy(buffer, 0, copy, 0, length);
			batch.Add(copy, curPos, length);
			curPos += length;
		}

		/// <exception cref="Db4oIOException"></exception>
		public override void Sync()
		{
			base.Sync();
			batch.Sync();
		}
	}
}

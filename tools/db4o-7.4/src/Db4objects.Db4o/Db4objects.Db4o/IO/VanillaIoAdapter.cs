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

namespace Db4objects.Db4o.IO
{
	/// <summary>base class for IoAdapters that delegate to other IoAdapters (decorator pattern)
	/// 	</summary>
	public abstract class VanillaIoAdapter : IoAdapter
	{
		protected IoAdapter _delegate;

		public VanillaIoAdapter(IoAdapter delegateAdapter)
		{
			_delegate = delegateAdapter;
		}

		/// <exception cref="Db4oIOException"></exception>
		protected VanillaIoAdapter(IoAdapter delegateAdapter, string path, bool lockFile, 
			long initialLength, bool readOnly) : this(delegateAdapter.Open(path, lockFile, initialLength
			, readOnly))
		{
		}

		/// <exception cref="Db4oIOException"></exception>
		public override void Close()
		{
			_delegate.Close();
		}

		public override void Delete(string path)
		{
			_delegate.Delete(path);
		}

		public override bool Exists(string path)
		{
			return _delegate.Exists(path);
		}

		/// <exception cref="Db4oIOException"></exception>
		public override long GetLength()
		{
			return _delegate.GetLength();
		}

		/// <exception cref="Db4oIOException"></exception>
		public override int Read(byte[] bytes, int length)
		{
			return _delegate.Read(bytes, length);
		}

		/// <exception cref="Db4oIOException"></exception>
		public override void Seek(long pos)
		{
			_delegate.Seek(pos);
		}

		/// <exception cref="Db4oIOException"></exception>
		public override void Sync()
		{
			_delegate.Sync();
		}

		/// <exception cref="Db4oIOException"></exception>
		public override void Write(byte[] buffer, int length)
		{
			_delegate.Write(buffer, length);
		}
	}
}

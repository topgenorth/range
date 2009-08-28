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
using Db4objects.Db4o.Foundation;
using Sharpen;

namespace Db4objects.Db4o.Foundation.Network
{
	/// <summary>
	/// Transport buffer for C/S mode to simulate a
	/// socket connection in memory.
	/// </summary>
	/// <remarks>
	/// Transport buffer for C/S mode to simulate a
	/// socket connection in memory.
	/// </remarks>
	internal class BlockingByteChannel
	{
		private const int DiscardBufferSize = 500;

		protected byte[] i_cache;

		internal bool i_closed = false;

		protected int i_readOffset;

		private int i_timeout;

		protected int i_writeOffset;

		protected readonly Lock4 i_lock = new Lock4();

		public BlockingByteChannel(int timeout)
		{
			i_timeout = timeout;
		}

		protected virtual int Available()
		{
			return i_writeOffset - i_readOffset;
		}

		protected virtual void CheckDiscardCache()
		{
			if (i_readOffset == i_writeOffset && i_cache.Length > DiscardBufferSize)
			{
				i_cache = null;
				i_readOffset = 0;
				i_writeOffset = 0;
			}
		}

		internal virtual void Close()
		{
			i_lock.Run(new _IClosure4_39(this));
		}

		private sealed class _IClosure4_39 : IClosure4
		{
			public _IClosure4_39(BlockingByteChannel _enclosing)
			{
				this._enclosing = _enclosing;
			}

			public object Run()
			{
				this._enclosing.i_closed = true;
				this._enclosing.i_lock.Awake();
				return null;
			}

			private readonly BlockingByteChannel _enclosing;
		}

		protected virtual void Makefit(int length)
		{
			if (i_cache == null)
			{
				i_cache = new byte[length];
			}
			else
			{
				// doesn't fit
				if (i_writeOffset + length > i_cache.Length)
				{
					// move, if possible
					if (i_writeOffset + length - i_readOffset <= i_cache.Length)
					{
						byte[] temp = new byte[i_cache.Length];
						System.Array.Copy(i_cache, i_readOffset, temp, 0, i_cache.Length - i_readOffset);
						i_cache = temp;
						i_writeOffset -= i_readOffset;
						i_readOffset = 0;
					}
					else
					{
						// else append
						byte[] temp = new byte[i_writeOffset + length];
						System.Array.Copy(i_cache, 0, temp, 0, i_cache.Length);
						i_cache = temp;
					}
				}
			}
		}

		/// <exception cref="Db4oIOException"></exception>
		public virtual int Read()
		{
			int ret = (int)i_lock.Run(new _IClosure4_73(this));
			return ret;
		}

		private sealed class _IClosure4_73 : IClosure4
		{
			public _IClosure4_73(BlockingByteChannel _enclosing)
			{
				this._enclosing = _enclosing;
			}

			public object Run()
			{
				this._enclosing.WaitForAvailable();
				int retVal = this._enclosing.i_cache[this._enclosing.i_readOffset++];
				this._enclosing.CheckDiscardCache();
				return retVal;
			}

			private readonly BlockingByteChannel _enclosing;
		}

		/// <exception cref="Db4oIOException"></exception>
		public virtual int Read(byte[] bytes, int offset, int length)
		{
			int ret = (int)i_lock.Run(new _IClosure4_87(this, length, bytes, offset));
			return ret;
		}

		private sealed class _IClosure4_87 : IClosure4
		{
			public _IClosure4_87(BlockingByteChannel _enclosing, int length, byte[] bytes, int
				 offset)
			{
				this._enclosing = _enclosing;
				this.length = length;
				this.bytes = bytes;
				this.offset = offset;
			}

			public object Run()
			{
				this._enclosing.WaitForAvailable();
				int avail = this._enclosing.Available();
				int toRead = length;
				if (avail < length)
				{
					toRead = avail;
				}
				System.Array.Copy(this._enclosing.i_cache, this._enclosing.i_readOffset, bytes, offset
					, toRead);
				this._enclosing.i_readOffset += toRead;
				this._enclosing.CheckDiscardCache();
				return toRead;
			}

			private readonly BlockingByteChannel _enclosing;

			private readonly int length;

			private readonly byte[] bytes;

			private readonly int offset;
		}

		public virtual void SetTimeout(int timeout)
		{
			i_timeout = timeout;
		}

		protected virtual void WaitForAvailable()
		{
			long beginTime = Runtime.CurrentTimeMillis();
			while (Available() == 0)
			{
				CheckClosed();
				i_lock.Snooze(i_timeout);
				if (IsTimeout(beginTime))
				{
					throw new Db4oIOException();
				}
			}
		}

		private bool IsTimeout(long start)
		{
			return Runtime.CurrentTimeMillis() - start >= i_timeout;
		}

		/// <exception cref="Db4oIOException"></exception>
		public virtual void Write(byte[] bytes)
		{
			Write(bytes, 0, bytes.Length);
		}

		/// <exception cref="Db4oIOException"></exception>
		public virtual void Write(byte[] bytes, int off, int len)
		{
			i_lock.Run(new _IClosure4_128(this, len, bytes, off));
		}

		private sealed class _IClosure4_128 : IClosure4
		{
			public _IClosure4_128(BlockingByteChannel _enclosing, int len, byte[] bytes, int 
				off)
			{
				this._enclosing = _enclosing;
				this.len = len;
				this.bytes = bytes;
				this.off = off;
			}

			public object Run()
			{
				this._enclosing.CheckClosed();
				this._enclosing.Makefit(len);
				System.Array.Copy(bytes, off, this._enclosing.i_cache, this._enclosing.i_writeOffset
					, len);
				this._enclosing.i_writeOffset += len;
				this._enclosing.i_lock.Awake();
				return null;
			}

			private readonly BlockingByteChannel _enclosing;

			private readonly int len;

			private readonly byte[] bytes;

			private readonly int off;
		}

		/// <exception cref="Db4oIOException"></exception>
		public virtual void Write(int i)
		{
			i_lock.Run(new _IClosure4_141(this, i));
		}

		private sealed class _IClosure4_141 : IClosure4
		{
			public _IClosure4_141(BlockingByteChannel _enclosing, int i)
			{
				this._enclosing = _enclosing;
				this.i = i;
			}

			public object Run()
			{
				this._enclosing.CheckClosed();
				this._enclosing.Makefit(1);
				this._enclosing.i_cache[this._enclosing.i_writeOffset++] = (byte)i;
				this._enclosing.i_lock.Awake();
				return null;
			}

			private readonly BlockingByteChannel _enclosing;

			private readonly int i;
		}

		public virtual void CheckClosed()
		{
			if (i_closed)
			{
				throw new Db4oIOException();
			}
		}
	}
}

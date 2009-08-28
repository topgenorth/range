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
using Db4objects.Db4o.IO;

namespace Db4objects.Db4o.IO
{
	/// <summary>Bounded handle into an IoAdapter: Can only access a restricted area.</summary>
	/// <remarks>Bounded handle into an IoAdapter: Can only access a restricted area.</remarks>
	public class IoAdapterWindow
	{
		private IoAdapter _io;

		private int _blockOff;

		private int _len;

		private bool _disabled;

		/// <param name="io">The delegate I/O adapter</param>
		/// <param name="blockOff">The block offset address into the I/O adapter that maps to the start index (0) of this window
		/// 	</param>
		/// <param name="len">The size of this window in bytes</param>
		public IoAdapterWindow(IoAdapter io, int blockOff, int len)
		{
			_io = io;
			_blockOff = blockOff;
			_len = len;
			_disabled = false;
		}

		/// <returns>Size of this I/O adapter window in bytes.</returns>
		public virtual int Length()
		{
			return _len;
		}

		/// <param name="off">Offset in bytes relative to the window start</param>
		/// <param name="data">Data to write into the window starting from the given offset</param>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="InvalidOperationException"></exception>
		public virtual void Write(int off, byte[] data)
		{
			CheckBounds(off, data);
			_io.BlockSeek(_blockOff + off);
			_io.Write(data);
		}

		/// <param name="off">Offset in bytes relative to the window start</param>
		/// <param name="data">Data buffer to read from the window starting from the given offset
		/// 	</param>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="InvalidOperationException"></exception>
		public virtual int Read(int off, byte[] data)
		{
			CheckBounds(off, data);
			_io.BlockSeek(_blockOff + off);
			return _io.Read(data);
		}

		/// <summary>Disable IO Adapter Window</summary>
		public virtual void Disable()
		{
			_disabled = true;
		}

		/// <summary>Flush IO Adapter Window</summary>
		public virtual void Flush()
		{
			if (!_disabled)
			{
				_io.Sync();
			}
		}

		private void CheckBounds(int off, byte[] data)
		{
			if (_disabled)
			{
				throw new InvalidOperationException();
			}
			if (data == null || off < 0 || off + data.Length > _len)
			{
				throw new ArgumentException();
			}
		}
	}
}

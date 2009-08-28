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
using System.IO;
using Db4objects.Db4o;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.IO;
using Db4objects.Db4o.Internal;
using Sharpen.IO;

namespace Db4objects.Db4o.IO
{
	/// <summary>IO adapter for random access files.</summary>
	/// <remarks>IO adapter for random access files.</remarks>
	public class RandomAccessFileAdapter : IoAdapter
	{
		private string _path;

		private RandomAccessFile _delegate;

		public RandomAccessFileAdapter()
		{
		}

		/// <exception cref="Db4oIOException"></exception>
		protected RandomAccessFileAdapter(string path, bool lockFile, long initialLength, 
			bool readOnly)
		{
			bool ok = false;
			try
			{
				_path = new Sharpen.IO.File(path).GetCanonicalPath();
				_delegate = new RandomAccessFile(_path, readOnly ? "r" : "rw");
				if (initialLength > 0)
				{
					_delegate.Seek(initialLength - 1);
					_delegate.Write(new byte[] { 0 });
				}
				if (lockFile)
				{
					Platform4.LockFile(_path, _delegate);
				}
				ok = true;
			}
			catch (IOException e)
			{
				throw new Db4oIOException(e);
			}
			finally
			{
				if (!ok)
				{
					Close();
				}
			}
		}

		/// <exception cref="Db4oIOException"></exception>
		public override void Close()
		{
			// FIXME: This is a temporary quickfix for a bug in Android.
			//        Remove after Android has been fixed.
			try
			{
				if (_delegate != null)
				{
					_delegate.Seek(0);
				}
			}
			catch (IOException)
			{
			}
			// ignore
			Platform4.UnlockFile(_path, _delegate);
			try
			{
				if (_delegate != null)
				{
					_delegate.Close();
				}
			}
			catch (IOException e)
			{
				throw new Db4oIOException(e);
			}
		}

		public override void Delete(string path)
		{
			new Sharpen.IO.File(path).Delete();
		}

		public override bool Exists(string path)
		{
			Sharpen.IO.File existingFile = new Sharpen.IO.File(path);
			return existingFile.Exists() && existingFile.Length() > 0;
		}

		/// <exception cref="Db4oIOException"></exception>
		public override long GetLength()
		{
			try
			{
				return _delegate.Length();
			}
			catch (IOException e)
			{
				throw new Db4oIOException(e);
			}
		}

		/// <exception cref="Db4oIOException"></exception>
		public override IoAdapter Open(string path, bool lockFile, long initialLength, bool
			 readOnly)
		{
			return new Db4objects.Db4o.IO.RandomAccessFileAdapter(path, lockFile, initialLength
				, readOnly);
		}

		/// <exception cref="Db4oIOException"></exception>
		public override int Read(byte[] bytes, int length)
		{
			try
			{
				return _delegate.Read(bytes, 0, length);
			}
			catch (IOException e)
			{
				throw new Db4oIOException(e);
			}
		}

		/// <exception cref="Db4oIOException"></exception>
		public override void Seek(long pos)
		{
			if (DTrace.enabled)
			{
				DTrace.RegularSeek.Log(pos);
			}
			try
			{
				_delegate.Seek(pos);
			}
			catch (IOException e)
			{
				throw new Db4oIOException(e);
			}
		}

		/// <exception cref="Db4oIOException"></exception>
		public override void Sync()
		{
			try
			{
				_delegate.GetFD().Sync();
			}
			catch (IOException e)
			{
				throw new Db4oIOException(e);
			}
		}

		/// <exception cref="Db4oIOException"></exception>
		public override void Write(byte[] buffer, int length)
		{
			try
			{
				_delegate.Write(buffer, 0, length);
			}
			catch (IOException e)
			{
				throw new Db4oIOException(e);
			}
		}
	}
}

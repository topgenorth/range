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
using System.IO;
using Db4objects.Db4o;
using Db4objects.Db4o.Ext;

namespace Sharpen.IO
{
    public class RandomAccessFile
    {
        private FileStream _stream;

#if !CF && !MONO
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        static extern int FlushFileBuffers(IntPtr fileHandle);
#endif

        public RandomAccessFile(String file, String fileMode)
        {
            try
            {
                _stream = new FileStream(file, FileMode.OpenOrCreate,
                    fileMode.Equals("rw") ? FileAccess.ReadWrite : FileAccess.Read);
            }
            catch (IOException x)
            {
                throw new DatabaseFileLockedException(file,x);
            }
        }

        public FileStream Stream
        {
            get { return _stream; }
        }

        public void Close()
        {
            _stream.Close();
        }

        public long Length()
        {
            return _stream.Length;
        }

        public int Read(byte[] bytes, int offset, int length)
        {
            return _stream.Read(bytes, offset, length);
        }

        public void Read(byte[] bytes)
        {
            _stream.Read(bytes, 0, bytes.Length);
        }

        public void Seek(long pos)
        {
            _stream.Seek(pos, SeekOrigin.Begin);
        }

        public void Sync()
        {
            _stream.Flush();

#if !CF && !MONO
            FlushFileBuffers(_stream.SafeFileHandle.DangerousGetHandle());
#endif
        }

        public RandomAccessFile GetFD()
        {
            return this;
        }

        public void Write(byte[] bytes)
        {
            Write(bytes, 0, bytes.Length);
        }

        public void Write(byte[] bytes, int offset, int length)
        {
            try
            {
                _stream.Write(bytes, offset, length);
            }
            catch (System.NotSupportedException e)
            {
                throw new Db4oIOException(e);
            }
        }
    }
}

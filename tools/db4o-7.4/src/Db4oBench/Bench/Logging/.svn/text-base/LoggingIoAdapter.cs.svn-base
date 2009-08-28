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
using Db4objects.Db4o.Bench.Logging;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.IO;
using Sharpen.IO;

namespace Db4objects.Db4o.Bench.Logging
{
	public class LoggingIoAdapter : VanillaIoAdapter
	{
		public const int LogRead = 1;

		public const int LogWrite = 2;

		public const int LogSync = 4;

		public const int LogSeek = 8;

		public const int LogAll = LogRead + LogWrite + LogSync + LogSeek;

		private readonly string _fileName;

		private readonly TextWriter _out;

		private int _config;

		public LoggingIoAdapter(IoAdapter delegateAdapter, string fileName, int config) : 
			base(delegateAdapter)
		{
			_fileName = fileName;
            try
            {
                _out = new StreamWriter(new FileStream(fileName, FileMode.Append, FileAccess.Write));
            }
            catch (FileNotFoundException e)
            {
                throw new Db4oIOException(e);
            }
        
			_config = config;
		}

        public LoggingIoAdapter(IoAdapter delegateAdapter, string fileName, int config, TextWriter existingOut)
            :
            base(delegateAdapter)
        {
            _fileName = fileName;
            _out = existingOut;
            _config = config;
        }


        public LoggingIoAdapter(IoAdapter delegateAdapter, string fileName) : this(delegateAdapter
			, fileName, LogAll)
		{
		}

		/// <exception cref="Db4oIOException"></exception>
		private LoggingIoAdapter(IoAdapter delegateAdapter, string path, bool lockFile, long
			 initialLength, string fileName, int config) : this(delegateAdapter.Open(path, lockFile
			, initialLength, false), fileName, config)
		{
		}

        private LoggingIoAdapter(IoAdapter delegateAdapter, string path, bool lockFile, long
             initialLength, string fileName, int config, TextWriter existingOut)
            : this(delegateAdapter.Open(path, lockFile
            , initialLength, false), fileName, config, existingOut)
        {
        }
		/// <exception cref="Db4oIOException"></exception>
		public override IoAdapter Open(string path, bool lockFile, long initialLength, bool
			 readOnly)
		{
            //if (_out == null)
            //{
                return new Db4objects.Db4o.Bench.Logging.LoggingIoAdapter(_delegate, path, lockFile
                    , initialLength, _fileName, _config);
            //}
            //else
            //{
            //    return new Db4objects.Db4o.Bench.Logging.LoggingIoAdapter(_delegate, path, lockFile
            //        , initialLength, _fileName, _config, _out);
            //}
		}

		/// <exception cref="Db4oIOException"></exception>
		public override void Close()
		{
			_out.Flush();
			_out.Close();
			base.Close();
		}

		/// <exception cref="Db4oIOException"></exception>
		public override int Read(byte[] bytes, int length)
		{
			if (Config(LogRead))
			{
				Println(LogConstants.ReadEntry + length);
			}
			return _delegate.Read(bytes, length);
		}

		/// <exception cref="Db4oIOException"></exception>
		public override void Seek(long pos)
		{
			if (Config(LogSeek))
			{
				Println(LogConstants.SeekEntry + pos);
			}
			_delegate.Seek(pos);
		}

		/// <exception cref="Db4oIOException"></exception>
		public override void Sync()
		{
			if (Config(LogSync))
			{
				Println(LogConstants.SyncEntry);
			}
			_delegate.Sync();
		}

		/// <exception cref="Db4oIOException"></exception>
		public override void Write(byte[] buffer, int length)
		{
			if (Config(LogWrite))
			{
				Println(LogConstants.WriteEntry + length);
			}
			_delegate.Write(buffer, length);
		}

		private void Println(string s)
		{
            try
            {
                _out.WriteLine(s);
            }
            catch (System.Exception e)      { }
		}

		private bool Config(int mask)
		{
			return (_config & mask) != 0;
		}
	}
}

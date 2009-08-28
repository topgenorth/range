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
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Handlers;

namespace Db4objects.Db4o.Internal
{
	internal sealed class Message
	{
		internal readonly TextWriter stream;

		internal Message(ObjectContainerBase a_stream, string msg)
		{
			stream = a_stream.ConfigImpl().OutStream();
			Print(msg, true);
		}

		internal Message(string a_StringParam, int a_intParam, TextWriter a_stream, bool 
			header)
		{
			stream = a_stream;
			Print(Db4objects.Db4o.Internal.Messages.Get(a_intParam, a_StringParam), header);
		}

		internal Message(string a_StringParam, int a_intParam, TextWriter a_stream) : this
			(a_StringParam, a_intParam, a_stream, true)
		{
		}

		private void Print(string msg, bool header)
		{
			if (stream != null)
			{
				if (header)
				{
					stream.WriteLine("[" + Db4oFactory.Version() + "   " + DateHandlerBase.Now() + "] "
						);
				}
				stream.WriteLine(" " + msg);
			}
		}
	}
}

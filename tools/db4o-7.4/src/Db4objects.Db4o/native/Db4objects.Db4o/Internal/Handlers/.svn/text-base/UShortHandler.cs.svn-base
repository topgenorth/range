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
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Marshall;


namespace Db4objects.Db4o.Internal.Handlers
{
	public class UShortHandler : IntegralTypeHandler
	{
        public override Object DefaultValue(){
            return (ushort)0;
        }
      
        public override Object Read(byte[] bytes, int offset){
            offset += 1;
            return (ushort) (bytes[offset] & 255 | (bytes[--offset] & 255) << 8);
        }
      
        public override int TypeID(){
            return 24;
        }
      
        public override void Write(Object obj, byte[] bytes, int offset){
            ushort us = (ushort)obj;
            offset += 2;
            bytes[--offset] = (byte)us;
            bytes[--offset] = (byte)(us >>= 8);
        }

        public override object Read(IReadContext context)
        {
            byte[] bytes = new byte[2];
            context.ReadBytes(bytes);
            return (ushort)(
                     bytes[1] & 255 |
                    (bytes[0] & 255) << 8
                );
        }

        public override void Write(IWriteContext context, object obj)
        {
            ushort us = (ushort)obj;
            context.WriteBytes(
                new byte[] { 
                    (byte)(us>>8),
                    (byte)us,
                });
        }

        public override IPreparedComparison InternalPrepareComparison(object obj)
        {
            return new PreparedComparasionFor<ushort>(((ushort)obj));
        }
    }
}

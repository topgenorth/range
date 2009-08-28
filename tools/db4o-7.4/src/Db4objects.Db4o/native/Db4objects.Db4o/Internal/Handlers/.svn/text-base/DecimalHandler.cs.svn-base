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
	public class DecimalHandler : IntegralTypeHandler
	{
        public override Object DefaultValue(){
            return (decimal)0;
        }
      
        public override Object Read(byte[] bytes, int offset){
            int[] ints = new int[4];
            offset += 3;
            for(int i = 0; i < 4; i ++){
                ints[i] = (bytes[offset] & 255 | (bytes[--offset] & 255) << 8 | (bytes[--offset] & 255) << 16 | bytes[--offset] << 24);
                offset +=7;
            }
            return new Decimal(ints);
        }

        public override int TypeID(){
            return 21;
        }
      
        public override void Write(Object obj, byte[] bytes, int offset){
            decimal dec = (decimal)obj;
            int[] ints = Decimal.GetBits(dec);
            offset += 4;
            for(int i = 0; i < 4; i ++){
                bytes[--offset] = (byte)ints[i];
                bytes[--offset] = (byte)(ints[i] >>= 8);
                bytes[--offset] = (byte)(ints[i] >>= 8);
                bytes[--offset] = (byte)(ints[i] >>= 8);
                offset += 8;
            }
        }
        public override object Read(IReadContext context)
        {
            byte[] bytes = new byte[16];
            int[] ints = new int[4];
            int offset = 4;
            context.ReadBytes(bytes);
            for (int i = 0; i < 4; i++)
            {
                ints[i] =   (
                             bytes[--offset] & 255 | 
                            (bytes[--offset] & 255) << 8 | 
                            (bytes[--offset] & 255) << 16 | 
                            (bytes[--offset] & 255) << 24
                            );
                offset += 8;
            }
            return new Decimal(ints);
        }

        public override void Write(IWriteContext context, object obj)
        {
            decimal dec = (decimal)obj;
            byte[] bytes = new byte[16];
            int offset = 4;
            int[] ints = Decimal.GetBits(dec);
            for (int i = 0; i < 4; i++)
            {
                bytes[--offset] = (byte)ints[i];
                bytes[--offset] = (byte)(ints[i] >>= 8);
                bytes[--offset] = (byte)(ints[i] >>= 8);
                bytes[--offset] = (byte)(ints[i] >>= 8);
                offset += 8;
            }
            context.WriteBytes(bytes);
        }

        public override IPreparedComparison InternalPrepareComparison(object obj)
        {
            return new PreparedComparasionFor<decimal>(((decimal) obj));
        }
	}
}

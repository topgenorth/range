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

namespace Db4objects.Db4o.Internal.Marshall
{
	/// <exclude></exclude>
	public class AspectType
	{
		public readonly byte _id;

		public static readonly Db4objects.Db4o.Internal.Marshall.AspectType Field = new Db4objects.Db4o.Internal.Marshall.AspectType
			((byte)1);

		public static readonly Db4objects.Db4o.Internal.Marshall.AspectType Translator = 
			new Db4objects.Db4o.Internal.Marshall.AspectType((byte)2);

		public static readonly Db4objects.Db4o.Internal.Marshall.AspectType Typehandler = 
			new Db4objects.Db4o.Internal.Marshall.AspectType((byte)3);

		private AspectType(byte id)
		{
			_id = id;
		}

		public static Db4objects.Db4o.Internal.Marshall.AspectType ForByte(byte b)
		{
			switch (b)
			{
				case 1:
				{
					return Field;
				}

				case 2:
				{
					return Translator;
				}

				case 3:
				{
					return Typehandler;
				}

				default:
				{
					throw new ArgumentException();
					break;
				}
			}
		}

		public virtual bool IsFieldMetadata()
		{
			return this == Db4objects.Db4o.Internal.Marshall.AspectType.Field || this == Db4objects.Db4o.Internal.Marshall.AspectType
				.Translator;
		}
	}
}

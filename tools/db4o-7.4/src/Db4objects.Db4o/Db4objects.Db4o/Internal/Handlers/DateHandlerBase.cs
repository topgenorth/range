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
using Db4objects.Db4o;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Handlers;
using Db4objects.Db4o.Internal.Marshall;
using Db4objects.Db4o.Marshall;
using Db4objects.Db4o.Reflect;

namespace Db4objects.Db4o.Internal.Handlers
{
	/// <summary>Shared (java/.net) logic for Date handling.</summary>
	/// <remarks>Shared (java/.net) logic for Date handling.</remarks>
	public abstract class DateHandlerBase : LongHandler
	{
		public override object Coerce(IReflector reflector, IReflectClass claxx, object obj
			)
		{
			return Handlers4.HandlerCanHold(this, reflector, claxx) ? obj : No4.Instance;
		}

		public abstract object CopyValue(object from, object to);

		public abstract override object DefaultValue();

		public abstract override object NullRepresentationInUntypedArrays();

		public override Type PrimitiveJavaClass()
		{
			return null;
		}

		protected override Type JavaClass()
		{
			return DefaultValue().GetType();
		}

		/// <exception cref="CorruptionException"></exception>
		public override object Read(MarshallerFamily mf, StatefulBuffer writer, bool redirect
			)
		{
			return mf._primitive.ReadDate(writer);
		}

		internal override object Read1(ByteArrayBuffer a_bytes)
		{
			return PrimitiveMarshaller().ReadDate(a_bytes);
		}

		public override void Write(object a_object, ByteArrayBuffer a_bytes)
		{
			// TODO: This is a temporary fix to prevent exceptions with
			// Marshaller.LEGACY.  
			if (a_object == null)
			{
				a_object = new DateTime(0);
			}
			a_bytes.WriteLong(((DateTime)a_object).Ticks);
		}

		public static string Now()
		{
			return Platform4.Format(Platform4.Now(), true);
		}

		public override object Read(IReadContext context)
		{
			long milliseconds = ((long)base.Read(context));
			return new DateTime(milliseconds);
		}

		public override void Write(IWriteContext context, object obj)
		{
			long milliseconds = ((DateTime)obj).Ticks;
			base.Write(context, milliseconds);
		}

		public override IPreparedComparison InternalPrepareComparison(object source)
		{
			long sourceDate = ((DateTime)source).Ticks;
			return new _IPreparedComparison_69(sourceDate);
		}

		private sealed class _IPreparedComparison_69 : IPreparedComparison
		{
			public _IPreparedComparison_69(long sourceDate)
			{
				this.sourceDate = sourceDate;
			}

			public int CompareTo(object target)
			{
				if (target == null)
				{
					return 1;
				}
				long targetDate = ((DateTime)target).Ticks;
				return sourceDate == targetDate ? 0 : (sourceDate < targetDate ? -1 : 1);
			}

			private readonly long sourceDate;
		}
	}
}

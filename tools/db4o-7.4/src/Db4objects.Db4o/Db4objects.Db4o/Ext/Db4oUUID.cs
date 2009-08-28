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
using Db4objects.Db4o;

namespace Db4objects.Db4o.Ext
{
	/// <summary>a unique universal identify for an object.</summary>
	/// <remarks>
	/// a unique universal identify for an object. <br /><br />The db4o UUID consists of
	/// two parts:<br /> - an indexed long for fast access,<br /> - the signature of the
	/// <see cref="IObjectContainer">IObjectContainer</see>
	/// the object was created with.
	/// <br /><br />Db4oUUIDs are valid representations of objects over multiple
	/// ObjectContainers
	/// </remarks>
	public class Db4oUUID
	{
		private readonly long longPart;

		private readonly byte[] signaturePart;

		/// <summary>constructs a Db4oUUID from a long part and a signature part</summary>
		/// <param name="longPart_">the long part</param>
		/// <param name="signaturePart_">the signature part</param>
		public Db4oUUID(long longPart_, byte[] signaturePart_)
		{
			longPart = longPart_;
			signaturePart = signaturePart_;
		}

		/// <summary>returns the long part of this UUID.</summary>
		/// <remarks>
		/// returns the long part of this UUID. <br /><br />To uniquely identify an object
		/// universally, db4o uses an indexed long and a reference to the
		/// Db4oDatabase object it was created on.
		/// </remarks>
		/// <returns>the long part of this UUID.</returns>
		public virtual long GetLongPart()
		{
			return longPart;
		}

		/// <summary>returns the signature part of this UUID.</summary>
		/// <remarks>
		/// returns the signature part of this UUID. <br /><br /> <br /><br />To uniquely
		/// identify an object universally, db4o uses an indexed long and a reference to
		/// the Db4oDatabase singleton object of the
		/// <see cref="IObjectContainer">IObjectContainer</see>
		/// it was created on. This method
		/// returns the signature of the Db4oDatabase object of the ObjectContainer: the
		/// signature of the origin ObjectContainer.
		/// </remarks>
		/// <returns>the signature of the Db4oDatabase for this UUID.</returns>
		public virtual byte[] GetSignaturePart()
		{
			return signaturePart;
		}

		public override bool Equals(object o)
		{
			if (this == o)
			{
				return true;
			}
			if (o == null || GetType() != o.GetType())
			{
				return false;
			}
			Db4objects.Db4o.Ext.Db4oUUID other = (Db4objects.Db4o.Ext.Db4oUUID)o;
			if (longPart != other.longPart)
			{
				return false;
			}
			if (signaturePart == null)
			{
				return other.signaturePart == null;
			}
			if (signaturePart.Length != other.signaturePart.Length)
			{
				return false;
			}
			for (int i = 0; i < signaturePart.Length; i++)
			{
				if (signaturePart[i] != other.signaturePart[i])
				{
					return false;
				}
			}
			return true;
		}

		public override int GetHashCode()
		{
			return (int)(longPart ^ ((longPart) >> (32 & 0x1f)));
		}

		public override string ToString()
		{
			string sig = string.Empty;
			for (int i = 0; i < signaturePart.Length; i++)
			{
				sig += signaturePart[i] + " ";
			}
			return "long " + longPart + " ,  signature " + sig;
		}
	}
}

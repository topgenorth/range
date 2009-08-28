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
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Tests.Common.Concurrency;
using Sharpen.Lang;

namespace Db4objects.Db4o.Tests.Common.Concurrency
{
	public class InvalidUUIDTestCase : Db4oClientServerTestCase
	{
		public static void Main(string[] args)
		{
			new InvalidUUIDTestCase().RunConcurrency();
		}

		public string name;

		protected override void Configure(IConfiguration config)
		{
			config.ObjectClass(this.GetType()).GenerateUUIDs(true);
		}

		protected override void Store()
		{
			name = "theOne";
			Store(this);
		}

		/// <exception cref="Exception"></exception>
		public virtual void Conc(IExtObjectContainer oc)
		{
			IObjectSet os = oc.Query(typeof(InvalidUUIDTestCase));
			if (os.Size() == 0)
			{
				// already deleted by other threads
				return;
			}
			Assert.AreEqual(1, os.Size());
			InvalidUUIDTestCase iu = (InvalidUUIDTestCase)os.Next();
			Db4oUUID myUuid = oc.GetObjectInfo(iu).GetUUID();
			Assert.IsNotNull(myUuid);
			byte[] mySignature = myUuid.GetSignaturePart();
			long myLong = myUuid.GetLongPart();
			long unknownLong = long.MaxValue - 100;
			byte[] unknownSignature = new byte[] { 1, 2, 4, 99, 33, 22 };
			Db4oUUID unknownLongPart = new Db4oUUID(unknownLong, mySignature);
			Db4oUUID unknownSignaturePart = new Db4oUUID(myLong, unknownSignature);
			Db4oUUID unknownBoth = new Db4oUUID(unknownLong, unknownSignature);
			Assert.IsNull(oc.GetByUUID(unknownLongPart));
			Assert.IsNull(oc.GetByUUID(unknownSignaturePart));
			Assert.IsNull(oc.GetByUUID(unknownBoth));
			Assert.IsNull(oc.GetByUUID(unknownLongPart));
			Thread.Sleep(500);
			oc.Delete(iu);
			oc.Commit();
			Assert.IsNull(oc.GetByUUID(myUuid));
		}
	}
}

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
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Assorted;

namespace Db4objects.Db4o.Tests.Common.Assorted
{
	public class QueryByInterface : AbstractDb4oTestCase
	{
		public static void Main(string[] args)
		{
			new QueryByInterface().RunSolo();
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			QueryByInterface.Ferrari f430 = new QueryByInterface.Ferrari(this, "F430");
			QueryByInterface.Ferrari f450 = new QueryByInterface.Ferrari(this, "F450");
			Store(f430);
			Store(f450);
			QueryByInterface.Bmw serie5 = new QueryByInterface.Bmw(this, "Serie 5");
			QueryByInterface.Bmw serie7 = new QueryByInterface.Bmw(this, "Serie 7");
			Store(serie5);
			Store(serie7);
		}

		public virtual void Test()
		{
			IQuery q = NewQuery();
			q.Constrain(typeof(QueryByInterface.ICar));
			q.Descend("name").Constrain("F450");
			IObjectSet result = q.Execute();
			Assert.AreEqual(1, result.Size());
			QueryByInterface.Ferrari car = (QueryByInterface.Ferrari)result.Next();
			Assert.AreEqual("F450", car.name);
		}

		public interface ICar
		{
		}

		public class Ferrari : QueryByInterface.ICar
		{
			public string name;

			public Ferrari(QueryByInterface _enclosing, string n)
			{
				this._enclosing = _enclosing;
				this.name = n;
			}

			public override string ToString()
			{
				return "Ferrari " + this.name;
			}

			private readonly QueryByInterface _enclosing;
		}

		public class Bmw : QueryByInterface.ICar
		{
			public string name;

			public Bmw(QueryByInterface _enclosing, string n)
			{
				this._enclosing = _enclosing;
				this.name = n;
			}

			public override string ToString()
			{
				return "BMW " + this.name;
			}

			private readonly QueryByInterface _enclosing;
		}
	}
}

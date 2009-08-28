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
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Tests.Common.Querying;

namespace Db4objects.Db4o.Tests.Common.Querying
{
	public class CascadeOnUpdate : AbstractDb4oTestCase
	{
		public static void Main(string[] arguments)
		{
			new CascadeOnUpdate().RunClientServer();
		}

		public class Atom
		{
			public CascadeOnUpdate.Atom child;

			public string name;

			public Atom()
			{
			}

			public Atom(CascadeOnUpdate.Atom child)
			{
				this.child = child;
			}

			public Atom(string name)
			{
				this.name = name;
			}

			public Atom(CascadeOnUpdate.Atom child, string name) : this(child)
			{
				this.name = name;
			}
		}

		public object child;

		protected override void Configure(IConfiguration conf)
		{
			conf.ObjectClass(this).CascadeOnUpdate(true);
		}

		protected override void Store()
		{
			CascadeOnUpdate cou = new CascadeOnUpdate();
			cou.child = new CascadeOnUpdate.Atom(new CascadeOnUpdate.Atom("storedChild"), "stored"
				);
			Db().Store(cou);
		}

		/// <exception cref="Exception"></exception>
		public virtual void Test()
		{
			Foreach(GetType(), new _IVisitor4_52(this));
			Reopen();
			Foreach(GetType(), new _IVisitor4_63());
		}

		private sealed class _IVisitor4_52 : IVisitor4
		{
			public _IVisitor4_52(CascadeOnUpdate _enclosing)
			{
				this._enclosing = _enclosing;
			}

			public void Visit(object obj)
			{
				CascadeOnUpdate cou = (CascadeOnUpdate)obj;
				((CascadeOnUpdate.Atom)cou.child).name = "updated";
				((CascadeOnUpdate.Atom)cou.child).child.name = "updated";
				this._enclosing.Db().Store(cou);
			}

			private readonly CascadeOnUpdate _enclosing;
		}

		private sealed class _IVisitor4_63 : IVisitor4
		{
			public _IVisitor4_63()
			{
			}

			public void Visit(object obj)
			{
				CascadeOnUpdate cou = (CascadeOnUpdate)obj;
				CascadeOnUpdate.Atom atom = (CascadeOnUpdate.Atom)cou.child;
				Assert.AreEqual("updated", atom.name);
				Assert.AreNotEqual("updated", atom.child.name);
			}
		}
	}
}

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
using Db4oUnit.Extensions.Fixtures;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Tests.Common.Assorted;
using Db4objects.Db4o.Tests.Util;

namespace Db4objects.Db4o.Tests.Common.Assorted
{
	public class AliasesTestCase : AbstractDb4oTestCase, IOptOutDefragSolo
	{
		public static void Main(string[] args)
		{
			new AliasesTestCase().RunSolo();
		}

		private int id;

		private IAlias alias;

		public class AFoo
		{
			public string foo;
		}

		public class ABar : AliasesTestCase.AFoo
		{
			public string bar;
		}

		public class BFoo
		{
			public string foo;
		}

		public class BBar : AliasesTestCase.BFoo
		{
			public string bar;
		}

		public class CFoo
		{
			public string foo;
		}

		public class CBar : AliasesTestCase.CFoo
		{
			public string bar;
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			AddACAlias();
			AliasesTestCase.CBar bar = new AliasesTestCase.CBar();
			bar.foo = "foo";
			bar.bar = "bar";
			Store(bar);
			id = (int)Db().GetID(bar);
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestAccessByChildClass()
		{
			AddABAlias();
			AliasesTestCase.BBar bar = (AliasesTestCase.BBar)RetrieveOnlyInstance(typeof(AliasesTestCase.BBar
				));
			AssertInstanceOK(bar);
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestAccessByParentClass()
		{
			AddABAlias();
			AliasesTestCase.BBar bar = (AliasesTestCase.BBar)RetrieveOnlyInstance(typeof(AliasesTestCase.BFoo
				));
			AssertInstanceOK(bar);
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestAccessById()
		{
			AddABAlias();
			AliasesTestCase.BBar bar = (AliasesTestCase.BBar)Db().GetByID(id);
			Db().Activate(bar, 2);
			AssertInstanceOK(bar);
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestAccessWithoutAlias()
		{
			RemoveAlias();
			AliasesTestCase.ABar bar = (AliasesTestCase.ABar)RetrieveOnlyInstance(typeof(AliasesTestCase.ABar
				));
			AssertInstanceOK(bar);
		}

		private void AssertInstanceOK(AliasesTestCase.BBar bar)
		{
			Assert.AreEqual("foo", bar.foo);
			Assert.AreEqual("bar", bar.bar);
		}

		private void AssertInstanceOK(AliasesTestCase.ABar bar)
		{
			Assert.AreEqual("foo", bar.foo);
			Assert.AreEqual("bar", bar.bar);
		}

		/// <exception cref="Exception"></exception>
		private void AddABAlias()
		{
			AddAlias("A", "B");
		}

		/// <exception cref="Exception"></exception>
		private void AddACAlias()
		{
			AddAlias("A", "C");
		}

		/// <exception cref="Exception"></exception>
		private void AddAlias(string storedLetter, string runtimeLetter)
		{
			RemoveAlias();
			alias = CreateAlias(storedLetter, runtimeLetter);
			Fixture().ConfigureAtRuntime(new _IRuntimeConfigureAction_104(this));
			Reopen();
		}

		private sealed class _IRuntimeConfigureAction_104 : IRuntimeConfigureAction
		{
			public _IRuntimeConfigureAction_104(AliasesTestCase _enclosing)
			{
				this._enclosing = _enclosing;
			}

			public void Apply(IConfiguration config)
			{
				config.AddAlias(this._enclosing.alias);
			}

			private readonly AliasesTestCase _enclosing;
		}

		/// <exception cref="Exception"></exception>
		private void RemoveAlias()
		{
			if (alias != null)
			{
				Fixture().ConfigureAtRuntime(new _IRuntimeConfigureAction_114(this));
				alias = null;
			}
			Reopen();
		}

		private sealed class _IRuntimeConfigureAction_114 : IRuntimeConfigureAction
		{
			public _IRuntimeConfigureAction_114(AliasesTestCase _enclosing)
			{
				this._enclosing = _enclosing;
			}

			public void Apply(IConfiguration config)
			{
				config.RemoveAlias(this._enclosing.alias);
			}

			private readonly AliasesTestCase _enclosing;
		}

		private WildcardAlias CreateAlias(string storedLetter, string runtimeLetter)
		{
			string className = Reflector().ForObject(new AliasesTestCase.ABar()).GetName();
			string storedPattern = className.Replace("ABar", storedLetter + "*");
			string runtimePattern = className.Replace("ABar", runtimeLetter + "*");
			return new WildcardAlias(storedPattern, runtimePattern);
		}
	}
}

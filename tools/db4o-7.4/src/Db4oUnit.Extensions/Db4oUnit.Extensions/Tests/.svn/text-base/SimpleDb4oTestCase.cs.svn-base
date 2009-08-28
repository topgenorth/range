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
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4oUnit.Extensions.Tests;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Foundation;

namespace Db4oUnit.Extensions.Tests
{
	public class SimpleDb4oTestCase : AbstractDb4oTestCase
	{
		public static readonly DynamicVariable ExpectedFixtureVariable = new DynamicVariable
			();

		public class Data
		{
		}

		private bool[] _everythingCalled = new bool[3];

		protected override void Configure(IConfiguration config)
		{
			Assert.AreSame(ExpectedFixture(), Fixture());
			Assert.IsTrue(EverythingCalledBefore(0));
			_everythingCalled[0] = true;
		}

		private IDb4oFixture ExpectedFixture()
		{
			return (IDb4oFixture)ExpectedFixtureVariable.Value;
		}

		protected override void Store()
		{
			Assert.IsTrue(EverythingCalledBefore(1));
			_everythingCalled[1] = true;
			Fixture().Db().Store(new SimpleDb4oTestCase.Data());
		}

		public virtual void TestResultSize()
		{
			Assert.IsTrue(EverythingCalledBefore(2));
			_everythingCalled[2] = true;
			Assert.AreEqual(1, Fixture().Db().QueryByExample(typeof(SimpleDb4oTestCase.Data))
				.Size());
		}

		public virtual bool EverythingCalled()
		{
			return EverythingCalledBefore(_everythingCalled.Length);
		}

		public virtual bool EverythingCalledBefore(int idx)
		{
			for (int i = 0; i < idx; i++)
			{
				if (!_everythingCalled[i])
				{
					return false;
				}
			}
			for (int i = idx; i < _everythingCalled.Length; i++)
			{
				if (_everythingCalled[i])
				{
					return false;
				}
			}
			return true;
		}
	}
}

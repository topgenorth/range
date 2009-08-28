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
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Internal;

namespace Db4oUnit.Extensions.Fixtures
{
	public abstract class AbstractSoloDb4oFixture : Db4oUnit.Extensions.Fixtures.AbstractDb4oFixture
	{
		private IExtObjectContainer _db;

		protected AbstractSoloDb4oFixture(IConfigurationSource configSource) : base(configSource
			)
		{
		}

		public sealed override void Open(Type testCaseClass)
		{
			Assert.IsNull(_db);
			IConfiguration config = Config();
			ApplyFixtureConfiguration(testCaseClass, config);
			_db = CreateDatabase(config).Ext();
		}

		/// <exception cref="Exception"></exception>
		public override void Close()
		{
			if (null != _db)
			{
				Assert.IsTrue(Db().Close());
				_db = null;
			}
		}

		public override bool Accept(Type clazz)
		{
			return !typeof(IOptOutSolo).IsAssignableFrom(clazz);
		}

		public override IExtObjectContainer Db()
		{
			return _db;
		}

		protected abstract IObjectContainer CreateDatabase(IConfiguration config);

		public override LocalObjectContainer FileSession()
		{
			return (LocalObjectContainer)_db;
		}

		public override void ConfigureAtRuntime(IRuntimeConfigureAction action)
		{
			action.Apply(Config());
		}
	}
}

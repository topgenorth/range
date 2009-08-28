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
using Db4oUnit.Extensions;
using Db4oUnit.Extensions.Fixtures;
using Db4oUnit.Extensions.Util;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;

namespace Db4oUnit.Extensions.Fixtures
{
	public class Db4oSolo : AbstractFileBasedDb4oFixture
	{
		private static readonly string File = "db4oSoloTest.yap";

		public Db4oSolo() : this(new IndependentConfigurationSource())
		{
		}

		public Db4oSolo(IConfigurationSource configSource) : base(configSource, FilePath(
			))
		{
		}

		public Db4oSolo(IFixtureConfiguration fixtureConfiguration) : this()
		{
			FixtureConfiguration(fixtureConfiguration);
		}

		protected override IObjectContainer CreateDatabase(IConfiguration config)
		{
			return Db4oFactory.OpenFile(config, GetAbsolutePath());
		}

		public override string Label()
		{
			return BuildLabel("SOLO");
		}

		/// <exception cref="Exception"></exception>
		public override void Defragment()
		{
			Defragment(FilePath());
		}

		private static string FilePath()
		{
			return CrossPlatformServices.DatabasePath(File);
		}
	}
}

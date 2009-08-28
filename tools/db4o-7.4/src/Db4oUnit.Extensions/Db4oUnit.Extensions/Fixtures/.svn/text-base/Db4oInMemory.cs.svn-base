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
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;

namespace Db4oUnit.Extensions.Fixtures
{
	public class Db4oInMemory : AbstractSoloDb4oFixture
	{
		public Db4oInMemory() : base(new IndependentConfigurationSource())
		{
		}

		public Db4oInMemory(IConfigurationSource configSource) : base(configSource)
		{
		}

		public Db4oInMemory(IFixtureConfiguration fc) : this()
		{
			FixtureConfiguration(fc);
		}

		private MemoryFile _memoryFile;

		protected override IObjectContainer CreateDatabase(IConfiguration config)
		{
			if (null == _memoryFile)
			{
				_memoryFile = new MemoryFile();
			}
			return ExtDb4oFactory.OpenMemoryFile(config, _memoryFile);
		}

		protected override void DoClean()
		{
			_memoryFile = null;
		}

		public override string Label()
		{
			return BuildLabel("IN-MEMORY");
		}

		/// <exception cref="Exception"></exception>
		public override void Defragment()
		{
		}
		// do nothing
		// defragment is file-based for now
	}
}

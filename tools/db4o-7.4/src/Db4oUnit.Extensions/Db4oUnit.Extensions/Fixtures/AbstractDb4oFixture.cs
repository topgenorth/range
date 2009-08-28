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
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Defragment;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Internal;

namespace Db4oUnit.Extensions.Fixtures
{
	public abstract class AbstractDb4oFixture : IDb4oFixture
	{
		private readonly CachingConfigurationSource _configSource;

		private IFixtureConfiguration _fixtureConfiguration;

		protected AbstractDb4oFixture(IConfigurationSource configSource)
		{
			_configSource = new CachingConfigurationSource(configSource);
		}

		public virtual void FixtureConfiguration(IFixtureConfiguration fc)
		{
			_fixtureConfiguration = fc;
		}

		/// <exception cref="Exception"></exception>
		public virtual void Reopen(Type testCaseClass)
		{
			Close();
			Open(testCaseClass);
		}

		public virtual IConfiguration Config()
		{
			return _configSource.Config();
		}

		public virtual void Clean()
		{
			DoClean();
			ResetConfig();
		}

		public abstract bool Accept(Type clazz);

		protected abstract void DoClean();

		protected virtual void ResetConfig()
		{
			_configSource.Reset();
		}

		/// <exception cref="Exception"></exception>
		protected virtual void Defragment(string fileName)
		{
			string targetFile = fileName + ".defrag.backup";
			DefragmentConfig defragConfig = new DefragmentConfig(fileName, targetFile);
			defragConfig.ForceBackupDelete(true);
			defragConfig.Db4oConfig(Config());
			Db4objects.Db4o.Defragment.Defragment.Defrag(defragConfig);
		}

		protected virtual string BuildLabel(string label)
		{
			if (null == _fixtureConfiguration)
			{
				return label;
			}
			return label + " - " + _fixtureConfiguration.GetLabel();
		}

		protected virtual void ApplyFixtureConfiguration(Type testCaseClass, IConfiguration
			 config)
		{
			if (null == _fixtureConfiguration)
			{
				return;
			}
			_fixtureConfiguration.Configure(testCaseClass, config);
		}

		public override string ToString()
		{
			return Label();
		}

		public abstract string Label();

		public abstract void Close();

		public abstract void ConfigureAtRuntime(IRuntimeConfigureAction arg1);

		public abstract IExtObjectContainer Db();

		public abstract void Defragment();

		public abstract LocalObjectContainer FileSession();

		public abstract void Open(Type arg1);
	}
}

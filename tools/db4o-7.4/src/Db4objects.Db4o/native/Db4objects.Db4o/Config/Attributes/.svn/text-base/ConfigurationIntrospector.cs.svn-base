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
using System.Reflection;

using Db4objects.Db4o.Internal;

namespace Db4objects.Db4o.Config.Attributes
{
	class ConfigurationIntrospector
	{
		private readonly Type _type;
		private Config4Class _classConfig;
		private readonly IConfiguration _config;

		public ConfigurationIntrospector(Type type, Config4Class classConfig, IConfiguration config)
		{
			if (null == type) throw new ArgumentNullException("type");
			if (null == config) throw new ArgumentNullException("config");
			_type = type;
			_classConfig = classConfig;
			_config = config;
		}

		public Type Type
		{
			get { return _type; }
		}

		public Config4Class ClassConfiguration
		{
			get
			{
				if (null == _classConfig)
				{
					_classConfig = (Config4Class)_config.ObjectClass(_type);
		}
				return _classConfig;
			}
		}

		public IConfiguration IConfiguration
		{
			get { return _config; }
		}		

		public void Apply()
		{
			Apply(_type);
			foreach (FieldInfo field in _type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
				Apply(field);
		}
		
		private void Apply(ICustomAttributeProvider provider)
		{
			foreach (object o in provider.GetCustomAttributes(false))
			{
				IDb4oAttribute attr = o as IDb4oAttribute;
				if (null == attr)
					continue;
				
				attr.Apply(provider, this);
			}
		}
	}
}

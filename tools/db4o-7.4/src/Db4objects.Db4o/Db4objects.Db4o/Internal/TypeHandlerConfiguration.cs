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
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Internal
{
	/// <exclude></exclude>
	public abstract class TypeHandlerConfiguration
	{
		protected readonly Config4Impl _config;

		private ITypeHandler4 _listTypeHandler;

		private ITypeHandler4 _mapTypeHandler;

		public abstract void Apply();

		public TypeHandlerConfiguration(Config4Impl config)
		{
			_config = config;
		}

		protected virtual void ListTypeHandler(ITypeHandler4 listTypeHandler)
		{
			_listTypeHandler = listTypeHandler;
		}

		protected virtual void MapTypeHandler(ITypeHandler4 mapTypehandler)
		{
			_mapTypeHandler = mapTypehandler;
		}

		public static bool Enabled()
		{
			return false;
		}

		protected virtual void RegisterCollection(Type clazz)
		{
			RegisterListTypeHandlerFor(clazz);
		}

		protected virtual void RegisterMap(Type clazz)
		{
			RegisterMapTypeHandlerFor(clazz);
		}

		protected virtual void IgnoreFieldsOn(Type clazz)
		{
			_config.RegisterTypeHandler(new SingleClassTypeHandlerPredicate(clazz), new IgnoreFieldsTypeHandler
				());
		}

		private void RegisterListTypeHandlerFor(Type clazz)
		{
			RegisterTypeHandlerFor(_listTypeHandler, clazz);
		}

		private void RegisterMapTypeHandlerFor(Type clazz)
		{
			RegisterTypeHandlerFor(_mapTypeHandler, clazz);
		}

		private void RegisterTypeHandlerFor(ITypeHandler4 typeHandler, Type clazz)
		{
			_config.RegisterTypeHandler(new SingleClassTypeHandlerPredicate(clazz), typeHandler
				);
		}
	}
}

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
using Db4objects.Db4o.Tests.Util;

namespace Db4objects.Db4o.Tests.Common.Migration
{
#if !CF
	internal class LegacyAdapterEmitter
	{
		private string _legacyAssembly;
		private string _version;

		public LegacyAdapterEmitter(string legacyAssembly, string version)
		{
			_legacyAssembly = legacyAssembly;
			_version = version;
		}

		public void Emit(string fname)
		{	
			CompilationServices.EmitAssembly(fname, new string[] {_legacyAssembly}, GetCode());
		}	

		public string GetCode()
		{
			if (_version.StartsWith("5")) return PascalCaseAdapter;
			return CamelCaseAdapter;
		}

		#region PascalCaseAdapter
		string PascalCaseAdapter
		{
			get
			{
				return CommonCode + @"
namespace Db4objects.Db4o.Config
{
   	public interface IConfiguration
	{
    }   
}

namespace Db4objects.Db4o.Query
{
    public interface IQuery
    {
    }   
}

namespace Db4objects.Db4o
{
	using Db4objects.Db4o.Ext;
    using Db4objects.Db4o.Config;
    using Db4objects.Db4o.Query;
    
	public class Db4oFactory
	{
		public static IObjectContainer OpenFile(string fname)
		{
			return new ObjectContainerAdapter(com.db4o.Db4o.OpenFile(fname));
		}

        public static IConfiguration Configure()
        {
            return null;
        }
	}

	class ObjectContainerAdapter : IExtObjectContainer
	{
		private readonly ObjectContainer _container;

		public ObjectContainerAdapter(ObjectContainer container)
		{
			_container = container;
		}

		public void Set(object o)
		{
			_container.Set(o);
		}
		
        public void Delete(object obj)
        {
            _container.Delete(obj);
        }

		public bool Close()
		{
			return _container.Close();
		}

		public IExtObjectContainer Ext()
		{
			return this;
		}

        public void Commit()
        {
            _container.Commit();
        }

        public IQuery Query()
        {
            return new QueryAdapter(_container.Query());
        }
	}

    class QueryAdapter : IQuery
    {
        private com.db4o.query.Query _query;        
        public QueryAdapter(com.db4o.query.Query query)
        {
            _query = query;
        }
    }
}
";
			}
		}
		#endregion

		#region CamelCaseAdapter
		string CamelCaseAdapter
		{
			get
			{
				return CommonCode + @"
namespace Db4objects.Db4o
{
	using Db4objects.Db4o.Ext;

	public class Db4oFactory
	{
		public static IObjectContainer OpenFile(string fname)
		{
			return new ObjectContainerAdapter(com.db4o.Db4o.openFile(fname));
		}
	}

	class ObjectContainerAdapter : IExtObjectContainer
	{
		private readonly ObjectContainer _container;

		public ObjectContainerAdapter(ObjectContainer container)
		{
			_container = container;
		}

		public void Set(object o)
		{
			_container.set(o);
		}

		public bool Close()
		{
			return _container.close();
		}

		public IExtObjectContainer Ext()
		{
			return this;
		}
	}
}
";
			}
		}
		#endregion

		#region CommonCode
		string CommonCode
		{
			get
			{
				return @"
using System;
using com.db4o;

namespace Db4objects.Db4o.Ext
{
	public interface IExtObjectContainer : IObjectContainer
	{
	}
}

namespace Db4objects.Db4o
{
	using Db4objects.Db4o.Ext;
    using Db4objects.Db4o.Query;
	
	public interface IObjectContainer
	{
		IQuery Query();
		void Set(object o);
		void Delete(object obj);
		IExtObjectContainer Ext();
		bool Close();
        void Commit();
	}
}

namespace Db4objects.Db4o.Foundation.IO
{
	using System.IO;

	public class File4
	{
		public static void Delete(string file)
		{
			if (File.Exists(file))
			{
				File.Delete(file);
			}
		}
	}
}

namespace Sharpen
{
	public class Runtime 
    {
		public static string Substring(string s, int startIndex, int endIndex)
		{
			return s.Substring(startIndex, endIndex - startIndex);
		}
    }
}

";
			}
		}
		#endregion
	}
#endif
}
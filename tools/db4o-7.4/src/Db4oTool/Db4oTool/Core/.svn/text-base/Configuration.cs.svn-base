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
using System.Collections.Generic;
using System.Diagnostics;

namespace Db4oTool.Core
{
	public class Configuration
	{
		private bool _caseSensitive;
		private readonly string _assemblyLocation;
		private readonly TraceSwitch _traceSwitch = new TraceSwitch("Db4oTool", "Db4oTool tracing level");
	    private readonly List<ITypeFilter> _filters = new List<ITypeFilter>();
		private bool _preserveDebugInfo;
		
		public Configuration(string assemblyLocation)
		{
			_assemblyLocation = assemblyLocation;
			_traceSwitch.Level = TraceLevel.Warning;
		}

		public bool CaseSensitive
		{
			get { return _caseSensitive; }
			set { _caseSensitive = value; }
		}

		public string AssemblyLocation
		{
			get { return _assemblyLocation; }
		}

		public TraceSwitch TraceSwitch
		{
			get { return _traceSwitch; }
		}

		public bool PreserveDebugInfo
		{
			get { return _preserveDebugInfo; }
			set { _preserveDebugInfo = value; }
		}

		public void AddFilter(ITypeFilter filter)
	    {
            if (null == filter) throw new ArgumentNullException("filter");
	        _filters.Add(filter);
	    }

        public bool Accept(Mono.Cecil.TypeDefinition typedef)
        {
            if (_filters.Count == 0) return true;
            foreach (ITypeFilter filter in _filters)
            {
                if (filter.Accept(typedef)) return true;
            }
            return false;
        }
    }
}

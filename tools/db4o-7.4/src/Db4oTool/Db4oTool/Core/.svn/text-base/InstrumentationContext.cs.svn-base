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
using System.Diagnostics;
using System.Reflection;
using Mono.Cecil;

namespace Db4oTool.Core
{
	public class InstrumentationContext
	{
		private AssemblyDefinition _assembly;
		private Configuration _configuration;
		private string _alternateAssemblyLocation;

        public InstrumentationContext(Configuration configuration, AssemblyDefinition assembly)
        {
			Init(configuration, assembly);
		}

		public InstrumentationContext(Configuration configuration)
		{
			Init(configuration, LoadAssembly(configuration));
		}

		public string AlternateAssemblyLocation
		{
			get { return _alternateAssemblyLocation; }
		}

		public Configuration Configuration
		{
			get { return _configuration; }
		}

		public TraceSwitch TraceSwitch
		{
			get { return _configuration.TraceSwitch; }
		}

		public AssemblyDefinition Assembly
		{
			get { return _assembly; }
		}

		public string AssemblyLocation
		{
			get { return _assembly.MainModule.Image.FileInformation.FullName; }
		}

		public TypeReference Import(Type type)
		{
			return _assembly.MainModule.Import(type);
		}

		public MethodReference Import(MethodBase method)
		{
			return _assembly.MainModule.Import(method);
		}

		public void SaveAssembly()
		{
			if (PreserveDebugInfo())
			{
				_assembly.MainModule.SaveSymbols();
			}
			AssemblyFactory.SaveAssembly(_assembly, AssemblyLocation);
		}

		private bool PreserveDebugInfo()
		{
			return _configuration.PreserveDebugInfo;
		}

		public void TraceWarning(string message, params object[] args)
		{
			if (TraceSwitch.TraceWarning)
			{
				Trace.WriteLine(string.Format(message, args));
			}
		}

		public void TraceInfo(string message, params object[] args)
		{
			if (TraceSwitch.TraceInfo)
			{
				Trace.WriteLine(string.Format(message, args));
			}
		}

		public void TraceVerbose(string format, params object[] args)
		{
			if (TraceSwitch.TraceVerbose)
			{
				Trace.WriteLine(string.Format(format, args));
			}
		}

		public bool Accept(TypeDefinition typedef)
		{
			return _configuration.Accept(typedef);
		}

		public bool IsAssemblySigned()
		{
			return _assembly.Name.HasPublicKey;
		}

		private void Init(Configuration configuration, AssemblyDefinition assembly)
		{
			_configuration = configuration;

			ConfigureCompactFrameworkAssemblyPath(assembly);
			SetupAssembly(assembly);
		}

		private void ConfigureCompactFrameworkAssemblyPath(AssemblyDefinition assembly)
		{
			_alternateAssemblyLocation = CompactFrameworkServices.FolderFor(assembly);
		}

		private static AssemblyDefinition LoadAssembly(Configuration configuration)
		{
			return AssemblyFactory.GetAssembly(configuration.AssemblyLocation);
		}

		private void SetupAssembly(AssemblyDefinition assembly)
		{
			_assembly = assembly;
			if (PreserveDebugInfo())
			{
				_assembly.MainModule.LoadSymbols();
			}
			_assembly.MainModule.FullLoad(); // resolves all references
		}
	}
}
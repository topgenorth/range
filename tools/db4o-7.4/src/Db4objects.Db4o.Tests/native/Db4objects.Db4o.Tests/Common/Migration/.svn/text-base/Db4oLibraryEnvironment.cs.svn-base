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
using System.IO;
using System.Reflection;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Tests.Util;
#if !CF
using Mono.Cecil;
#endif
using File=Sharpen.IO.File;

namespace Db4objects.Db4o.Tests.Common.Migration
{

	[Serializable]
	class InvokeInstanceMethod
	{
		private readonly string _typeName;
		private readonly string _methodName;
		private readonly object[] _arguments;

		public InvokeInstanceMethod(string typeName, string methodName, object[] arguments)
		{
			_typeName = typeName;
			_methodName = methodName;
			_arguments = arguments;
		}

		public void Execute()
		{
			Type type = Type.GetType(_typeName);
			MethodInfo method =
				type.GetMethod(_methodName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
			try
			{
				method.Invoke(Activator.CreateInstance(type), _arguments);
			}
			catch (Exception x)
			{
				throw new Exception(x.ToString());
			}
		}
    }

	[Serializable]
	class InstallAssemblyResolver
	{
		private readonly string _assembly;
		private readonly string _assemblyName;

		public InstallAssemblyResolver(string assembly)
		{	
			_assembly = assembly;
			_assemblyName = Path.GetFileNameWithoutExtension(_assembly);
        }

#if !CF        
		public void Execute()
		{
			AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
		}

		Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			if (SimpleName(args.Name) == _assemblyName)
			{
				return Assembly.LoadFrom(_assembly);
			}
			return null;
		}
#endif

        private string SimpleName(string name)
		{
			return name.Split(',')[0];
		}
	}

	public class Db4oLibraryEnvironment : IDisposable
	{
		private readonly AppDomain _domain;

		private readonly string _targetAssembly;

		private string _version;

		public Db4oLibraryEnvironment(File file, File additionalAssembly)
		{
			_targetAssembly = file.GetAbsolutePath();
#if !CF
			_domain = CreateDomain(SetUpBaseDirectory());
			try
			{
				SetUpAssemblyResolver();
				SetUpLegacyAdapter();
			}
			catch (Exception x)
			{
				Dispose();
				throw new Exception("Failed to setup environment for '" + _targetAssembly + "'", x);
			}
#endif
        }

#if !CF
		private string SetUpBaseDirectory()
		{
			string baseDirectory = BaseDirectory();
			CopyAssemblies(baseDirectory);
			return baseDirectory;
		}

		private string BaseDirectory()
		{
			return IOServices.BuildTempPath("migration-domain-" + Version());
		}

		private void SetUpLegacyAdapter()
		{
			if (!Db4oLibrarian.IsLegacyVersion(Version())) return;

			string adapterAssembly = Path.Combine(BaseDirectory(), "Db4objects.Db4o.dll");
			new LegacyAdapterEmitter(_targetAssembly, Version()).Emit(adapterAssembly);

		}

		private void SetUpAssemblyResolver()
		{
			_domain.DoCallBack(new CrossAppDomainDelegate(new InstallAssemblyResolver(_targetAssembly).Execute));
		}

		private static AppDomain CreateDomain(string baseDirectory)
		{
			AppDomainSetup setup = new AppDomainSetup();
			setup.ApplicationBase = baseDirectory;
			return AppDomain.CreateDomain(Path.GetFileName(baseDirectory), null, setup);
		}

		private void CopyAssemblies(string domainBase)
		{
			CleanStrongName(IOServices.CopyTo(_targetAssembly, domainBase));
			CleanStrongName(IOServices.CopyEnclosingAssemblyTo(GetType(), domainBase));
			CleanStrongName(IOServices.CopyEnclosingAssemblyTo(typeof(Db4oUnit.ITest), domainBase));
			CleanStrongName(IOServices.CopyEnclosingAssemblyTo(typeof(Db4oUnit.Extensions.IDb4oTestCase), domainBase));
		}

		private void CleanStrongName(string path)
		{
			AssemblyDefinition asm = AssemblyFactory.GetAssembly(path);
			CleanStrongNames(asm.Modules);
			CleanStrongName(asm.Name);
			AssemblyFactory.SaveAssembly(asm, path);
		}

		private static void CleanStrongNames(ModuleDefinitionCollection modules)
		{
			foreach (ModuleDefinition m in modules)
			{
				CleanStrongNames(m.AssemblyReferences);
			}
		}

		private static void CleanStrongNames(AssemblyNameReferenceCollection references)
		{
			foreach (AssemblyNameReference name in references)
			{
				if (name.Name.StartsWith("Db4objects.Db4o")
				    || name.Name.StartsWith("Db4oUnit"))
				{
					CleanStrongName(name);
				}
			}
		}

		private static void CleanStrongName(AssemblyNameDefinition nameDefinition)
		{
			nameDefinition.PublicKey = new byte[0];
			nameDefinition.PublicKeyToken = new byte[0];
		}

		private static void CleanStrongName(AssemblyNameReference name)
		{
			name.Version = new Version();
			name.PublicKeyToken = new byte[0];
		}
#endif

		public string Version()
		{
			if (null != _version) return _version;
			return _version = GetVersion();
		}

		private string GetVersion()
		{
#if !CF
			return System.Reflection.Assembly.ReflectionOnlyLoadFrom(_targetAssembly).GetName().Version.ToString();
#else
			return System.Reflection.Assembly.LoadFrom(_targetAssembly).GetName().Version.ToString();
#endif
		}

		public void InvokeInstanceMethod(Type type, string methodName, params object[] args)
		{
#if !CF
			_domain.DoCallBack(new CrossAppDomainDelegate(new InvokeInstanceMethod(ReflectPlatform.FullyQualifiedName(type), methodName, args).Execute));
#endif
        }

		public void Dispose()
		{
#if !CF
			AppDomain.Unload(_domain);
#endif
		}
	}

}
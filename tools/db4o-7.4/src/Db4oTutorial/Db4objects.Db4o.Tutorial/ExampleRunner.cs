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
namespace Db4objects.Db4o.Tutorial
{
    using System;
    using System.IO;
    using System.Reflection;
    
	using Db4objects.Db4o.Tutorial.F1;
    
    public class ExampleRunner
    {   
        /// <summary>
        /// Executes the method passed as argument if its signature is
        /// correct.
        /// </summary>
        /// <returns>true if the method was executed, false otherwise</returns>
        delegate bool Executor(MethodInfo method);
        
        Executor[] _executors = new Executor[] {
            new Executor(PlainExecutor),
            new Executor(ContainerExecutor),
            new Executor(LocalServerExecutor),
            new Executor(RemoteServerExecutor)
        };
        
        public void Reset()
        {
            File.Delete(Util.YapFileName);
        }
        
        public void Run(string typeName, string method, TextWriter console)
        {
            TextWriter saved = Console.Out;         
            Console.SetOut(console);
            try
            {
                RunExample(typeName, method);               
            }
            finally
            {
                Console.SetOut(saved);
            }
        }
        
        void RunExample(string typeName, string method)
        {
        		Type type = typeof(Util).Assembly.GetType(typeName, true);
			MethodInfo example = type.GetMethod(method, BindingFlags.IgnoreCase|BindingFlags.Static|BindingFlags.Public);
            
            bool found = false;
            foreach (Executor _e in _executors)
            {
                if (_e(example))
                {
                    found = true;
                    break;
                }
            }
            
            if (!found)
            {
                throw new ArgumentException("No executor found for method '" + example + "'");
            }
        }
    
        static bool ContainerExecutor(MethodInfo method)
        {
            if (!CheckSignature(method, typeof(IObjectContainer)))
            {
                return false;
            }       
            
            IObjectContainer container = Db4oFactory.OpenFile(Util.YapFileName);
            try
            {
                method.Invoke(null, new object[] { container });
            }
            finally
            {
                container.Close();
            }
            return true;
        }
        
        static bool LocalServerExecutor(MethodInfo method)
        {
            if (!CheckSignature(method, typeof(IObjectServer)))
            {
                return false;
            }
            
            IObjectServer server = Db4oFactory.OpenServer(Util.YapFileName, 0);
            try
            {                
                method.Invoke(null, new object[] { server });
            }
            finally
            {
                server.Close();
            }
            return true;
        }
        
        static bool RemoteServerExecutor(MethodInfo method)
        {
            if (!CheckSignature(method, typeof(int), typeof(string), typeof(string)))
            {
                return false;
            }
            
            IObjectServer server = Db4oFactory.OpenServer(Util.YapFileName, Util.ServerPort);
            try
            {   
                server.GrantAccess(Util.ServerUser, Util.ServerPassword);
                method.Invoke(null, new object[] { Util.ServerPort, Util.ServerUser, Util.ServerPassword });                
            }
            finally
            {
                server.Close();
            }
            return true;
        }
    
        static bool PlainExecutor(MethodInfo method)
        {
            if (0 != method.GetParameters().Length)
            {
                return false;
            }
            method.Invoke(null, new object[0]);
            return true;
        }
        
        static bool CheckSignature(MethodInfo method, params Type[] types)
        {
            ParameterInfo[] parameters = method.GetParameters();
            if (types.Length != parameters.Length)
            {
                return false;
            }
            
            for (int i=0; i<parameters.Length; ++i)
            {
                if (types[i] != parameters[i].ParameterType)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

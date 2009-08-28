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
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Tests.Util;
using Db4oUnit.Extensions.Fixtures;

namespace Db4objects.Db4o.Tests.CLI1.Aliases
{
    /// <summary>
    /// </summary>
    public class JavaFromNetAliasesTestCase : BaseAliasesTestCase, IOptOutCS
    {
#if !CF
        public void TestAccessingJavaFromDotnet()
        {
            if (!JavaServices.CanRunJavaCompatibilityTests())
            {
                return;
            }

            GenerateJavaData();
            using (IObjectContainer container = OpenJavaDataFile())
            {
                AssertAliasedData(container);
            }
        }

        public void TestUpdatingAliasedDataSameSession()
        {
            if (!JavaServices.CanRunJavaCompatibilityTests())
            {
                return;
            }

            GenerateJavaData();
            using (IObjectContainer container = OpenJavaDataFile())
            {
                string[] newNames = UpdateAliasedData(container);
                AssertAliasedData(QueryAliasedData(container), newNames);
            }
        }

        public void TestUpdatingAliasedDataDifferentSession()
        {
            if (!JavaServices.CanRunJavaCompatibilityTests())
            {
                return;
            }

            GenerateJavaData();
            string[] newNames = UpdateAliasedData();
            using (IObjectContainer container = OpenJavaDataFile())
            {
                AssertAliasedData(QueryAliasedData(container), newNames);
            }
        }

        private string[] UpdateAliasedData()
        {
            using (IObjectContainer container = OpenJavaDataFile())
            {
                return UpdateAliasedData(container);
            }
        }

        private string[] UpdateAliasedData(IObjectContainer container)
        {
            ArrayList newNames = new ArrayList();
            foreach (IPerson person in QueryAliasedData(container))
            {
                string newName = person.Name + "*";
                person.Name = newName;
                container.Store(person);
                newNames.Add(newName);
            }

            // new item
            string newItemName = "orestes";
            container.Store(CreateAliasedData(newItemName));
            newNames.Add(newItemName);

            container.Commit();
            return (string[])newNames.ToArray(typeof(string));
        }

        private void ConfigureAliases(IConfiguration configuration)
        {
            configuration.AddAlias(new TypeAlias("com.db4o.test.aliases.Person2", GetTypeName(GetAliasedDataType())));
            //	        configuration.AddAlias(
            //	            new WildcardAlias(
            //	                "com.db4o.test.aliases.*",
            //	                CurrentNamespace + ".*, " + CurrentAssemblyName));
            configuration.AddAlias(
                new TypeAlias("com.db4o.ext.Db4oDatabase", GetTypeName(typeof(Db4oDatabase))));
        }

        private IObjectContainer OpenJavaDataFile()
        {
            IConfiguration configuration = Db4oFactory.NewConfiguration();
            ConfigureAliases(configuration);
            return Db4oFactory.OpenFile(configuration, GetJavaDataFile());
        }

        private static string GetJavaDataFile()
        {
            return IOServices.BuildTempPath("java.yap");
        }

        private void GenerateJavaData()
        {
            DeleteOldDataFile();
            CompileJavaProgram();
        	RunJavaProgram();
        }

    	private static void RunJavaProgram()
    	{
    		string stdout = JavaServices.java("com.db4o.test.aliases.Program", GetJavaDataFile());
    		Console.WriteLine(stdout);
    	}

    	private static void DeleteOldDataFile()
    	{
    		File.Delete(GetJavaDataFile());
    	}

    	private void CompileJavaProgram()
        {
            String code = @"
package com.db4o.test.aliases;

import com.db4o.*;

class Person2 {
	String _name;
	public Person2(String name) {
		_name = name;
	}
}

public class Program {
	public static void main(String[] args) {
		String fname = args[0];
		ObjectContainer container = Db4o.openFile(fname);
		container.set(new Person2(""Homer Simpson""));
		container.set(new Person2(""John Cleese""));
		container.close();
		System.out.println(""success"");
	}
}";

    		JavaServices.ResetJavaTempPath();
    		string stdout = JavaServices.CompileJavaCode("com/db4o/test/aliases/Program.java", code);
    		Console.WriteLine(stdout);
        }

#endif 

        private string GetTypeName(Type type)
        {
            return type.FullName + ", " + type.Assembly.GetName().Name;
        }
    }
}

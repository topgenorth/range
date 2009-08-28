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
using Db4objects.Db4o.Reflect;

namespace Db4oTool.Core{

	using System;
	
	using Db4objects.Db4o;
	using Db4objects.Db4o.Ext;
	using Db4objects.Db4o.Internal;
	using Db4objects.Db4o.Foundation;

    public class Statistics {
      
        public void Run(String filename) {
            if (new Sharpen.IO.File(filename).Exists()) {
                IObjectContainer con1;
                try { 
                    con1 = Db4oFactory.OpenFile(filename);
                    PrintHeader("STATISTICS");
                    Console.WriteLine("File: " + filename);
                    PrintStats(con1, filename);
                    con1.Close();
                      
                }  catch (Exception e) { 
                    Console.WriteLine("Statistics failed for file: \'" + filename + "\'");
                    Console.WriteLine(e.Message);
                    Sharpen.Runtime.PrintStackTrace(e);
                                         
                }
            } else {
                Console.WriteLine("File not found: \'" + filename + "\'");
            }
        }
      
        private void PrintStats(IObjectContainer con, String filename) {
            Tree unavailable = new TreeString(REMOVE);
            Tree noConstructor = new TreeString(REMOVE);
            IStoredClass[] internalClasses = con.Ext().StoredClasses();
            for (int i1 = 0; i1 < internalClasses.Length; i1++) {
                try { 
                    IReflectClass clazz1 = con.Ext().Reflector().ForName(internalClasses[i1].GetName());
                    try { 
                        clazz1.NewInstance();
                    }  catch (Exception) { 
                        noConstructor = noConstructor.Add(new TreeString(internalClasses[i1].GetName()));
                    }
                }  catch (Exception) { 
                    unavailable = unavailable.Add(new TreeString(internalClasses[i1].GetName()));
                                         
                }
            }
            unavailable = unavailable.RemoveLike(new TreeString(REMOVE));
            noConstructor = noConstructor.RemoveLike(new TreeString(REMOVE));
            if (unavailable != null) {
                PrintHeader("UNAVAILABLE");
                unavailable.Traverse(new StatisticsPrintKey());
            }
            if (noConstructor != null) {
                PrintHeader("NO PUBLIC CONSTRUCTOR");
                noConstructor.Traverse(new StatisticsPrintKey());
            }
            PrintHeader("CLASSES");
            Console.WriteLine("Number of objects per class:");
            if (internalClasses.Length > 0) {
                Tree all1 = new TreeStringObject(internalClasses[0].GetName(), internalClasses[0]);
                for (int i1 = 1; i1 < internalClasses.Length; i1++) {
                    all1 = all1.Add(new TreeStringObject(internalClasses[i1].GetName(), internalClasses[i1]));
                }
                all1.Traverse(new StatisticsPrintNodes());
            }
            PrintHeader("SUMMARY");
            Console.WriteLine("File: " + filename);
            Console.WriteLine("Stored classes: " + internalClasses.Length);
            if (unavailable != null) {
                Console.WriteLine("Unavailable classes: " + unavailable.Size());
            }
            if (noConstructor != null) {
                Console.WriteLine("Classes without public constructors: " + noConstructor.Size());
            }
            Console.WriteLine("Total number of objects: " + (ids.Size() - 1));
        }
      
        private void PrintHeader(String str) {
            int starcount = (39 - str.Length) / 2;
            string stars = "";
            for (int i1 = 0; i1 < starcount; i1++) {
                stars += "*";
            }
            Console.WriteLine("\n\n" + stars + " " + str + " " + stars);
        }

        internal static TreeInt ids = new TreeInt(0);
        private static String REMOVE = "XXxxREMOVExxXX";
    }

    internal class StatisticsPrintKey : IVisitor4{
        public void Visit(Object obj){
            Console.WriteLine(((TreeString)obj)._key);           
        }
    }

    internal class StatisticsPrintNodes : IVisitor4{
        public void Visit(Object obj){
            TreeStringObject node = (TreeStringObject)obj;
            long[] newIDs = ((IStoredClass)node._object).GetIDs();
            for (int j = 0; j < newIDs.Length; j ++) {
                if (Statistics.ids.Find(new TreeInt((int)newIDs[j])) == null) {
                    Statistics.ids = (TreeInt)Statistics.ids.Add(new TreeInt((int)newIDs[j]));
                }
            }
            Console.WriteLine(node._key + ": " + newIDs.Length);
        }

    }
}
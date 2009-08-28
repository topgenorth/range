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

namespace Db4objects.Db4o {

	/// <exclude />
    public class Dynamic {

		private const BindingFlags AllMembers = BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        public static object GetProperty(object obj, string prop){
            if(obj != null){
                Type type = TypeForObject(obj);
                try {
                    PropertyInfo pi = type.GetProperty(prop, AllMembers);
                    return pi.GetValue(obj,null);
                } catch {
                }
            }
            return null;
        }

        public static void SetProperty(object obj, string prop, object val){
            if(obj != null){
                Type type = TypeForObject(obj);
                try {
                    PropertyInfo pi = type.GetProperty(prop, AllMembers);
                    pi.SetValue(obj, val, null);
                } catch {
                }
            }
        }

        private static Type TypeForObject(object obj){
            Type type = obj as Type;
            if(type != null){
                return type;
            }
            return obj.GetType();
        }
    }
}

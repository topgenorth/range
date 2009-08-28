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
using Sharpen.Lang;
using Db4objects.Db4o;

namespace Db4objects.Db4o.Config {

	/// <exclude />
    public class TDictionary : IObjectTranslator {

        public void OnActivate(IObjectContainer objectContainer, object obj, object members){
            IDictionary dict = (IDictionary)obj;
            dict.Clear();
            if(members != null){
                Entry[] entries = (Entry[]) members;
                for(int i = 0; i < entries.Length; i++){
                    if(entries[i].key != null && entries[i].value != null){
                        dict[entries[i].key] =  entries[i].value;
                    }
                }
            }
        }

        public Object OnStore(IObjectContainer objectContainer, object obj){
            IDictionary dict = (IDictionary)obj;
            Entry[] entries = new Entry[dict.Count];
            IDictionaryEnumerator e = dict.GetEnumerator();
            e.Reset();
            for(int i = 0; i < dict.Count; i++){
                e.MoveNext();
                entries[i] = new Entry();
                entries[i].key = e.Key;
                entries[i].value = e.Value;
            }
            return entries;
        }

        public System.Type StoredClass(){
            return typeof(Entry[]);
        }
    }
}

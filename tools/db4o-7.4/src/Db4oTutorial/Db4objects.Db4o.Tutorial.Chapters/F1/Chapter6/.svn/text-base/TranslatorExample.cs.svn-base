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
using System.Globalization;

using Db4objects.Db4o;

namespace Db4objects.Db4o.Tutorial.F1.Chapter6
{
	public class TranslatorExample : Util
    {
        public static void Main(string[] args)
        {
            TryStoreWithCallConstructors();
            TryStoreWithoutCallConstructors();
            StoreWithTranslator();
        }
        
        public static void TryStoreWithCallConstructors()
        {
            Db4oFactory.Configure().ExceptionsOnNotStorable(true);
            Db4oFactory.Configure().ObjectClass(typeof(CultureInfo))
                .CallConstructor(true);
            TryStoreAndRetrieve();
        }
        
        public static void TryStoreWithoutCallConstructors()
        {
            Db4oFactory.Configure().ObjectClass(typeof(CultureInfo))
                .CallConstructor(false);
            // trying to store objects that hold onto
            // system resources can be pretty nasty
            // uncomment the following line to see
            // how nasty it can be
            //TryStoreAndRetrieve();
        }
        
        public static void StoreWithTranslator()
        {
            Db4oFactory.Configure().ObjectClass(typeof(CultureInfo))
                .Translate(new CultureInfoTranslator());
            TryStoreAndRetrieve();
            Db4oFactory.Configure().ObjectClass(typeof(CultureInfo))
                .Translate(null);
        }
        
        public static void TryStoreAndRetrieve()
        {
            IObjectContainer db = Db4oFactory.OpenFile(Util.YapFileName);
            try
            {
                string[] champs = new string[] { "Ayrton Senna", "Nelson Piquet" };
                LocalizedItemList LocalizedItemList = new LocalizedItemList(CultureInfo.CreateSpecificCulture("pt-BR"), champs);
                System.Console.WriteLine("ORIGINAL: {0}", LocalizedItemList);
                db.Store(LocalizedItemList);
            }
            catch (Exception x)
            {
                System.Console.WriteLine(x);
                return;
            }
            finally
            {
                db.Close();
            }
            db = Db4oFactory.OpenFile(Util.YapFileName);
            try
            {
                IObjectSet result = db.QueryByExample(typeof(LocalizedItemList));
                while (result.HasNext())
                {
                    LocalizedItemList LocalizedItemList = (LocalizedItemList)result.Next();
                    System.Console.WriteLine("RETRIEVED: {0}", LocalizedItemList);
                    db.Delete(LocalizedItemList);
                }
            }
            finally
            {
                db.Close();
            }
        }
    }
}
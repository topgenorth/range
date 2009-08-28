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
using System.Collections;
using System.Reflection;

namespace Db4objects.Db4o.Reflect.Net
{
	public class NetField : Db4objects.Db4o.Reflect.IReflectField
	{
		private readonly Db4objects.Db4o.Reflect.IReflector reflector;

		protected readonly System.Reflection.FieldInfo field;

        private static IList _transientMarkers;

		public NetField(Db4objects.Db4o.Reflect.IReflector reflector, System.Reflection.FieldInfo field
			)
		{
			this.reflector = reflector;
			this.field = field;
		}

        public override string ToString()
        {
            return string.Format("NetField({0})", field);
        }

		public virtual string GetName()
		{
			return field.Name;
		}

		public virtual Db4objects.Db4o.Reflect.IReflectClass GetFieldType()
		{
			return reflector.ForClass(field.FieldType);
		}

		public virtual bool IsPublic()
		{
			return field.IsPublic;
		}

		public virtual bool IsStatic()
		{
			return field.IsStatic;
		}

		public virtual bool IsTransient()
		{
            return IsTransient(field);
		}

		public virtual void SetAccessible()
		{	
		}

		public virtual object Get(object onObject)
		{
			try
			{
				return field.GetValue(onObject);
			}
			catch
			{
				return null;
			}
		}

		public virtual void Set(object onObject, object attribute)
		{
			try
			{
				field.SetValue(onObject, attribute);
			}
			catch
			{
			}
		}
		
		public object IndexEntry(object orig)
		{
			return orig;
		}
		
		public Db4objects.Db4o.Reflect.IReflectClass IndexType()
		{
			return GetFieldType();
		}

        public static bool IsTransient(FieldInfo field)
        {
            if (field.IsNotSerialized) return true;
            if (field.IsDefined(typeof(TransientAttribute), true)) return true;
            if (_transientMarkers == null) return false;
            return CheckForTransient(field.GetCustomAttributes(true));
        }

        private static bool CheckForTransient(object[] attributes)
        {   
            if (attributes == null) return false;

            foreach (object attribute in attributes)
            {
                string attributeName = attribute.GetType().FullName;
                if (_transientMarkers.Contains(attributeName)) return true;
            }
            return false;
        }

        public static void MarkTransient(System.Type attributeType)
        {
            MarkTransient(attributeType.FullName);
        }

        public static void MarkTransient(string attributeName)
        {
            if (_transientMarkers == null)
            {
                _transientMarkers = new ArrayList();
            }
            else if (_transientMarkers.Contains(attributeName))
            {
                return;
            }
            _transientMarkers.Add(attributeName);
        }

	    public static void ResetTransientMarkers()
	    {
            _transientMarkers = null;
	    }
	}
}

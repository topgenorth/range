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
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal.CS;
using Db4objects.Db4o.Reflect;
using Db4objects.Db4o.Reflect.Generic;

namespace Db4objects.Db4o.Internal.CS
{
	public class ClassInfoHelper
	{
		private Hashtable4 _classMetaTable = new Hashtable4();

		private Hashtable4 _genericClassTable = new Hashtable4();

		public virtual ClassInfo GetClassMeta(IReflectClass claxx)
		{
			string className = claxx.GetName();
			if (IsSystemClass(className))
			{
				return ClassInfo.NewSystemClass(className);
			}
			ClassInfo existing = LookupClassMeta(className);
			if (existing != null)
			{
				return existing;
			}
			return NewUserClassMeta(claxx);
		}

		private ClassInfo NewUserClassMeta(IReflectClass claxx)
		{
			ClassInfo classMeta = ClassInfo.NewUserClass(claxx.GetName());
			classMeta.SetSuperClass(MapSuperclass(claxx));
			RegisterClassMeta(claxx.GetName(), classMeta);
			classMeta.SetFields(MapFields(claxx.GetDeclaredFields()));
			return classMeta;
		}

		private ClassInfo MapSuperclass(IReflectClass claxx)
		{
			IReflectClass superClass = claxx.GetSuperclass();
			if (superClass != null)
			{
				return GetClassMeta(superClass);
			}
			return null;
		}

		private FieldInfo[] MapFields(IReflectField[] fields)
		{
			FieldInfo[] fieldsMeta = new FieldInfo[fields.Length];
			for (int i = 0; i < fields.Length; ++i)
			{
				IReflectField field = fields[i];
				bool isArray = field.GetFieldType().IsArray();
				IReflectClass fieldClass = isArray ? field.GetFieldType().GetComponentType() : field
					.GetFieldType();
				bool isPrimitive = fieldClass.IsPrimitive();
				// TODO: need to handle NArray, currently it ignores NArray and alway sets NArray flag false.
				fieldsMeta[i] = new FieldInfo(field.GetName(), GetClassMeta(fieldClass), isPrimitive
					, isArray, false);
			}
			return fieldsMeta;
		}

		private static bool IsSystemClass(string className)
		{
			// TODO: We should send the whole class meta if we'd like to support
			// java and .net communication (We have this request in our user forum
			// http://developer.db4o.com/forums/thread/31504.aspx). If we only want
			// to support java & .net platform separately, then this method should
			// be moved to Platform4.
			return className.StartsWith("java");
		}

		private ClassInfo LookupClassMeta(string className)
		{
			return (ClassInfo)_classMetaTable.Get(className);
		}

		private void RegisterClassMeta(string className, ClassInfo classMeta)
		{
			_classMetaTable.Put(className, classMeta);
		}

		public virtual GenericClass ClassMetaToGenericClass(GenericReflector reflector, ClassInfo
			 classMeta)
		{
			if (classMeta.IsSystemClass())
			{
				return (GenericClass)reflector.ForName(classMeta.GetClassName());
			}
			string className = classMeta.GetClassName();
			// look up from generic class table.
			GenericClass genericClass = LookupGenericClass(className);
			if (genericClass != null)
			{
				return genericClass;
			}
			IReflectClass reflectClass = reflector.ForName(className);
			if (reflectClass != null)
			{
				return (GenericClass)reflectClass;
			}
			GenericClass genericSuperClass = null;
			ClassInfo superClassMeta = classMeta.GetSuperClass();
			if (superClassMeta != null)
			{
				genericSuperClass = ClassMetaToGenericClass(reflector, superClassMeta);
			}
			genericClass = new GenericClass(reflector, null, className, genericSuperClass);
			RegisterGenericClass(className, genericClass);
			FieldInfo[] fields = classMeta.GetFields();
			GenericField[] genericFields = new GenericField[fields.Length];
			for (int i = 0; i < fields.Length; ++i)
			{
				ClassInfo fieldClassMeta = fields[i].GetFieldClass();
				string fieldName = fields[i].GetFieldName();
				GenericClass genericFieldClass = ClassMetaToGenericClass(reflector, fieldClassMeta
					);
				genericFields[i] = new GenericField(fieldName, genericFieldClass, fields[i]._isPrimitive
					);
			}
			genericClass.InitFields(genericFields);
			return genericClass;
		}

		private GenericClass LookupGenericClass(string className)
		{
			return (GenericClass)_genericClassTable.Get(className);
		}

		private void RegisterGenericClass(string className, GenericClass classMeta)
		{
			_genericClassTable.Put(className, classMeta);
			((GenericReflector)classMeta.Reflector()).Register(classMeta);
		}
	}
}

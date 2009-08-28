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
using Sharpen.Lang;
using Db4objects.Db4o.Reflect.Core;
using Db4objects.Db4o.Foundation;

namespace Db4objects.Db4o.Reflect.Net
{
	/// <summary>Reflection implementation for Class to map to .NET reflection.</summary>
	/// <remarks>Reflection implementation for Class to map to .NET reflection.</remarks>
	public class NetClass : Db4objects.Db4o.Reflect.Core.IConstructorAwareReflectClass
	{
		protected readonly Db4objects.Db4o.Reflect.IReflector _reflector;
		
		protected readonly Db4objects.Db4o.Reflect.Net.NetReflector _netReflector;

		private readonly System.Type _type;

		private ReflectConstructorSpec _constructor;

		private object[] constructorParams;
		
	    private string _name;
	    
	    private Db4objects.Db4o.Reflect.IReflectField[] _fields;

	    public NetClass(Db4objects.Db4o.Reflect.IReflector reflector, Db4objects.Db4o.Reflect.Net.NetReflector netReflector, System.Type clazz)
		{
			if(reflector == null)
			{
				throw new NullReferenceException();
			}
			
			if(netReflector == null)
			{
				throw new NullReferenceException();
			}
			
			_reflector = reflector;
			_netReflector = netReflector;
			_type = clazz;
			_constructor = ReflectConstructorSpec.UnspecifiedConstructor;
		}

		public virtual Db4objects.Db4o.Reflect.IReflectClass GetComponentType()
		{
			return _reflector.ForClass(_type.GetElementType());
		}

		private IReflectConstructor[] GetDeclaredConstructors()
		{
			System.Reflection.ConstructorInfo[] constructors = _type.GetConstructors();
			IReflectConstructor[] reflectors = new IReflectConstructor
				[constructors.Length];
			for (int i = 0; i < constructors.Length; i++)
			{
				reflectors[i] = new Db4objects.Db4o.Reflect.Net.NetConstructor(_reflector, constructors[i]);
			}
			return reflectors;
		}

		public virtual Db4objects.Db4o.Reflect.IReflectField GetDeclaredField(string name)
		{
			foreach (IReflectField field in GetDeclaredFields())
			{
				if (field.GetName() == name) return field;
			}
			return null;
		}

		public virtual Db4objects.Db4o.Reflect.IReflectField[] GetDeclaredFields()
		{
			if (_fields == null)
			{
				_fields = CreateDeclaredFieldsArray();
			}
			return _fields;
		}
		
		private Db4objects.Db4o.Reflect.IReflectField[] CreateDeclaredFieldsArray()
		{	
			System.Reflection.FieldInfo[] fields = Sharpen.Runtime.GetDeclaredFields(_type);
			Db4objects.Db4o.Reflect.IReflectField[] reflectors = new Db4objects.Db4o.Reflect.IReflectField[fields.Length];
			for (int i = 0; i < reflectors.Length; i++)
			{
				reflectors[i] = CreateField(fields[i]);
			}
			return reflectors;
		}

		protected virtual IReflectField CreateField(FieldInfo field)
		{
			return new Db4objects.Db4o.Reflect.Net.NetField(_reflector, field);
		}

		public virtual Db4objects.Db4o.Reflect.IReflectClass GetDelegate()
		{
			return this;
		}

		public virtual Db4objects.Db4o.Reflect.IReflectMethod GetMethod(
			string methodName,
			Db4objects.Db4o.Reflect.IReflectClass[] paramClasses)
		{
			try
			{
                System.Reflection.MethodInfo method = Sharpen.Runtime.GetMethod(_type, methodName, Db4objects.Db4o.Reflect.Net.NetReflector
					.ToNative(paramClasses));
				if (method == null)
				{
					return null;
				}
				return new Db4objects.Db4o.Reflect.Net.NetMethod(_reflector, method);
			}
			catch
			{
				return null;
			}
		}

		public virtual string GetName()
		{
            if (_name == null)
            {
                _name = TypeReference.FromType(_type).GetUnversionedName();
            }
            return _name;
		}

		public virtual Db4objects.Db4o.Reflect.IReflectClass GetSuperclass()
		{
			return _reflector.ForClass(_type.BaseType);
		}

		public virtual bool IsAbstract()
		{
			return _type.IsAbstract;
		}

		public virtual bool IsArray()
		{
			return _type.IsArray;
		}

		public virtual bool IsAssignableFrom(Db4objects.Db4o.Reflect.IReflectClass type)
		{
			if (!(type is Db4objects.Db4o.Reflect.Net.NetClass))
			{
				return false;
			}
			return _type.IsAssignableFrom(((Db4objects.Db4o.Reflect.Net.NetClass)type).GetNetType());
		}

		public virtual bool IsInstance(object obj)
		{
			return _type.IsInstanceOfType(obj);
		}

		public virtual bool IsInterface()
		{
			return _type.IsInterface;
		}

		public virtual bool IsCollection()
		{
			return _reflector.IsCollection(this);
		}

		public virtual bool IsPrimitive()
		{
			return _type.IsPrimitive
			       || _type == typeof(System.DateTime)
			       || _type == typeof(decimal);
		}

		public virtual object NewInstance()
		{
			CreateConstructor();
			return _constructor.NewInstance();
		}

		private static bool CanCreate(Type type)
		{
			return !type.IsAbstract;
		}

		public virtual System.Type GetNetType()
		{
			return _type;
		}

		public virtual Db4objects.Db4o.Reflect.IReflector Reflector()
		{
			return _reflector;
		}
		
		public virtual IReflectConstructor GetSerializableConstructor()
		{
#if !CF
			return new SerializationConstructor(GetNetType());
#endif
			return null;
		}

		public override string ToString()
		{
			return "NetClass(" + _type + ")";
		}

		public virtual object NullValue() 
		{
			return _netReflector.NullValue(this);
		}
	
		private void CreateConstructor() 
		{
			if(!_constructor.CanBeInstantiated().IsUnspecified())
			{
				return;
			}
			_constructor = ConstructorSupport.CreateConstructor(this, _type, _netReflector.Configuration(), GetDeclaredConstructors());
		}
		
		public virtual bool EnsureCanBeInstantiated() {
			CreateConstructor();
			return _constructor.CanBeInstantiated().DefiniteYes();
		}
		
	}
}

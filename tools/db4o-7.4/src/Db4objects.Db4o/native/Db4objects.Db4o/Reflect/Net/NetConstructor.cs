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
namespace Db4objects.Db4o.Reflect.Net
{

	/// <remarks>Reflection implementation for Constructor to map to .NET reflection.</remarks>
	public class NetConstructor : Db4objects.Db4o.Reflect.Core.IReflectConstructor
	{
		private readonly Db4objects.Db4o.Reflect.IReflector reflector;

		private readonly System.Reflection.ConstructorInfo constructor;

		public NetConstructor(Db4objects.Db4o.Reflect.IReflector reflector, System.Reflection.ConstructorInfo
			 constructor)
		{
			this.reflector = reflector;
			this.constructor = constructor;
			Db4objects.Db4o.Internal.Platform4.SetAccessible(constructor);
		}

		public virtual Db4objects.Db4o.Reflect.IReflectClass[] GetParameterTypes()
		{
			return Db4objects.Db4o.Reflect.Net.NetReflector.ToMeta(reflector, Sharpen.Runtime.GetParameterTypes(constructor));
		}

		public virtual object NewInstance(object[] parameters)
		{
			try
			{
				return constructor.Invoke(parameters);
			}
			catch
			{
				return null;
			}
		}
	}
}

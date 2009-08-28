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
using System.Text;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Delete;
using Db4objects.Db4o.Internal.Handlers;
using Db4objects.Db4o.Marshall;
using Db4objects.Db4o.Reflect;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Internal.Handlers
{
	public sealed class StringBufferHandler : ITypeHandler4, IBuiltinTypeHandler, ISecondClassTypeHandler
		, IVariableLengthTypeHandler, IEmbeddedTypeHandler
	{
		private IReflectClass _classReflector;

		public void Defragment(IDefragmentContext context)
		{
			StringHandler(context).Defragment(context);
		}

		/// <exception cref="Db4oIOException"></exception>
		public void Delete(IDeleteContext context)
		{
			StringHandler(context).Delete(context);
		}

		public object Read(IReadContext context)
		{
			object read = StringHandler(context).Read(context);
			if (null == read)
			{
				return null;
			}
			return new StringBuilder((string)read);
		}

		public void Write(IWriteContext context, object obj)
		{
			StringHandler(context).Write(context, obj.ToString());
		}

		private ITypeHandler4 StringHandler(IContext context)
		{
			return Handlers(context)._stringHandler;
		}

		private HandlerRegistry Handlers(IContext context)
		{
			return ((IInternalObjectContainer)context.ObjectContainer()).Handlers();
		}

		public IPreparedComparison PrepareComparison(IContext context, object obj)
		{
			return StringHandler(context).PrepareComparison(context, obj);
		}

		public IReflectClass ClassReflector()
		{
			return _classReflector;
		}

		public void RegisterReflector(IReflector reflector)
		{
			_classReflector = reflector.ForClass(typeof(StringBuilder));
		}
	}
}

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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Activation;
using Db4objects.Db4o.Internal.Delete;
using Db4objects.Db4o.Internal.Handlers;
using Db4objects.Db4o.Internal.Marshall;
using Db4objects.Db4o.Marshall;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Typehandlers
{
	/// <summary>Typehandler for classes that implement java.util.Map.</summary>
	/// <remarks>Typehandler for classes that implement java.util.Map.</remarks>
	/// <decaf.ignore.jdk11></decaf.ignore.jdk11>
	public class MapTypeHandler : ITypeHandler4, IFirstClassHandler, ICanHoldAnythingHandler
		, IVariableLengthTypeHandler
	{
		public virtual IPreparedComparison PrepareComparison(IContext context, object obj
			)
		{
			// TODO Auto-generated method stub
			return null;
		}

		public virtual void Write(IWriteContext context, object obj)
		{
			IDictionary map = (IDictionary)obj;
			WriteElementCount(context, map);
			WriteElements(context, map);
		}

		public virtual object Read(IReadContext context)
		{
			IDictionary map = (IDictionary)((UnmarshallingContext)context).PersistentObject();
			int elementCount = context.ReadInt();
			ITypeHandler4 elementHandler = ElementTypeHandler(context, map);
			for (int i = 0; i < elementCount; i++)
			{
				object key = context.ReadObject(elementHandler);
				object value = context.ReadObject(elementHandler);
				map.Add(key, value);
			}
			return map;
		}

		private void WriteElementCount(IWriteContext context, IDictionary map)
		{
			context.WriteInt(map.Count);
		}

		private void WriteElements(IWriteContext context, IDictionary map)
		{
			ITypeHandler4 elementHandler = ElementTypeHandler(context, map);
			IEnumerator elements = map.Keys.GetEnumerator();
			while (elements.MoveNext())
			{
				object key = elements.Current;
				context.WriteObject(elementHandler, key);
				context.WriteObject(elementHandler, map[key]);
			}
		}

		private ObjectContainerBase Container(IContext context)
		{
			return ((IInternalObjectContainer)context.ObjectContainer()).Container();
		}

		private ITypeHandler4 ElementTypeHandler(IContext context, IDictionary map)
		{
			// TODO: If all elements in the map are of one type,
			//       it is possible to use a more specific handler
			return Container(context).Handlers().UntypedObjectHandler();
		}

		/// <exception cref="Db4oIOException"></exception>
		public virtual void Delete(IDeleteContext context)
		{
			if (!context.CascadeDelete())
			{
				return;
			}
			ITypeHandler4 handler = ElementTypeHandler(context, null);
			int elementCount = context.ReadInt();
			for (int i = elementCount; i > 0; i--)
			{
				handler.Delete(context);
				handler.Delete(context);
			}
		}

		public virtual void Defragment(IDefragmentContext context)
		{
			ITypeHandler4 handler = ElementTypeHandler(context, null);
			int elementCount = context.ReadInt();
			for (int i = elementCount; i > 0; i--)
			{
				context.Defragment(handler);
				context.Defragment(handler);
			}
		}

		public void CascadeActivation(ActivationContext4 context)
		{
			IDictionary map = (IDictionary)context.TargetObject();
			IEnumerator keys = (map).Keys.GetEnumerator();
			while (keys.MoveNext())
			{
				object key = keys.Current;
				context.CascadeActivationToChild(key);
				context.CascadeActivationToChild(map[key]);
			}
		}

		public virtual ITypeHandler4 ReadCandidateHandler(QueryingReadContext context)
		{
			return this;
		}

		public virtual void CollectIDs(QueryingReadContext context)
		{
			int elementCount = context.ReadInt();
			ITypeHandler4 elementHandler = context.Container().Handlers().UntypedObjectHandler
				();
			for (int i = 0; i < elementCount; i++)
			{
				context.ReadId(elementHandler);
				context.SkipId(elementHandler);
			}
		}
	}
}

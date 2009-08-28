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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Activation;
using Db4objects.Db4o.Internal.Delete;
using Db4objects.Db4o.Internal.Handlers;
using Db4objects.Db4o.Internal.Marshall;
using Db4objects.Db4o.Internal.Slots;
using Db4objects.Db4o.Marshall;
using Db4objects.Db4o.Typehandlers;

namespace Db4objects.Db4o.Internal.Handlers
{
	/// <exclude></exclude>
	public class NullFieldAwareTypeHandler : IFieldAwareTypeHandler
	{
		public static readonly IFieldAwareTypeHandler Instance = new NullFieldAwareTypeHandler
			();

		public virtual void AddFieldIndices(ObjectIdContextImpl context, Slot oldSlot)
		{
		}

		public virtual void ClassMetadata(Db4objects.Db4o.Internal.ClassMetadata classMetadata
			)
		{
		}

		public virtual void CollectIDs(CollectIdContext context, string fieldName)
		{
		}

		public virtual void DeleteMembers(DeleteContextImpl deleteContext, bool isUpdate)
		{
		}

		public virtual void ReadVirtualAttributes(ObjectReferenceContext context)
		{
		}

		public virtual bool SeekToField(ObjectHeaderContext context, FieldMetadata field)
		{
			return false;
		}

		public virtual void Defragment(IDefragmentContext context)
		{
		}

		/// <exception cref="Db4oIOException"></exception>
		public virtual void Delete(IDeleteContext context)
		{
		}

		public virtual object Read(IReadContext context)
		{
			return null;
		}

		public virtual void Write(IWriteContext context, object obj)
		{
		}

		public virtual IPreparedComparison PrepareComparison(IContext context, object obj
			)
		{
			return null;
		}

		public virtual ITypeHandler4 UnversionedTemplate()
		{
			return null;
		}

		public virtual object DeepClone(object context)
		{
			return null;
		}

		public virtual void CascadeActivation(ActivationContext4 context)
		{
		}

		public virtual void CollectIDs(QueryingReadContext context)
		{
		}

		public virtual ITypeHandler4 ReadCandidateHandler(QueryingReadContext context)
		{
			return null;
		}
	}
}

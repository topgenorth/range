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
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Fieldindex;
using Db4objects.Db4o.Internal.Query.Processor;

namespace Db4objects.Db4o.Internal.Fieldindex
{
	public class IndexedPath : IndexedNodeBase
	{
		public static IIndexedNode NewParentPath(IIndexedNode next, QCon constraint)
		{
			if (!CanFollowParent(constraint))
			{
				return null;
			}
			return new Db4objects.Db4o.Internal.Fieldindex.IndexedPath((QConObject)constraint
				.Parent(), next);
		}

		private static bool CanFollowParent(QCon con)
		{
			QCon parent = con.Parent();
			FieldMetadata parentField = GetYapField(parent);
			if (null == parentField)
			{
				return false;
			}
			FieldMetadata conField = GetYapField(con);
			if (null == conField)
			{
				return false;
			}
			return parentField.HasIndex() && parentField.HandlerClassMetadata(con.Transaction
				().Container()).IsAssignableFrom(conField.ContainingClass());
		}

		private static FieldMetadata GetYapField(QCon con)
		{
			QField field = con.GetField();
			if (null == field)
			{
				return null;
			}
			return field.GetYapField();
		}

		private IIndexedNode _next;

		public IndexedPath(QConObject parent, IIndexedNode next) : base(parent)
		{
			_next = next;
		}

		public override IEnumerator GetEnumerator()
		{
			return new IndexedPathIterator(this, _next.GetEnumerator());
		}

		public override int ResultSize()
		{
			throw new NotSupportedException();
		}
	}
}

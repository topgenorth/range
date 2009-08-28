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
using Db4objects.Db4o.Instrumentation.Api;
using Db4objects.Db4o.NativeQueries.Expr.Cmp.Operand;

namespace Db4objects.Db4o.NativeQueries.Expr.Cmp.Operand
{
	public class FieldValue : ComparisonOperandDescendant
	{
		private readonly IFieldRef _field;

		public FieldValue(IComparisonOperandAnchor root, IFieldRef field) : base(root)
		{
			_field = field;
		}

		public virtual string FieldName()
		{
			return _field.Name;
		}

		public override bool Equals(object other)
		{
			if (!base.Equals(other))
			{
				return false;
			}
			Db4objects.Db4o.NativeQueries.Expr.Cmp.Operand.FieldValue casted = (Db4objects.Db4o.NativeQueries.Expr.Cmp.Operand.FieldValue
				)other;
			return _field.Equals(casted._field);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() * 29 + _field.GetHashCode();
		}

		public override string ToString()
		{
			return base.ToString() + "." + _field;
		}

		public override void Accept(IComparisonOperandVisitor visitor)
		{
			visitor.Visit(this);
		}

		public virtual IFieldRef Field
		{
			get
			{
				return _field;
			}
		}
	}
}

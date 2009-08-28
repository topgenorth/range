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
using Db4objects.Db4o.Internal;

namespace Db4objects.Db4o.Internal
{
	[System.Serializable]
	public class FieldIndexException : Exception
	{
		private string _className;

		private string _fieldName;

		public FieldIndexException(FieldMetadata field) : this(null, null, field)
		{
		}

		public FieldIndexException(string msg, FieldMetadata field) : this(msg, null, field
			)
		{
		}

		public FieldIndexException(Exception cause, FieldMetadata field) : this(null, cause
			, field)
		{
		}

		public FieldIndexException(string msg, Exception cause, FieldMetadata field) : this
			(msg, cause, field.ContainingClass().GetName(), field.GetName())
		{
		}

		public FieldIndexException(string msg, Exception cause, string className, string 
			fieldName) : base(EnhancedMessage(msg, className, fieldName), cause)
		{
			_className = className;
			_fieldName = fieldName;
		}

		public virtual string ClassName()
		{
			return _className;
		}

		public virtual string FieldName()
		{
			return _fieldName;
		}

		private static string EnhancedMessage(string msg, string className, string fieldName
			)
		{
			string enhancedMessage = "Field index for " + className + "#" + fieldName;
			if (msg != null)
			{
				enhancedMessage += ": " + msg;
			}
			return enhancedMessage;
		}
	}
}

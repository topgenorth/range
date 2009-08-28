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
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Internal;

namespace Db4objects.Db4o.Internal
{
	/// <exclude></exclude>
	public class StoredClassImpl : IStoredClass
	{
		private readonly Transaction _transaction;

		private readonly ClassMetadata _classMetadata;

		public StoredClassImpl(Transaction transaction, ClassMetadata classMetadata)
		{
			if (classMetadata == null)
			{
				throw new ArgumentException();
			}
			_transaction = transaction;
			_classMetadata = classMetadata;
		}

		public virtual long[] GetIDs()
		{
			return _classMetadata.GetIDs(_transaction);
		}

		public virtual string GetName()
		{
			return _classMetadata.GetName();
		}

		public virtual IStoredClass GetParentStoredClass()
		{
			ClassMetadata parentClassMetadata = _classMetadata.GetAncestor();
			if (parentClassMetadata == null)
			{
				return null;
			}
			return new Db4objects.Db4o.Internal.StoredClassImpl(_transaction, parentClassMetadata
				);
		}

		public virtual IStoredField[] GetStoredFields()
		{
			IStoredField[] fieldMetadata = _classMetadata.GetStoredFields();
			IStoredField[] storedFields = new IStoredField[fieldMetadata.Length];
			for (int i = 0; i < fieldMetadata.Length; i++)
			{
				storedFields[i] = new StoredFieldImpl(_transaction, (FieldMetadata)fieldMetadata[
					i]);
			}
			return storedFields;
		}

		public virtual bool HasClassIndex()
		{
			return _classMetadata.HasClassIndex();
		}

		// TODO: Write test case.
		public virtual void Rename(string newName)
		{
			_classMetadata.Rename(newName);
		}

		public virtual IStoredField StoredField(string name, object type)
		{
			FieldMetadata fieldMetadata = (FieldMetadata)_classMetadata.StoredField(name, type
				);
			if (fieldMetadata == null)
			{
				return null;
			}
			return new StoredFieldImpl(_transaction, fieldMetadata);
		}

		public override int GetHashCode()
		{
			return _classMetadata.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (GetType() != obj.GetType())
			{
				return false;
			}
			return _classMetadata.Equals(((Db4objects.Db4o.Internal.StoredClassImpl)obj)._classMetadata
				);
		}

		public virtual int InstanceCount()
		{
			return _classMetadata.InstanceCount(_transaction);
		}
	}
}

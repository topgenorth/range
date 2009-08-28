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
using Mono.Cecil;

namespace Db4objects.Db4o.Instrumentation.Cecil
{
	public class CecilRef<T> where T : MemberReference
	{
		public static T GetReference(object type)
		{
			return ((CecilRef<T>)type).Reference;
		}

		private readonly CecilReferenceProvider _provider;
		protected T _reference;

	    public CecilRef(CecilReferenceProvider provider, T reference)
		{
			_provider = provider;
			_reference = reference;
		}

		protected ITypeRef TypeRef(TypeReference type)
		{
			return _provider.ForCecilType(type);
		}

		public T Reference
		{
			get { return _reference; }
		    set { _reference = value; }
		}

		public virtual string Name
		{
			get { return _reference.Name;  }
		}

		public override string ToString()
		{
			return Name;
		}
	}
}

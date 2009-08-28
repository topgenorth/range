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
using Db4objects.Db4o.Tests.Common.Reflect.Custom;

namespace Db4objects.Db4o.Tests.Common.Reflect.Custom
{
	/// <summary>Models an external storage model to which db4o have to be adapted to.</summary>
	/// <remarks>
	/// Models an external storage model to which db4o have to be adapted to.
	/// This particular one is a tuple based persistence mechanism modeled after the GigaSpaces
	/// persistence API.
	/// There are only two types of fields supported: string and int which are mapped
	/// to java.lang.String and java.lang.Integer.
	/// </remarks>
	public interface IPersistenceProvider
	{
		void InitContext(PersistenceContext context);

		void CloseContext(PersistenceContext context);

		void Purge(string url);

		void CreateEntryClass(PersistenceContext context, string className, string[] fieldNames
			, string[] fieldTypes);

		void CreateIndex(PersistenceContext context, string className, string fieldName);

		void DropIndex(PersistenceContext context, string className, string fieldName);

		void DropEntryClass(PersistenceContext context, string className);

		void Insert(PersistenceContext context, PersistentEntry entry);

		void Update(PersistenceContext context, PersistentEntry entry);

		int Delete(PersistenceContext context, string className, object uid);

		IEnumerator Select(PersistenceContext context, PersistentEntryTemplate template);
	}
}

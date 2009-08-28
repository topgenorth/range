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
using Db4objects.Db4o.Types;

namespace Db4objects.Db4o.Types
{
	/// <summary>factory and other methods for database-aware collections.</summary>
	/// <remarks>factory and other methods for database-aware collections.</remarks>
	[System.ObsoleteAttribute(@"since 7.0")]
	public interface IDb4oCollections
	{
		/// <summary>creates a new database-aware linked list.</summary>
		/// <remarks>
		/// creates a new database-aware linked list.
		/// <br/><br/>Usage:<br/>
		/// - declare an IList variable in your persistent class.<br/>
		/// - fill this variable with this method.<br/><br/>
		/// <b>Example:</b><br/><br/>
		/// <code>
		/// <pre>
		/// class MyClass{
		/// IList myList;
		/// }
		/// MyClass myObject = new MyClass();
		/// myObject.myList = objectContainer.Ext().Collections().NewLinkedList();
		/// </pre>
		/// </code><br/><br/>
		/// 
		/// </remarks>
		/// <returns>
		/// 
		/// <see cref="IDb4oList">IDb4oList</see>
		/// 
		/// </returns>
		/// <seealso cref="IDb4oList">IDb4oList</seealso>
		IDb4oList NewLinkedList();

		/// <summary>creates a new database-aware HashMap.</summary>
		/// <remarks>
		/// creates a new database-aware HashMap.
		/// <br/><br/>
		/// This map will call the hashCode() method on the key objects to calculate the
		/// hash value. Since the hash value is stored to the ObjectContainer, key objects
		/// will have to return the same hashCode() value in every CLR session.
		/// <br/><br/>
		/// Usage:<br/>
		/// - declare an IDictionary variable in your persistent class.<br/>
		/// - fill the variable with this method.<br/><br/>
		/// <b>Example:</b><br/><br/>
		/// <code>
		/// <pre>
		/// class MyClass{
		/// IDictionary dict;
		/// }
		/// MyClass myObject = new MyClass();
		/// myObject.dict = objectContainer.Ext().Collections().NewHashMap(0);
		/// </pre>
		/// </code><br/><br/>
		/// </remarks>
		/// <param name="initialSize">the initial size of the HashMap</param>
		/// <returns>
		/// <see cref="IDb4oMap">IDb4oMap</see>
		/// </returns>
		/// <seealso cref="IDb4oMap">IDb4oMap</seealso>
		IDb4oMap NewHashMap(int initialSize);

		/// <summary>creates a new database-aware IdentityHashMap.</summary>
		/// <remarks>
		/// creates a new database-aware IdentityHashMap.
		/// <br/><br/>
		/// Only first class objects already stored to the ObjectContainer (Objects with a db4o ID)
		/// can be used as keys for this type of Map. The internal db4o ID will be used as
		/// the hash value.
		/// <br/><br/>
		/// Usage:<br/>
		/// - declare an IDictionary variable in your persistent class.<br/>
		/// - fill the variable with this method.<br/><br/>
		/// <b>Example:</b><br/><br/>
		/// <code>
		/// <pre>
		/// public class MyClass{
		/// public IDictionary  dict;
		/// }
		/// MyClass myObject = new MyClass();
		/// myObject.dict = objectContainer.Ext().Collections().NewIdentityHashMap(0);
		/// </pre>
		/// </code><br/><br/>
		/// 
		/// </remarks>
		/// <param name="initialSize">the initial size of the IdentityHashMap</param>
		/// <returns>
		/// 
		/// <see cref="IDb4oMap">IDb4oMap</see>
		/// </returns>
		/// <seealso cref="IDb4oMap">IDb4oMap</seealso>
		IDb4oMap NewIdentityHashMap(int initialSize);
	}
}

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
using Db4objects.Db4o;
using Db4objects.Db4o.Config;

namespace Db4objects.Db4o.Config
{
	/// <summary>
	/// interface to allow instantiating objects by calling specific constructors.
	/// 
	/// </summary>
	/// <remarks>
	/// interface to allow instantiating objects by calling specific constructors.
	/// <br/><br/>
	/// By writing classes that implement this interface, it is possible to
	/// define which constructor is to be used during the instantiation of a stored object.
	/// <br/><br/>
	/// Before starting a db4o session, translator classes that implement the
	/// <code>ObjectConstructor</code> or
	/// <see cref="IObjectTranslator">IObjectTranslator</see>
	/// need to be registered.<br/><br/>
	/// Example:<br/>
	/// <code>
	/// IConfiguration config = Db4oFactory.Configure();<br/>
	/// IObjectClass oc = config.ObjectClass("Namespace.ClassName");<br/>
	/// oc.Translate(new FooTranslator());
	/// </code><br/><br/>
	/// </remarks>
	public interface IObjectConstructor : IObjectTranslator
	{
		/// <summary>db4o calls this method when a stored object needs to be instantiated.</summary>
		/// <remarks>
		/// db4o calls this method when a stored object needs to be instantiated.
		/// <br /><br />
		/// </remarks>
		/// <param name="container">the ObjectContainer used</param>
		/// <param name="storedObject">
		/// the object stored with
		/// <see cref="IObjectTranslator.OnStore">ObjectTranslator.onStore</see>
		/// .
		/// </param>
		/// <returns>the instantiated object.</returns>
		object OnInstantiate(IObjectContainer container, object storedObject);
	}
}

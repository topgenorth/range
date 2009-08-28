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
namespace Db4objects.Db4o.Config
{
	/// <summary>
	/// Implement this interface when implementing special custom Aliases
	/// for classes, packages or namespaces.
	/// 
	/// </summary>
	/// <remarks>
	/// Implement this interface when implementing special custom Aliases
	/// for classes, packages or namespaces.
	/// <br/><br/>Aliases can be used to persist classes in the running
	/// application to different persistent classes in a database file
	/// or on a db4o server.
	/// <br/><br/>Two simple Alias implementations are supplied along with
	/// db4o:<br/>
	/// -
	/// <see cref="TypeAlias">TypeAlias</see>
	/// provides an #equals() resolver to match
	/// names directly.<br/>
	/// -
	/// <see cref="WildcardAlias">WildcardAlias</see>
	/// allows simple pattern matching
	/// with one single '*' wildcard character.<br/>
	/// <br/>
	/// It is possible to create
	/// own complex
	/// <see cref="IAlias">IAlias</see>
	/// constructs by creating own resolvers
	/// that implement the
	/// <see cref="IAlias">IAlias</see>
	/// interface.
	/// <br/><br/>
	/// Four examples of concrete usecases:
	/// <br/><br/>
	/// <code>
	/// <b>// Creating an Alias for a single class</b><br/>
	/// Db4oFactory.Configure().AddAlias(<br/>
	///   new TypeAlias("Tutorial.Pilot", "Tutorial.Driver"));<br/>
	/// <br/><br/>
	/// <b>// Accessing a Java package from a .NET assembly </b><br/>
	/// Db4oFactory.Configure().AddAlias(<br/>
	///   new WildcardAlias(<br/>
	///     "com.f1.*",<br/>
	///     "Tutorial.F1.*, Tutorial"));<br/>
	/// <br/><br/>
	/// <b>// Using a different local .NET assembly</b><br/>
	/// Db4o.configure().addAlias(<br/>
	///   new WildcardAlias(<br/>
	///     "Tutorial.F1.*, F1Race",<br/>
	///     "Tutorial.F1.*, Tutorial"));<br/>
	/// <br/><br/>
	/// </code>
	/// <br/><br/>Aliases that translate the persistent name of a class to
	/// a name that already exists as a persistent name in the database
	/// (or on the server) are not permitted and will throw an exception
	/// when the database file is opened.
	/// <br/><br/>Aliases should be configured before opening a database file
	/// or connecting to a server.
	/// 
	/// </remarks>
	public interface IAlias
	{
		/// <summary>return the stored name for a runtime name or null if not handled.</summary>
		/// <remarks>return the stored name for a runtime name or null if not handled.</remarks>
		string ResolveRuntimeName(string runtimeTypeName);

		/// <summary>return the runtime name for a stored name or null if not handled.</summary>
		/// <remarks>return the runtime name for a stored name or null if not handled.</remarks>
		string ResolveStoredName(string storedTypeName);
	}
}

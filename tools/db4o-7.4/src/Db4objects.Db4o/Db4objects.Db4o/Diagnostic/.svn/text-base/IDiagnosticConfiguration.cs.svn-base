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
using Db4objects.Db4o.Diagnostic;

namespace Db4objects.Db4o.Diagnostic
{
	/// <summary>provides methods to configure the behaviour of db4o diagnostics.</summary>
	/// <remarks>
	/// provides methods to configure the behaviour of db4o diagnostics.
	/// <br/><br/>Diagnostic system can be enabled on a running db4o database
	/// to notify a user about possible problems or misconfigurations.
	/// Diagnostic listeners can be be added and removed with calls
	/// to this interface.
	/// To install the most basic listener call:<br/>
	/// <code>Db4oFactory.Configure().Diagnostic().AddListener(new DiagnosticToConsole());</code>
	/// </remarks>
	/// <seealso cref="IConfiguration.Diagnostic">IConfiguration.Diagnostic</seealso>
	/// <seealso cref="IDiagnosticListener">IDiagnosticListener</seealso>
	public interface IDiagnosticConfiguration
	{
		/// <summary>adds a DiagnosticListener to listen to Diagnostic messages.</summary>
		/// <remarks>adds a DiagnosticListener to listen to Diagnostic messages.</remarks>
		void AddListener(IDiagnosticListener listener);

		/// <summary>removes all DiagnosticListeners.</summary>
		/// <remarks>removes all DiagnosticListeners.</remarks>
		void RemoveAllListeners();
	}
}

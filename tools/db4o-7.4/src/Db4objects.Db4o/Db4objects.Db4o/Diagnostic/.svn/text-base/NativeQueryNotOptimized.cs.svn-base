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
using Db4objects.Db4o.Diagnostic;
using Db4objects.Db4o.Query;

namespace Db4objects.Db4o.Diagnostic
{
	/// <summary>Diagnostic, if Native Query can not be run optimized.</summary>
	/// <remarks>Diagnostic, if Native Query can not be run optimized.</remarks>
	public class NativeQueryNotOptimized : DiagnosticBase
	{
		private readonly Predicate _predicate;

		private readonly Exception _details;

		public NativeQueryNotOptimized(Predicate predicate, Exception details)
		{
			_predicate = predicate;
			_details = details;
		}

		public override object Reason()
		{
			if (_details == null)
			{
				return _predicate;
			}
			return _predicate != null ? _predicate.ToString() : string.Empty + "\n" + _details
				.Message;
		}

		public override string Problem()
		{
			return "Native Query Predicate could not be run optimized";
		}

		public override string Solution()
		{
			return "This Native Query was run by instantiating all objects of the candidate class. "
				 + "Consider simplifying the expression in the Native Query method. If you feel that "
				 + "the Native Query processor should understand your code better, you are invited to "
				 + "post yout query code to db4o forums at http://developer.db4o.com/forums";
		}
	}
}

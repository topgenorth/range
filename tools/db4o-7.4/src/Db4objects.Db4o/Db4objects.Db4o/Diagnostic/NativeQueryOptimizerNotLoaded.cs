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

namespace Db4objects.Db4o.Diagnostic
{
	public class NativeQueryOptimizerNotLoaded : DiagnosticBase
	{
		private int _reason;

		private readonly Exception _details;

		public const int NqNotPresent = 1;

		public const int NqConstructionFailed = 2;

		public NativeQueryOptimizerNotLoaded(int reason, Exception details)
		{
			_reason = reason;
			_details = details;
		}

		public override string Problem()
		{
			return "Native Query Optimizer could not be loaded";
		}

		public override object Reason()
		{
			switch (_reason)
			{
				case NqNotPresent:
				{
					return AppendDetails("Native query not present.");
				}

				case NqConstructionFailed:
				{
					return AppendDetails("Native query couldn't be instantiated.");
				}

				default:
				{
					return AppendDetails("Reason not specified.");
					break;
				}
			}
		}

		public override string Solution()
		{
			return "If you to have the native queries optimized, please check that the native query jar is present in the class-path.";
		}

		private object AppendDetails(string reason)
		{
			if (_details == null)
			{
				return reason;
			}
			return reason + "\n" + _details.ToString();
		}
	}
}

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
	/// <summary>Diagnostic to recommend Defragment when needed.</summary>
	/// <remarks>Diagnostic to recommend Defragment when needed.</remarks>
	public class DefragmentRecommendation : DiagnosticBase
	{
		private readonly DefragmentRecommendation.DefragmentRecommendationReason _reason;

		public DefragmentRecommendation(DefragmentRecommendation.DefragmentRecommendationReason
			 reason)
		{
			_reason = reason;
		}

		public class DefragmentRecommendationReason
		{
			internal readonly string _message;

			public DefragmentRecommendationReason(string reason)
			{
				_message = reason;
			}

			public static readonly DefragmentRecommendation.DefragmentRecommendationReason DeleteEmbeded
				 = new DefragmentRecommendation.DefragmentRecommendationReason("Delete Embedded not supported on old file format."
				);
		}

		public override string Problem()
		{
			return "Database file format is old or database is highly fragmented.";
		}

		public override object Reason()
		{
			return _reason._message;
		}

		public override string Solution()
		{
			return "Defragment the database.";
		}
	}
}

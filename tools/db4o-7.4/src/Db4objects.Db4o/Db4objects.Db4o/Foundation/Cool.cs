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
using Db4objects.Db4o.Foundation;
using Sharpen.Lang;

namespace Db4objects.Db4o.Foundation
{
	/// <summary>A collection of cool static methods that should be part of the runtime environment but are not.
	/// 	</summary>
	/// <remarks>A collection of cool static methods that should be part of the runtime environment but are not.
	/// 	</remarks>
	/// <exclude></exclude>
	public class Cool
	{
		public static void SleepIgnoringInterruption(long millis)
		{
			try
			{
				Thread.Sleep(millis);
			}
			catch (Exception)
			{
			}
		}

		/// <summary>
		/// Keeps executing a block of code until it either returns false or millisecondsTimeout
		/// elapses.
		/// </summary>
		/// <remarks>
		/// Keeps executing a block of code until it either returns false or millisecondsTimeout
		/// elapses.
		/// </remarks>
		/// <param name="millisecondsTimeout"></param>
		/// <param name="block"></param>
		public static void LoopWithTimeout(long millisecondsTimeout, IConditionalBlock block
			)
		{
			StopWatch watch = new AutoStopWatch();
			do
			{
				if (!block.Run())
				{
					break;
				}
			}
			while (watch.Peek() < millisecondsTimeout);
		}
	}
}

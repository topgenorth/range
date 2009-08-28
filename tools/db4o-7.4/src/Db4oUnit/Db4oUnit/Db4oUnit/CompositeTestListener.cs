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
using Db4oUnit;

namespace Db4oUnit
{
	public class CompositeTestListener : ITestListener
	{
		private readonly ITestListener _listener1;

		private readonly ITestListener _listener2;

		public CompositeTestListener(ITestListener listener1, ITestListener listener2)
		{
			_listener1 = listener1;
			_listener2 = listener2;
		}

		public virtual void RunFinished()
		{
			_listener1.RunFinished();
			_listener2.RunFinished();
		}

		public virtual void RunStarted()
		{
			_listener1.RunStarted();
			_listener2.RunStarted();
		}

		public virtual void TestFailed(ITest test, Exception failure)
		{
			_listener1.TestFailed(test, failure);
			_listener2.TestFailed(test, failure);
		}

		public virtual void TestStarted(ITest test)
		{
			_listener1.TestStarted(test);
			_listener2.TestStarted(test);
		}
	}
}

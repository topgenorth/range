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
using Db4oUnit.Extensions;
using Db4objects.Db4o.Tests.Common.TA;

namespace Db4objects.Db4o.Tests.Common
{
	public class AllTests : Db4oTestSuite
	{
		protected override Type[] TestCases()
		{
			return new Type[] { typeof(Db4objects.Db4o.Tests.Common.Acid.AllTests), typeof(Db4objects.Db4o.Tests.Common.Activation.AllTests
				), typeof(Db4objects.Db4o.Tests.Common.Assorted.AllTests), typeof(Db4objects.Db4o.Tests.Common.Btree.AllTests
				), typeof(Db4objects.Db4o.Tests.Common.Classindex.AllTests), typeof(Db4objects.Db4o.Tests.Common.Config.AllTests
				), typeof(Db4objects.Db4o.Tests.Common.Constraints.AllTests), typeof(Db4objects.Db4o.Tests.Common.CS.AllTests
				), typeof(Db4objects.Db4o.Tests.Common.Defragment.AllTests), typeof(Db4objects.Db4o.Tests.Common.Events.AllTests
				), typeof(Db4objects.Db4o.Tests.Common.Exceptions.AllTests), typeof(Db4objects.Db4o.Tests.Common.Ext.AllTests
				), typeof(Db4objects.Db4o.Tests.Common.Fatalerror.AllTests), typeof(Db4objects.Db4o.Tests.Common.Fieldindex.AllTests
				), typeof(Db4objects.Db4o.Tests.Common.Foundation.AllTests), typeof(Db4objects.Db4o.Tests.Common.Freespace.AllTests
				), typeof(Db4objects.Db4o.Tests.Common.Handlers.AllTests), typeof(Db4objects.Db4o.Tests.Common.Header.AllTests
				), typeof(Db4objects.Db4o.Tests.Common.Interfaces.AllTests), typeof(Db4objects.Db4o.Tests.Common.Internal.AllTests
				), typeof(Db4objects.Db4o.Tests.Common.IO.AllTests), typeof(Db4objects.Db4o.Tests.Common.Refactor.AllTests
				), typeof(Db4objects.Db4o.Tests.Common.References.AllTests), typeof(Db4objects.Db4o.Tests.Common.Reflect.AllTests
				), typeof(Db4objects.Db4o.Tests.Common.Regression.AllTests), typeof(Db4objects.Db4o.Tests.Common.Querying.AllTests
				), typeof(Db4objects.Db4o.Tests.Common.Set.AllTests), typeof(Db4objects.Db4o.Tests.Common.Soda.AllTests
				), typeof(Db4objects.Db4o.Tests.Common.Stored.AllTests), typeof(AllCommonTATests
				), typeof(Db4objects.Db4o.Tests.Common.TP.AllTests), typeof(Db4objects.Db4o.Tests.Common.Types.AllTests
				), typeof(Db4objects.Db4o.Tests.Common.Uuid.AllTests), typeof(Db4objects.Db4o.Tests.Util.Test.AllTests
				) };
		}
	}
}

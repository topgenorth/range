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
using Db4objects.Db4o.Tests.Common.Events;
using Db4objects.Db4o.Tests.Common.Migration;
using Db4oUnit.Extensions;

namespace Db4objects.Db4o.Tests
{
	public class AllTests : Db4oTestSuite
	{
		public static int Main(string[] args)
		{
#if CF
			return new AllTests().RunSolo();
//            return new AllTests().RunClientServer();
#else
//			return new Common.Assorted.IndexCreateDropTestCase().RunSolo();
//			return new Common.Migration.AllTests().RunSolo();
//			return new Common.Refle ct.Custom.AllTests().RunSolo();
//			return new AllTests().RunSolo();
//			return new AllTests().RunClientServer();
//			return new AllTestsConcurrency().RunConcurrencyAll();
		    return new AllTests().RunAll();
#endif
		}
		
		protected override Type[] TestCases()
		{
			return new Type[]
				{	
#if CF_3_5
					typeof(Compact.AllTests),
#endif
                    typeof(Db4objects.Db4o.Tests.Common.Migration.AllTests),
                    typeof(Db4objects.Db4o.Tests.Common.TA.AllTests),
                    typeof(Db4objects.Db4o.Tests.Common.AllTests),
                    typeof(Db4objects.Db4o.Tests.Jre5.Collections.Typehandler.AllTests),
                    typeof(Db4objects.Db4o.Tests.Jre5.Annotation.AllTests),
                    typeof(Db4objects.Db4o.Tests.CLI1.AllTests),
                    typeof(Db4objects.Db4o.Tests.CLI2.AllTests),
                    typeof(Db4objects.Db4o.Tests.SharpenLang.AllTests),
                    typeof(AllTestsConcurrency),
				};
		}
	}
}

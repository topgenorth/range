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
using System.Globalization;
using System.Threading;

class Foo
{
	public static void Bar()
	{
		throw new ApplicationException();
	}
}

public class PreserveDebugInfoSubject : ITestCase
{
	public void Test()
	{
        CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
        try
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            try
            {
                Foo.Bar();
            }
            catch (Exception x)
            {
                string message = x.ToString();
                Assert.IsTrue(message.Contains("PreserveDebugInfoSubject.cs:line 10"), message);
            }
        }
        finally
        {
            Thread.CurrentThread.CurrentUICulture = currentUICulture;
        }
	}
}
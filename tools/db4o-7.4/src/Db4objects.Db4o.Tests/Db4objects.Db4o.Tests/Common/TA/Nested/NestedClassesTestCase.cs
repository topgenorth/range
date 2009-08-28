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
using Db4oUnit.Extensions;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.TA;
using Db4objects.Db4o.Tests.Common.TA.Nested;
using Sharpen;

namespace Db4objects.Db4o.Tests.Common.TA.Nested
{
	/// <summary>
	/// TODO: This test case will fail when run against JDK1.3/JDK1.4 (though it will run green against
	/// JDK1.2 and JDK1.5+) because the synthetic "this$0" field is final.
	/// </summary>
	/// <remarks>
	/// TODO: This test case will fail when run against JDK1.3/JDK1.4 (though it will run green against
	/// JDK1.2 and JDK1.5+) because the synthetic "this$0" field is final.
	/// See http://developer.db4o.com/Resources/view.aspx/Reference/Implementation_Strategies/Type_Handling/Final_Fields/Final_Fields_Specifics
	/// </remarks>
	public class NestedClassesTestCase : AbstractDb4oTestCase, IOptOutTA
	{
		public static void Main(string[] args)
		{
			new NestedClassesTestCase().RunSolo();
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			OuterClass outerObject = new OuterClass();
			outerObject._foo = 42;
			IActivatable objOne = (IActivatable)outerObject.CreateInnerObject();
			Store(objOne);
			IActivatable objTwo = (IActivatable)outerObject.CreateInnerObject();
			Store(objTwo);
		}

		/// <exception cref="Exception"></exception>
		protected override void Configure(IConfiguration config)
		{
			config.Add(new TransparentActivationSupport());
		}

		/// <exception cref="Exception"></exception>
		public virtual void Test()
		{
			string property = Runtime.GetProperty("java.version");
			if (property != null && property.StartsWith("1.3"))
			{
				Sharpen.Runtime.Err.WriteLine("IGNORED: " + GetType() + " will fail when run against JDK1.3/JDK1.4"
					);
				return;
			}
			IObjectSet query = Db().Query(typeof(OuterClass.InnerClass));
			while (query.HasNext())
			{
				OuterClass.InnerClass innerObject = (OuterClass.InnerClass)query.Next();
				Assert.IsNull(innerObject.GetOuterObjectWithoutActivation());
				Assert.AreEqual(42, innerObject.GetOuterObject().Foo());
			}
		}
	}
}

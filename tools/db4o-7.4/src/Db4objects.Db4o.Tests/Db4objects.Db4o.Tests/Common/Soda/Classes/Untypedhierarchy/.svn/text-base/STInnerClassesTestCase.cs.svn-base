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
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Soda.Classes.Untypedhierarchy;
using Db4objects.Db4o.Tests.Common.Soda.Util;

namespace Db4objects.Db4o.Tests.Common.Soda.Classes.Untypedhierarchy
{
	/// <summary>
	/// epaul:
	/// Shows a bug.
	/// </summary>
	/// <remarks>
	/// epaul:
	/// Shows a bug.
	/// carlrosenberger:
	/// Fixed!
	/// The error was due to the the behaviour of STCompare.java.
	/// It compared the syntetic fields in inner classes also.
	/// I changed the behaviour to neglect all fields that
	/// contain a "$".
	/// </remarks>
	/// <author>&lt;a href="mailto:Paul-Ebermann@gmx.de"&gt;Paul Ebermann</a></author>
	/// <version>0.1</version>
	public class STInnerClassesTestCase : SodaBaseTestCase
	{
		public class Parent
		{
			public object child;

			public Parent(STInnerClassesTestCase _enclosing, object o)
			{
				this._enclosing = _enclosing;
				// Generierter package-Name
				this.child = o;
			}

			public override string ToString()
			{
				return "Parent[" + this.child + "]";
			}

			public Parent(STInnerClassesTestCase _enclosing)
			{
				this._enclosing = _enclosing;
			}

			private readonly STInnerClassesTestCase _enclosing;
		}

		public class FirstClass
		{
			public object childFirst;

			public FirstClass(STInnerClassesTestCase _enclosing, object o)
			{
				this._enclosing = _enclosing;
				this.childFirst = o;
			}

			public override string ToString()
			{
				return "First[" + this.childFirst + "]";
			}

			public FirstClass(STInnerClassesTestCase _enclosing)
			{
				this._enclosing = _enclosing;
			}

			private readonly STInnerClassesTestCase _enclosing;
		}

		public STInnerClassesTestCase()
		{
		}

		public override object[] CreateData()
		{
			return new object[] { new STInnerClassesTestCase.Parent(this, new STInnerClassesTestCase.FirstClass
				(this, "Example")), new STInnerClassesTestCase.Parent(this, new STInnerClassesTestCase.FirstClass
				(this, "no Example")) };
		}

		/// <summary>Only</summary>
		public virtual void TestNothing()
		{
			IQuery q = NewQuery();
			q.Descend("child");
			SodaTestUtil.Expect(q, _array);
		}
		//SodaTest.log(q);
		// STSomeClasses
	}
}

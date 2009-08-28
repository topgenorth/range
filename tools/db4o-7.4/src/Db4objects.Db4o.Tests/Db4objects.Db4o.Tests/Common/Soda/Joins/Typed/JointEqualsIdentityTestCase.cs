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
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Soda.Joins.Typed;

namespace Db4objects.Db4o.Tests.Common.Soda.Joins.Typed
{
	public class JointEqualsIdentityTestCase : AbstractDb4oTestCase
	{
		public class TestSubject
		{
			public string _name;

			public JointEqualsIdentityTestCase.TestSubject _child;

			public TestSubject(string name, JointEqualsIdentityTestCase.TestSubject child)
			{
				_name = name;
				_child = child;
			}
		}

		/// <exception cref="Exception"></exception>
		protected override void Store()
		{
			JointEqualsIdentityTestCase.TestSubject subjectA = new JointEqualsIdentityTestCase.TestSubject
				("A", null);
			JointEqualsIdentityTestCase.TestSubject subjectB = new JointEqualsIdentityTestCase.TestSubject
				("B", subjectA);
			JointEqualsIdentityTestCase.TestSubject subjectC = new JointEqualsIdentityTestCase.TestSubject
				("C", subjectA);
			Store(subjectA);
			Store(subjectB);
			Store(subjectC);
		}

		public virtual void TestJointEqualsIdentity()
		{
			JointEqualsIdentityTestCase.TestSubject child = RetrieveChild();
			IQuery query = NewQuery(typeof(JointEqualsIdentityTestCase.TestSubject));
			IConstraint constraint = query.Descend("_name").Constrain("B").Equal();
			constraint.And(query.Descend("_child").Constrain(child).Identity());
			Assert.AreEqual(1, query.Execute().Size());
		}

		private JointEqualsIdentityTestCase.TestSubject RetrieveChild()
		{
			IQuery query = NewQuery(typeof(JointEqualsIdentityTestCase.TestSubject));
			query.Descend("_child").Constrain(null);
			return (JointEqualsIdentityTestCase.TestSubject)query.Execute().Next();
		}
	}
}

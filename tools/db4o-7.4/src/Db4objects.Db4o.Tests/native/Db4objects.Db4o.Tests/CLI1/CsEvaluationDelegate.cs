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
using Db4objects.Db4o.Query;
using Db4oUnit;
using Db4oUnit.Extensions;

namespace Db4objects.Db4o.Tests.CLI1
{
    public class CsEvaluationDelegate : AbstractDb4oTestCase
    {
        internal CsEvaluationDelegate child;
        internal String name;

        override protected void Store()
        {
            name = "one";
            Store(this);
            CsEvaluationDelegate se1 = new CsEvaluationDelegate();
            se1.child = new CsEvaluationDelegate();
            se1.child.name = "three";
            se1.name = "two";
            Store(se1);
        }

        public void TestStaticMethodDelegate()
        {
            RunEvaluationDelegateTest(new EvaluationDelegate(Evaluate));
        }

        public void TestInstanceMethodDelegate()
        {
            RunEvaluationDelegateTest(new EvaluationDelegate(new NameCondition("three").Evaluate));
        }

        void RunEvaluationDelegateTest(EvaluationDelegate evaluation)
        {
            IQuery q1 = NewQuery();
            IQuery cq1 = q1;
            q1.Constrain(GetType());
            cq1 = cq1.Descend("child");
            cq1.Constrain(evaluation);
            IObjectSet os = q1.Execute();
            Assert.AreEqual(1, os.Size());
            CsEvaluationDelegate se = (CsEvaluationDelegate)os.Next();
            Assert.AreEqual("two", se.name);
        }

        public static void Evaluate(ICandidate candidate)
        {
            CsEvaluationDelegate obj = ((CsEvaluationDelegate)candidate.GetObject());
            candidate.Include(obj.name.Equals("three"));
        }

        class NameCondition
        {
            string _name;

            public NameCondition(string name)
            {
                _name = name;
            }

            public void Evaluate(ICandidate candidate)
            {
                CsEvaluationDelegate obj = ((CsEvaluationDelegate)candidate.GetObject());
                candidate.Include(obj.name.Equals(_name));
            }
        }
    }
}

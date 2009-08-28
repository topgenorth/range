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
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4objects.Db4o;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Concurrency;

namespace Db4objects.Db4o.Tests.Common.Concurrency
{
	public class ParameterizedEvaluationTestCase : Db4oClientServerTestCase
	{
		public static void Main(string[] args)
		{
			new ParameterizedEvaluationTestCase().RunConcurrency();
		}

		public string str;

		protected override void Store()
		{
			Store("one");
			Store("fun");
			Store("ton");
			Store("sun");
		}

		private void Store(string str)
		{
			ParameterizedEvaluationTestCase pe = new ParameterizedEvaluationTestCase();
			pe.str = str;
			Store(pe);
		}

		public virtual void Conc(IExtObjectContainer oc)
		{
			Assert.AreEqual(2, QueryContains(oc, "un").Size());
		}

		private IObjectSet QueryContains(IExtObjectContainer oc, string str)
		{
			IQuery q = oc.Query();
			q.Constrain(typeof(ParameterizedEvaluationTestCase));
			q.Constrain(new ParameterizedEvaluationTestCase.MyEvaluation(str));
			return q.Execute();
		}

		[System.Serializable]
		public class MyEvaluation : IEvaluation
		{
			public string str;

			public MyEvaluation(string str)
			{
				this.str = str;
			}

			public virtual void Evaluate(ICandidate candidate)
			{
				ParameterizedEvaluationTestCase pe = (ParameterizedEvaluationTestCase)candidate.GetObject
					();
				bool inc = pe.str.IndexOf(str) != -1;
				candidate.Include(inc);
			}
		}
	}
}

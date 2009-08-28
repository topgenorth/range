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
using System.Collections;
using Db4objects.Db4o.Query;
using Db4oUnit;
using Db4oUnit.Extensions;

namespace Db4objects.Db4o.Tests.CLI1
{
	public enum CsEnumState
	{
		None,
		Open,
		Running,
		Closed
	}

	/// <summary>
	/// enums
	/// </summary>
	public class CsEnum : AbstractDb4oTestCase
	{
		CsEnumState _state;

		public CsEnum()
		{
		}

		public CsEnum(CsEnumState state)
		{
			_state = state;
		}

		public CsEnumState State
		{
			get
			{
				return _state;
			}

			set
			{
				_state = value;
			}
		}

		override protected void Store()
		{
			Store(new CsEnum(CsEnumState.Open));
			Store(new CsEnum(CsEnumState.Closed));
			Store(new CsEnum(CsEnumState.Running));
		}

		public void TestValueConstrain()
		{
			IQuery q = NewQuery(typeof(CsEnum));
			IObjectSet os = q.Execute();
			Assert.AreEqual(3, os.Count);

			TstQueryByEnum(CsEnumState.Open);
			TstQueryByEnum(CsEnumState.Closed);
		}

		public void TestOrConstrain()
		{
			IQuery q = NewQuery(typeof(CsEnum));
			q.Descend("_state").Constrain(CsEnumState.Open).Or(
				q.Descend("_state").Constrain(CsEnumState.Running));
			
			EnsureObjectSet(q.Execute(), CsEnumState.Open, CsEnumState.Running);
		}

		public void TestQBE()
		{
			TstQBE(3, CsEnumState.None); // None is the zero/uninitialized value
			TstQBE(1, CsEnumState.Closed);
			TstQBE(1, CsEnumState.Open);
			TstQBE(1, CsEnumState.Running);
		}

		private void TstQBE(int expectedCount, CsEnumState value)
		{
			IObjectSet os = Db().Get(new CsEnum(value));
			Assert.AreEqual(expectedCount, os.Size());
		}

		private void EnsureObjectSet(IObjectSet os, params CsEnumState[] expected)
		{
			Assert.AreEqual(expected.Length, os.Size());
			ArrayList l = new ArrayList();
			while (os.HasNext())
			{
				l.Add(((CsEnum)os.Next()).State);
			}
			
			foreach (CsEnumState e in expected)
			{	
				Assert.IsTrue(l.Contains(e));
				l.Remove(e);
			}
		}

		void TstQueryByEnum(CsEnumState template)
		{
			IQuery q = NewQuery(typeof(CsEnum));
			q.Descend("_state").Constrain(template);

			IObjectSet os = q.Execute();
			Assert.AreEqual(1, os.Size());
			Assert.AreEqual(template, ((CsEnum)os.Next()).State);
		}
	}
}

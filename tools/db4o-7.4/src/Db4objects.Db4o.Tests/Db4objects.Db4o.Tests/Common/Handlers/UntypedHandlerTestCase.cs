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
using Db4objects.Db4o.Tests.Common.Handlers;

namespace Db4objects.Db4o.Tests.Common.Handlers
{
	public class UntypedHandlerTestCase : TypeHandlerTestCaseBase
	{
		public static void Main(string[] args)
		{
			new UntypedHandlerTestCase().RunSolo();
		}

		public class Item
		{
			public object _member;

			public Item(object member)
			{
				_member = member;
			}

			public override bool Equals(object obj)
			{
				if (obj == this)
				{
					return true;
				}
				if (!(obj is UntypedHandlerTestCase.Item))
				{
					return false;
				}
				UntypedHandlerTestCase.Item other = (UntypedHandlerTestCase.Item)obj;
				if (this._member.GetType().IsArray)
				{
					return ArraysEquals((object[])this._member, (object[])other._member);
				}
				return this._member.Equals(other._member);
			}

			private bool ArraysEquals(object[] arr1, object[] arr2)
			{
				if (arr1.Length != arr2.Length)
				{
					return false;
				}
				for (int i = 0; i < arr1.Length; i++)
				{
					if (!arr1[i].Equals(arr2[i]))
					{
						return false;
					}
				}
				return true;
			}

			public override int GetHashCode()
			{
				int hash = 7;
				hash = 31 * hash + (null == _member ? 0 : _member.GetHashCode());
				return hash;
			}

			public override string ToString()
			{
				return "[" + _member + "]";
			}
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestStoreIntItem()
		{
			DoTestStoreObject(new UntypedHandlerTestCase.Item(3355));
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestStoreStringItem()
		{
			DoTestStoreObject(new UntypedHandlerTestCase.Item("one"));
		}

		/// <exception cref="Exception"></exception>
		public virtual void TestStoreArrayItem()
		{
			DoTestStoreObject(new UntypedHandlerTestCase.Item(new string[] { "one", "two", "three"
				 }));
		}
	}
}

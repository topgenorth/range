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
using Db4objects.Db4o.Reflect;

namespace Db4objects.Db4o.Tests.Common.Reflect
{
	public class ReflectArrayTestCase : AbstractDb4oTestCase
	{
		public virtual void TestNewInstance()
		{
			string[][] a23 = NewStringMatrix(2, 3);
			Assert.AreEqual(2, a23.Length);
			for (int i = 0; i < a23.Length; ++i)
			{
				Assert.AreEqual(3, a23[i].Length);
			}
		}

		private string[][] NewStringMatrix(int x, int y)
		{
			return (string[][])NewInstance(typeof(string), new int[] { x, y });
		}

		public virtual void TestIsNDimensional()
		{
			IReflectClass arrayOfArrayOfString = ReflectClass(typeof(string[][]));
			Assert.IsTrue(arrayOfArrayOfString.IsArray());
			IReflectClass arrayOfString = ReflectClass(typeof(string[]));
			Assert.AreSame(arrayOfString, arrayOfArrayOfString.GetComponentType());
			Assert.IsTrue(ArrayReflector().IsNDimensional(arrayOfArrayOfString));
			Assert.IsFalse(ArrayReflector().IsNDimensional(arrayOfString));
		}

		public virtual void TestDimensions()
		{
			string[][] array = NewStringMatrix(3, 4);
			ArrayAssert.AreEqual(new int[] { 3, 4 }, ArrayReflector().Dimensions(array));
		}

		private object NewInstance(Type elementType, int[] dimensions)
		{
			return ArrayReflector().NewInstance(ReflectClass(elementType), dimensions);
		}

		private IReflectArray ArrayReflector()
		{
			return Reflector().Array();
		}
	}
}

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
using Db4objects.Db4o.TA;
using Db4oUnit;

unsafe public class PointerContainer
{
	public int* foo;
}

public delegate void FooHandler();

public class DelegateContainer
{
	public FooHandler foo;

	public EventHandler<EventArgs> bar;

	public EventHandler baz;
}

public class IDoHaveSerializableFields : DelegateContainer
{
	public int fooBar;
}

public class BaseClassWithSerializableField
{
	public int foo;
}

public class DerivedClass : BaseClassWithSerializableField
{
	public EventHandler<EventArgs> bar;
}

public class TAUnsafeInstrumentationSubject : ITestCase
{
	public void TestDelegateIsNotInstrumented()
	{
		Assert.IsFalse(typeof(IActivatable).IsAssignableFrom(typeof(FooHandler)));
	}

	public void TestClassWithoutPersistentFieldsAreNotInstrumented()
	{
		Assert.IsFalse(typeof(IActivatable).IsAssignableFrom(typeof(DelegateContainer)));
		Assert.IsFalse(typeof(IActivatable).IsAssignableFrom(typeof(PointerContainer)));
	}

	public void TestOneSerializableFieldTriggersInstrumentation()
	{
		Assert.IsTrue(typeof(IActivatable).IsAssignableFrom(typeof(IDoHaveSerializableFields)));
		Assert.IsTrue(typeof(IActivatable).IsAssignableFrom(typeof(DerivedClass)));
	}
}


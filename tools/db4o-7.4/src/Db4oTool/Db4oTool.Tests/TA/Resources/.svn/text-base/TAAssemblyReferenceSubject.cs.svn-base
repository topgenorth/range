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
using System.Runtime.CompilerServices;
using Db4objects.Db4o.Activation;
using Db4objects.Db4o.TA;
using Db4oTool.Tests.TA; // MockActivator
using Db4oUnit;

public class Task : ProjectItem
{
	public bool Finished;

    public Task(string name)
        : base(name)
    {
    }

	override public bool Equals(object o)
    {
        Task other = o as Task;
        if (other == null) return false;

        // foreign field access
        return _name == other._name;
    }

    override public int GetHashCode()
    {
        return _name.GetHashCode();
    }
}

// This class is filtered out from the instrumented set
// by a custom rule.
//
// This shouldn't however affect its ability
// to activate instrumented classes.
class FilteredOutByName
{
	public static bool CheckTask(Task task)
	{
		return task.Finished;
	}
}

class TAAssemblyReferenceSubject : ITestCase
{
	public void TestFilteredOutClassesStillActivateForeignFields()
	{
		Assert.IsFalse(IsActivatable(typeof(FilteredOutByName)));

		Task t = new Task("test");
		MockActivator activator = ActivatorFor(t);

		FilteredOutByName.CheckTask(t);

		Assert.AreEqual(1, activator.ReadCount);
	}

    public void TestIsActivatable()
    {
        Assert.IsTrue(IsActivatable(typeof(Task)));
    }

    public void TestPropertyGetter()
    {
        Task p = new Task("test");
        MockActivator activator = ActivatorFor(p);

        Assert.AreEqual(0, activator.ReadCount);
        Assert.AreEqual("test", p.Name);
        Assert.AreEqual(1, activator.ReadCount);
    }

    public void TestForeignFieldAccess()
    {
        Task p1 = new Task("test");
        Task p2 = new Task("test");

        MockActivator a1 = ActivatorFor(p1);
        MockActivator a2 = ActivatorFor(p2);

        Assert.IsTrue(p1.Equals(p2));

        Assert.AreEqual(1, a1.ReadCount, "a1");
        Assert.AreEqual(1, a2.ReadCount, "a2");
    }

    private MockActivator ActivatorFor(Task p)
    {
        MockActivator activator = new MockActivator();
        ((IActivatable)p).Bind(activator);
        return activator;
    }

    private static bool IsActivatable(Type type)
    {
        return typeof(IActivatable).IsAssignableFrom(type);
    }
}
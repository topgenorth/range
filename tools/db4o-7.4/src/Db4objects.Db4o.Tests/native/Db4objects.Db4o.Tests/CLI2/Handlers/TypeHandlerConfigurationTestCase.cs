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
using System.Collections;
using System.Collections.Generic;
using Db4objects.Db4o.Reflect;
using Db4oUnit;
using Db4oUnit.Extensions;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Tests.Common.Handlers;
using Db4objects.Db4o.Typehandlers;

using Sharpen.Util;

namespace Db4objects.Db4o.Tests.CLI2.Handlers
{

    public class TypeHandlerConfigurationTestCase : AbstractDb4oTestCase
    {
        public class Holder
        {
            public object _storedObject;

            public Holder(object storedObject)
            {
                _storedObject = storedObject;
            }
        }

        protected override void Store()
        {
            AddMetadata(new ArrayList());
            AddMetadata(new List<object>());
        }

        private void AddMetadata(object storedObject)
        {
            Store(new Holder(storedObject));
        }

        public virtual void Test()
        {

            // disabled in 7.4

            // AssertSingleNullTypeHandlerAspect(typeof(ArrayList));
            //AssertSingleTypeHandlerAspect(typeof (ArrayList), typeof (ListTypeHandler));
            //AssertSingleTypeHandlerAspect(typeof(List<object>), typeof(ListTypeHandler));
            
        }

        private void AssertSingleNullTypeHandlerAspect(Type storedClass)
        {
            AssertSingleTypeHandlerAspect(storedClass, typeof(IgnoreFieldsTypeHandler));
        }

        private void AssertSingleTypeHandlerAspect(Type storedClass, Type typeHandlerClass
            )
        {
            IntByRef aspectCount = new IntByRef(0);
            ClassMetadata classMetadata = ClassMetadata(storedClass);
            classMetadata.ForEachDeclaredAspect(new _IProcedure4_51(aspectCount, typeHandlerClass
                ));
        }

        private sealed class _IProcedure4_51 : IProcedure4
        {
            public _IProcedure4_51(IntByRef aspectCount, Type typeHandlerClass)
            {
                this.aspectCount = aspectCount;
                this.typeHandlerClass = typeHandlerClass;
            }

            public void Apply(object arg)
            {
                aspectCount.value++;
                Assert.IsSmaller(2, aspectCount.value);
                ClassAspect aspect = (ClassAspect)arg;
                Assert.IsInstanceOf(typeof(TypeHandlerAspect), aspect);
                TypeHandlerAspect typeHandlerAspect = (TypeHandlerAspect)aspect;
                Assert.IsInstanceOf(typeHandlerClass, typeHandlerAspect._typeHandler);
            }

            private readonly IntByRef aspectCount;

            private readonly Type typeHandlerClass;
        }

        private ClassMetadata ClassMetadata(Type clazz)
        {
            IReflectClass claxx = Container().Reflector().ForClass(clazz);
            return Container().ClassMetadataForReflectClass(claxx);
        }
    }
}

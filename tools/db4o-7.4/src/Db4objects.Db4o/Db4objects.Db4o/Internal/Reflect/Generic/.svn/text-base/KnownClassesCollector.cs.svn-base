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
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Reflect;
using Db4objects.Db4o.Reflect.Generic;

namespace Db4objects.Db4o.Internal.Reflect.Generic
{
	public class KnownClassesCollector
	{
		private readonly ObjectContainerBase _container;

		private readonly KnownClassesRepository _repository;

		public KnownClassesCollector(ObjectContainerBase container, KnownClassesRepository
			 repository)
		{
			_container = container;
			_repository = repository;
		}

		public virtual IReflectClass[] Collect()
		{
			Collection4 classes = new Collection4();
			CollectKnownClasses(classes);
			return (IReflectClass[])classes.ToArray(new IReflectClass[classes.Size()]);
		}

		private void CollectKnownClasses(Collection4 classes)
		{
			IListener collectingListener = NewCollectingClassListener(classes);
			_repository.AddListener(collectingListener);
			try
			{
				CollectKnownClasses(classes, Iterators.Copy(_repository.Classes()));
			}
			finally
			{
				_repository.RemoveListener(collectingListener);
			}
		}

		private IListener NewCollectingClassListener(Collection4 classes)
		{
			return new _IListener_37(this, classes);
		}

		private sealed class _IListener_37 : IListener
		{
			public _IListener_37(KnownClassesCollector _enclosing, Collection4 classes)
			{
				this._enclosing = _enclosing;
				this.classes = classes;
			}

			public void OnEvent(object addedClass)
			{
				this._enclosing.CollectKnownClass(classes, (IReflectClass)addedClass);
			}

			private readonly KnownClassesCollector _enclosing;

			private readonly Collection4 classes;
		}

		private void CollectKnownClasses(Collection4 collector, IEnumerator knownClasses)
		{
			while (knownClasses.MoveNext())
			{
				IReflectClass clazz = (IReflectClass)knownClasses.Current;
				CollectKnownClass(collector, clazz);
			}
		}

		private void CollectKnownClass(Collection4 classes, IReflectClass clazz)
		{
			if (IsInternalClass(clazz))
			{
				return;
			}
			if (IsSecondClass(clazz))
			{
				return;
			}
			if (clazz.IsArray())
			{
				return;
			}
			classes.Add(clazz);
		}

		private bool IsInternalClass(IReflectClass clazz)
		{
			return _container._handlers.IclassInternal.IsAssignableFrom(clazz);
		}

		private bool IsSecondClass(IReflectClass clazz)
		{
			ClassMetadata clazzMeta = _container.ClassMetadataForReflectClass(clazz);
			return clazzMeta != null && clazzMeta.IsSecondClass();
		}
	}
}

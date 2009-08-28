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
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Activation;

namespace Db4objects.Db4o.Internal.Activation
{
	/// <exclude></exclude>
	public class ActivationContext4
	{
		private readonly Transaction _transaction;

		private readonly object _targetObject;

		private readonly IActivationDepth _depth;

		public ActivationContext4(Transaction transaction, object obj, IActivationDepth depth
			)
		{
			_transaction = transaction;
			_targetObject = obj;
			_depth = depth;
		}

		public virtual void CascadeActivationToTarget(ClassMetadata classMetadata, bool doDescend
			)
		{
			IActivationDepth depth = doDescend ? _depth.Descend(classMetadata) : _depth;
			CascadeActivation(classMetadata, TargetObject(), depth);
		}

		public virtual void CascadeActivationToChild(object obj)
		{
			if (obj == null)
			{
				return;
			}
			ClassMetadata classMetadata = Container().ClassMetadataForObject(obj);
			if (classMetadata == null || classMetadata.IsPrimitive())
			{
				return;
			}
			IActivationDepth depth = _depth.Descend(classMetadata);
			CascadeActivation(classMetadata, obj, depth);
		}

		private void CascadeActivation(ClassMetadata classMetadata, object obj, IActivationDepth
			 depth)
		{
			if (!depth.RequiresActivation())
			{
				return;
			}
			if (depth.Mode().IsDeactivate())
			{
				Container().StillToDeactivate(_transaction, obj, depth, false);
			}
			else
			{
				// FIXME: [TA] do we need to check for isValueType here?
				if (classMetadata.IsValueType())
				{
					classMetadata.ActivateFields(_transaction, obj, depth);
				}
				else
				{
					Container().StillToActivate(_transaction, obj, depth);
				}
			}
		}

		public virtual ObjectContainerBase Container()
		{
			return _transaction.Container();
		}

		public virtual object TargetObject()
		{
			return _targetObject;
		}
	}
}

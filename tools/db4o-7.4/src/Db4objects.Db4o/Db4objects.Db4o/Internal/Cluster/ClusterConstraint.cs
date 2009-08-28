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

namespace Db4objects.Db4o.Internal.Cluster
{
	/// <exclude></exclude>
	public class ClusterConstraint : IConstraint
	{
		internal readonly Db4objects.Db4o.Cluster.Cluster _cluster;

		internal readonly IConstraint[] _constraints;

		public ClusterConstraint(Db4objects.Db4o.Cluster.Cluster cluster, IConstraint[] constraints
			)
		{
			_cluster = cluster;
			_constraints = constraints;
		}

		private Db4objects.Db4o.Internal.Cluster.ClusterConstraint Compatible(IConstraint
			 with)
		{
			if (!(with is Db4objects.Db4o.Internal.Cluster.ClusterConstraint))
			{
				throw new ArgumentException();
			}
			Db4objects.Db4o.Internal.Cluster.ClusterConstraint other = (Db4objects.Db4o.Internal.Cluster.ClusterConstraint
				)with;
			if (other._constraints.Length != _constraints.Length)
			{
				throw new ArgumentException();
			}
			return other;
		}

		public virtual IConstraint And(IConstraint with)
		{
			return Join(with, true);
		}

		public virtual IConstraint Or(IConstraint with)
		{
			return Join(with, false);
		}

		private IConstraint Join(IConstraint with, bool isAnd)
		{
			lock (_cluster)
			{
				Db4objects.Db4o.Internal.Cluster.ClusterConstraint other = Compatible(with);
				IConstraint[] newConstraints = new IConstraint[_constraints.Length];
				for (int i = 0; i < _constraints.Length; i++)
				{
					newConstraints[i] = isAnd ? _constraints[i].And(other._constraints[i]) : _constraints
						[i].Or(other._constraints[i]);
				}
				return new Db4objects.Db4o.Internal.Cluster.ClusterConstraint(_cluster, newConstraints
					);
			}
		}

		public virtual IConstraint Equal()
		{
			lock (_cluster)
			{
				for (int i = 0; i < _constraints.Length; i++)
				{
					_constraints[i].Equal();
				}
				return this;
			}
		}

		public virtual IConstraint Greater()
		{
			lock (_cluster)
			{
				for (int i = 0; i < _constraints.Length; i++)
				{
					_constraints[i].Greater();
				}
				return this;
			}
		}

		public virtual IConstraint Smaller()
		{
			lock (_cluster)
			{
				for (int i = 0; i < _constraints.Length; i++)
				{
					_constraints[i].Smaller();
				}
				return this;
			}
		}

		public virtual IConstraint Identity()
		{
			lock (_cluster)
			{
				for (int i = 0; i < _constraints.Length; i++)
				{
					_constraints[i].Identity();
				}
				return this;
			}
		}

		public virtual IConstraint ByExample()
		{
			lock (_cluster)
			{
				for (int i = 0; i < _constraints.Length; i++)
				{
					_constraints[i].ByExample();
				}
				return this;
			}
		}

		public virtual IConstraint Like()
		{
			lock (_cluster)
			{
				for (int i = 0; i < _constraints.Length; i++)
				{
					_constraints[i].Like();
				}
				return this;
			}
		}

		public virtual IConstraint StartsWith(bool caseSensitive)
		{
			lock (_cluster)
			{
				for (int i = 0; i < _constraints.Length; i++)
				{
					_constraints[i].StartsWith(caseSensitive);
				}
				return this;
			}
		}

		public virtual IConstraint EndsWith(bool caseSensitive)
		{
			lock (_cluster)
			{
				for (int i = 0; i < _constraints.Length; i++)
				{
					_constraints[i].EndsWith(caseSensitive);
				}
				return this;
			}
		}

		public virtual IConstraint Contains()
		{
			lock (_cluster)
			{
				for (int i = 0; i < _constraints.Length; i++)
				{
					_constraints[i].Contains();
				}
				return this;
			}
		}

		public virtual IConstraint Not()
		{
			lock (_cluster)
			{
				for (int i = 0; i < _constraints.Length; i++)
				{
					_constraints[i].Not();
				}
				return this;
			}
		}

		public virtual object GetObject()
		{
			throw new NotSupportedException();
		}
	}
}

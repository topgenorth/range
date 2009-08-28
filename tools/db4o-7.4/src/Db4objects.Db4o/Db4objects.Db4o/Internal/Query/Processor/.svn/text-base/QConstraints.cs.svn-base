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
using Db4objects.Db4o.Internal.Query.Processor;
using Db4objects.Db4o.Query;

namespace Db4objects.Db4o.Internal.Query.Processor
{
	/// <summary>Array of constraints for queries.</summary>
	/// <remarks>
	/// Array of constraints for queries.
	/// Necessary to be returned to Query#constraints()
	/// </remarks>
	/// <exclude></exclude>
	public class QConstraints : QCon, IConstraints
	{
		private IConstraint[] i_constraints;

		internal QConstraints(Transaction a_trans, IConstraint[] constraints) : base(a_trans
			)
		{
			i_constraints = constraints;
		}

		internal override IConstraint Join(IConstraint a_with, bool a_and)
		{
			lock (StreamLock())
			{
				if (!(a_with is QCon))
				{
					return null;
				}
				// resolving multiple constraints happens in QCon for
				// a_with, so we simply turn things around
				return ((QCon)a_with).Join1(this, a_and);
			}
		}

		public virtual IConstraint[] ToArray()
		{
			lock (StreamLock())
			{
				return i_constraints;
			}
		}

		public override IConstraint Contains()
		{
			lock (StreamLock())
			{
				for (int i = 0; i < i_constraints.Length; i++)
				{
					i_constraints[i].Contains();
				}
				return this;
			}
		}

		public override IConstraint Equal()
		{
			lock (StreamLock())
			{
				for (int i = 0; i < i_constraints.Length; i++)
				{
					i_constraints[i].Equal();
				}
				return this;
			}
		}

		public override IConstraint Greater()
		{
			lock (StreamLock())
			{
				for (int i = 0; i < i_constraints.Length; i++)
				{
					i_constraints[i].Greater();
				}
				return this;
			}
		}

		public override IConstraint Identity()
		{
			lock (StreamLock())
			{
				for (int i = 0; i < i_constraints.Length; i++)
				{
					i_constraints[i].Identity();
				}
				return this;
			}
		}

		public override IConstraint Not()
		{
			lock (StreamLock())
			{
				for (int i = 0; i < i_constraints.Length; i++)
				{
					i_constraints[i].Not();
				}
				return this;
			}
		}

		public override IConstraint Like()
		{
			lock (StreamLock())
			{
				for (int i = 0; i < i_constraints.Length; i++)
				{
					i_constraints[i].Like();
				}
				return this;
			}
		}

		public override IConstraint StartsWith(bool caseSensitive)
		{
			lock (StreamLock())
			{
				for (int i = 0; i < i_constraints.Length; i++)
				{
					i_constraints[i].StartsWith(caseSensitive);
				}
				return this;
			}
		}

		public override IConstraint EndsWith(bool caseSensitive)
		{
			lock (StreamLock())
			{
				for (int i = 0; i < i_constraints.Length; i++)
				{
					i_constraints[i].EndsWith(caseSensitive);
				}
				return this;
			}
		}

		public override IConstraint Smaller()
		{
			lock (StreamLock())
			{
				for (int i = 0; i < i_constraints.Length; i++)
				{
					i_constraints[i].Smaller();
				}
				return this;
			}
		}

		public override object GetObject()
		{
			lock (StreamLock())
			{
				object[] objects = new object[i_constraints.Length];
				for (int i = 0; i < i_constraints.Length; i++)
				{
					objects[i] = i_constraints[i].GetObject();
				}
				return objects;
			}
		}
	}
}

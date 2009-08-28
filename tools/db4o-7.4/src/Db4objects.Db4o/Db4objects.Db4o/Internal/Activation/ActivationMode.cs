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
using Db4objects.Db4o.Internal.Activation;

namespace Db4objects.Db4o.Internal.Activation
{
	public sealed class ActivationMode
	{
		public static readonly ActivationMode Activate = new ActivationMode();

		public static readonly ActivationMode Deactivate = new ActivationMode();

		public static readonly ActivationMode Peek = new ActivationMode();

		public static readonly ActivationMode Prefetch = new ActivationMode();

		public static readonly ActivationMode Refresh = new ActivationMode();

		private ActivationMode()
		{
		}

		public override string ToString()
		{
			if (IsActivate())
			{
				return "ACTIVATE";
			}
			if (IsDeactivate())
			{
				return "DEACTIVATE";
			}
			if (IsPrefetch())
			{
				return "PREFETCH";
			}
			if (IsRefresh())
			{
				return "REFRESH";
			}
			return "PEEK";
		}

		public bool IsDeactivate()
		{
			return this == Deactivate;
		}

		public bool IsActivate()
		{
			return this == Activate;
		}

		public bool IsPeek()
		{
			return this == Peek;
		}

		public bool IsPrefetch()
		{
			return this == Prefetch;
		}

		public bool IsRefresh()
		{
			return this == Refresh;
		}
	}
}

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
using Db4objects.Db4o.Diagnostic;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.TA;

namespace Db4objects.Db4o.TA
{
	public class NotTransparentActivationEnabled : DiagnosticBase
	{
		private ClassMetadata _class;

		public NotTransparentActivationEnabled(ClassMetadata clazz)
		{
			_class = clazz;
		}

		public override string Problem()
		{
			return "An object of class " + _class + " was stored. Instances of this class very likely are not subject to transparent activation.";
		}

		public override object Reason()
		{
			return _class;
		}

		public override string Solution()
		{
			return "Use a TA aware class with equivalent functionality or ensure that this class provides a sensible implementation of the "
				 + typeof(IActivatable).FullName + " interface and the implicit TA hooks, either manually or by applying instrumentation.";
		}
	}
}

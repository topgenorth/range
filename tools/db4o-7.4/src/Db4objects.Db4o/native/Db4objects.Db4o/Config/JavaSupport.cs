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
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.CS;
using Db4objects.Db4o.Internal.CS.Messages;
using Db4objects.Db4o.Query;
using Sharpen.Lang;

namespace Db4objects.Db4o.Config
{
	public class JavaSupport : IConfigurationItem
	{
		public void Prepare(IConfiguration config)
		{
			config.AddAlias(new WildcardAlias("com.db4o.ext.*", "Db4objects.Db4o.Ext.*, Db4objects.Db4o"));
			config.AddAlias(new TypeAlias("com.db4o.foundation.ChainedRuntimeException", FullTypeNameFor(typeof(Exception))));

			config.AddAlias(new TypeAlias("com.db4o.StaticField", FullTypeNameFor(typeof(StaticField))));
			config.AddAlias(new TypeAlias("com.db4o.StaticClass", FullTypeNameFor(typeof(StaticClass))));

			config.AddAlias(new TypeAlias("com.db4o.query.Evaluation", FullTypeNameFor(typeof(IEvaluation))));
			config.AddAlias(new TypeAlias("com.db4o.query.Candidate", FullTypeNameFor(typeof(ICandidate))));

			config.AddAlias(new WildcardAlias("com.db4o.internal.query.processor.*", "Db4objects.Db4o.Internal.Query.Processor.*, Db4objects.Db4o"));
			//config.AddAlias(new WildcardAlias("com.db4o.query.*", "Db4objects.Db4o.Query.*, Db4objects.Db4o"));

			config.AddAlias(new TypeAlias("com.db4o.foundation.Collection4", FullTypeNameFor(typeof(Collection4))));
			config.AddAlias(new TypeAlias("com.db4o.foundation.List4", FullTypeNameFor(typeof(List4))));
			config.AddAlias(new TypeAlias("com.db4o.User", FullTypeNameFor(typeof(User))));

			config.AddAlias(new TypeAlias("com.db4o.internal.cs.ClassInfo", FullTypeNameFor(typeof(ClassInfo))));
			config.AddAlias(new TypeAlias("com.db4o.internal.cs.FieldInfo", FullTypeNameFor(typeof(FieldInfo))));

			config.AddAlias(new TypeAlias("com.db4o.internal.cs.messages.MUserMessage$UserMessagePayload", FullTypeNameFor(typeof(MUserMessage.UserMessagePayload))));
			config.AddAlias(new WildcardAlias("com.db4o.internal.cs.messages.*", "Db4objects.Db4o.Internal.CS.Messages.*, Db4objects.Db4o"));

			config.AddAlias(new TypeAlias("java.lang.Throwable", FullTypeNameFor(typeof(Exception))));
			config.AddAlias(new TypeAlias("java.lang.RuntimeException", FullTypeNameFor(typeof(Exception))));
			config.AddAlias(new TypeAlias("java.lang.Exception", FullTypeNameFor(typeof(Exception))));
		}

		private static string FullTypeNameFor(Type type)
		{
			return TypeReference.FromType(type).GetUnversionedName();
		}

		public void Apply(IInternalObjectContainer container)
		{
		}
	}
}

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
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;

namespace Db4objects.Db4o.Internal
{
	public class Config4Field : Config4Abstract, IObjectField, IDeepClone
	{
		private readonly Config4Class _configClass;

		private static readonly KeySpec QueryEvaluationKey = new KeySpec(true);

		private static readonly KeySpec IndexedKey = new KeySpec(TernaryBool.Unspecified);

		protected Config4Field(Config4Class a_class, KeySpecHashtable4 config) : base(config
			)
		{
			_configClass = a_class;
		}

		internal Config4Field(Config4Class a_class, string a_name)
		{
			_configClass = a_class;
			SetName(a_name);
		}

		private Config4Class ClassConfig()
		{
			return _configClass;
		}

		internal override string ClassName()
		{
			return ClassConfig().GetName();
		}

		public virtual object DeepClone(object param)
		{
			return new Db4objects.Db4o.Internal.Config4Field((Config4Class)param, _config);
		}

		public virtual void QueryEvaluation(bool flag)
		{
			_config.Put(QueryEvaluationKey, flag);
		}

		public virtual void Rename(string newName)
		{
			ClassConfig().Config().Rename(new Db4objects.Db4o.Rename(ClassName(), GetName(), 
				newName));
			SetName(newName);
		}

		public virtual void Indexed(bool flag)
		{
			PutThreeValued(IndexedKey, flag);
		}

		public virtual void InitOnUp(Transaction systemTrans, FieldMetadata yapField)
		{
			ObjectContainerBase anyStream = systemTrans.Container();
			if (!anyStream.MaintainsIndices())
			{
				return;
			}
			if (!yapField.SupportsIndex())
			{
				Indexed(false);
			}
			TernaryBool indexedFlag = _config.GetAsTernaryBool(IndexedKey);
			if (indexedFlag.DefiniteNo())
			{
				yapField.DropIndex(systemTrans);
				return;
			}
			if (UseExistingIndex(systemTrans, yapField))
			{
				return;
			}
			if (!indexedFlag.DefiniteYes())
			{
				return;
			}
			yapField.CreateIndex();
		}

		private bool UseExistingIndex(Transaction systemTrans, FieldMetadata yapField)
		{
			return yapField.GetIndex(systemTrans) != null;
		}

		internal virtual bool QueryEvaluation()
		{
			return _config.GetAsBoolean(QueryEvaluationKey);
		}
	}
}

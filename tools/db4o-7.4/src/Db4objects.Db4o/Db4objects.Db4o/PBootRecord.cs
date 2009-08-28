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
using Db4objects.Db4o;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Internal;

namespace Db4objects.Db4o
{
	/// <summary>Old database boot record class.</summary>
	/// <remarks>
	/// Old database boot record class.
	/// This class was responsible for storing the last timestamp id,
	/// for holding a reference to the Db4oDatabase object of the
	/// ObjectContainer and for holding on to the UUID index.
	/// This class is no longer needed with the change to the new
	/// fileheader. It still has to stay here to be able to read
	/// old databases.
	/// </remarks>
	/// <exclude></exclude>
	/// <persistent></persistent>
	public class PBootRecord : P1Object, IInternal4
	{
		public Db4oDatabase i_db;

		public long i_versionGenerator;

		public MetaIndex i_uuidMetaIndex;

		public virtual MetaIndex GetUUIDMetaIndex()
		{
			return i_uuidMetaIndex;
		}

		public virtual void Write(LocalObjectContainer file)
		{
			SystemData systemData = file.SystemData();
			i_versionGenerator = systemData.LastTimeStampID();
			i_db = systemData.Identity();
			file.ShowInternalClasses(true);
			try
			{
				Store(2);
			}
			finally
			{
				file.ShowInternalClasses(false);
			}
		}
	}
}

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
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.CS;
using Db4objects.Db4o.Internal.CS.Messages;
using Db4objects.Db4o.Reflect.Generic;

namespace Db4objects.Db4o.Internal.CS.Messages
{
	public class MClassMeta : MsgObject, IServerSideMessage
	{
		public virtual bool ProcessAtServer()
		{
			ObjectContainerBase stream = Stream();
			Unmarshall();
			try
			{
				lock (StreamLock())
				{
					ClassInfo classMeta = (ClassInfo)ReadObjectFromPayLoad();
					GenericClass genericClass = stream.GetClassMetaHelper().ClassMetaToGenericClass(Stream
						().Reflector(), classMeta);
					if (genericClass != null)
					{
						Transaction trans = stream.SystemTransaction();
						ClassMetadata yapClass = stream.ProduceClassMetadata(genericClass);
						if (yapClass != null)
						{
							stream.CheckStillToSet();
							yapClass.SetStateDirty();
							yapClass.Write(trans);
							trans.Commit();
							StatefulBuffer returnBytes = stream.ReadWriterByID(trans, yapClass.GetID());
							Write(Msg.ObjectToClient.GetWriter(returnBytes));
							return true;
						}
					}
				}
			}
			catch (Exception e)
			{
			}
			Write(Msg.Failed);
			return true;
		}
	}
}

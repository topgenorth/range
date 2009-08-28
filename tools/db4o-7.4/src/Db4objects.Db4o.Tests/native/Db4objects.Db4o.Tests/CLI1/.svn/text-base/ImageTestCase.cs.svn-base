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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using Db4objects.Db4o.Config;

using Db4oUnit;
using Db4oUnit.Extensions;

namespace Db4objects.Db4o.Tests.CLI1
{
	public class ImageTestCase : AbstractDb4oTestCase
	{
#if !CF
		public class ImageTranslator : IObjectConstructor
		{
			public object OnInstantiate(IObjectContainer container, object obj)
			{
				byte[] data = (byte[])obj;
				using (MemoryStream stream = new MemoryStream(data))
				{
					return Image.FromStream(stream);
				}
			}

			public object OnStore(IObjectContainer container, object obj)
			{
				Image img = (Image)obj;
				using (MemoryStream stream = new MemoryStream())
				{
					img.Save(stream, ImageFormat.Bmp);
					return stream.ToArray();
				}
			}

			public void OnActivate(IObjectContainer container, object applicationObject, object storedObject)
			{
			}

			public Type StoredClass()
			{
				return typeof(byte[]);
			}
		}

		public const int width = 128;
		public const int height = 64;

		protected override void Configure(IConfiguration cfg)
		{
			cfg.ObjectClass(typeof(Bitmap)).Translate(new ImageTranslator());
		}

		protected override void Store()
		{
			Bitmap b = new Bitmap(width, height);
			Db().Store(b);
		}

		public void _TestImage()
		{
			Bitmap b = (Bitmap) RetrieveOnlyInstance(typeof (Bitmap));
			Assert.IsNotNull(b);
			Assert.AreEqual(width, b.Width);
			Assert.AreEqual(height, b.Height);
		}
#endif
	}
}
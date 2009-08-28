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
using System.Windows.Forms;

namespace Db4objects.Db4o.Tutorial
{
	/// <summary>
	/// Description of UIStyle.
	/// </summary>
	public class UIStyle
	{
		public static readonly Color Db4oGrey = Color.FromArgb(0xFF, 0x66, 0x61, 0x77);
		
		public static readonly Color Db4oGreen = Color.FromArgb(0xFF, 0xAD, 0xD6, 0x5C);
    
		public static readonly Color BackColor = Color.FromArgb(0xFF, 0x83, 0x83, 0x83);
		
		public static readonly Color TextColor = Color.White;
		
		public static void Apply(Control control)
		{
			control.BackColor = UIStyle.BackColor;
			control.ForeColor = UIStyle.TextColor;
		}
		
		public static void ApplyConsoleStyle(Control control)
		{
			control.BackColor = UIStyle.BackColor;
			control.ForeColor = UIStyle.Db4oGreen;
		}
		
		public static void ApplyButtonStyle(Control control)
		{
			control.ForeColor = UIStyle.Db4oGrey;
			control.BackColor = UIStyle.Db4oGreen;
		}
		
		private UIStyle()
		{
		}
	}
}

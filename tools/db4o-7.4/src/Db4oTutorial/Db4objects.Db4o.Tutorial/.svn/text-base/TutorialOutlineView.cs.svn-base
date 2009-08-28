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
using System.Collections;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI;

namespace Db4objects.Db4o.Tutorial
{
	/// <summary>
	/// Description of TutorialOutlineView.
	/// </summary>
	public class TutorialOutlineView : DockContent
	{
		MainForm _main;
		TreeView _tree;
		
		public TutorialOutlineView(MainForm main)
		{
			_main = main;
			
			this.Text = "Tutorial Outline";
			this.DockableAreas = (DockAreas.Float |
			                      DockAreas.DockLeft |
			                      DockAreas.DockRight);
			
			this.ClientSize = new System.Drawing.Size(295, 347);
			this.DockPadding.Bottom = 2;
			this.DockPadding.Top = 2;
			this.ShowHint = DockState.DockLeft;
			this.CloseButton = false;
			
			TutorialOutlineViewControl control = new TutorialOutlineViewControl();			
			control.Dock = DockStyle.Fill;			
			_tree = control.TreeView;	
			_tree.AfterSelect += new TreeViewEventHandler(_tree_AfterSelect);
			this.Controls.Add(control);
		}
		
		private void _tree_AfterSelect(object sender, TreeViewEventArgs args)
		{
			_main.NavigateTutorial((string)args.Node.Tag);
		}
		
		override protected void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			
			LoadTutorialOutline();
		}
		
		delegate void LoadFirstTopicFunction();
		
		void LoadTutorialOutline()
		{			
			TreeNode current = new TreeNode();
			TreeNode root = current;
			TreeNode currentParent = null;			
			Stack nodes = new Stack();			

			string path = _main.GetTutorialFilePath("outline.html");
			if (!File.Exists(path))
			{
				return;
			}
			
			XmlTextReader reader = new XmlTextReader(path);
			while (reader.Read())
			{
				string name = reader.Name;				
				switch (name)
				{
					case "li":
						{	
							reader.ReadStartElement("li");							
							
							string href = reader.GetAttribute("href");
							
							reader.ReadStartElement("a");
							string description = reader.ReadString();
							
							current = new TreeNode(description);
							current.Tag = href;
							
							currentParent.Nodes.Add(current);
							
							reader.ReadEndElement();
							reader.ReadEndElement();
							
							break;
						}
						
					case "ul":
						{	
							if (reader.IsStartElement())
							{
								nodes.Push(currentParent);
								currentParent = current;
							}
							else
							{
								currentParent = (TreeNode)nodes.Pop();
							}
							break;
						}
				}
			}
			
			foreach (TreeNode node in root.Nodes)
			{
				_tree.Nodes.Add(node);
			}
			_tree.ExpandAll();
			
			BeginInvoke(new LoadFirstTopicFunction(LoadFirstTopic));
		}
		
		void LoadFirstTopic()
		{
			_tree.SelectedNode = _tree.Nodes[0];
		}
		
		string LoadFile(string fname)
		{
			using (TextReader reader = File.OpenText(fname))
			{
				return reader.ReadToEnd();
			}
		}
	}
}

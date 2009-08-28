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
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WeifenLuo.WinFormsUI;
using AxSHDocVw;

namespace Db4objects.Db4o.Tutorial
{
	/// <summary>
	/// Description of WebBrowserView.
	/// </summary>
	public class WebBrowserView : DockContent, IOleClientSite, IDocHostUIHandler
	{
		AxWebBrowser _webBrowser;
				
		object _external;
		
		public WebBrowserView()
		{			
			this.CloseButton = false;
			this.DockableAreas = DockAreas.Document;
			this.Text = "Web Browser";
			
			WebBrowserViewControl control = new WebBrowserViewControl();
			control.Dock = DockStyle.Fill;
			_webBrowser = control.WebBrowser;
			_webBrowser.TitleChange += new AxSHDocVw.DWebBrowserEvents2_TitleChangeEventHandler(_webBrowser_TitleChange);
			
			this.Controls.Add(control);
		}
		
		/// <summary>
		/// object that will be exposed inside the WebBrowser through
		/// the window.external property.
		/// </summary>
		public object External
		{
			get
			{
				return _external;
			}
			
			set
			{
				_external = value;
			}
		}
		
		public void Navigate(string url)
		{			
			object noParam1 = null;
			object noParam2 = null;
			object noParam3 = null;
			object noParam4 = null;
			_webBrowser.Navigate(url,
			                     ref noParam1,
			                     ref noParam2,
			                     ref noParam3,
			                     ref noParam4);
		}
		
		delegate void LoadStartPageFunction();
		
		override protected void OnLoad(EventArgs args)
		{
			(_webBrowser.GetOcx() as IOleObject).SetClientSite(this);			
			base.OnLoad(args);
		}
		
		void _webBrowser_TitleChange(object sender, AxSHDocVw.DWebBrowserEvents2_TitleChangeEvent e)
		{	
			this.Text = e.text;
		}
		
		#region IOleClientSite implementation
		void IOleClientSite.SaveObject()
		{			
		}
		
		void IOleClientSite.GetMoniker(uint dwAssign, uint dwWhichMoniker, ref object ppmk)
		{			
		}
		
		void IOleClientSite.GetContainer(ref object ppContainer)
		{
			ppContainer = this;			
		}
		
		void IOleClientSite.ShowObject()
		{			
		}
		
		void IOleClientSite.OnShowWindow(bool fShow)
		{			
		}
		
		void IOleClientSite.RequestNewObjectLayout()
		{			
		}
		#endregion
		
		#region IDocHostUIHandler implementation
		uint IDocHostUIHandler.ShowContextMenu(uint dwID, ref tagPOINT ppt,
		                     object pcmdtReserved,
		                     object pdispReserved)
		{
			return 0;
		}
		
		void IDocHostUIHandler.GetHostInfo(ref DOCHOSTUIINFO pInfo)
		{
			pInfo.dwFlags |= (uint)(DOCHOSTUIFLAG.DOCHOSTUIFLAG_NO3DBORDER |
			                  DOCHOSTUIFLAG.DOCHOSTUIFLAG_DISABLE_SCRIPT_INACTIVE);
			
		}
		
		void IDocHostUIHandler.ShowUI(uint dwID, ref object pActiveObject, ref object pCommandTarget, ref object pFrame, ref object pDoc)
		{			
		}
		
		void IDocHostUIHandler.HideUI()
		{			
		}
		
		void IDocHostUIHandler.UpdateUI()
		{			
		}
		
		void IDocHostUIHandler.EnableModeless(int fEnable)
		{			
		}
		
		void IDocHostUIHandler.OnDocWindowActivate(int fActivate)
		{			
		}
		
		void IDocHostUIHandler.OnFrameWindowActivate(int fActivate)
		{			
		}
		
		void IDocHostUIHandler.ResizeBorder(ref tagRECT prcBorder, int pUIWindow, int fRameWindow)
		{			
		}
				
		uint IDocHostUIHandler.TranslateAccelerator(ref tagMSG lpMsg, ref Guid pguidCmdGroup, uint nCmdID)
		{			
			// let CTRL+A, CTRL+C to go through
			const int VK_CONTROL = 0x11;
			if (GetAsyncKeyState(VK_CONTROL) < 0)
			{
				switch (lpMsg.wParam &= 0xFF)
				{
					case 'A':
					case 'C':
						return 1;
				}
			}
			return 0;
		}
		
		void IDocHostUIHandler.GetOptionKeyPath(ref string pchKey, uint dw)
		{			
		}
		
		object IDocHostUIHandler.GetDropTarget(ref object pDropTarget)
		{		
			return pDropTarget;
		}		
		
		void IDocHostUIHandler.GetExternal(out object ppDispatch)
		{			
			ppDispatch = _external;
		}		
		
		uint IDocHostUIHandler.TranslateUrl(uint dwTranslate,
		                  string pchURLIn,
		                  ref string ppchURLOut)
		{			
			return 0;
		}
		
		IDataObject IDocHostUIHandler.FilterDataObject(IDataObject pDO)
		{			
			return pDO;
		}
		#endregion
		
		[DllImport("User32.dll")]
		static extern short GetAsyncKeyState(int key);
	}
}

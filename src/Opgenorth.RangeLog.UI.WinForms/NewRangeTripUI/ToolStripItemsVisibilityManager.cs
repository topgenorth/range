using System.Collections.Generic;
using System.Windows.Forms;

using Opgenorth.RangeLog.Core;
using Opgenorth.RangeLog.Core.UI;


namespace Opgenorth.RangeLog.WinForms.NewRangeTripUI
{
    public class ToolStripItemsVisibilityManager : IShowUIElements
    {
        private readonly ToolStrip _toolStrip;


        private readonly IVisitor<ToolStrip> _showToolStripItemsForAddNew;


        private readonly IVisitor<ToolStrip> _hideToolStripItemsForAddNew;


        public ToolStripItemsVisibilityManager(ToolStrip toolStrip, IEnumerable<string> controlNames)
        {
            _showToolStripItemsForAddNew = new ToolStripItemVisibilityVisitor(controlNames, true);
            _hideToolStripItemsForAddNew = new ToolStripItemVisibilityVisitor(controlNames, false);
            _toolStrip = toolStrip;
        }

        public void Show()
        {
            _showToolStripItemsForAddNew.Visit(_toolStrip);
        }

        public void Hide()
        {
            _hideToolStripItemsForAddNew.Visit(_toolStrip);
        }
    }
}
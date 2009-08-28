using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Opgenorth.RangeLog.Core;


namespace Opgenorth.RangeLog.WinForms
{
    public class ToolStripItemVisibilityVisitor : IVisitor<ToolStrip>
    {
        private readonly IEnumerable<string> _buttonsToDisplay;


        private readonly bool _showItems;


        public ToolStripItemVisibilityVisitor(IEnumerable<string> buttonsToDisplay, bool showItems)
        {
            _buttonsToDisplay = buttonsToDisplay;
            _showItems = showItems;
        }

        public void Visit(ToolStrip toolStrip)
        {
            foreach (ToolStripItem item  in toolStrip.Items)
            {
                item.Visible = _buttonsToDisplay.Contains(item.Name) && _showItems;
            }
        }
    }
}
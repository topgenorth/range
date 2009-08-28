using System.Windows.Forms;

using Microsoft.Practices.ServiceLocation;

using Opgenorth.RangeLog.Core;
using Opgenorth.RangeLog.Core.Data.db4o;


namespace Opgenorth.RangeLog.WinForms
{
    public class ShowDatabaseInStatusBarVisitor : IVisitor<StatusStrip>
    {
        private readonly string _statusLabelName;


        public ShowDatabaseInStatusBarVisitor(string statusLabelName)
        {
            _statusLabelName = statusLabelName;
        }

        public void Visit(StatusStrip statusStrip)
        {
            var statusLabel = (ToolStripStatusLabel) statusStrip.Items[_statusLabelName];
            if (statusLabel == null)
            {
                return;
            }
            var config = ServiceLocator.Current.GetInstance<IDb4oConfiguration>();
            statusLabel.Text = config.GetFilename();
        }
    }
}
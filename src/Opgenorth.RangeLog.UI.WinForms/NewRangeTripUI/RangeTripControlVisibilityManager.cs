using System.Windows.Forms;

using Opgenorth.RangeLog.Core.UI;
using Opgenorth.RangeLog.WinForms.Controls;


namespace Opgenorth.RangeLog.WinForms.NewRangeTripUI
{
    public class RangeTripControlVisibilityManager : IShowUIElements
    {
        public static readonly string ControlName = "NewRangeTripView";
        private readonly Panel _container;
        private Control _singleRangeTripView;
        private readonly IShowUIElements _toolStripItemsForControl;

        public RangeTripControlVisibilityManager(Panel container, IShowUIElements items)
        {
            _container = container;
            _toolStripItemsForControl = items;
        }

        public Control TheControl
        {
            get { return _singleRangeTripView; }
        }

        public void Show()
        {
            _singleRangeTripView = new ctlSingleRangeTripView
                                       {
                                           Name = ControlName, Dock = DockStyle.Fill
                                       };
            _container.Controls.Add(_singleRangeTripView);
            _toolStripItemsForControl.Show();
        }

        public void Hide()
        {
            _container.Controls.RemoveByKey(ControlName);
            _toolStripItemsForControl.Hide();
        }
    }
}
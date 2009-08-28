using System;
using System.Windows.Forms;
using Opgenorth.RangeLog.Core.UI;

namespace Opgenorth.RangeLog.WinForms.Controls
{
    public partial class ctlSingleRangeTripView : UserControl, IRangeTripView
    {
        public ctlSingleRangeTripView()
        {
            InitializeComponent();
        }

        public DateTime DateOfTrip
        {
            get { return dateTimePicker1.Value; }
        }
        public int RoundsFired
        {
            get { return Convert.ToInt32(maskedTextBox1.Text); }
        }
        public string Firearm
        {
            get { return txtFirearm.Text.Trim(); }
        }
        public string Comments
        {
            get { return txtComments.Text.Trim(); }
        }
    }
}

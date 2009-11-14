using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

using AutoMapper;

using Microsoft.Practices.ServiceLocation;

using Opgenorth.RangeLog.Core;
using Opgenorth.RangeLog.Core.Data.db4o;
using Opgenorth.RangeLog.Core.Domain;
using Opgenorth.RangeLog.Core.Domain.Impl;
using Opgenorth.RangeLog.Core.UI;
using Opgenorth.RangeLog.WinForms.NewRangeTripUI;


namespace Opgenorth.RangeLog.WinForms
{
    public partial class RangeTripsView : Form
    {
        private readonly IVisitor<StatusStrip> _showDatabaseLocation;
        private readonly IShowUIElements _newRangeTripUI;
        private readonly IRangeTripUIElements _gridUI;
        private readonly string[] _toolStripItemsForAddNewUI = new[]
                                                                   {
                                                                       "_backFromNewToolStripButton", "_saveToolStripButton"
                                                                   };
        private readonly string[] _toolStripItemsForGrid = new[]
                                                               {
                                                                   "_newToolStripButton", 
                                                                   "_deleteToolStripButton",
                                                                   "_editToolStripButton"
                                                               };
        private BindingList<ITrip> _trips;

        public RangeTripsView()
        {
            InitializeComponent();
            _gridUI = new RangeTripGridVisibilityManager(_panel1, new ToolStripItemsVisibilityManager(_toolStrip1, _toolStripItemsForGrid));
            _newRangeTripUI = new RangeTripControlVisibilityManager(_panel1, new ToolStripItemsVisibilityManager(_toolStrip1, _toolStripItemsForAddNewUI));
            _showDatabaseLocation = new ShowDatabaseInStatusBarVisitor(_databaseLocationStatusLabel.Name);
        }

        private void _backFromNewToolStripButton_Click(object sender, EventArgs e)
        {
            _newRangeTripUI.Hide();
            _gridUI.Show(_trips);
        }

        private void _deleteRow_Click(object sender, EventArgs e)
        {
            var repo = ServiceLocator.Current.GetInstance<IDb4oTripRepository>();
            foreach (var trip in _gridUI.SelectedTrips)
            {
                repo.Remove(trip);
                _trips.Remove(trip);
            }
        }

        private void _newToolStripButton_Click(object sender, EventArgs e)
        {
            _gridUI.Hide();
            _newRangeTripUI.Show();
        }

        private void _saveToolStripButton_Click(object sender, EventArgs e)
        {
            var control = (IRangeTripView) _panel1.Controls.Find("NewRangeTripView", true)[0];
            var newTrip = Mapper.Map<IRangeTripView, SimpleRangeTrip>(control);

            SaveNewTripToDatabase(newTrip);

            _newRangeTripUI.Hide();
            _gridUI.Show(_trips);
        }

        private void SaveNewTripToDatabase(ITrip newTrip)
        {
            var repo = ServiceLocator.Current.GetInstance<IDb4oTripRepository>();
            repo.Add(newTrip);
            _trips.Add(newTrip);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _showDatabaseLocation.Visit(_statusStrip1);
            LoadTrips();
            _gridUI.Show(_trips);
        }

        private void LoadTrips()
        {
            var repo = ServiceLocator.Current.GetInstance<IDb4oTripRepository>();
            _trips = new BindingList<ITrip>(repo.FetchAll().ToList())
                         {
                             AllowNew = true,
                             AllowRemove = true,
                             AllowEdit = false
                         };
        }
    }
}
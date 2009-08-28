using System.Collections.Generic;
using System.Windows.Forms;

using Opgenorth.RangeLog.Core.Domain;
using Opgenorth.RangeLog.Core.UI;
using Opgenorth.RangeLog.WinForms.Controls;


namespace Opgenorth.RangeLog.WinForms.NewRangeTripUI
{
    public interface IRangeTripUIElements : IShowUIElements
    {
        void Show(IEnumerable<ITrip> trips);
        IEnumerable<ITrip> SelectedTrips { get; }
    }

    public class RangeTripGridVisibilityManager : IRangeTripUIElements
    {
        public static readonly string ControlName = "RangeTripGrid";
        private readonly Panel _container;
        private DataGridView _grid;
        private readonly IShowUIElements _toolStripItemsForGrid;

        public RangeTripGridVisibilityManager(Panel container, IShowUIElements toolStripItemsForGrid)
        {
            _container = container;
            _toolStripItemsForGrid = toolStripItemsForGrid;
        }

        public IEnumerable<ITrip> SelectedTrips
        {
            get
            {
                var trips = new List<ITrip>(_grid.SelectedRows.Count + 1);

                if (_grid.SelectedRows.Count == 0)
                {
                    if (_grid.CurrentRow != null)
                    {
                        trips.Add((ITrip)_grid.CurrentRow.DataBoundItem);
                    }
                }
                else
                {
                    foreach (DataGridViewRow selectedRow in _grid.SelectedRows)
                    {
                        trips.Add((ITrip) selectedRow.DataBoundItem);
                    }
                }
                return trips;
            }
        }

        public void Show(IEnumerable<ITrip> trips)
        {
            _container.Controls.Clear();
            _grid = null;

            Show();
            _grid.DataSource = trips;
        }

        public void Show()
        {
            var builder = new RangeTripGridBuilder();
            _grid = builder.Build();
            _container.Controls.Add(_grid);
            _grid.Dock = DockStyle.Fill;
            _grid.Name = ControlName;

            _toolStripItemsForGrid.Show();
        }

        public void Hide()
        {
            _container.Controls.Clear();
            _toolStripItemsForGrid.Hide();
        }
    }
}
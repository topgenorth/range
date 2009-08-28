using System.Windows.Forms;

namespace Opgenorth.RangeLog.WinForms.Controls
{
    public class RangeTripGridBuilder
    {
        public static readonly string GridName = "RangeTripsDataGridView";

        private static DataGridViewColumn AddColumnToGrid(DataGridView grid, string dataProperty, string header, int width)
        {
            var colIndex = grid.Columns.Add("_" + dataProperty + "Column", header);
            var col = grid.Columns[colIndex];
            col.DataPropertyName = dataProperty;
            col.ReadOnly = true;
            col.Width = width;
            return col;
        }
        public DataGridView Build()
        {
            var grid = new DataGridView
                            {
                               AutoGenerateColumns = false,
                               Name = "RangeTrips",
                               AllowUserToAddRows = false,
                               AllowUserToDeleteRows = false
                            };
            AddColumnToGrid(grid, "DateOfTrip", "Date", 120);
            AddColumnToGrid(grid, "Firearm", "Firearm", 66);
            AddColumnToGrid(grid, "RoundsFired", "Rounds", 69);
            var commentCol = AddColumnToGrid(grid, "Comments", "Comments", 60);
            commentCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            return grid;
        }
    }
}
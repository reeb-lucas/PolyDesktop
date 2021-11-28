using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PolyDesktopGUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ScriptStats : Page
    {
        private string connectionString = "server=satou.cset.oit.edu,5433; database=PolyDestopn; UID=PolyCode; password=P0lyC0d3";
        private DataTable dataTable;
        public ScriptStats()
        {
            this.InitializeComponent();
            ExecuteQuery("SELECT * FROM PolyDestopn.dbo.script");
        }
        private async void ExecuteQuery(string query)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = query;
                dataTable = new DataTable();

                using (var dataAdapter = new SqlDataAdapter(command))
                {
                    dataAdapter.Fill(dataTable);
                }
            }

            BindTable(dataTable, dataGrid);

            if (dataTable.Rows.Count == 0)
            {
                dataGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                dataGrid.Visibility = Visibility.Visible;
            }
        }

        private void BindTable(DataTable table, DataGrid grid)
        {
            // Generate columns with index binding

            grid.Columns.Clear();
            grid.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.Collapsed;

            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (table.Columns[i].ColumnName == "Full SQL Statement")
                {
                    // Treat 'Full SQL Statement' column differently.
                    grid.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.VisibleWhenSelected;
                }
                else
                {
                    grid.Columns.Add(new DataGridTextColumn()
                    {
                        Header = table.Columns[i].ColumnName,
                        Binding = new Binding { Path = new PropertyPath("[" + i.ToString() + "]") }
                    });
                }
            }

            // Post-process 'SQL Statement' column.
            if (table.Columns.Contains("SQL Statement"))
            {
                var column = table.Columns["SQL Statement"];

                foreach (DataRow row in table.Rows)
                {
                    string sqlStatement = ((row[column] as string) ?? string.Empty).Trim();
                    row[column] = string.Join(' ', sqlStatement.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries)).Substring(0, 80) + "...";
                }

                table.AcceptChanges();
            }

            RefreshContents(table, grid);
        }

        private void RefreshContents(DataTable table, DataGrid grid)
        {
            // Create collection
            var collection = new ObservableCollection<object>();
            foreach (DataRow row in table.Rows)
            {
                collection.Add(row.ItemArray);
            }

            grid.ItemsSource = collection;
        }

        private void dataGrid_Sorting(object sender, DataGridColumnEventArgs e)
        {
            var currentSortDirection = e.Column.SortDirection;

            foreach (var column in dataGrid.Columns)
            {
                column.SortDirection = null;
            }

            var sortOrder = "ASC";

            if ((currentSortDirection == null || currentSortDirection == DataGridSortDirection.Descending))
            {
                e.Column.SortDirection = DataGridSortDirection.Ascending;
            }
            else
            {
                sortOrder = "DESC";
                e.Column.SortDirection = DataGridSortDirection.Descending;
            }

            var dataView = dataTable.DefaultView;
            dataView.Sort = e.Column.Header + " " + sortOrder;
            dataTable = dataView.ToTable();

            RefreshContents(dataTable, dataGrid);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ExecuteQuery("SELECT * FROM PolyDestopn.dbo.script");
        }
    }
}

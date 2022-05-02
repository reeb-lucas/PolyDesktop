using ControlzEx.Theming;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
/**************************************************************
 * Copyright (c) 2021
 * Authors: Jacob Pressley, Jerron Rhen
 * Filename: DesktopProperties.xaml.cs
 * Date Created: 2/3/2022
 * Modifications: 
 **************************************************************/
/**************************************************************
 * Overview: querys the DB and populates a table with the data.
 *          columns are dynamically added from database, so this code is completely independant of the content of the table.
 *      
 **************************************************************/

namespace PolyDesktopGUI_WPF
{
    /// <summary>
    /// Interaction logic for DesktopProperties.xaml
    /// </summary>
    public partial class DesktopProperties : Page
    {
        private string connectionString = "server=satou.cset.oit.edu,5433; database=PolyDesktop; UID=PolyCode; password=P0lyC0d3";
        private DataTable dataTable;
        public DesktopProperties()
        {
            InitializeComponent();
            ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithAppMode;
            ThemeManager.Current.SyncTheme();
            ExecuteQuery("SELECT * FROM PolyDesktop.dbo.desktop");
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
        private void BindTable(DataTable table, DataGrid grid) //bind table to the data gathered by the query
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
                    row[column] = string.Join(" ", sqlStatement.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries)).Substring(0, 80) + "...";
                }

                table.AcceptChanges();
            }

            RefreshContents(table, grid);
        }
        private void RefreshContents(DataTable table, DataGrid grid) // Create collection
        {
            var collection = new ObservableCollection<object>();
            foreach (DataRow row in table.Rows)
            {
                collection.Add(row.ItemArray);
            }

            grid.ItemsSource = collection;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
        }
    }
}

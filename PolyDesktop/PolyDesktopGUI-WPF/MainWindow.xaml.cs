/**************************************************************
 * Copyright (c) 2022
 * Author: Tyler Lucas
 * Filename: MainWindow.xaml.cs
 * Date Created: 1/23/2022
 * Modifications: 1/23/2022 - Created Main Window with no functionality
 *                1/25/2022 - Began adding navigation functionality
 * 
 **************************************************************/
/**************************************************************
 * Overview:
 *      This window holds all pages for the PolyDesktop application
 *      
 **************************************************************/
using ControlzEx.Theming;
using MahApps.Metro.Controls;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace PolyDesktopGUI_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        static string localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PolyDesktop\\Presets\\";
        DirectoryInfo di = Directory.CreateDirectory(localApplicationData); //Create directory if not exist
        string filename = System.IO.Path.Combine(localApplicationData, "Preset"); //filepath for presets with the word Prest appended to make future code easier
        private string connectionString = "server=satou.cset.oit.edu,5433; database=PolyDesktop; UID=PolyCode; password=P0lyC0d3";
        string[] bucket;
        public MainWindow()
        {
            InitializeComponent();
            ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithAppMode;
            ThemeManager.Current.SyncTheme();
            App.Current.Properties["AdvancedMode"] = false;
        }
        private void EditDeskPre_Click(object sender, RoutedEventArgs e)
        {
            ModePickerFlyout.IsOpen = false;
            NavFrame.Navigate(new EditPresets());
        }

        private void ViewDeskPro_Click(object sender, RoutedEventArgs e)
        {
            ModePickerFlyout.IsOpen = false;
            NavFrame.Navigate(new DesktopProperties());
        }
        /// <summary>
        /// Uses navString to change the frame to the .xaml specified
        /// </summary>
        /// <param name="navString">
        /// Contains a .xaml file name
        /// </param>
        public void Nav(string navString)
        {
            Uri uri = new Uri(navString, UriKind.Relative);

            NavFrame.Navigate(uri);
        }

        private void Tutorial_Click(object sender, RoutedEventArgs e)
        {
            ModePickerFlyout.IsOpen = false;
        }
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            ModePickerFlyout.IsOpen = false;
            NavFrame.Navigate(new SettingsPage()); //Settings
        }
        private void StartPDButton_Click(object sender, RoutedEventArgs e)
        {
            ModePickerFlyout.IsOpen = true;
        }
        private void BasicButton_Click(object sender, RoutedEventArgs e)
        {
            ModePickerFlyout.IsOpen = false;
            NavFrame.Navigate(new BasicModePage()); //Basic
        }
        private void TabButton_Click(object sender, RoutedEventArgs e)
        {
            ModePickerFlyout.IsOpen = false;
            NavFrame.Navigate(new TabModePage()); //Tabs
        }
        private void GroupButton_Click(object sender, RoutedEventArgs e)
        {
            ModePickerFlyout.IsOpen = false;
            NavFrame.Navigate(new GroupModePage()); //Group
        }
        private void PresetButton_Click(object sender, RoutedEventArgs e)
        {
            PresetPickerFlyout.IsOpen = true;
            ModePickerFlyout.IsOpen = false;
            SearchListBox.ItemsSource = GatherPresets(SearchBox.Text);
        }
        private void search_QueryChanged(object sender, TextChangedEventArgs e)
        {
            SearchListBox.ItemsSource = GatherPresets(SearchBox.Text);
        }
        public Preset[] GatherPresets(string searchTerm = "") //returns all presets in an observable object for the listview to display
        {
            Preset[] container = new Preset[Directory.GetFiles(localApplicationData).Length]; //sees how many files are in the directory for presets and sets the array size
            int j = 0;
            for (int i = 0; i < 99; i++)
            {
                try
                {
                    string temp = File.ReadAllText(filename + i + ".txt");
                    bucket = temp.Split(','); //used to Split(",")
                    if (searchTerm != "")
                    {
                        if (bucket[0].IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            Preset preset = new Preset();
                            preset.Name = bucket[0];
                            preset.Mode = bucket[1];
                            preset.numComputers = Int32.Parse(bucket[2]);
                            container[i - j] = preset;
                        }
                        else
                        {
                            j++;
                        }
                    }
                    else
                    {
                        Preset preset = new Preset();
                        preset.Name = bucket[0];
                        preset.Mode = bucket[1];
                        preset.numComputers = Int32.Parse(bucket[2]);
                        container[i] = preset;
                    }
                }
                catch
                {
                    if (i == 0)
                    {
                        //No Presets Found
                    }
                    break;
                }
            }
            return container;
        }
        private void SearchListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) //Adding computer to preset with default nickname being the computer name
        {
            Preset target = (Preset)SearchListBox.SelectedItem;
            if (target != null)
            {
                Computer[] computers = new Computer[target.numComputers];
                computers = GatherComputers();
                if (target.Mode == "Tab")
                {
                    NavFrame.Navigate(new TabModePage(computers, target.numComputers)); //Tabs
                }
                else if (target.Mode == "Group")
                {
                    //NavFrame.Navigate(new GroupModePage(computers, target.numComputers)); //Tabs
                }
                ModePickerFlyout.IsOpen = false;
                PresetPickerFlyout.IsOpen = false;
            }
        }
        public Computer[] GatherComputers() //returns all computers in a preset in an observable array to populate listview
        {
            try
            {
                string temp = File.ReadAllText(filename + SearchListBox.SelectedIndex + ".txt");
                bucket = temp.Split(',');
                Computer[] container = new Computer[Int32.Parse(bucket[2])];
                try
                {
                    int j = 1;
                    for (int i = 0; i < 99; i++)
                    {
                        j += 2;
                        Computer comp = new Computer();
                        if (bucket[j] != null)
                        {
                            comp.ID = bucket[j];
                            comp.Name = ExecuteQuery(j);
                            if (bucket[j + 1] != null)
                            {
                                comp.Nickname = bucket[j + 1];
                            }
                            else
                            {
                                comp.Nickname = comp.Name;
                            }
                        }
                        container[i] = comp;
                    }
                }
                catch
                {
                    return container;
                }
            }
            catch { }
            return new Computer[0];
        }
        private string ExecuteQuery(int index) //fetch computer name given c_ID
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT c_name FROM PolyDesktop.dbo.desktop WHERE c_ID = " + bucket[index];
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        string name = "No Name Found";
                        if (reader.HasRows)
                        {
                            reader.Read();
                            name = reader.GetString(0);
                            reader.Close();
                        }
                        return name;
                    }
                }
            }
        }
    }
}

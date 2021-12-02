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

/**************************************************************
 * Copyright (c) 2021
 * Author: Jacob Pressley
 * Filename: EditPresets.xaml.cs
 * Date Created: 11/29/2021
 * Modifications:
 **************************************************************/
/**************************************************************
 * Overview:    
 * Preset Saving Standard: "Name, Mode, # of computers, Computer ID, Nickname, Computer ID, Nickname, ..."
 *                          user can only have up to 100 presets
 **************************************************************/

namespace PolyDesktopGUI
{
    public sealed partial class EditPresets : Page
    {
        static string localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string filename = Path.Combine(localApplicationData, "Preset");
        private string connectionString = "server=satou.cset.oit.edu,5433; database=PolyDestopn; UID=PolyCode; password=P0lyC0d3";
        string[] bucket;
        public EditPresets()
        {
            this.InitializeComponent();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        public Preset[] Presets { get { return GatherPresets(); } }
        public Preset[] GatherPresets()
        {
            Preset[] container = new Preset[100];
            for (int i = 0; i < 99; i++)
            {
                try
                {
                    string temp = File.ReadAllText(filename + i + ".txt");
                    string[] bucket = temp.Split(", ");
                    Preset preset = new Preset();
                    preset.Name = bucket[0];
                    preset.Mode = bucket[1];
                    preset.numComputers = Int32.Parse(bucket[2]);
                    container[i] = preset;
                }
                catch
                {
                    break;
                }
            }
            return container;
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string temp = File.ReadAllText(filename + PresetList.SelectedIndex + ".txt");
            bucket = temp.Split(", ");
            NameBox.Text = bucket[0];
            ModeBox.PlaceholderText = bucket[1];
            ComputerTable.ItemsSource = Computers;
        }
        private void ComputerTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ComputerTable.SelectedItem != null)
            {
                //popout to view/change nickname, and remove computer
            }
        }
        private void AddComputerButton_Click(object sender, RoutedEventArgs e)
        {
            //popup to add computer from list to bucket and set nickname
        }
        public Computer[] Computers { get { return GatherComputers(); } }
        public Computer[] GatherComputers()
        {
            Computer[] container = new Computer[100];
            try
            {
                string temp = File.ReadAllText(filename + PresetList.SelectedIndex + ".txt");
                bucket = temp.Split(", ");
                int j = 1;
                for (int i = 0; i < 99; i++)
                {
                    j += 2;
                    Computer preset = new Computer();
                    preset.ID = bucket[j];
                    if (bucket[j] != null)
                    {
                        preset.Name = ExecuteQuery(j);
                    }
                    preset.Nickname = bucket[j + 1];
                    container[i] = preset;
                }
            }
            catch
            {
                return container;
            }
            
            return container;
        }
        private string ExecuteQuery(int index)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT c_name FROM PolyDestopn.dbo.desktop WHERE c_ID = " + bucket[index];
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
        private void PresetSaveButton_Click(object sender, RoutedEventArgs e)
        {
            //write back to file using bucket object
        }
        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(filename + 0 + ".txt", TestBox.Text);
        }
    }
    public class Preset
    {
        public string Name { get; set; }
        public string Mode { get; set; }
        public int numComputers { get; set; }
    }
    public class Computer
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
    }
}

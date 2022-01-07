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
 * Modifications: 12/6/2021 - Added additional coments
 **************************************************************/
/**************************************************************
 * Overview: Allows user to edit the presets saved in .txt files and allow users to delete presets.
 * Preset Saving Standard: "Name, Mode, # of computers, Computer ID, Nickname, Computer ID, Nickname, ..."
 *                          user can only have up to 100 presets
 *                          each preset can have up to 100 computers
 **************************************************************/

namespace PolyDesktopGUI
{
    public sealed partial class EditPresets : Page
    {
        static string localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string filename = Path.Combine(localApplicationData, "Preset"); //filepath for presets with the word Prest appended to make future code easier
        private string connectionString = "server=satou.cset.oit.edu,5433; database=PolyDesktop; UID=PolyCode; password=P0lyC0d3";
        string[] bucket;
        public EditPresets()
        {
            this.InitializeComponent();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e) //back to main menu
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        public Preset[] Presets { get { return GatherPresets(); } }
        public Preset[] GatherPresets() //returns all presets in an observable object for the listview to display
        {
            Preset[] container = new Preset[Directory.GetFiles(localApplicationData).Length]; //sees how many files are in the directory for presets and sets the array size
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
                    PresetList.Header = "Presets";
                }
                catch
                {
                    if (i == 0)
                    {
                        PresetList.Header = "No Presets Found";
                    }
                    break;
                }
            }
            return container;
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) //when preset is selected, display all preset info from file and populate computertable
        {
            try
            {
                string temp = File.ReadAllText(filename + PresetList.SelectedIndex + ".txt");
                bucket = temp.Split(", ");
                NameBox.Text = bucket[0];
                ModeBox.PlaceholderText = bucket[1];
                ComputerTable.ItemsSource = Computers;
                int index = 0;
                for (int i = 2; i < bucket.Length; i += 2)
                {
                    if (bucket[i] != "")
                    {
                        index++;
                    }
                }
                NumBlock.Text = (index - 1).ToString();

            }
            catch { }
        }
        private void ComputerTable_SelectionChanged(object sender, SelectionChangedEventArgs e) //ListView object holding all computers in a preset
        {
            if(ComputerTable.SelectedItem != null)
            {
                int index = 1;
                for (int i = 0; i < ComputerTable.SelectedIndex + 1; i++)
                {
                    index += 2;
                }
                if (bucket[index] != null)
                {
                    FlyoutIDBlock.Text = bucket[index];
                    FlyoutNameBlock.Text = ExecuteQuery(index);
                    FlyoutNicknameBox.Text = bucket[index + 1];
                }
                FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender); //flyout with computer info and oportunity to change nickname
            }
        }
        public Computer[] Computers { get { return GatherComputers(); } }
        public Computer[] GatherComputers() //returns all computers in an observable array to populate listview
        {   try
            {
                string temp = File.ReadAllText(filename + PresetList.SelectedIndex + ".txt");
                bucket = temp.Split(", ");
                Computer[] container = new Computer[Int32.Parse(bucket[2])];
                try
                {
                    int j = 1;
                    for (int i = 0; i < 99; i++)
                    {
                        j += 2;
                        Computer preset = new Computer();
                        if (bucket[j] != null)
                        {
                            preset.ID = bucket[j];
                            preset.Name = ExecuteQuery(j);
                            if (bucket[j + 1] != null)
                            {
                                preset.Nickname = bucket[j + 1];
                            }
                            else
                            {
                                preset.Nickname = preset.Name;
                            }
                        }
                        container[i] = preset;
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
        public Computer[] AllComputers { get { return GatherComputers(); } }
        public Computer[] GatherAllComputers() //returns all computers in an observable array to populate listview
        {
            try
            {
                Computer[] container = new Computer[Int32.Parse(bucket[2])];
                try
                {
                    int j = 1;
                    for (int i = 0; i < 99; i++)
                    {
                        j += 2;
                        Computer preset = new Computer();
                        if (bucket[j] != null)
                        {
                            preset.ID = bucket[j];
                            using (var connection = new SqlConnection(connectionString))
                            {
                                string sql = "SELECT c_name FROM PolyDesktop.dbo.desktop";
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
                                        preset.Name = name;
                                    }
                                }
                            }
                            preset.Nickname = preset.Name;
                        }
                        container[i] = preset;
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
        private void NicknameChangeButton_Click(object sender, RoutedEventArgs e)
        {
            int index = 2;
            for (int i = 0; i < ComputerTable.SelectedIndex + 1; i++)
            {
                index += 2;
            }
            if (FlyoutNicknameBox.Text == null)
            {
                bucket[index] = bucket[index - 1];

            }
            else if (bucket[index] != null)
            {
                bucket[index] = NormalizeInput(FlyoutNicknameBox.Text);
            }
        }
        private void PresetSaveButton_Click(object sender, RoutedEventArgs e) //write back to file using bucket object
        {
            if (PresetList.SelectedIndex != -1)
            {
                string saveString = NormalizeInput(NameBox.Text) + ", " + bucket[1] + ", " + NormalizeInput(NumBlock.Text);
                for (int i = 3; i < bucket.Length; i++)
                {
                    saveString = saveString + ", " + bucket[i];
                }
                if (saveString != null)
                {
                    File.WriteAllText(filename + PresetList.SelectedIndex + ".txt", saveString);
                    ComputerTable.ItemsSource = Computers;
                }
            }
            PresetList.ItemsSource = Presets;
        }
        private void AddComputerButton_Click(object sender, RoutedEventArgs e) //popup to add computer from list to bucket and set nickname
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);

        }
        private void TestButton_Click(object sender, RoutedEventArgs e) //this fills the computer with test presets
        {
            File.WriteAllText(filename + 0 + ".txt", "TestPreset1, Tab, 3, 14252351, TestNickname 0, 162, TestNickname 1, 158964, TestNickname 2");
            File.WriteAllText(filename + 1 + ".txt", "TestPreset2, Group, 6, 14252351, TestNickname 0, 162, TestNickname 1, 158964, TestNickname 2, 213286983, TestNickname 3, 102538501, TestNickname 4, 25389172, TestNickname 5");
            File.WriteAllText(filename + 2 + ".txt", "TestPreset3, Basic, 4, 14252351, TestNickname 0, 162, TestNickname 1, 158964, TestNickname 2, 213286983, TestNickname 3");
            File.WriteAllText(filename + 3 + ".txt", "TestPreset4, Overlay, 5, 14252351, TestNickname 0, 162, TestNickname 1, 158964, TestNickname 2, 213286983, TestNickname 3, 102538501, TestNickname 4");
            PresetList.ItemsSource = Presets;
        }

        private void ModeBox_SelectionChanged(object sender, SelectionChangedEventArgs e) //write new mode to bucket
        {
            if (PresetList.Header.ToString() != "No Presets Found")
            {
                bucket[1] = ModeBox.SelectedValue.ToString();
            }
        }

        private string NormalizeInput(string input) //remove commas from input to make sure file is structured correctly
        {
            return input.Replace(",", "");
        }

        private void DeletePresetButton_Click(object sender, RoutedEventArgs e) //pop-up to confirm deletion
        { 
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
        private void FlyoutDeletePresetButton_Click(object sender, RoutedEventArgs e) //delete .txt file for index selected and shift all following files up a name
        {
            File.Delete(filename + PresetList.SelectedIndex + ".txt");
            for (int i = PresetList.SelectedIndex + 1; i < 100; i++)
            {
                try
                {
                    System.IO.File.Move(filename + i + ".txt", filename + (i - 1) + ".txt");
                }
                catch { }
            }
            PresetList.ItemsSource = Presets;
        }

        private void search_QueryChanged(SearchBox sender, SearchBoxQueryChangedEventArgs args)
        {

        }
    }
    public class Preset
    {
        public string Name { get; set; }
        public string Mode { get; set; }
        public int numComputers { get; set; }
        public bool isFull { get; set; } = false;
    }
    public class Computer
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public bool isFull { get; set; } = false;
    }
}

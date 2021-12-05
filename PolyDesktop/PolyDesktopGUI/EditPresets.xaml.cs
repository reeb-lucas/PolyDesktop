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
        private void ComputerTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            }
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
        private void PresetSaveButton_Click(object sender, RoutedEventArgs e)
        {
            //write back to file using bucket object
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
        private void AddComputerButton_Click(object sender, RoutedEventArgs e)
        {
            //popup to add computer from list to bucket and set nickname
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(filename + 0 + ".txt", "TestPreset1, Tab, 3, 0, TestNickname 0, 1, TestNickname 1, 2, TestNickname 2");
            File.WriteAllText(filename + 1 + ".txt", "TestPreset2, Group, 6, 0, TestNickname 0, 1, TestNickname 1, 2, TestNickname 2, 3, TestNickname 3, 4, TestNickname 4, 5, TestNickname 5");
            File.WriteAllText(filename + 2 + ".txt", "TestPreset3, Basic, 4, 0, TestNickname 0, 1, TestNickname 1, 2, TestNickname 2, 3, TestNickname 3");
            File.WriteAllText(filename + 3 + ".txt", "TestPreset4, Overlay, 5, 0, TestNickname 0, 1, TestNickname 1, 2, TestNickname 2, 3, TestNickname 3, 4, TestNickname 4");
        }

        private void ModeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bucket[1] = ModeBox.SelectedValue.ToString();
        }

        private string NormalizeInput(string input)
        {
            return input.Replace(",", "");
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

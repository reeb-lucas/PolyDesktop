using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
            string[] bucket = temp.Split(", ");
            NameBox.Text = bucket[0];
            ModeBox.PlaceholderText = bucket[1];
            ComputerTable.ItemsSource = Computers;
        }
        private void ComputerTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //popout to view/change nickname, and remove computer
            //if cell is empty, popup to add computer
        }
        public Computer[] Computers { get { return GatherComputers(); } }
        public Computer[] GatherComputers()
        {
            Computer[] container = new Computer[100];
            try
            {
                string temp = File.ReadAllText(filename + PresetList.SelectedIndex + ".txt");
                string[] bucket = temp.Split(", ");
                int j = 1;
                for (int i = 0; i < 99; i++)
                {
                    j += 2;
                    Computer preset = new Computer();
                    preset.ID = bucket[j];
                    preset.Name = "SAMPLENAME";
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
        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(filename + 1 + ".txt", TestBox.Text);
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

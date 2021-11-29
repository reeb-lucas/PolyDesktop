using System;
using System.Collections.Generic;
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
 *      
 * Preset Saving Standard: "Name, Mode, # of computers, Computer ID, Nickname, Computer ID, Nickname, ..."
 *                          nickname will be void if none is present "Computer ID, , ..."
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
        private void WriteButton_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(filename + "0.txt", WriteBox.Text);
        }
        private void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            ReadBlock.Text = File.ReadAllText(filename + "0.txt");

        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        public Preset[] Presets { get; } = new Preset[]
        {
            ("Preset1", "Group", 6),
            ("Preset2", "Tab", 3)
        };
        public Preset[] GatherPresets()
        {
            Preset[] container = new Preset[100];
            for (int i = 0; i < 99; i++)
            {
                string temp = File.ReadAllText(filename + i + ".txt"); //TODO: parse string and make Preset objects
            }
            return container;
        }
    }
    public class Preset
    {
        public string Name { get; set; }
        public string Mode { get; set; }
        public int numComputers { get; set; }
        public static implicit operator Preset((string Name, string Mode, int numComputers) info)
        {
            return new Preset { Name = info.Name, Mode = info.Mode, numComputers = info.numComputers };
        }
    }
}

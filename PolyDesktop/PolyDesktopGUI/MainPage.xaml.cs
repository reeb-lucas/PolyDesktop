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
using Windows.UI.ViewManagement;
/**************************************************************
 * Copyright (c) 2021
 * Author: Tyler Lucas, Jacob Pressley
 * Filename: MainPage.xaml.cs
 * Date Created: 11/16/2021
 * Modifications: 11/16/2021 - Created Main Menu, Buttons do not have functionality
 *                11/29/2021 - all main buttons have functionality
 **************************************************************/
/**************************************************************
 * Overview:
 *      This file creates the MainPage or Main Menu with 6 basic buttons. Functionality for each button
 *      is described in the function comments.
 **************************************************************/

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PolyDesktopGUI
{
    public sealed partial class MainPage : Page
    {
        static string localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string filename = Path.Combine(localApplicationData, "Preset"); //filepath for presets with the word Prest appended to make future code easier
        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.GetForCurrentView().SetPreferredMinSize(
                new Size(
                    500, // Width
                    1000 // Height
                    )
                );//TODO: Fix Height stuck at 750 pixels
        }

        private void DesktopPropertiesButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DesktopProperties));
        }

        private void ScriptStatsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ScriptStats));
        }

        private void DesktopPresetsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EditPresets));
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
            //start preset
        }
        private void BasicMode_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BasicMode));
        }

        private void TabMode_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TabMode));
        }

        private void GroupMode_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GroupMode));
        }

        private void OverlayMode_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(OverlayMode));
        }
    }
}

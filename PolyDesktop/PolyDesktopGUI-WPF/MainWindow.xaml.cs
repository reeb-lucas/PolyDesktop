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
using System.Windows;
using System.Windows.Navigation;

namespace PolyDesktopGUI_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
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
    }
}

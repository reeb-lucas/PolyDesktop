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
using System;
using System.Windows;
using System.Windows.Navigation;

namespace PolyDesktopGUI_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartPDBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (StartPDBox.SelectedIndex == 0)
                NavFrame.Navigate(new BasicModePage()); //Preset
            if (StartPDBox.SelectedIndex == 1)
                NavFrame.Navigate(new BasicModePage()); //Basic
            if (StartPDBox.SelectedIndex == 2)
                NavFrame.Navigate(new BasicModePage()); //Tab
            if (StartPDBox.SelectedIndex == 3)
                NavFrame.Navigate(new BasicModePage()); //Group

        }
        private void EditDeskPre_Click(object sender, RoutedEventArgs e)
        {
            //NavFrame.Navigate(new EditDesktopPrests());
        }

        private void ViewDeskPro_Click(object sender, RoutedEventArgs e)
        {
            //NavFrame.Navigate(new ViewDesktopProperties());
        }

        private void ViewScirptS_Click(object sender, RoutedEventArgs e)
        {
            //NavFrame.Navigate(new ViewSctriptStats());
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

        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

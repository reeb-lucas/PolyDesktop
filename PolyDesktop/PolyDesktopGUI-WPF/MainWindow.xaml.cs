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
        private int count = 0;
        public MainWindow()
        {
            count++;
            InitializeComponent();
            if (count == 1)
            {
                Uri uri = new Uri("MainPage.xaml", UriKind.Relative);
                NavFrame.Navigate(uri);
            }

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

        
    }
}

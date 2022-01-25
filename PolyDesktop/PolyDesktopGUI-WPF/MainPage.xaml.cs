/**************************************************************
 * Copyright (c) 2022
 * Author: Tyler Lucas
 * Filename: ChoosePolyDesktopType.xaml.cs
 * Date Created: 1/25/2022
 * Modifications: 1/25/2022 - Created start button to bring a pop-up where the user chooses remoting mode
 * 
 **************************************************************/
/**************************************************************
 * Overview:
 *      This page is the main menu providing
 *      
 **************************************************************/
using System;
using System.Windows;
using System.Windows.Controls;


namespace PolyDesktopGUI_WPF
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void StartPDButton_Click(object sender, RoutedEventArgs e)
        {
            var CPD = new ChoosePolyDesktopType();
            CPD.Show();
        }
    }
}

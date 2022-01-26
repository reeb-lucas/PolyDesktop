/**************************************************************
 * Copyright (c) 2022
 * Author: Tyler Lucas
 * Filename: ChoosePolyDesktopType.xaml.cs
 * Date Created: 1/25/2022
 * Modifications: 1/25/2022 - Created Choose window, began implementing functionality for basic mode
 * 
 **************************************************************/
/**************************************************************
 * Overview:
 *      This window allows the user to select the desktop remoting mode
 *      
 **************************************************************/
using System;
using System.Windows;


namespace PolyDesktopGUI_WPF
{
    /// <summary>
    /// Interaction logic for ChoosePolyDesktopType.xaml
    /// </summary>
    public partial class ChoosePolyDesktopType : Window
    {
        public ChoosePolyDesktopType()
        {
            InitializeComponent();
        }


        private void BasicModeButton_Click(object sender, RoutedEventArgs e)
        {
            var BM = new BasicModePage();
            BM.NavigationService.Navigate(new Uri("BasicModePage.xaml", UriKind.Relative));
            this.Close();
        }
    }
}

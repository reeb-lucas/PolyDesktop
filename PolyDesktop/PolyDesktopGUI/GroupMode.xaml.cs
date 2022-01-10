using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

/**************************************************************
 * Copyright (c) 2021
 * Author: Tyler Lucas
 * Filename: TabMode.xaml.cs
 * Date Created: 11/30/2021
 * Modifications: 11/30/2021 - Created Group Mode File, WIP label waiting on remoting capabilities and feature implementation
 *                12/4/2021 - Implemented navigation back to main menu with back button
 **************************************************************/
/**************************************************************
 * Overview:
 *      This page displays multiple desktops with one in "focus" at a larger size with the remaining desktops at the side in a scrolable column
 **************************************************************/

namespace PolyDesktopGUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GroupMode : Page
    {
        public GroupMode()
        {
            this.InitializeComponent();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}

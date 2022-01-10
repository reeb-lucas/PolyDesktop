using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


/**************************************************************
 * Copyright (c) 2021
 * Author: Tyler Lucas
 * Filename: OverlayMode.xaml.cs
 * Date Created: 11/30/2021
 * Modifications: 11/30/2021 - Created Overlay Mode File, WIP label waiting on remoting capabilities and feature implementation
 *                12/4/2021 - Implemented navigation back to main menu with back button
 **************************************************************/
/**************************************************************
 * Overview:
 *      This page displays any number of desktops overlayed at 55% opacity to allow for simultaneous work at full screen resolution
 **************************************************************/

namespace PolyDesktopGUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OverlayMode : Page
    {
        public OverlayMode()
        {
            this.InitializeComponent();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}

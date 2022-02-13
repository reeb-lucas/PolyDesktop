using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

/**************************************************************
 * Copyright (c) 2021
 * Author: Tyler Lucas
 * Filename: TabMode.xaml.cs
 * Date Created: 11/30/2021
 * Modifications: 11/30/2021 - Created Tab Mode File, WIP label waiting on remoting capabilities and feature implementation
 *                12/4/2021 - Implemented navigation back to main menu with back button
 *                1/4/2022 - Sucessfully implemented browser-like UI and navigation. Currently defaults to Main Menu page, 
 *                           waiting on remoting capabilities
 *                1/15/2021 - Added PolyBay Tab home screen with back button
 **************************************************************/
/**************************************************************
 * Overview:
 *      This page displays a browser-like navigation system for the user to access differernt desktops and the PolyBay drive system
 **************************************************************/

namespace PolyDesktopGUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TabMode : Page
    {
        public TabMode()
        {
            this.InitializeComponent();
            InitPolyBayTab();
        }

        private void InitPolyBayTab()
        {
            Frame frame = new Frame();
            PolyBayTab.Content = frame;
            frame.Navigate(typeof(PolyBayTab));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Tabs_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            sender.TabItems.Remove(args.Tab);
        }

        private void TabView_AddTabButtonClick(TabView sender, object args)
        {
            var newTab = new TabViewItem();
            //newTab.IconSource = new SymbolIconSource() { Symbol = Symbol.Document }; //TODO: give actual symbol
            newTab.Header = "New Document";

            // The Content of a TabViewItem is often a frame which hosts a page.
            Frame frame = new Frame();
            newTab.Content = frame;
            frame.Navigate(typeof(MainPage)); //TODO: display desktopstream here

            sender.TabItems.Add(newTab);
        }
    }
}

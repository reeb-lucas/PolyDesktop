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
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using NetworkCommsDotNet.Connections;
using Hardwarespecs;
using ChatClient;
using PolyDesktopGUI_WPF.MVVM.View;

namespace PolyDesktopGUI_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public List<MetroTabItem> m_tabItemList;
        public MainWindow()
        {
            ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithAppMode;
            ThemeManager.Current.SyncTheme();
            InitializeComponent();
            HardwareSpecPuller.WriteToDB();

            m_tabItemList = new List<MetroTabItem>();
            //  m_VNCList = new List<VncPage>();

            //this tab will hold PolyChat
            MetroTabItem tabPolyChat = new MetroTabItem();
            tabPolyChat.Header = "PolyChat";

            Frame PolyCFrame = new Frame();
            tabPolyChat.Content = PolyCFrame;
            ChatMain polyChat = new ChatMain();
            PolyCFrame.Navigate(polyChat);
            m_tabItemList.Add(tabPolyChat);


            //this tab will hold PolyBay
            MetroTabItem tabPolyBay = new MetroTabItem();
            tabPolyBay.Header = "PolyBay";

            Frame PolyBFrame = new Frame();
            tabPolyBay.Content = PolyBFrame;
            PolyBay polyBay = new PolyBay();
            PolyBFrame.Navigate(polyBay);
            m_tabItemList.Add(tabPolyBay);

            

            //bind tab list
            tabControl.DataContext = m_tabItemList;
            tabControl.SelectedIndex = 0;

        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MetroTabItem tab = tabControl.SelectedItem as MetroTabItem;
            if (tab != null && tab.Header != null)
            {
                //if (tab.Header.Equals("+"))
                //{
                //    //clear tabControl binding
                //    tabControl.DataContext = null;

                //    //adds new tab, binds tab control, and seclects
                //    MetroTabItem newTab = this.AddTabItem();
                //    tabControl.DataContext = m_tabItemList;
                //    tabControl.SelectedItem = newTab;
                //}
            }
        }
       
    }
}

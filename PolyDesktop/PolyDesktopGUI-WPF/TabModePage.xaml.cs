/**************************************************************
 * Copyright (c) 2022
 * Author: Tyler Lucas
 * Filename: MainWindow.xaml.cs
 * Date Created:  2/8/2022
 * Modifications: 2/8/2022  - Basic xaml tab mode mock up created
 *                2/13/2022 - Began working on dynamic tab system with Metro TabControl 
 * 
 **************************************************************/
/**************************************************************
 * Overview:
 *      This window holds all pages for the PolyDesktop application
 *      
 **************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ControlzEx.Theming;
using MahApps.Metro.Controls;

namespace PolyDesktopGUI_WPF
{
    /// <summary>
    /// Interaction logic for TabModePage.xaml
    /// </summary>
    public partial class TabModePage : Page
    {
        private List<MetroTabItem> m_tabItemList;
        public TabModePage()
        {
            InitializeComponent();

            m_tabItemList = new List<MetroTabItem>();

            //this tab will hold PolyBay
            MetroTabItem tabPolyBay = new MetroTabItem();
            tabPolyBay.Header = "PolyBay";
            

            //TODO: make content PolyBay
            TextBlock PBtext = new TextBlock();
            PBtext.Text = "Put PolyBay here";
            tabPolyBay.Content = PBtext;
            m_tabItemList.Add(tabPolyBay);

            //this tab acts as button to add new tabs
            MetroTabItem tabAdder = new MetroTabItem();
            tabAdder.Header = "+";
            tabAdder.MouseLeftButtonUp += TabAdder_MouseLeftButtonUp;

            m_tabItemList.Add(tabAdder);

            //Add tabAdder tab
            this.AddTabItem();

            //bind tab list
            tabControl.DataContext = m_tabItemList;
            tabControl.SelectedIndex = 0;

        }

        private void TabAdder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.AddTabItem();
        }

        private MetroTabItem AddTabItem()
        {
            int count = m_tabItemList.Count;

            //tab creation
            MetroTabItem tab = new MetroTabItem();
            tab.Header = string.Format("Test Tab {0}", count); //TODO: Computer Name here
            tab.Name = string.Format("TestTab{0}", count); //TODO: Computer Name here
            tab.CloseButtonEnabled = true;

            //adds content to tab
            //TODO: add VNC content
            TextBlock testtext = new TextBlock();
            testtext.Text = "Put VNC here";
            tab.Content = testtext;

            m_tabItemList.Insert(count - 1, tab);
            return tab;
        }
    }
}

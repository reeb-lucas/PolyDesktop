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
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using NetworkCommsDotNet.Connections;

namespace PolyDesktopGUI_WPF
{
    /// <summary>
    /// Interaction logic for TabModePage.xaml
    /// </summary>
    public partial class TabModePage : Page
    {
        private List<MetroTabItem> m_tabItemList;
        private List<VncPage> m_VNCList;
        static string localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PolyDesktop\\Presets\\";
        DirectoryInfo di = Directory.CreateDirectory(localApplicationData); //Create directory if not exist
        string filename = System.IO.Path.Combine(localApplicationData, "Preset"); //filepath for presets with the word Prest appended to make future code easier
        private string connectionString = "server=satou.cset.oit.edu,5433; database=PolyDesktop; UID=PolyCode; password=P0lyC0d3";
        public TabModePage()
        {
            InitializeComponent();

            m_tabItemList = new List<MetroTabItem>();
            m_VNCList = new List<VncPage>();

            //tab to space the rest out
            MetroTabItem tabSpace = new MetroTabItem();
            tabSpace.Header = "      ";
            tabSpace.Focusable = false;
            m_tabItemList.Add(tabSpace);

            //this tab will hold PolyBay
            MetroTabItem tabPolyBay = new MetroTabItem();
            tabPolyBay.Header = "PolyBay";


            //TODO: make content PolyBay
            //TextBlock PBtext = new TextBlock();
            //PBtext.Text = "Put PolyBay here";
            //tabPolyBay.Content = PBtext;
            //m_tabItemList.Add(tabPolyBay);

            Frame PolyBFrame = new Frame();
            tabPolyBay.Content = PolyBFrame;
            PolyBay polyBay = new PolyBay();
            PolyBFrame.Navigate(polyBay);
            m_tabItemList.Add(tabPolyBay);

            //this tab acts as button to add new tabs
            MetroTabItem tabAdder = new MetroTabItem();
            tabAdder.Header = "+";

            m_tabItemList.Add(tabAdder);

            //bind tab list
            tabControl.DataContext = m_tabItemList;
            tabControl.SelectedIndex = 0;

        }
        private MetroTabItem AddTabItem()
        {
            int count = m_tabItemList.Count;

            //tab creation
            MetroTabItem tab = new MetroTabItem();
            tab.Header = string.Format("Computer {0}", count - 2);
            tab.Name = string.Format("Computer{0}", count - 2);
            tab.CloseButtonEnabled = true;

            //Give the abiliby to right click and change the Nickname
            ContextMenu nameMenu = new ContextMenu();
            MenuItem change = new MenuItem();
            change.Header = "Change Nickname";
            change.Click += Change_Click;
            MenuItem remove = new MenuItem();
            remove.Header = "Remove Nickname";
            remove.Click += Remove_Click;
            nameMenu.Items.Add(change);
            nameMenu.Items.Add(remove);
            tab.ContextMenu = nameMenu;

            //adds content to tab
            Frame VncFrame = new Frame();
            tab.Content = VncFrame;
            VncPage localSession = new VncPage(this);
            VncFrame.Navigate(localSession);

            m_tabItemList.Insert(count - 1, tab);
            m_VNCList.Insert(count - 3, localSession);
            return tab;
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MetroTabItem tab = tabControl.SelectedItem as MetroTabItem;
            if (tab != null && tab.Header != null)
            {
                if (tab.Header.Equals("+"))
                {
                    //clear tabControl binding
                    tabControl.DataContext = null;

                    //adds new tab, binds tab control, and seclects
                    MetroTabItem newTab = this.AddTabItem();
                    tabControl.DataContext = m_tabItemList;
                    tabControl.SelectedItem = newTab;
                }
            }
        }
        public void UpdateNames() //Changed this to only update the currently selected tab
        {
            if (m_tabItemList != null && m_VNCList[tabControl.SelectedIndex - 2].GetConnectedName() != "")
            {
                m_tabItemList[tabControl.SelectedIndex].Header = m_VNCList[tabControl.SelectedIndex - 2].GetConnectedName();
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
            for(int i = 0; i < m_VNCList.Count; i++)
            {
                m_VNCList[i].Disconnect();
            }
            Connection.StopListening();
        }
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            ComputerNameBlock.Text = m_VNCList[tabControl.SelectedIndex - 2].GetConnectedName();
            NicknameFlyout.IsOpen = true;
        }
        private void Nickname_Changed(object sender, TextChangedEventArgs e)
        {
            if (NameBox.Text.Replace(",", "") == "")
            {
                Remove_Click(sender, e);
            }
            else
            {
                m_tabItemList[tabControl.SelectedIndex].Header = NameBox.Text.Replace(",", "");
            }
        }
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            m_tabItemList[tabControl.SelectedIndex].Header = m_VNCList[tabControl.SelectedIndex - 2].GetConnectedName();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            PresetFlyout.IsOpen = true;
        }

        private void PresetSaveButton_Click(object sender, RoutedEventArgs e)
        {
            string bucket = PresetNameBox.Text.Replace(",", "") + ",Tab," + (m_VNCList.Count - 1) + ",";
            for (int i = 2; i < m_tabItemList.Count - 2; i++)
            {
                string ID = "";
                using (var connection = new SqlConnection(connectionString))
                {
                    string sql = "SELECT c_ID FROM PolyDesktop.dbo.desktop WHERE c_name = \'" + m_VNCList[i - 2].GetConnectedName() + "\'";
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                ID = reader.GetInt32(0).ToString();
                                reader.Close();
                            }
                        }
                    }
                }
                bucket += ID;
                bucket += ",";
                bucket += m_tabItemList[i].Header.ToString().Replace(",", "");
                bucket += ",";
            }
            bucket = bucket.TrimEnd(',');
            File.WriteAllText(filename + Directory.GetFiles(localApplicationData).Length + ".txt", bucket);
            PresetFlyout.IsOpen = false;
        }
        private void NNSaveButton_Click(object sender, RoutedEventArgs e)
        {
            NicknameFlyout.IsOpen = false;
        }
        private void CloseTab(object sender, RoutedEventArgs e) //TODO: do this when a tab is closed
        {
            m_VNCList[tabControl.SelectedIndex - 2].Disconnect();
            tabControl.Items.MoveCurrentToNext();
        }
    }
}

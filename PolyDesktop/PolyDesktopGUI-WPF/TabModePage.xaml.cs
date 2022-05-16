/**************************************************************
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
using OmotVnc.View.ViewModel;
/**********************************************************
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * NEtworkCommsDotNet, protobuf-net
**********************************************************/
namespace PolyDesktopGUI_WPF
{
    /// <summary>
    /// Interaction logic for TabModePage.xaml
    /// </summary>
    public partial class TabModePage : Page
    {
        private List<MetroTabItem> m_tabItemList;
        public List<VncPage> m_VNCList;
        PolyBay m_polyBay;
        static string localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PolyDesktop\\Presets\\";
        DirectoryInfo di = Directory.CreateDirectory(localApplicationData); //Create directory if not exist
        string filename = System.IO.Path.Combine(localApplicationData, "Preset"); //filepath for presets with the word Prest appended to make future code easier
        private string connectionString = "server=satou.cset.oit.edu,5433; database=PolyDesktop; UID=PolyCode; password=P0lyC0d3";
        int prevIndex = 2; //used for dynamic connections
        public TabModePage(Computer[] source = null, int num = 0)
        {
            InitializeComponent();

            m_tabItemList = new List<MetroTabItem>();
            m_VNCList = new List<VncPage>();

            //tab to space the rest out
            MetroTabItem tabSpace = new MetroTabItem();
            tabSpace.Header = "   ";
            tabSpace.Focusable = false;
            m_tabItemList.Add(tabSpace);

            //this tab will hold PolyBay
            MetroTabItem tabPolyBay = new MetroTabItem();
            tabPolyBay.Header = "PolyBay";

            Frame PolyBFrame = new Frame();
            tabPolyBay.Content = PolyBFrame;
            m_polyBay = new PolyBay(this);
            PolyBFrame.Navigate(m_polyBay);
            m_tabItemList.Add(tabPolyBay);

            //this tab acts as button to add new tabs
            MetroTabItem tabAdder = new MetroTabItem();
            tabAdder.Header = "+";

            m_tabItemList.Add(tabAdder);

            //bind tab list
            tabControl.DataContext = m_tabItemList;
            tabControl.SelectedIndex = 2;

            if(source != null)
            {
                for(int i = 0; i < num; i++)
                {
                    AddTabItem(source[i]);
                }
            }
        }
        private MetroTabItem AddTabItem(Computer computer = null)
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
            VncPage localSession;
            if(computer != null)
            {
                localSession = new VncPage(null, null, computer.Name);
                tab.Header = computer.Nickname;
            }
            else
            {
                localSession = new VncPage(this);
            }
            VncFrame.Navigate(localSession);

            m_tabItemList.Insert(count - 1, tab);
            m_VNCList.Insert(count - 3, localSession);
            prevIndex = tabControl.SelectedIndex;
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
                else if (tab.Header.Equals("PolyBay")) //PolyBay
                {
                    m_polyBay.UpdateConnectedList();
                }
                else if (tabControl.SelectedIndex >= 2 && (m_VNCList[tabControl.SelectedIndex - 2].GetConnectedName() != "") && m_VNCList.Count > 1) //dynamic connection
                {
                    m_VNCList[tabControl.SelectedIndex - 2].Reconnect();
                    if (prevIndex == -1)
                        prevIndex = 2;
                    if (prevIndex >= 2 && prevIndex - 2 < m_VNCList.Count)
                        m_VNCList[prevIndex - 2].Disconnect();
                    prevIndex = tabControl.SelectedIndex;
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
            if ((bool)(App.Current.Properties["AdvancedMode"]) == true)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Users cannot create Presets while in Advanced Mode", "Advanced Mode", System.Windows.MessageBoxButton.OK);
            }
            else
            {
                PresetFlyout.IsOpen = true;
            }
        }

        private void PresetSaveButton_Click(object sender, RoutedEventArgs e)
        {
            string bucket = PresetNameBox.Text.Replace(",", "") + ",Tab," + m_VNCList.Count + ",";
            for (int i = 2; i < m_tabItemList.Count - 1; i++)
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
        private void tabControl_TabItemClosingEvent(object sender, BaseMetroTabControl.TabItemClosingEventArgs e)
        {
            int tempIndex = tabControl.SelectedIndex;
            m_tabItemList.RemoveAt(tempIndex);
            m_VNCList[tempIndex - 2].Disconnect();
            m_VNCList.Remove(m_VNCList[tempIndex - 2]);
            tabControl.DataContext = null;
            tabControl.DataContext = m_tabItemList;
            tabControl.SelectedIndex = tempIndex - 1;
        }
    }
}

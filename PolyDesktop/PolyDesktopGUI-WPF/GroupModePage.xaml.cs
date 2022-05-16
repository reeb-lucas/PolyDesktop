using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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

namespace PolyDesktopGUI_WPF
{
    /// <summary>
    /// Interaction logic for GroupModePage.xaml
    /// </summary>
    public partial class GroupModePage : Page
    {
        private List<VncPageGroup> m_VNCList;
        private int connectedComputers;
        static string localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PolyDesktop\\Presets\\";
        DirectoryInfo di = Directory.CreateDirectory(localApplicationData); //Create directory if not exist
        string filename = System.IO.Path.Combine(localApplicationData, "Preset"); //filepath for presets with the word Prest appended to make future code easier
        private string connectionString = "server=satou.cset.oit.edu,5433; database=PolyDesktop; UID=PolyCode; password=P0lyC0d3";
        public GroupModePage(int numConnection = 2, Computer[] source = null)
        {
            InitializeComponent();

            connectedComputers = numConnection;
            m_VNCList = new List<VncPageGroup>();

            if(source != null)
            {
                DisplayComputers(connectedComputers, source);
            }
            else
            {
                DisplayComputers(connectedComputers);
            }
        }

        private void DisplayComputers(int connectedComputers, Computer[] source = null)
        {
            int count = m_VNCList.Count;
            int i = 0;
            if (connectedComputers < 2)
            {
                connectedComputers = 2;
            }
            if (connectedComputers == 2)
            {
                //column Width and Height not enumerated, should fill whole grid
                ColumnDefinition column = new ColumnDefinition();
                BaseGrid.ColumnDefinitions.Add(column);
                ColumnDefinition column2 = new ColumnDefinition();
                BaseGrid.ColumnDefinitions.Add(column2);
                //row Width and Height not enumerated, should fill whole grid
                RowDefinition row = new RowDefinition();
                BaseGrid.RowDefinitions.Add(row);

                //first connection
                VncPageGroup localSession1 = null;
                if (source == null)
                    localSession1 = new VncPageGroup(null, this);
                else
                    localSession1 = new VncPageGroup(null, this, source[i]);
                i++;
                Frame VncFrame1 = new Frame();
                Grid.SetColumn(VncFrame1, 0);
                Grid.SetRow(VncFrame1, 0);
                VncFrame1.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 2;
                VncFrame1.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
                BaseGrid.Children.Add(VncFrame1);
                VncFrame1.Navigate(localSession1);
                m_VNCList.Insert(count, localSession1);

                //second connection
                VncPageGroup localSession2 = null;
                if (source == null)
                    localSession2 = new VncPageGroup(null, this);
                else
                    localSession2 = new VncPageGroup(null, this, source[i]);
                i++;
                Frame VncFrame2 = new Frame();
                Grid.SetColumn(VncFrame2, 1);
                Grid.SetRow(VncFrame2, 0);
                VncFrame2.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 2;
                VncFrame2.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
                BaseGrid.Children.Add(VncFrame2);
                VncFrame2.Navigate(localSession2);
                m_VNCList.Insert(count, localSession2);
            }
            else if (connectedComputers == 3)
            {
                //column Width and Height not enumerated, should fill whole grid
                ColumnDefinition column = new ColumnDefinition();
                BaseGrid.ColumnDefinitions.Add(column);
                ColumnDefinition column2 = new ColumnDefinition();
                BaseGrid.ColumnDefinitions.Add(column2);
                ColumnDefinition column3 = new ColumnDefinition();
                BaseGrid.ColumnDefinitions.Add(column3);
                //row Width and Height not enumerated, should fill whole grid
                RowDefinition row = new RowDefinition();
                BaseGrid.RowDefinitions.Add(row);

                //first connection
                VncPageGroup localSession1 = null;
                if (source == null)
                    localSession1 = new VncPageGroup(null, this);
                else
                    localSession1 = new VncPageGroup(null, this, source[i]);
                i++;
                Frame VncFrame1 = new Frame();
                Grid.SetColumn(VncFrame1, 0);
                Grid.SetRow(VncFrame1, 0);
                VncFrame1.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame1.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
                BaseGrid.Children.Add(VncFrame1);
                VncFrame1.Navigate(localSession1);
                m_VNCList.Insert(count, localSession1);

                //second connection
                VncPageGroup localSession2 = null;
                if (source == null)
                    localSession2 = new VncPageGroup(null, this);
                else
                    localSession2 = new VncPageGroup(null, this, source[i]);
                i++;
                Frame VncFrame2 = new Frame();
                Grid.SetColumn(VncFrame2, 1);
                Grid.SetRow(VncFrame2, 0);
                VncFrame2.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame2.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
                BaseGrid.Children.Add(VncFrame2);
                VncFrame2.Navigate(localSession2);
                m_VNCList.Insert(count, localSession2);

                //third connection
                VncPageGroup localSession3 = null;
                if (source == null)
                    localSession3 = new VncPageGroup(null, this);
                else
                    localSession3 = new VncPageGroup(null, this, source[i]);
                i++;
                Frame VncFrame3 = new Frame();
                Grid.SetColumn(VncFrame3, 2);
                Grid.SetRow(VncFrame3, 0);
                VncFrame3.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame3.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
                BaseGrid.Children.Add(VncFrame3);
                VncFrame3.Navigate(localSession3);
                m_VNCList.Insert(count, localSession3);

            }
            else if (connectedComputers == 4)
            {
                //column Width and Height not enumerated, should fill whole grid
                ColumnDefinition column = new ColumnDefinition();
                BaseGrid.ColumnDefinitions.Add(column);
                ColumnDefinition column2 = new ColumnDefinition();
                BaseGrid.ColumnDefinitions.Add(column2);
                //row Width and Height not enumerated, should fill whole grid
                RowDefinition row = new RowDefinition();
                BaseGrid.RowDefinitions.Add(row);
                RowDefinition row2 = new RowDefinition();
                BaseGrid.RowDefinitions.Add(row2);

                //first connection
                VncPageGroup localSession1 = null;
                if (source == null)
                    localSession1 = new VncPageGroup(null, this);
                else
                    localSession1 = new VncPageGroup(null, this, source[i]);
                i++;
                Frame VncFrame1 = new Frame();
                Grid.SetColumn(VncFrame1, 0);
                Grid.SetRow(VncFrame1, 0);
                VncFrame1.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 2;
                VncFrame1.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame1);
                VncFrame1.Navigate(localSession1);
                m_VNCList.Insert(count, localSession1);

                //second connection
                VncPageGroup localSession2 = null;
                if (source == null)
                    localSession2 = new VncPageGroup(null, this);
                else
                    localSession2 = new VncPageGroup(null, this, source[i]);
                i++;
                Frame VncFrame2 = new Frame();
                Grid.SetColumn(VncFrame2, 1);
                Grid.SetRow(VncFrame2, 0);
                VncFrame2.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 2;
                VncFrame2.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame2);
                VncFrame2.Navigate(localSession2);
                m_VNCList.Insert(count, localSession2);

                //third connection
                VncPageGroup localSession3 = null;
                if (source == null)
                    localSession3 = new VncPageGroup(null, this);
                else
                    localSession3 = new VncPageGroup(null, this, source[i]);
                i++;
                Frame VncFrame3 = new Frame();
                Grid.SetColumn(VncFrame3, 0);
                Grid.SetRow(VncFrame3, 1);
                VncFrame3.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 2;
                VncFrame3.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame3);
                VncFrame3.Navigate(localSession3);
                m_VNCList.Insert(count, localSession3);

                //fourth connection
                VncPageGroup localSession4 = null;
                if (source == null)
                    localSession4 = new VncPageGroup(null, this);
                else
                    localSession4 = new VncPageGroup(null, this, source[i]);
                i++;
                Frame VncFrame4 = new Frame();
                Grid.SetColumn(VncFrame4, 1);
                Grid.SetRow(VncFrame4, 1);
                VncFrame4.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 2;
                VncFrame4.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame4);
                VncFrame4.Navigate(localSession4);
                m_VNCList.Insert(count, localSession4);
            }
            else if (connectedComputers == 5)
            {
                //column Width and Height not enumerated, should fill whole grid
                ColumnDefinition column = new ColumnDefinition();
                BaseGrid.ColumnDefinitions.Add(column);
                ColumnDefinition column2 = new ColumnDefinition();
                BaseGrid.ColumnDefinitions.Add(column2);
                ColumnDefinition column3 = new ColumnDefinition();
                BaseGrid.ColumnDefinitions.Add(column3);
                //row Width and Height not enumerated, should fill whole grid
                RowDefinition row = new RowDefinition();
                BaseGrid.RowDefinitions.Add(row);
                RowDefinition row2 = new RowDefinition();
                BaseGrid.RowDefinitions.Add(row2);

                //first connection
                VncPageGroup localSession1 = null;
                if (source == null)
                    localSession1 = new VncPageGroup(null, this);
                else
                    localSession1 = new VncPageGroup(null, this, source[i]);
                i++;
                Frame VncFrame1 = new Frame();
                Grid.SetColumn(VncFrame1, 0);
                Grid.SetRow(VncFrame1, 0);
                VncFrame1.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame1.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame1);
                VncFrame1.Navigate(localSession1);
                m_VNCList.Insert(count, localSession1);

                //second connection
                VncPageGroup localSession2 = null;
                if (source == null)
                    localSession2 = new VncPageGroup(null, this);
                else
                    localSession2 = new VncPageGroup(null, this, source[i]);
                i++;
                Frame VncFrame2 = new Frame();
                Grid.SetColumn(VncFrame2, 1);
                Grid.SetRow(VncFrame2, 0);
                VncFrame2.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame2.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame2);
                VncFrame2.Navigate(localSession2);
                m_VNCList.Insert(count, localSession2);

                //third connection
                VncPageGroup localSession3 = null;
                if (source == null)
                    localSession3 = new VncPageGroup(null, this);
                else
                    localSession3 = new VncPageGroup(null, this, source[i]);
                i++;
                Frame VncFrame3 = new Frame();
                Grid.SetColumn(VncFrame3, 2);
                Grid.SetRow(VncFrame3, 0);
                VncFrame3.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame3.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame3);
                VncFrame3.Navigate(localSession3);
                m_VNCList.Insert(count, localSession3);

                //fourth connection
                VncPageGroup localSession4 = null;
                if (source == null)
                    localSession4 = new VncPageGroup(null, this);
                else
                    localSession4 = new VncPageGroup(null, this, source[i]);
                i++;
                Frame VncFrame4 = new Frame();
                Grid.SetColumn(VncFrame4, 0);
                Grid.SetRow(VncFrame4, 1);
                VncFrame4.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame4.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame4);
                VncFrame4.Navigate(localSession4);
                m_VNCList.Insert(count, localSession4);

                //fifth connection
                VncPageGroup localSession5 = null;
                if (source == null)
                    localSession5 = new VncPageGroup(null, this);
                else
                    localSession5 = new VncPageGroup(null, this, source[i]);
                i++;
                Frame VncFrame5 = new Frame();
                Grid.SetColumn(VncFrame5, 1);
                Grid.SetRow(VncFrame5, 1);
                VncFrame5.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame5.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame5);
                VncFrame5.Navigate(localSession5);
                m_VNCList.Insert(count, localSession5);
            }
            else if(connectedComputers == 6)
            {
                //column Width and Height not enumerated, should fill whole grid
                ColumnDefinition column = new ColumnDefinition();
                BaseGrid.ColumnDefinitions.Add(column);
                ColumnDefinition column2 = new ColumnDefinition();
                BaseGrid.ColumnDefinitions.Add(column2);
                ColumnDefinition column3 = new ColumnDefinition();
                BaseGrid.ColumnDefinitions.Add(column3);
                //row Width and Height not enumerated, should fill whole grid
                RowDefinition row = new RowDefinition();
                BaseGrid.RowDefinitions.Add(row);
                RowDefinition row2 = new RowDefinition();
                BaseGrid.RowDefinitions.Add(row2);

                //first connection
                VncPageGroup localSession1 = null;
                if (source == null)
                    localSession1 = new VncPageGroup(null, this);
                else
                    localSession1 = new VncPageGroup(null, this, source[i]);
                i++;
                Frame VncFrame1 = new Frame();
                Grid.SetColumn(VncFrame1, 0);
                Grid.SetRow(VncFrame1, 0);
                VncFrame1.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame1.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame1);
                VncFrame1.Navigate(localSession1);
                m_VNCList.Insert(count, localSession1);

                //second connection
                VncPageGroup localSession2 = null;
                if (source == null)
                    localSession2 = new VncPageGroup(null, this);
                else
                    localSession2 = new VncPageGroup(null, this, source[i]);
                i++;
                Frame VncFrame2 = new Frame();
                Grid.SetColumn(VncFrame2, 1);
                Grid.SetRow(VncFrame2, 0);
                VncFrame2.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame2.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame2);
                VncFrame2.Navigate(localSession2);
                m_VNCList.Insert(count, localSession2);

                //third connection
                VncPageGroup localSession3 = null;
                if (source == null)
                    localSession3 = new VncPageGroup(null, this);
                else
                    localSession3 = new VncPageGroup(null, this, source[i]);
                i++;
                Frame VncFrame3 = new Frame();
                Grid.SetColumn(VncFrame3, 2);
                Grid.SetRow(VncFrame3, 0);
                VncFrame3.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame3.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame3);
                VncFrame3.Navigate(localSession3);
                m_VNCList.Insert(count, localSession3);

                //fourth connection
                VncPageGroup localSession4 = null;
                if (source == null)
                    localSession4 = new VncPageGroup(null, this);
                else
                    localSession4 = new VncPageGroup(null, this, source[i]);
                i++;
                Frame VncFrame4 = new Frame();
                Grid.SetColumn(VncFrame4, 0);
                Grid.SetRow(VncFrame4, 1);
                VncFrame4.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame4.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame4);
                VncFrame4.Navigate(localSession4);
                m_VNCList.Insert(count, localSession4);

                //fifth connection
                VncPageGroup localSession5 = null;
                if (source == null)
                    localSession5 = new VncPageGroup(null, this);
                else
                    localSession5 = new VncPageGroup(null, this, source[i]);
                i++;
                Frame VncFrame5 = new Frame();
                Grid.SetColumn(VncFrame5, 1);
                Grid.SetRow(VncFrame5, 1);
                VncFrame5.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame5.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame5);
                VncFrame5.Navigate(localSession5);
                m_VNCList.Insert(count, localSession5);

                //sixth connection
                VncPageGroup localSession6 = null;
                if (source == null)
                    localSession6 = new VncPageGroup(null, this);
                else
                    localSession6 = new VncPageGroup(null, this, source[i]);
                i++;
                Frame VncFrame6 = new Frame();
                Grid.SetColumn(VncFrame6, 2);
                Grid.SetRow(VncFrame6, 1);
                VncFrame6.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame6.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame6);
                VncFrame6.Navigate(localSession6);
                m_VNCList.Insert(count, localSession6);

                AddButton.IsEnabled = false;
            }
            else //If there is an abnormal # of connections, default to 2
            {
                DisplayComputers(2);
            }
        }
        private void AddConnection(object sender, RoutedEventArgs e) //TODO: FIX
        {
            NavigationService.Navigate(new GroupModePage(connectedComputers + 1)); //Group
        }
        private void RemoveConnection(object sender, RoutedEventArgs e) //TODO: Fix
        {
            NavigationService.Navigate(new GroupModePage(connectedComputers - 1)); //Group
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
            for (int i = 0; i < m_VNCList.Count; i++)
            {
                m_VNCList[i].Disconnect();
            }
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
            string bucket = PresetNameBox.Text.Replace(",", "") + ",Group," + connectedComputers + ",";
            for (int i = 0; i < connectedComputers; i++)
            {
                string ID = "";
                using (var connection = new SqlConnection(connectionString))
                {
                    string sql = "SELECT c_ID FROM PolyDesktop.dbo.desktop WHERE c_name = \'" + m_VNCList[i].GetConnectedName() + "\'";
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
                bucket += m_VNCList[i].ComputerNameBox.Text.Replace(",", "");
                bucket += ",";
            }
            bucket = bucket.TrimEnd(',');
            File.WriteAllText(filename + Directory.GetFiles(localApplicationData).Length + ".txt", bucket);
            PresetFlyout.IsOpen = false;
        }
    }
}
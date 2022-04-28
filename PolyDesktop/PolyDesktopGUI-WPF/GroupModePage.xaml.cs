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

namespace PolyDesktopGUI_WPF
{
    /// <summary>
    /// Interaction logic for GroupModePage.xaml
    /// </summary>
    public partial class GroupModePage : Page
    {
        private List<VncPageGroup> m_VNCList;
        private int connectedComputers;
        private GroupModePage[] pages;
        public GroupModePage(int numConnection = 1)
        {
            InitializeComponent();
            //TODO: have user choose computers to connect to, this will likely change VncPage creation in DisplayComputers
            //temp hard coding for testing
            connectedComputers = numConnection;
            m_VNCList = new List<VncPageGroup>();

            DisplayComputers(connectedComputers);
        }

        private void DisplayComputers(int connectedComputers, List<VncPageGroup> vncList = null, string[] nickNames = null)
        {
            int count = m_VNCList.Count;
            if (connectedComputers < 1)
            {
                connectedComputers = 1;
            }
            if (connectedComputers == 1)
            {
                //column Width and Height not enumerated, should fill whole grid
                ColumnDefinition column = new ColumnDefinition();              
                BaseGrid.ColumnDefinitions.Add(column);
                //row Width and Height not enumerated, should fill whole grid
                RowDefinition row = new RowDefinition();              
                BaseGrid.RowDefinitions.Add(row);

                VncPageGroup localSession = new VncPageGroup(null, this);
                Frame VncFrame = new Frame();
                Grid.SetColumn(VncFrame, 0);
                Grid.SetRow(VncFrame, 0);
                BaseGrid.Children.Add(VncFrame);
                VncFrame.Navigate(localSession);
                m_VNCList.Insert(count, localSession);
            }
            else if (connectedComputers == 2)
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
                VncPageGroup localSession1 = new VncPageGroup(null, this);
                Frame VncFrame1 = new Frame();
                Grid.SetColumn(VncFrame1, 0);
                Grid.SetRow(VncFrame1, 0);
                VncFrame1.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 2;
                VncFrame1.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
                BaseGrid.Children.Add(VncFrame1);
                VncFrame1.Navigate(localSession1);
                m_VNCList.Insert(count, localSession1);

                //second connection
                VncPageGroup localSession2 = new VncPageGroup(null, this);
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
                VncPageGroup localSession1 = new VncPageGroup(null, this);
                Frame VncFrame1 = new Frame();
                Grid.SetColumn(VncFrame1, 0);
                Grid.SetRow(VncFrame1, 0);
                VncFrame1.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame1.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
                BaseGrid.Children.Add(VncFrame1);
                VncFrame1.Navigate(localSession1);
                m_VNCList.Insert(count, localSession1);

                //second connection
                VncPageGroup localSession2 = new VncPageGroup(null, this);
                Frame VncFrame2 = new Frame();
                Grid.SetColumn(VncFrame2, 1);
                Grid.SetRow(VncFrame2, 0);
                VncFrame2.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame2.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
                BaseGrid.Children.Add(VncFrame2);
                VncFrame2.Navigate(localSession2);
                m_VNCList.Insert(count, localSession2);

                //third connection
                VncPageGroup localSession3 = new VncPageGroup(null, this);
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
                VncPageGroup localSession1 = new VncPageGroup(null, this);
                Frame VncFrame1 = new Frame();
                Grid.SetColumn(VncFrame1, 0);
                Grid.SetRow(VncFrame1, 0);
                VncFrame1.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 2;
                VncFrame1.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame1);
                VncFrame1.Navigate(localSession1);
                m_VNCList.Insert(count, localSession1);

                //second connection
                VncPageGroup localSession2 = new VncPageGroup(null, this);
                Frame VncFrame2 = new Frame();
                Grid.SetColumn(VncFrame2, 1);
                Grid.SetRow(VncFrame2, 0);
                VncFrame2.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 2;
                VncFrame2.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame2);
                VncFrame2.Navigate(localSession2);
                m_VNCList.Insert(count, localSession2);

                //third connection
                VncPageGroup localSession3 = new VncPageGroup(null, this);
                Frame VncFrame3 = new Frame();
                Grid.SetColumn(VncFrame3, 0);
                Grid.SetRow(VncFrame3, 1);
                VncFrame3.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 2;
                VncFrame3.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame3);
                VncFrame3.Navigate(localSession3);
                m_VNCList.Insert(count, localSession3);

                //fourth connection
                VncPageGroup localSession4 = new VncPageGroup(null, this);
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
                VncPageGroup localSession1 = new VncPageGroup(null, this);
                Frame VncFrame1 = new Frame();
                Grid.SetColumn(VncFrame1, 0);
                Grid.SetRow(VncFrame1, 0);
                VncFrame1.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame1.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame1);
                VncFrame1.Navigate(localSession1);
                m_VNCList.Insert(count, localSession1);

                //second connection
                VncPageGroup localSession2 = new VncPageGroup(null, this);
                Frame VncFrame2 = new Frame();
                Grid.SetColumn(VncFrame2, 1);
                Grid.SetRow(VncFrame2, 0);
                VncFrame2.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame2.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame2);
                VncFrame2.Navigate(localSession2);
                m_VNCList.Insert(count, localSession2);

                //third connection
                VncPageGroup localSession3 = new VncPageGroup(null, this);
                Frame VncFrame3 = new Frame();
                Grid.SetColumn(VncFrame3, 2);
                Grid.SetRow(VncFrame3, 0);
                VncFrame3.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame3.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame3);
                VncFrame3.Navigate(localSession3);
                m_VNCList.Insert(count, localSession3);

                //fourth connection
                VncPageGroup localSession4 = new VncPageGroup(null, this);
                Frame VncFrame4 = new Frame();
                Grid.SetColumn(VncFrame4, 0);
                Grid.SetRow(VncFrame4, 1);
                VncFrame4.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame4.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame4);
                VncFrame4.Navigate(localSession4);
                m_VNCList.Insert(count, localSession4);

                //fifth connection
                VncPageGroup localSession5 = new VncPageGroup(null, this);
                Frame VncFrame5 = new Frame();
                Grid.SetColumn(VncFrame5, 1);
                Grid.SetRow(VncFrame5, 1);
                VncFrame5.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame5.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame5);
                VncFrame4.Navigate(localSession5);
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
                VncPageGroup localSession1 = new VncPageGroup(null, this);
                Frame VncFrame1 = new Frame();
                Grid.SetColumn(VncFrame1, 0);
                Grid.SetRow(VncFrame1, 0);
                VncFrame1.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame1.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame1);
                VncFrame1.Navigate(localSession1);
                m_VNCList.Insert(count, localSession1);

                //second connection
                VncPageGroup localSession2 = new VncPageGroup(null, this);
                Frame VncFrame2 = new Frame();
                Grid.SetColumn(VncFrame2, 1);
                Grid.SetRow(VncFrame2, 0);
                VncFrame2.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame2.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame2);
                VncFrame2.Navigate(localSession2);
                m_VNCList.Insert(count, localSession2);

                //third connection
                VncPageGroup localSession3 = new VncPageGroup(null, this);
                Frame VncFrame3 = new Frame();
                Grid.SetColumn(VncFrame3, 2);
                Grid.SetRow(VncFrame3, 0);
                VncFrame3.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame3.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame3);
                VncFrame3.Navigate(localSession3);
                m_VNCList.Insert(count, localSession3);

                //fourth connection
                VncPageGroup localSession4 = new VncPageGroup(null, this);
                Frame VncFrame4 = new Frame();
                Grid.SetColumn(VncFrame4, 0);
                Grid.SetRow(VncFrame4, 1);
                VncFrame4.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame4.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame4);
                VncFrame4.Navigate(localSession4);
                m_VNCList.Insert(count, localSession4);

                //fifth connection
                VncPageGroup localSession5 = new VncPageGroup(null, this);
                Frame VncFrame5 = new Frame();
                Grid.SetColumn(VncFrame5, 1);
                Grid.SetRow(VncFrame5, 1);
                VncFrame5.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame5.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame5);
                VncFrame5.Navigate(localSession5);
                m_VNCList.Insert(count, localSession5);

                //sixth connection
                VncPageGroup localSession6 = new VncPageGroup(null, this);
                Frame VncFrame6 = new Frame();
                Grid.SetColumn(VncFrame6, 2);
                Grid.SetRow(VncFrame6, 1);
                VncFrame6.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 3;
                VncFrame6.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                BaseGrid.Children.Add(VncFrame6);
                VncFrame6.Navigate(localSession6);
                m_VNCList.Insert(count, localSession6);

            }
            else if (connectedComputers >= 6)
            {
                
                for (int i = 0; i <= connectedComputers / 6; ++i)
                {
                    pages[i] = new GroupModePage();
                    NavigationService.Navigate(pages[i]);
                }
                int remainderComputers = connectedComputers % 6;
                if (remainderComputers > 0)
                    DisplayComputers(remainderComputers);
            }
            else //If there are no connectedComputers, give user ability to add one through a VncFrame
            {
                DisplayComputers(1);
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

        private void PrevPageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
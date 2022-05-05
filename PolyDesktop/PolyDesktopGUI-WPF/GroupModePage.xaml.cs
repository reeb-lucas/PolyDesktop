﻿using System;
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
        private List<VncPage> m_VNCList;
        private int connectedComputers;
        public GroupModePage(int numConnection = 1)
        {
            InitializeComponent();
            //TODO: have user choose computers to connect to, this will likely change VncPage creation in DisplayComputers
            //temp hard coding for testing
            connectedComputers = numConnection;
            m_VNCList = new List<VncPage>();

            DisplayComputers(connectedComputers);
        }

        private void DisplayComputers(int connectedComputers)
        {
            int count = m_VNCList.Count;
            if (connectedComputers == 1)
            {
                //column Width and Height not enumerated, should fill whole grid
                ColumnDefinition column = new ColumnDefinition();              
                BaseGrid.ColumnDefinitions.Add(column);
                //row Width and Height not enumerated, should fill whole grid
                RowDefinition row = new RowDefinition();              
                BaseGrid.RowDefinitions.Add(row);
                
                VncPage localSession = new VncPage();
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
                VncPage localSession1 = new VncPage();
                Frame VncFrame1 = new Frame();
                Grid.SetColumn(VncFrame1, 0);
                Grid.SetRow(VncFrame1, 0);
                BaseGrid.Children.Add(VncFrame1);
                VncFrame1.Navigate(localSession1);
                m_VNCList.Insert(count, localSession1);

                //second connection
                VncPage localSession2 = new VncPage();
                Frame VncFrame2 = new Frame();
                Grid.SetColumn(VncFrame2, 1);
                Grid.SetRow(VncFrame2, 0);
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
                VncPage localSession1 = new VncPage();
                Frame VncFrame1 = new Frame();
                Grid.SetColumn(VncFrame1, 0);
                Grid.SetRow(VncFrame1, 0);
                BaseGrid.Children.Add(VncFrame1);
                VncFrame1.Navigate(localSession1);
                m_VNCList.Insert(count, localSession1);

                //second connection
                VncPage localSession2 = new VncPage();
                Frame VncFrame2 = new Frame();
                Grid.SetColumn(VncFrame2, 1);
                Grid.SetRow(VncFrame2, 0);
                BaseGrid.Children.Add(VncFrame2);
                VncFrame2.Navigate(localSession2);
                m_VNCList.Insert(count, localSession2);

                //third connection
                VncPage localSession3 = new VncPage();
                Frame VncFrame3 = new Frame();
                Grid.SetColumn(VncFrame3, 2);
                Grid.SetRow(VncFrame3, 0);
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
                VncPage localSession1 = new VncPage();
                Frame VncFrame1 = new Frame();
                Grid.SetColumn(VncFrame1, 0);
                Grid.SetRow(VncFrame1, 0);
                BaseGrid.Children.Add(VncFrame1);
                VncFrame1.Navigate(localSession1);
                m_VNCList.Insert(count, localSession1);

                //second connection
                VncPage localSession2 = new VncPage();
                Frame VncFrame2 = new Frame();
                Grid.SetColumn(VncFrame2, 1);
                Grid.SetRow(VncFrame2, 0);
                BaseGrid.Children.Add(VncFrame2);
                VncFrame2.Navigate(localSession2);
                m_VNCList.Insert(count, localSession2);

                //third connection
                VncPage localSession3 = new VncPage();
                Frame VncFrame3 = new Frame();
                Grid.SetColumn(VncFrame3, 0);
                Grid.SetRow(VncFrame3, 1);
                BaseGrid.Children.Add(VncFrame3);
                VncFrame3.Navigate(localSession3);
                m_VNCList.Insert(count, localSession3);

                //fourth connection
                VncPage localSession4 = new VncPage();
                Frame VncFrame4 = new Frame();
                Grid.SetColumn(VncFrame4, 1);
                Grid.SetRow(VncFrame4, 1);
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
                VncPage localSession1 = new VncPage();
                Frame VncFrame1 = new Frame();
                Grid.SetColumn(VncFrame1, 0);
                Grid.SetRow(VncFrame1, 0);
                BaseGrid.Children.Add(VncFrame1);
                VncFrame1.Navigate(localSession1);
                m_VNCList.Insert(count, localSession1);

                //second connection
                VncPage localSession2 = new VncPage();
                Frame VncFrame2 = new Frame();
                Grid.SetColumn(VncFrame2, 1);
                Grid.SetRow(VncFrame2, 0);
                BaseGrid.Children.Add(VncFrame2);
                VncFrame2.Navigate(localSession2);
                m_VNCList.Insert(count, localSession2);

                //third connection
                VncPage localSession3 = new VncPage();
                Frame VncFrame3 = new Frame();
                Grid.SetColumn(VncFrame3, 2);
                Grid.SetRow(VncFrame3, 0);
                BaseGrid.Children.Add(VncFrame3);
                VncFrame3.Navigate(localSession3);
                m_VNCList.Insert(count, localSession3);

                //fourth connection
                VncPage localSession4 = new VncPage();
                Frame VncFrame4 = new Frame();
                Grid.SetColumn(VncFrame4, 0);
                Grid.SetRow(VncFrame4, 1);
                BaseGrid.Children.Add(VncFrame4);
                VncFrame4.Navigate(localSession4);
                m_VNCList.Insert(count, localSession4);

                //fifth connection
                VncPage localSession5 = new VncPage();
                Frame VncFrame5 = new Frame();
                Grid.SetColumn(VncFrame5, 1);
                Grid.SetRow(VncFrame5, 1);
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
                VncPage localSession1 = new VncPage();
                Frame VncFrame1 = new Frame();
                Grid.SetColumn(VncFrame1, 0);
                Grid.SetRow(VncFrame1, 0);
                BaseGrid.Children.Add(VncFrame1);
                VncFrame1.Navigate(localSession1);
                m_VNCList.Insert(count, localSession1);

                //second connection
                VncPage localSession2 = new VncPage();
                Frame VncFrame2 = new Frame();
                Grid.SetColumn(VncFrame2, 1);
                Grid.SetRow(VncFrame2, 0);
                BaseGrid.Children.Add(VncFrame2);
                VncFrame2.Navigate(localSession2);
                m_VNCList.Insert(count, localSession2);

                //third connection
                VncPage localSession3 = new VncPage();
                Frame VncFrame3 = new Frame();
                Grid.SetColumn(VncFrame3, 2);
                Grid.SetRow(VncFrame3, 0);
                BaseGrid.Children.Add(VncFrame3);
                VncFrame3.Navigate(localSession3);
                m_VNCList.Insert(count, localSession3);

                //fourth connection
                VncPage localSession4 = new VncPage();
                Frame VncFrame4 = new Frame();
                Grid.SetColumn(VncFrame4, 0);
                Grid.SetRow(VncFrame4, 1);
                BaseGrid.Children.Add(VncFrame4);
                VncFrame4.Navigate(localSession4);
                m_VNCList.Insert(count, localSession4);

                //fifth connection
                VncPage localSession5 = new VncPage();
                Frame VncFrame5 = new Frame();
                Grid.SetColumn(VncFrame5, 1);
                Grid.SetRow(VncFrame5, 1);
                BaseGrid.Children.Add(VncFrame5);
                VncFrame5.Navigate(localSession5);
                m_VNCList.Insert(count, localSession5);

                //sixth connection
                VncPage localSession6 = new VncPage();
                Frame VncFrame6 = new Frame();
                Grid.SetColumn(VncFrame6, 2);
                Grid.SetRow(VncFrame6, 1);
                BaseGrid.Children.Add(VncFrame6);
                VncFrame6.Navigate(localSession6);
                m_VNCList.Insert(count, localSession6);

            }
            else if (connectedComputers >= 6)
            {
                Array pages = new Array[1];
                for (int i = 0; i <= connectedComputers / 6; i++)
                {
                    //TODO: do i pages of 6 computers in a 3 x 3, add arrows to these pages
                    //column Width and Height not enumerated, should fill whole grid
                    //EXCEPTION Width of Prev Button and Next Button set as not necessacary to
                    //be uniform.
                    ColumnDefinition prevButtonColumn = new ColumnDefinition();
                    prevButtonColumn.SetValue(WidthProperty, 25);
                    BaseGrid.ColumnDefinitions.Add(prevButtonColumn);
                    ColumnDefinition column = new ColumnDefinition();
                    BaseGrid.ColumnDefinitions.Add(column);
                    ColumnDefinition column2 = new ColumnDefinition();
                    BaseGrid.ColumnDefinitions.Add(column2);
                    ColumnDefinition column3 = new ColumnDefinition();
                    BaseGrid.ColumnDefinitions.Add(column3);
                    ColumnDefinition nextButtonColumn = new ColumnDefinition();
                    nextButtonColumn.SetValue(WidthProperty, 25);
                    BaseGrid.ColumnDefinitions.Add(nextButtonColumn);
                    //row Width and Height not enumerated, should fill whole grid
                    RowDefinition row = new RowDefinition();
                    BaseGrid.RowDefinitions.Add(row);
                    RowDefinition row2 = new RowDefinition();
                    BaseGrid.RowDefinitions.Add(row2);
                    //TODO: index pages by i for arrow navigation
                    //TODO: Arrow click event calls OverSixConnectedNav to flip pages
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

        /// <summary>
        /// Used to navigate between pages when over six computers are connected.
        /// Is passed current page and direction to navigate through pages
        /// </summary>
        /// <param name="currentPageNum">Page that arrow was clicked on</param>
        /// <param name="direction">0 if left arrow, 1 if right arrow</param>
        private void OverSixConnectedNav(int currentPageNum, int direction)
        {
            //TODO: change pages in this function
        }
        private void AddConnection(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GroupModePage(connectedComputers + 1)); //Group
        }
        private void RemoveConnection(object sender, RoutedEventArgs e)
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
    }
}
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
        private List<VncPage> m_VNCList;
        private int connectedComputers;
        public GroupModePage()
        {
            InitializeComponent();
            //TODO: have user choose computers to connect to, this will likely change VncPage creation in DisplayComputers
            //temp hard coding for testing
            connectedComputers = 1;
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
                //side by side
            }
            else if (connectedComputers == 3)
            {
                //side by side out to 3
            }
            else if (connectedComputers == 4)
            {
                //2 x 2
            }
            else if (connectedComputers == 5)
            {
                //3 on top 2 on bottom
            }
            else if (connectedComputers >= 6)
            {
                for (int i = 0; i <= connectedComputers / 6; i++)
                {
                    //do multiple pages of 6 computers in a 3 x 2, can flip through with arrows
                }
                int remainderComputers = connectedComputers % 6;
                if (remainderComputers > 0)
                    DisplayComputers(remainderComputers);
            }
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
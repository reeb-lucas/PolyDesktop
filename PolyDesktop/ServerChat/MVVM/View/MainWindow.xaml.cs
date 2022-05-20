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
using System.Windows.Shapes;

namespace ServerChat.MVVM.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UsernameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UsernameHint.Visibility = Visibility.Visible;
            if (UsernameBox.Text.Length > 0)
            {
                UsernameHint.Visibility = Visibility.Hidden;
            }
        }

        private void ServerAddressBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ServerAddressHint.Visibility = Visibility.Visible;
            if (ServerAddressBox.Text.Length > 0)
            {
                ServerAddressHint.Visibility = Visibility.Hidden;
            }
        }

        private void ServerPortBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ServerPortHint.Visibility = Visibility.Visible;
            if (ServerPortBox.Text.Length > 0)
            {
                ServerPortHint.Visibility = Visibility.Hidden;
            }
        }

        private void MessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            //Send the message in textbox if "Enter" key is pressed, and the textbox is not empty.
            if (e.Key == Key.Return && MessageBox.Text != "")
            {
                SendButton.Command.Execute(SendButton.Content);
                MessageBox.Text = ""; //Clear Content
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendButton.Command.Execute(SendButton.Content);
            MessageBox.Text = ""; //Clear Content
        }

        private void ChangeListButton_Click(object sender, RoutedEventArgs e)
        {
            //Change content of button, then change visibility of listview objects
            if (ChangeListButton.Content.ToString() == "Show Help Queue")
            {
                ChangeListButton.Content = "Show Connected Users";
                ConnectedUsers.Visibility = Visibility.Collapsed;
                HelpQueueUsers.Visibility = Visibility.Visible;
            }
            else if (ChangeListButton.Content.ToString() == "Show Connected Users")
            {
                ChangeListButton.Content = "Show Help Queue";
                HelpQueueUsers.Visibility = Visibility.Collapsed;
                ConnectedUsers.Visibility = Visibility.Visible;
            }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            //Make connected users visible after user is connected
            //(This assumes connection works on the first click)
            ConnectedUsers.Visibility = Visibility.Visible;
        }
    }
}

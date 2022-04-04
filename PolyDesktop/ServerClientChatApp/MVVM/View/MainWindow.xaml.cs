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

namespace ServerClientChatApp
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
    }
}

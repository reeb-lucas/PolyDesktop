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
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            if ((bool)(App.Current.Properties["AdvancedMode"]) == true)
            {
                AdvancedSwitch.IsOn = true;
            }
            else
            {
                AdvancedSwitch.IsOn = false;
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
        }
        private void AdvancedSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if(AdvancedSwitch.IsOn)
            {
                App.Current.Properties["AdvancedMode"] = true;
            }
            else
            {
                App.Current.Properties["AdvancedMode"] = false;
            }
        }
    }
}

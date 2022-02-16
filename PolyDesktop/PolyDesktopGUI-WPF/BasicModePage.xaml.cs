using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
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
using OmotVnc;
using OmotVnc.View.ViewModel;
using PollRobots.OmotVnc.Controls;

namespace PolyDesktopGUI_WPF
{
    /// <summary>
    /// Interaction logic for BasicModePage.xaml
    /// </summary>
    public partial class BasicModePage : Page
    {
        VncPage _child;
        public BasicModePage()
        {
            InitializeComponent();

            ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithAppMode;
            ThemeManager.Current.SyncTheme();
            _child = new VncPage();
            VncFrame.Navigate(_child);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
            //_child.Disconnect();
        }
    }
}

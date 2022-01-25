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
    /// Interaction logic for BasicModePage.xaml
    /// </summary>
    public partial class BasicModePage : Page
    {
        public BasicModePage()
        {
            InitializeComponent();

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var MW = new BasicModePage();
            MW.NavigationService.Navigate("MainPage.xaml");
        }
    }
}

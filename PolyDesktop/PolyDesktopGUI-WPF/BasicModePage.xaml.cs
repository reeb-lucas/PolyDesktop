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
        private int _scale;
        private bool _scaleToFit;
        private bool _useLocalCursor;

        private ConnectionDialog connectionDialog;

        private RelayCommand _setScaleCommand;
        private RelayCommand _setScaleToFitCommand;
        private RelayCommand _refreshCommand;
        private RelayCommand _connectCommand;
        private RelayCommand _disconnectCommand;
        private RelayCommand _toggleLocalCursorCommand;
        public BasicModePage()
        {
            InitializeCommands();

            InitializeComponent();

            ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithAppMode;
            ThemeManager.Current.SyncTheme();

            DataContext = this;

            SearchListBox.ItemsSource = GatherAllComputers();
        }

        private async void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
            await VncHost.DisconnectAsync();
        }
        public Computer[] AllComputers { get { return GatherAllComputers(); } }
        public Computer[] GatherAllComputers(string searchTerm = null) //returns up to 5 computers in an observable array to populate listview
        {
            string connectionString = "server=satou.cset.oit.edu,5433; database=PolyDesktop; UID=PolyCode; password=P0lyC0d3";
            Computer[] container = new Computer[5];

            using (var connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT c_ID, c_name FROM PolyDesktop.dbo.desktop";
                if (searchTerm != null)
                {
                    sql = "SELECT c_ID, c_name FROM PolyDesktop.dbo.desktop WHERE c_name LIKE'%" + searchTerm + "%' OR c_ID LIKE '%" + searchTerm + "%'";
                }
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        for (int i = 0; i < 5 && reader.Read(); i++) //only returns a max of 15 results
                        {
                            Computer temp = new Computer();
                            temp.ID = reader.GetInt32(0).ToString();
                            temp.Name = reader.GetString(1); //UUUUUUHHHHHHH, I can't get more than the first row
                            temp.Nickname = temp.Name;
                            container[i] = temp;
                        }
                        reader.Close();
                    }
                }
            }
            return container;
        }
        private void search_QueryChanged(object sender, TextChangedEventArgs e)
        {
            SearchListBox.ItemsSource = GatherAllComputers(SearchBox.Text);
        }
        private async void SearchListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) //Adding computer to preset with default nickname being the computer name
        {
            ComputerPanel.Visibility = Visibility.Hidden;
            //start Connection
            await VncHost.ConnectAsync(SearchListBox.SelectedValue.ToString(), 5901, "1234"); //TODO: PW CHANGE
            await Task.Delay(150);
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
        }
        private void InitializeCommands()
        {
            _setScaleCommand = new RelayCommand((param) =>
            {
                int scale;

                if (param != null && int.TryParse(param.ToString(), out scale))
                {
                    Scale = scale;
                }
                else
                {
                    //TODO: show message box?
                }
            });

            _setScaleToFitCommand = new RelayCommand((param) =>
            {
                ScaleToFit = !ScaleToFit;
            });

            _refreshCommand = new RelayCommand(async (param) =>
            {
                await VncHost.UpdateAsync();
            });

            _connectCommand = new RelayCommand(async (param) =>
            {
                var server = string.Empty;
                var port = 5900;
                var password = string.Empty;

                if (connectionDialog != null)
                {
                    server = connectionDialog.Server;
                    port = connectionDialog.Port;
                    password = connectionDialog.Password;
                }

                connectionDialog = new ConnectionDialog
                {
                    Server = server,
                    Port = port,
                    Password = password
                };

                var dialog = connectionDialog;

                if (false == dialog.ShowDialog())
                {
                    return;
                }

                await VncHost.ConnectAsync(dialog.Server, dialog.Port, dialog.Password);
            });

            _disconnectCommand = new RelayCommand(async (param) =>
            {
                await VncHost.DisconnectAsync();
            });

            _toggleLocalCursorCommand = new RelayCommand((param) =>
            {
                UseLocalCursor = !UseLocalCursor;
            });
        }

        /// <summary>
        /// Gets the command that sets the scale.
        /// </summary>
        public ICommand SetScaleCommand
        {
            get
            {
                return _setScaleCommand;
            }
        }

        /// <summary>
        /// Gets the command that toggles the scale to fit the window.
        /// </summary>
        public ICommand SetScaleToFitCommand
        {
            get
            {
                return _setScaleToFitCommand;
            }
        }

        /// <summary>
        /// Gets the command that refreshes the VNC host.
        /// </summary>
        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand;
            }
        }

        /// <summary>
        /// Gets the command that toggles the local cursor.
        /// </summary>
        public ICommand ToggleLocalCursorCommand
        {
            get
            {
                return _toggleLocalCursorCommand;
            }
        }

        /// <summary>
        /// Gets the command that connects to the VNC host.
        /// </summary>
        public ICommand ConnectCommand
        {
            get
            {
                return _connectCommand;
            }
        }

        /// <summary>
        /// Gets the command that disconnects the VNC host.
        /// </summary>
        public ICommand DisconnectCommand
        {
            get
            {
                return _disconnectCommand;
            }
        }

        /// <summary>
        /// Gets or sets the scale in percentage.
        /// </summary>
        public int Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a flag that indicates if the scale should fit the window.
        /// </summary>
        public bool ScaleToFit
        {
            get
            {
                return _scaleToFit;
            }
            set
            {
                _scaleToFit = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets a bool property that indicates if the local cursor should be enabled.
        /// </summary>
        public bool UseLocalCursor
        {
            get
            {
                return _useLocalCursor;
            }
            set
            {
                _useLocalCursor = value;
                RaisePropertyChanged();
            }
        }

        ///// <summary>Handles the window being closed.</summary>
        ///// <param name="e">The parameter is not used.</param>
        //protected override void OnClosed(EventArgs e)
        //{
        //    VncHost.DisconnectAsync();
        //
        //    base.OnClosed(e);
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            var propertyChangedEventHandler = PropertyChanged;

            if (propertyChangedEventHandler != null)
            {
                propertyChangedEventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

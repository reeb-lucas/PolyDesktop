using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using OmotVnc;
using OmotVnc.View.ViewModel;

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

            DataContext = this;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
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

using ServerChat.MVVM.Core;
using ServerChat.MVVM.Model;
using ServerChat.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ServerChat.MVVM.ViewModel
{
    class MainViewModel
    {
        public ObservableCollection<UserModel> Users { get; set; }
        public ObservableCollection<UserModel> HelpQueue { get; set; }
        public ObservableCollection<string> Messages { get; set; }
        public RelayCommand PopHelpQueueCommand { get; set; }
        public RelayCommand ConnectToServerCommand { get; set; }
        public RelayCommand RequestHelpCommand { get; set; }
        public RelayCommand DisconnectFromServerCommand { get; set; }
        public RelayCommand SendMessageCommand { get; set; }
        public string Username { get; set; }
        public string ServerAddress { get; set; }
        public string ServerPort { get; set; }
        public string Message { get; set; }

        private Server _server;
        public MainViewModel()
        {
            Users = new ObservableCollection<UserModel>();
            HelpQueue = new ObservableCollection<UserModel>();
            Messages = new ObservableCollection<string>();
            _server = new Server();
            _server.ConnectedEvent += UserConnected;
            _server.HelpRequestEvent += UserHelpRequest;
            _server.MsgReceivedEvent += MessageReceived;
            _server.UserDisconnectedEvent += RemoveUser;
            _server.PopHelpQueueEvent += PopHelpQueue;

            //Initialize Relay Commands
            #region

            //Connection button requires Username, ServerAddress and ServerPort fields to all be filled with values to be pressed
            ConnectToServerCommand = new RelayCommand(o => _server.ConnectToSever(Username, ServerAddress, Int32.Parse(ServerPort)),
                o => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(ServerAddress) && !string.IsNullOrEmpty(ServerPort));

            RequestHelpCommand = new RelayCommand(o => _server.RequestHelp(Username), o => !string.IsNullOrEmpty(Username));

            SendMessageCommand = new RelayCommand(o => _server.SendMessageToServer(Message), o => !string.IsNullOrEmpty(Message));

            PopHelpQueueCommand = new RelayCommand(o => _server.PopHelpQueue()); //TESTING
            #endregion
        }

        private void PopHelpQueue()
        {
            if(HelpQueue.Count > 0)
            {
                Application.Current.Dispatcher.Invoke(() => HelpQueue.RemoveAt(0));
            }
        }

        private void RemoveUser()
        {
            var uid = _server.PacketReader.ReadMessage();
            var user = Users.Where(x => x.UID == uid).FirstOrDefault();
            Application.Current.Dispatcher.Invoke(() => Users.Remove(user));
        }
        private void MessageReceived()
        {
            var msg = _server.PacketReader.ReadMessage();
            Application.Current.Dispatcher.Invoke(() => Messages.Add(msg));
        }
        private void UserConnected()
        {
            var user = new UserModel
            {
                Username = _server.PacketReader.ReadMessage(),
                UID = _server.PacketReader.ReadMessage()
            };

            if (!Users.Any(x => x.UID == user.UID))
            {
                Application.Current.Dispatcher.Invoke(() => Users.Add(user));
            }
        }

        private void UserHelpRequest()
        {
            var uid = _server.PacketReader.ReadMessage();
            var user = Users.Where(x => x.UID == uid).FirstOrDefault();

            if (!HelpQueue.Any(x => x.UID == user.UID))
            {
                Application.Current.Dispatcher.Invoke(() => HelpQueue.Add(user));
            }
        }
    }
}

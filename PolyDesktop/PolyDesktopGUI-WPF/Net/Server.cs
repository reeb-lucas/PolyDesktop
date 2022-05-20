using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using ChatClient.Net.IO;
using ChatServer.Net.IO;

namespace ChatClient.Net
{
    class Server
    {
        TcpClient _client;
        public PacketReader PacketReader;

        public event Action ConnectedEvent;
        public event Action HelpRequestEvent;
        public event Action MsgReceivedEvent;
        public event Action UserDisconnectedEvent;
        public event Action PopHelpQueueEvent;

        public Server()
        {
            _client = new TcpClient();
        }

        public void ConnectToSever(string username, string serverAddress, int serverPort)
        {
            if(!_client.Connected)
            {
                try
                {
                    _client.Connect(serverAddress, serverPort);
                    PacketReader = new PacketReader(_client.GetStream());

                    if (!string.IsNullOrEmpty(username))
                    {
                        var connectPacket = new PacketBuilder();
                        connectPacket.WriteOpCode(0);
                        connectPacket.WriteMessage(username);
                        _client.Client.Send(connectPacket.GetPacketBytes());
                    }

                    ReadPackets();
                }
                catch
                {
                    //TODO, Add error handling here, display error connecting message somewhere
                }
            }
        }

        public void RequestHelp(string username)
        {
            try 
            {
                if (_client.Connected)
                {
                    var requestPacket = new PacketBuilder();
                    requestPacket.WriteOpCode(15);
                    requestPacket.WriteMessage(username);
                    try
                    {
                        _client.Client.Send(requestPacket.GetPacketBytes());
                    }
                    catch
                    {
                        //TODO, Add error message to display somewhere
                    }
                }
            }
            catch
            {
                //TODO, Add error handling here, display error connecting message somewhere
            }
        }

        public void PopHelpQueue()
        {
            var popQueuePacket = new PacketBuilder();
            popQueuePacket.WriteOpCode(20);
            try
            {
                _client.Client.Send(popQueuePacket.GetPacketBytes());
            }
            catch
            {
                //TODO, Add error message to display somewhere
            }
        }

        private void ReadPackets()
        {
            Task.Run(() =>
            { 
                while(true)
                {
                    try 
                    {
                        var opcode = PacketReader.ReadByte();
                        switch (opcode)
                        {
                            case 1:
                                ConnectedEvent?.Invoke();
                                break;
                            case 5:
                                MsgReceivedEvent?.Invoke();
                                break;
                            case 10:
                                UserDisconnectedEvent?.Invoke();
                                break;
                            case 15:
                                HelpRequestEvent?.Invoke();
                                break;
                            case 20:
                                PopHelpQueueEvent?.Invoke();
                                break;
                            default:
                                Console.WriteLine("Error: Wrong opcode received");
                                break;
                        }
                    }
                    catch 
                    {
                        //TODO: Display Error Message of forceful disconnection
                    }
                }
            });
        }

        public void SendMessageToServer(string message)
        {
            var messagePacket = new PacketBuilder();
            messagePacket.WriteOpCode(5);
            messagePacket.WriteMessage(message);
            try
            {
                _client.Client.Send(messagePacket.GetPacketBytes());
            }
            catch
            {
                //TODO, Add error message to display somewhere
            }
        }
    }
}

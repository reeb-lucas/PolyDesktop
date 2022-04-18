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
        public event Action MsgReceivedEvent;
        public event Action UserDisconnectedEvent;

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

        //WIP
        /*public void DisconnectFromServer()
        {
            _client.Close();
        }*/

        private void ReadPackets()
        {
            Task.Run(() =>
            { 
                while(true)
                {
                    var opcode = PacketReader.ReadByte();
                    switch(opcode)
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
                        default:
                            Console.WriteLine("???");
                            break;
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

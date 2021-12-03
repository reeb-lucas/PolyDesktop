/******************************************************************
 * Copyright (c) 2021
 * Author: Nate Arnold
 * Filename: Client.cs
 * Date Created: 11/20/2021
 * Modifications: 
 * 
******************************************************************/

/**************************************************************
 * Overview:
 *      Client class to connect to a specified server
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PolyDesktopClient
{
    class Client
    {
        private static readonly Socket ClientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
        private const int PORT = 8001;

        static void Main()
        {
            Console.Title = "Client";
            ConnectToServer();
            RequestLoop();
            Disconnect();
        }

        //NOTE: CURRENTLY JUST RUNNING ON LOOPBACK(AKA LOCAL MACHINE)
        private static void ConnectToServer()
        {
            int attempts = 0;

            while (!ClientSocket.Connected)
            {
                try
                {
                    attempts++;
                    Console.WriteLine("Connection attempt " + attempts);
                    // Change IPAddress.Loopback to a remote IP to connect to a remote host.
                    ClientSocket.Connect(IPAddress.Loopback, PORT);
                }
                catch (SocketException)
                {
                    Console.Clear();
                }
            }

            Console.Clear();
            Console.WriteLine("Connected");
        }

        private static void RequestLoop()
        {
            //For now change to a Menu loop, later functionality will be from buttons

            //Send a request string to server based on functionality requested

            Console.WriteLine("Press Keys\n1: Get Current Time \n2. Disconnect");

            bool exit = false;

            do
            {
                // Read key presses
                if (Console.ReadKey().Key == ConsoleKey.D1)
                {
                    SendRequest("get time");
                    Console.WriteLine("\n");
                }
                else if (Console.ReadKey().Key == ConsoleKey.D2) // TODO FIX: Have to double click keys other than key "1"
                {
                    SendRequest("exit");

                    Disconnect();
                }
                else
                {
                    //Probably should just be handled client side, but current framework uses server logic
                    SendRequest("Invalid");
                }

                ReceiveResponse();

            } while (exit == false);
        }

        private static void Disconnect()
        {
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
            Environment.Exit(0);
        }

        private static void SendRequest(string request)
        {
            SendString(request);
        }

        private static void ReceiveResponse()
        {
            var buffer = new byte[2048];
            int received = ClientSocket.Receive(buffer, SocketFlags.None);

            if (received == 0) return;

            var data = new byte[received];
            Array.Copy(buffer, data, received);
            string text = Encoding.ASCII.GetString(data);
            Console.WriteLine(text);
        }

        private static void SendString(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }
    }
}

/******************************************************************
 * Copyright (c) 2021
 * Author: Nate Arnold
 * Filename: Server.cs
 * Date Created: 11/20/2021
 * Modifications: 
 * 
******************************************************************/

/**************************************************************
 * Overview:
 *      Server class to listen and connect to multiple clients
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PolyDesktopServer
{
    class Server
    {
        private static Socket serverSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
        private static readonly List<Socket> clientSockets = new List<Socket>();
        private const int BUFFER_SIZE = 2048;
        private const int PORT = 8001;
        private static readonly byte[] buffer = new byte[BUFFER_SIZE];

        static void Main()
        {
            Console.Title = "Server";
            StartServer();
            Console.ReadLine(); // When we press enter close everything
            CloseAllSockets();
        }


        private static void StartServer()
        {
            Console.WriteLine("Starting Server...");
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));
            serverSocket.Listen();
            serverSocket.BeginAccept(AcceptCallback, null);
            Console.WriteLine("Server setup complete");
        }


        /******************************************************************
         * 
         * Close all connected client 
         * (We do not need to shutdown the server socket as its connections
         * are already closed with the clients).
         * 
        ******************************************************************/
        private static void CloseAllSockets()
        {
            foreach (Socket socket in clientSockets)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

            serverSocket.Close();
        }

        /******************************************************************
         * 
         * Listen to accept a client connection
         * If accepted, add the connected client to clientSockets List
         * Begin listening again
         * 
        ******************************************************************/
        private static void AcceptCallback(IAsyncResult AR)
        {
            Socket socket;

            try
            {
                socket = serverSocket.EndAccept(AR);
            }
            catch (ObjectDisposedException)
            {
                return;
            }

            // Client accepted
            clientSockets.Add(socket);
            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);
            Console.WriteLine("Client connected, waiting for request...");

            // Begin listening again
            serverSocket.BeginAccept(AcceptCallback, null);
        }

        /******************************************************************
         * 
         * Listen for client input
         * If input is received, read input and run corresponding code
         * Begin listening for input again
         * 
        ******************************************************************/
        private static void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;
            int received;

            try
            {
                received = current.EndReceive(AR);
            }
            catch (SocketException)
            {
                Console.WriteLine("Client forcefully disconnected");
                // Don't shutdown because the socket may be disposed and its disconnected anyway.
                current.Close();
                clientSockets.Remove(current);
                return;
            }

            //Input detected
            //TODO Change to functions, functions will replaced by PolyDesktop functionality

            //Recieve request string
            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);
            Console.WriteLine("Received Text: " + text);

            if (text == "get time") // Client requested time
            {
                Console.WriteLine("Text is a get time request");
                byte[] data = Encoding.ASCII.GetBytes(DateTime.Now.ToLongTimeString());
                current.Send(data);
                Console.WriteLine("Time sent to client");
            }
            else if (text == "exit") // Client wants to exit gracefully
            {
                // Always Shutdown before closing
                current.Shutdown(SocketShutdown.Both);
                current.Close();
                clientSockets.Remove(current);
                Console.WriteLine("Client disconnected");
                return;
            }
            else
            {
                Console.WriteLine("Text is an invalid request");
                byte[] data = Encoding.ASCII.GetBytes("Invalid request");
                current.Send(data);
                Console.WriteLine("Warning Sent");
            }

            // Begin listening again
            current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
        }
    }
}


﻿// 
// Licensed to the Apache Software Foundation (ASF) under one
// or more contributor license agreements.  See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership.  The ASF licenses this file
// to you under the Apache License, Version 2.0 (the
// "License"); you may not use this file except in compliance
// with the License.  You may obtain a copy of the License at
// 
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.
// 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Tools;
using NetworkCommsDotNet.Connections;
using NetworkCommsDotNet.Connections.TCP;

namespace DebugTests
{
    static class SSLTest
    {
        static byte[] sendArray = new byte[] { 3, 45, 200, 10, 9, 8, 7, 45, 96, 123 };

        static bool serverMode;

        public static void RunExample()
        {
            NetworkComms.ConnectionEstablishTimeoutMS = 600000;

            //Create a suitable certificate if it does not exist
            if (!File.Exists("testCertificate.pfx"))
            {
                CertificateDetails details = new CertificateDetails("CN=networkcomms.net", DateTime.Now, DateTime.Now.AddYears(1));
                SSLTools.CreateSelfSignedCertificatePFX(details, "testCertificate.pfx");
            }

            //Load the certificate
            X509Certificate cert = new X509Certificate2("testCertificate.pfx");

            IPAddress localIPAddress = IPAddress.Parse("::1");

            Console.WriteLine("Please select mode:");
            Console.WriteLine("1 - Server (Listens for connections)");
            Console.WriteLine("2 - Client (Creates connections to server)");

            //Read in user choice
            if (Console.ReadKey(true).Key == ConsoleKey.D1) serverMode = true;
            else serverMode = false;

            if (serverMode)
            {
                NetworkComms.AppendGlobalIncomingPacketHandler<byte[]>("Data", (header, connection, data) =>
                {
                    Console.WriteLine("Received data (" + data.Length + ") from " + connection.ToString());
                });

                //Establish handler
                NetworkComms.AppendGlobalConnectionEstablishHandler((connection) =>
                {
                    Console.WriteLine("Connection established - " + connection);
                });

                //Close handler
                NetworkComms.AppendGlobalConnectionCloseHandler((connection) =>
                {
                    Console.WriteLine("Connection closed - " + connection);
                });

                SSLOptions sslOptions = new SSLOptions(cert, true, true);
                TCPConnectionListener listener = new TCPConnectionListener(NetworkComms.DefaultSendReceiveOptions, 
                    ApplicationLayerProtocolStatus.Enabled, sslOptions);
                Connection.StartListening(listener, new IPEndPoint(localIPAddress, 10000), true);

                Console.WriteLine("\nListening for TCP (SSL) messages on:");
                foreach (IPEndPoint localEndPoint in Connection.ExistingLocalListenEndPoints(ConnectionType.TCP)) 
                   Console.WriteLine("{0}:{1}", localEndPoint.Address, localEndPoint.Port);

                Console.WriteLine("\nPress any key to quit.");
                ConsoleKeyInfo key = Console.ReadKey(true);
            }
            else
            {
                ConnectionInfo serverInfo = new ConnectionInfo(new IPEndPoint(localIPAddress, 10000));

                SSLOptions sslOptions = new SSLOptions("networkcomms.net", true);
                //SSLOptions sslOptions = new SSLOptions(cert, true);

                TCPConnection conn = TCPConnection.GetConnection(serverInfo, NetworkComms.DefaultSendReceiveOptions, sslOptions);
                conn.SendObject("Data", sendArray);
                Console.WriteLine("Sent data to server.");

                Console.WriteLine("\nClient complete. Press any key to quit.");
                Console.ReadKey(true);
            }

            NetworkComms.Shutdown();
        }
    }
}

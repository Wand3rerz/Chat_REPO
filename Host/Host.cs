﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Host
{
    class Host
    {
        private static Dictionary<int, string> userList;
        private static int id;
        
        static void Main(string[] args)
        {
            IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];
            TcpListener server = new TcpListener(ip, 8888);
            TcpClient client = default(TcpClient);

            try
            {
                server.Start();
                Console.WriteLine("Booted");
                Console.Read();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Read();
            }

            while (true)
            {
                client = server.AcceptTcpClient();

                byte[] receivedBuffer = new byte[100];
                
            }
        }

        public static void NewClient(string name)
        {
            id = 0;
            id = new Random().Next(1, 10000);
            if (name != null)
            {
                userList.Add(id, name);
            }
        }
    }
}
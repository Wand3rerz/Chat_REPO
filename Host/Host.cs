using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    class Host
    {
        private static Dictionary<int, string> userList;
        private static int id;
        private static List<TcpClient> clientList;
        
        static void Main(string[] args)
        {   
            IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];
            TcpListener server = new TcpListener(ip, 8888);
            TcpClient client = default(TcpClient);

            try
            {
                server.Start();
                Console.WriteLine("Booted");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Read();
            }

            clientList = new List<TcpClient>();
            
            while (true)
            {
                client = server.AcceptTcpClient();
                clientList.Add(client);
                
                byte[] receivedBuffer = new byte[1024];
                NetworkStream stream = client.GetStream();

                stream.Read(receivedBuffer, 0, receivedBuffer.Length);

                StringBuilder msg = new StringBuilder();

                foreach (byte b in receivedBuffer)
                {
                    if (b.Equals(00))
                    {
                        break;
                    }
                    else
                    {
                        msg.Append(Convert.ToChar(b).ToString());
                    }
                }
                
                Console.WriteLine(msg.ToString() + msg.Length);
                SendToClients(msg.ToString());

            }
        }

        private static void SendToClients(string msgToSend)
        {
            Console.WriteLine(clientList.Count.ToString());
            Console.ReadKey();

            foreach (TcpClient c in clientList)
            {

                Int64 byteCount = Encoding.ASCII.GetByteCount(msgToSend + 1);
                
                Byte[] sendData = new byte[byteCount];

                sendData = Encoding.ASCII.GetBytes(msgToSend);
            
                Console.WriteLine(msgToSend + " send");
            
                NetworkStream stream = c.GetStream();    
                stream.Write(sendData, 0, sendData.Length);
            }
        }
    }
}
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client
{
    class Client
    {
        private static string serverIp = "localhost";
        private static int port = 8888;
        private static bool connected = false;
        private static string name = "";
        
        static void Main(string[] args)
        {   
            GetName();
            
            while (connected)
            {
                string input = Console.ReadLine();

                if (input != null && input != String.Empty)
                {
                    TcpClient client = new TcpClient(serverIp, port);

                    Int64 byteCount = Encoding.ASCII.GetByteCount(input + 1);
                    
                    byte[] sendData = new byte[byteCount];

                    sendData = Encoding.ASCII.GetBytes(name + ": " + input + ";");
                    
                    NetworkStream stream = client.GetStream();
                    stream.Write(sendData, 0, sendData.Length);
                    
                    stream.Close();
                    client.Close();
                }
                
            
            }   
        }
        
        private static void GetName()
        {
            Console.WriteLine("Name:");
            string temp = Console.ReadLine();

            if (temp != null && temp != string.Empty)
            {
                name = temp;
                Console.WriteLine(name + "connected");

                connected = true;
            }
            else
            {
                Console.WriteLine("Name Invalid");
                GetName();
            }

        }
    }
}
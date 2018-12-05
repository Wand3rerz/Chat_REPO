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
        
        static void Main(string[] args)
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (input != null && input != String.Empty)
                {
                    TcpClient client = new TcpClient(serverIp, port);

                    Int64 byteCount = Encoding.ASCII.GetByteCount(input + 1);
                    
                    byte[] sendData = new byte[byteCount];

                    sendData = Encoding.ASCII.GetBytes(input + ";");
                    
                    NetworkStream stream = client.GetStream();
                    stream.Write(sendData, 0, sendData.Length);
                    
                    stream.Close();
                    client.Close();
                }
                
            
            }   
        }
    }
}
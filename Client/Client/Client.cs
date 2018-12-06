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
        private static NetworkStream stream;
        
        static void Main(string[] args)
        {   
            GetName();
            TcpClient client = new TcpClient(serverIp, port);
            
            while (connected)
            {
                string input = Console.ReadLine();
                
                if (input != null && input != String.Empty)
                {
                    Int64 byteCount = Encoding.ASCII.GetByteCount(input + 1);
                    byte[] sendData = new byte[byteCount];
                    sendData = Encoding.ASCII.GetBytes(name + ": " + input);
                    stream = client.GetStream();
                    stream.Write(sendData, 0, sendData.Length);
                    
                    byte[] receivedBuffer = new byte[100];
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
                    
                    Console.WriteLine(msg.ToString());
                    
                }
            }   
                            
            stream.Close();
            client.Close();
        }
        
        private static void GetName()
        {
            Console.WriteLine("Name:");
            string temp = Console.ReadLine();

            if (temp != null && temp != string.Empty)
            {
                name = temp;
                Console.WriteLine(name + " connected");

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
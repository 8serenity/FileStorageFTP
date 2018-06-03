using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FileStorageClient
{
    class Program
    {
        static void Main(string[] args)
        {

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8);
            TcpClient client = new TcpClient();

            try
            {
                client.Connect(endPoint);
                NetworkStream stream = client.GetStream();

                Console.WriteLine("Please enter a full path of the file");
                string destinationOfFile = Console.ReadLine();

                byte[] data = Encoding.Default.GetBytes(destinationOfFile);

                stream.WriteAsync(data, 0, data.Length);
                client.Close();
            }
            catch (Exception)
            {

                throw;
            }



        }
    }
}

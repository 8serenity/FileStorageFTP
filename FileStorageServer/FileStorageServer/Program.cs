using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

/*
    Продолжение практики. Написать асинхронный файлообменник на основе протокола TCP.
    Клиент отправляет файл указанный пользователем (посредством пути). Файлообменник принимает и сохраняет файл.
     */

namespace FileStorageServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8);

            TcpListener server = new TcpListener(endPoint);

            try
            {
                server.Start();

                while (true)
                {
                    Console.WriteLine("Waiting for incoming connection");

                    TcpClient client = server.AcceptTcpClient();

                    Task.Factory.StartNew(() => ProcessData(client));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                server.Stop();
            }

        }

        static void ProcessData(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            try
            {
                do
                {
                    byte[] data = new byte[256];
                    int bytes = stream.Read(data, 0, data.Length);
                    string source = Encoding.Default.GetString(data, 0, bytes);
                    string destination = source.Substring(source.LastIndexOf('\\') + 1);
                    File.Copy(source, destination, true);
                }while (stream.DataAvailable);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                stream.Close();
                client.Close();
            }
        }
    }
}

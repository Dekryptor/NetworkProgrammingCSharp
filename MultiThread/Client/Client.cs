using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MultiThread.Client
{
    public class Client
    {
        const int NUMBER_OF_THREADS = 2000;
        void Work(object obj)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            TcpClient client = new TcpClient();
            client.Connect(ep);

            StringBuilder sb = new StringBuilder();
            using (NetworkStream stream = client.GetStream())
            {
                string request = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + '\0';
                Console.WriteLine("sent: " + request);
                stream.Write(Encoding.ASCII.GetBytes(request), 0, request.Length);

                int i;
                while ((i = stream.ReadByte()) != 0)
                {
                    sb.Append((char)i);
                }
            }
            client.Close();

            Console.WriteLine(sb.ToString());
        }

        public void Start()
        {
            for (int i = 0; i < NUMBER_OF_THREADS; i++)
            {
                ThreadPool.QueueUserWorkItem(Work);
            }
        }
    }
}

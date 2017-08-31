using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace SocketConsole
{
    class Program
    {
        public static Hashtable clientsList = new Hashtable();

        static void Main(string[] args)
        {
            SocketOne();
            SocketTwo();
            MultiServerSocket();
        }

        static void SocketOne()
        {
            byte[] buffer = new byte[1000];
            byte[] msg = Encoding.ASCII.GetBytes("From server\n");
            string data = null;

            IPHostEntry iphostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = iphostInfo.AddressList[0];
            IPEndPoint localEndpoint = new IPEndPoint(ipAddress, 32000);

            ConsoleKeyInfo key;
            int count = 0;

            Socket sock = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            sock.Bind(localEndpoint);
            sock.Listen(5);

            while (true)
            {
                Console.WriteLine("\nWaiting for clients..{0}", count);
                Socket confd = sock.Accept();

                int b = confd.Receive(buffer);
                data += Encoding.ASCII.GetString(buffer, 0, b);

                Console.WriteLine("" + data);
                data = null;

                confd.Send(msg);

                Console.WriteLine("\n<< Continue 'y' , Exit 'e'>>");
                key = Console.ReadKey();
                if (key.KeyChar == 'e')
                {
                    Console.WriteLine("\nExiting..Handled {0} clients", count);
                    confd.Close();
                    System.Threading.Thread.Sleep(5000);
                    break;
                }
                confd.Close();
                count++;
            }
        }

        static void SocketTwo()
        {
            IPAddress ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
            TcpListener serverSocket = new TcpListener(ipAddress, 8888);
            TcpClient clientSocket = default(TcpClient);
            serverSocket.Start();
            Console.WriteLine(">> Server Started");
            clientSocket = serverSocket.AcceptTcpClient();
            Console.WriteLine(">> Accept connection from client");

            int requestCount = 0;
            while (true)
            {
                try
                {
                    requestCount += 1;
                    NetworkStream networkStream = clientSocket.GetStream();
                    byte[] bytesFrom = new byte[10025];
                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    string dataFromClient = Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    Console.WriteLine(">> Data from Client - " + dataFromClient);
                    string serverResponse = "Last message from client: " + dataFromClient;
                    Byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();
                    Console.WriteLine(">> " + serverResponse);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine(">> exit");
            Console.ReadLine();
        }

        static void MultiServerSocket()
        {
            IPAddress ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
            TcpListener serverSocket = new TcpListener(ipAddress, 8888);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;

            serverSocket.Start();
            Console.WriteLine(" >> " + "Server Started");

            counter = 0;
            while (true)
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine(" >> " + "Client No:" + Convert.ToString(counter) + " started!");

                byte[] bytesFrom = new byte[10025];
                string dataFromClient = null;

                NetworkStream networkStream = clientSocket.GetStream();
                networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));

                clientsList.Add(dataFromClient, clientSocket);

                broadcast(dataFromClient + " Joined ", dataFromClient, false);

                Console.WriteLine(dataFromClient + " Joined chat room ");

                HandleClient client = new HandleClient();
                client.startClient(clientSocket, dataFromClient, clientsList);
            }

            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine(" >> " + "exit");
            Console.ReadLine();
        }

        public static void broadcast(string msg, string uName, bool flag)
        {
            foreach (DictionaryEntry Item in clientsList)
            {
                TcpClient broadcastSocket;
                broadcastSocket = (TcpClient)Item.Value;
                NetworkStream broadcastStream = broadcastSocket.GetStream();
                Byte[] broadcastBytes = null;

                if (flag == true)
                {
                    broadcastBytes = Encoding.ASCII.GetBytes(uName + " says : " + msg);
                }
                else
                {
                    broadcastBytes = Encoding.ASCII.GetBytes(msg);
                }

                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                broadcastStream.Flush();
            }
        }  //end broadcast function
    }
}

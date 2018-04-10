using System;
using System.Net;
using System.Net.Sockets;

namespace TCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        private static TcpListener _listener;
        public static void StartServer()
        {
            System.Net.IPAddress localIPAddress = System.Net.IPAddress.Parse("10.91.173.201");
            IPEndPoint ipLocal = new IPEndPoint(localIPAddress, 8888);
            _listener = new TcpListener(ipLocal);
            _listener.Start();
            WaitForClientConnect();
        }
        private static void WaitForClientConnect()
        {
            object obj = new object();
            _listener.BeginAcceptTcpClient(new System.AsyncCallback(OnClientConnect), obj);
        }
        private static void OnClientConnect(IAsyncResult asyn)
        {
            try
            {
                TcpClient clientSocket = default(TcpClient);
                clientSocket = _listener.EndAcceptTcpClient(asyn);
                HandleClientRequest clientReq = new HandleClientRequest(clientSocket);
                clientReq.StartClient();
            }
            catch (Exception se)
            {
                throw;
            }

            WaitForClientConnect();
        }
    }
}

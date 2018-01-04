using System;
using System.Net.Sockets;

namespace SocketCore.Server
{
    public class ConnectedClient
    {
        private Socket m_clientSocket;
        SocketListener m_listener;

        public event TCPTerminal_MessageRecivedDel MessageReceived
        {
            add
            {
                m_listener.MessageReceived += value;
            }
            remove
            {
                m_listener.MessageReceived -= value;
            }
        }
        public event TCPTerminal_DisconnectDel Disconnected
        {
            add
            {
                m_listener.Disconnected += value;
            }
            remove
            {
                m_listener.Disconnected -= value;
            }
        }

        public ConnectedClient(Socket clientSocket)
        {
            m_clientSocket = clientSocket;
            m_listener = new SocketListener();
        }

        public void StartListen()
        {
            m_listener.StartReciving(m_clientSocket);
        }

        public void Send(byte[] buffer)
        {
            if (m_clientSocket == null)
            {
                throw new Exception("Can't send data. ConnectedClient is Closed!");
            }
            m_clientSocket.Send(buffer);
            
        }

        public void Stop()
        {
            m_listener.StopListening();
            m_clientSocket = null;
        }
    }
}

using System;
using System.Text;
using System.Net.Sockets;


namespace SocketServer
{
    public class CSocketPacket
    {
        public Socket thisSocket;
        public byte[] dataBuffer = new byte[1];
    }
}

using System.Net.Sockets;

namespace SocketCore
{
    public delegate void TCPTerminal_MessageRecivedDel(Socket socket, byte[] message);
    public delegate void TCPTerminal_ConnectDel(Socket socket);
    public delegate void TCPTerminal_DisconnectDel(Socket socket);
}

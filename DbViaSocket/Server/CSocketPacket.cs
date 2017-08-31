using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbViaSocketServer
{
    public class CSocketPacket
    {
        public System.Net.Sockets.Socket thisSocket;
        public byte[] dataBuffer = new byte[1];
    }
}

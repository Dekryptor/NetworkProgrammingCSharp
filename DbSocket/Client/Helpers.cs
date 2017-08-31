using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DbSocket.Client
{
    public static class Helpers
    {
        /// <summary>
        /// Serialize object to array of bytes
        /// </summary>
        public static byte[] SerializeData(Object obj)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(ms, obj);

            return ms.ToArray();
        }

        /// <summary>
        /// Deserialize array of bytes to object
        /// </summary>
        public static object DeserializeData(byte[] byteArray)
        {
            MemoryStream ms = new MemoryStream(byteArray);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            ms.Position = 0;

            return binaryFormatter.Deserialize(ms);
        }

        /// <summary>
        /// Check input ip, port
        /// </summary>
        public static bool CheckInput(string ip, string port)
        {
            bool ret = false;
            Match matchIP = Regex.Match(ip, @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
            Match matchPort = Regex.Match(port, @"\d");
            if (matchIP.Success && matchPort.Success)
                ret = true;
            return ret;
        }
    }
}

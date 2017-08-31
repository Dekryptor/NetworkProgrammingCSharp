using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DbViaSocketClient
{
    public static class Utilities
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
    }
}

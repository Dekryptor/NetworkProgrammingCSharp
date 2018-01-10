using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace NetworkConsole
{
    public class BinarySerializer
    {
        public static byte[] Serialize(object obj)
        {
            using (System.IO.MemoryStream serializationStream = new System.IO.MemoryStream())
            {
                IFormatter formater = new BinaryFormatter();
                formater.Serialize(serializationStream, obj);
                return serializationStream.ToArray();
            }
        }

        public static TObj Deserialize<TObj>(byte[] data)
        {
            using (System.IO.MemoryStream serializationStream = new System.IO.MemoryStream(data, false))
            {
                IFormatter formater = new BinaryFormatter();
                return (TObj)formater.Deserialize(serializationStream);
            }
        }
    }
}

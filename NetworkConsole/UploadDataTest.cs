using System;
using System.Net;
using System.Text;

namespace NetworkConsole
{
    class UploadDataTest
    {
        public static void Main(string[] argv)
        {
            WebClient wc = new WebClient();

            string data = "This is the data to post";
            byte[] dataarray = Encoding.ASCII.GetBytes(data);

            wc.UploadData(argv[0], dataarray);
        }
    }
}

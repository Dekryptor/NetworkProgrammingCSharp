using System;
using System.Net;
using System.Text;

namespace NetworkConsole
{
    class DownloadFileTest
    {
        public static void Main(string[] argv)
        {
            WebClient wc = new WebClient();
            string filename = "webpage.htm";
            wc.DownloadFile(argv[0], filename);
            Console.WriteLine("file downloaded");
            byte[] response = wc.DownloadData(argv[0]);
            Console.WriteLine(Encoding.ASCII.GetString(response));
        }
    }
}

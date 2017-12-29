using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            client.Start();

            //press any key to exit
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}

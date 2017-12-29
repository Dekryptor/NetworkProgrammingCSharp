using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring.Server
{
    class Program
    {
        PerformanceCounter perfCPUCounter = new PerformanceCounter("Processor Information", "% Processor Time", "Total");
        PerformanceCounter perfMemCounter = new PerformanceCounter("Memory", "Available MBytes");
        PerformanceCounter perfSystemCounter = new PerformanceCounter("System", "System Up Time");

        static void Main(string[] args)
        {
            Console.Write("Enter server url: ");
            var url = Console.ReadLine();
            Console.Clear();

            //ServerStatusBy(url);
            var on = IsMachineOnline(url);
            Console.WriteLine(on);
            Console.ReadLine();
        }

        private static void ServerStatusBy(string url)
        {
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send(url);
            //PingReply reply2 = pingSender.Send(new IPAddress(new byte[] { 127, 0, 0, 1 }), 3000);

            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine("IP Address: {0} ", reply.Address.ToString());
                Console.WriteLine("RoundTrip time: {0} ", reply.RoundtripTime);
                Console.WriteLine("Time to live: {0} ", reply.Options.Ttl);
                Console.WriteLine("Don't fragment: {0} ", reply.Options.DontFragment);
                Console.WriteLine("Buffer size: {0} ", reply.Buffer.Length);
            }
            else
            {
                Console.WriteLine(reply.Status);
            }
        }

        private static bool IsMachineUp(string hostName)
        {
            bool retVal = false;
            try
            {
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();
                // Use the default Ttl value which is 128,
                // but change the fragmentation behavior.
                options.DontFragment = true;
                // Create a buffer of 32 bytes of data to be transmitted.
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;

                PingReply reply = pingSender.Send(hostName, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                Console.WriteLine(ex.Message);
            }
            return retVal;
        }

        //Check If Machine is Online or Offline 
        //using WMI (without Ping Service)
        private static bool IsMachineOnline(string hostName)
        {
            bool retVal = false;
            ManagementScope scope = new ManagementScope(string.Format(@"\\{0}\root\cimv2", hostName));
            ManagementClass os = new ManagementClass(scope, new ManagementPath("Win32_OperatingSystem"), null);
            try
            {
                ManagementObjectCollection instances = os.GetInstances();
                foreach (ManagementObject mo in instances)
                {
                    string wlanCard = (string)mo["InstanceName"];
                    bool active;
                    if (!bool.TryParse((string)mo["Active"], out active))
                    {
                        active = false;
                    }
                    byte[] ssid = (byte[])mo["Ndis80211SsId"];
                }

                retVal = true;
            }
            catch (Exception ex)
            {
                retVal = false;
                Console.WriteLine(ex.Message);
            }
            return retVal;
        }
    }
}

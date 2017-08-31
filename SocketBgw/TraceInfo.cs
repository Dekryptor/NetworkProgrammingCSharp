using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysLogs
{
    public class TraceInfo: IComparable<TraceInfo>
    {
        public int STT { get; set; }
        public string MA { get; set; }
        public string SIP { get; set; }
        public string OS { get; set; }

        public override string ToString()
        {
            return "MA: " + MA + ", SIP: " + SIP + ", OS" + OS + ";" + Environment.NewLine;
        }

        public int CompareTo(TraceInfo that)
        {
            if (this.MA == that.MA)
            {
                if (this.SIP == that.SIP)
                {
                    return this.OS.CompareTo(that.OS);
                }
                return that.SIP.CompareTo(this.SIP);
            }
            return that.MA.CompareTo(this.MA);
        }
    }
}

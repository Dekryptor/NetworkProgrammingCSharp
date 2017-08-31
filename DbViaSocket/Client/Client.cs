using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace DbViaSocketClient
{
    public partial class frmClient : Form
    {
        public frmClient()
        {
            InitializeComponent();
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress[] ipAddress = null;
                Match matchIP = Regex.Match(txtServerAdr.Text, @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
                if(matchIP.Success)
                    ipAddress = Dns.GetHostAddresses(txtServerAdr.Text);

                int port = 0;
                Match matchPort = Regex.Match(txtServerPort.Text, @"\d");
                if (matchPort.Success)
                    port = Int32.Parse(txtServerPort.Text);
                
                //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8001);
                IPEndPoint endPoint = new IPEndPoint(ipAddress[0], port);
                using (Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP))
                {
                    clientSocket.Connect(endPoint);
                    byte[] data = new byte[1024 * 5000];
                    clientSocket.Receive(data);
                    DataTable dt = (DataTable)Utilities.DeserializeData(data);
                    dataGridView.DataSource = dt;
                    clientSocket.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

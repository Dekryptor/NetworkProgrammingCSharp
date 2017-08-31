using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using System.Net.Sockets;

namespace DbViaSocketServer
{
    public partial class frmServer : Form
    {
        public AsyncCallback pfnWorkerCallBack;
        private Socket socketServer = null;
        public frmServer()
        {
            InitializeComponent();
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            btnStartServer.Text = String.Format(@"In progress...");
            Application.DoEvents();

            //IPAddress[] ipAddress = Dns.GetHostAddresses(txtAddServer.Text);
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 8001);
            //IPAddress ipAddress = IPAddress.Parse(txtServerIP.Text);
            //IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 8001);

            using (socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP))
            {
                socketServer.Bind(ipEndPoint);
                socketServer.Listen(100);

                //Socket for client
                using (Socket socketClient = socketServer.Accept())
                {
                    DataTable table = GetData();
                    //send table to Client
                    socketClient.Send(Utillities.SerializeData(table));

                    //socketClient.Receive()

                    socketClient.Close();
                }

                socketServer.Close();
            }

            btnStartServer.Text = "&Start Server";
            Application.DoEvents();
        }

        private void btStopServer_Click(object sender, EventArgs e)
        {

        }

        private DataTable GetData()
        {
            DataTable dt = new DataTable();

            string query = "SELECT * FROM Sample";
            DataInteractor dataUtils = new DataInteractor();
            dt = dataUtils.LoadData(query);

            return dt;
        }

        

        //private void btnStartServer_Click(object sender, EventArgs e)
        //{
        //    btnStartServer.Text = String.Format("In process...");
        //    Application.DoEvents();

        //    //AsynchronousSocketListener.StartListening();

        //    btnStartServer.Text = "&Start Server";
        //    Application.DoEvents();
        //}
        
        //private DataTable getData()
        //{
        //    DataTable dt = new DataTable();
        //    DataRow dr = null;

        //    dt.Columns.Add(new DataColumn("IntegerValue", typeof(Int32)));
        //    dt.Columns.Add(new DataColumn("StringValue", typeof(string)));
        //    dt.Columns.Add(new DataColumn("DateTimeValue", typeof(DateTime)));
        //    dt.Columns.Add(new DataColumn("BooleanValue", typeof(bool)));

        //    for (int i = 1; i < 100; i++)
        //    {
        //        dr = dt.NewRow();
        //        dr[0] = i;
        //        dr[1] = "Item " + i.ToString();
        //        dr[2] = DateTime.Now;
        //        dr[3] = (i % 2 != 0) ? true : false;

        //        dt.Rows.Add(dr);
        //    }
        //    return dt;
        //}
    }
}

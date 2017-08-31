using System;
using System.Drawing;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text.RegularExpressions;


namespace DbSocket.Server
{
    public partial class SocketServer : Form
    {
        public AsyncCallback pfnWorkerCallBack;
        public static TcpListener listener;
        public static Socket m_socListener;
        public static Socket m_socWorker;
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        //public static bool _isRunning = true;
        public static bool isListening = false;

        public SocketServer()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                int port = 8221;
                Match matchPort = Regex.Match(port.ToString(), @"\d");
                if (matchPort.Success)
                    port = Int32.Parse(txtServerPort.Text);
                listener = new TcpListener(IPAddress.Any, port);
                m_socListener = listener.Server;

                m_socListener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                m_socListener.ReceiveTimeout = 5000;
                // Start listening and process connections here.
                listener.Start();
                m_socListener.BeginAccept(new AsyncCallback(OnClientConnect), null);
                isListening = true;
                
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try 
            {
                isListening = false;

                if (listener != null)
                {
                    listener.Stop();
                    if (m_socListener != null)
                    {
                        if (m_socListener.Connected)
                        {
                            m_socListener.Shutdown(SocketShutdown.Both);
                            m_socListener.Disconnect(true);
                        }
                        m_socListener.Close();
                    }

                    if (m_socWorker != null)
                    { 
                        if (m_socWorker.Connected)
                        {
                            m_socWorker.Shutdown(SocketShutdown.Both);
                            m_socWorker.Disconnect(true);
                        }
                        m_socWorker.Close();
                    }
                }

                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void OnClientConnect(IAsyncResult async)
        {
            ////original
            //m_socWorker = m_socListener.EndAccept(asyn);
            //WaitForData(m_socWorker);

            //if (async.AsyncState == null)
            if (isListening == true)
            {
                m_socWorker = m_socListener.EndAccept(async);
            }

            //Begin: send data to client
            if (m_socWorker != null && m_socWorker.Connected)
            {
                DataTable table = GetData();
                int byteSend = m_socWorker.Send(Helpers.SerializeData(table));
            }
            WaitForData(m_socWorker);
            //End
        }

        public void WaitForData(Socket socket)
        {
            if (pfnWorkerCallBack == null)
            {
                pfnWorkerCallBack = new AsyncCallback(OnDataReceived);
            }
            MyStateSocket stateSocket = new MyStateSocket();

            if (socket != null)
            {
                stateSocket.workSocket = socket;
                socket.BeginReceive(stateSocket._dataBuffer, 0, stateSocket._dataBuffer.Length, SocketFlags.None, pfnWorkerCallBack, stateSocket);
            }
        }

        public void OnDataReceived(IAsyncResult async)
        {
            MyStateSocket stateSocket = (MyStateSocket)async.AsyncState;
            
            byte[] data = new byte[1024 * 5000];
            int receivedDataLength = stateSocket.workSocket.Receive(data);
            string stringData = Encoding.ASCII.GetString(data, 0, receivedDataLength + 1);
            txtDataReceive.Invoke(new MethodInvoker(delegate
            {
                txtDataReceive.Text = txtDataReceive.Text + stringData;
                txtDataReceive.Focus();
                txtDataReceive.SelectionStart = txtDataReceive.Text.Length;
            }));
            
            WaitForData(m_socWorker);

            ////delete record
            //bool ret = false;
            //if (stringData.Contains("DELETE "))
            //{
            //    ret = DeleteRecord(stringData);
            //}

            //if (ret)
            //{
            //    txtDataReceive.Invoke(new MethodInvoker(delegate
            //    {
            //        txtDataReceive.Text = txtDataReceive.Text + "One record was deleted..." + Environment.NewLine;
            //        txtDataReceive.Focus();
            //        txtDataReceive.SelectionStart = txtDataReceive.Text.Length;
            //    }));
            //}
        }

        private bool DeleteRecord(string query)
        {
            bool ret = false;

            DataInteractor dataUtils = new DataInteractor();
            ret = dataUtils.NoneQuery(query);

            return ret;
        }

        private DataTable GetData()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("IntegerValue", typeof(Int32)));
            dt.Columns.Add(new DataColumn("StringValue", typeof(string)));
            dt.Columns.Add(new DataColumn("DateTimeValue", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("BooleanValue", typeof(bool)));

            for (int i = 1; i < 100; i++)
            {
                dr = dt.NewRow();
                dr[0] = i;
                dr[1] = "Item " + i.ToString();
                dr[2] = DateTime.Now;
                dr[3] = (i % 2 != 0) ? true : false;

                dt.Rows.Add(dr);
            }
            return dt;
        }

        private DataTable GetData2()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT [IntergerValue], [StringValue], [DateTimeValue], [BooleanValue] FROM Sample";
            DataInteractor dataUtils = new DataInteractor();
            dt = dataUtils.LoadData(query);

            return dt;
        }
    }

    public class MyStateSocket
    {
        // Size of receive buffer.
        //public const int _bufferSize = 1024;
        public const int _bufferSize = 1;

        // Client socket.
        public Socket workSocket = null;
        // Receive buffer.
        public byte[] _dataBuffer = new byte[_bufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }
}
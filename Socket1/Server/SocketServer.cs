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


namespace SocketServer
{
    public partial class SocketServer : Form
    {
        public AsyncCallback pfnWorkerCallBack;
        public TcpListener listener;
        public Socket m_socListener;
        public Socket m_socWorker;
        public bool startClick = false;
        public bool stopClick = false;
        //new
        // Thread signal.
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        public SocketServer()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //startClick = true;
            //try
            //{
            //    IPAddress[] ipAddress = null;
            //    int port = 0;
            //    bool checkInput = Utilities.CheckInput(txtServerIP.Text, txtServerPort.Text);

            //    if (checkInput)
            //    {
            //        ipAddress = Dns.GetHostAddresses(txtServerIP.Text);
            //        port = Int32.Parse(txtServerPort.Text);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Invalid IP, Port", "Error");
            //        txtServerIP.Select();
            //        return;
            //    }
                
            //    IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, port);
            //    listener = new TcpListener(IPAddress.Any, port);
            //    m_socListener = listener.Server;
                
            //    LingerOption lingerOption = new LingerOption(true, 10);
            //    m_socListener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, lingerOption);
            //    // start listening and process connections here.
            //    listener.Start();
            //    m_socListener.BeginAccept(new AsyncCallback(OnClientConnect), null);
            //}
            //catch (SocketException se)
            //{
            //    MessageBox.Show(se.Message);
            //}

            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 11000);
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                
            // Bind the socket to the local endpoint and listen for incoming connections.
            try 
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.
                    //Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                    // Wait until a connection is made before continuing.
                    allDone.WaitOne();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            allDone.Set();

            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
        }

        public void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.
                state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read more data.
                content = state.sb.ToString();
                if (content.IndexOf("<EOF>") > -1)
                {
                    // All the data has been read from the client. Display it on the console.
                    txtDataReceive.Invoke(new MethodInvoker(delegate
                    {
                        txtDataReceive.Text = txtDataReceive.Text + content;
                    }));
                    // Echo the data back to the client.
                    Send(handler, content);
                }
                else
                {
                    // Not all data received. Get more.
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
                }
            }
        }

        private void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                //Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            stopClick = true;
            if (m_socListener != null && m_socListener.Connected)
            {
                listener.Stop();
                m_socListener.Shutdown(SocketShutdown.Both);
                m_socListener.Disconnect(true);
                m_socListener.Dispose();
                m_socListener.Close();
            }

            if (m_socWorker != null)
            {
                if (m_socWorker.Connected)
                    m_socWorker.Shutdown(SocketShutdown.Both);
                m_socWorker.Disconnect(true);
                m_socWorker.Dispose();
                m_socWorker.Close();
            }

            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        //public void OnClientConnect(IAsyncResult asyn)
        //{
        //    ////original
        //    //m_socWorker = m_socListener.EndAccept(asyn);
        //    //WaitForData(m_socWorker);
            
        //    if (!stopClick && asyn.AsyncState == null)
        //    {
        //        //TcpClient client = listener.AcceptTcpClient(); // Get client connection
        //        m_socWorker = m_socListener.EndAccept(asyn);
        //    }
                
        //    //Begin: send data to client
        //    if (m_socWorker != null && m_socWorker.Connected)
        //    {
        //        DataTable table = GetData();
        //        int byteSend = m_socWorker.Send(Utilities.SerializeData(table));
        //    }
        //    WaitForData(m_socWorker);
        //    //End
        //}

        //public void WaitForData(Socket socket)
        //{
        //    if (pfnWorkerCallBack == null)
        //    {
        //        pfnWorkerCallBack = new AsyncCallback(OnDataReceived);
        //    }
        //    CSocketPacket theSocPkt = new CSocketPacket();
            
        //    if (socket != null)
        //    {
        //        theSocPkt.thisSocket = socket;
        //        // now start to listen for any data...
        //        socket.BeginReceive(theSocPkt.dataBuffer, 0, theSocPkt.dataBuffer.Length, SocketFlags.None, pfnWorkerCallBack, theSocPkt);
        //    }
        //}

        //public void OnDataReceived(IAsyncResult asyn)
        //{
        //    //CSocketPacket theSockId = (CSocketPacket)asyn.AsyncState;
        //    ////end receive...
        //    //int iRx = 0;
        //    //iRx = theSockId.thisSocket.EndReceive(asyn);
        //    //char[] chars = new char[iRx + 1];
        //    ////Decoder decoder = Encoding.Unicode.GetDecoder();
        //    //Decoder decoder = Encoding.UTF8.GetDecoder();
        //    //int charLen = decoder.GetChars(theSockId.dataBuffer, 0, iRx, chars, 0);
        //    //String strData = new System.String(chars);

        //    //txtDataReceive.Invoke(new MethodInvoker(delegate
        //    //{
        //    //    txtDataReceive.Text = txtDataReceive.Text + strData;
        //    //}));
        //    //WaitForData(m_socWorker);

        //    //
        //    CSocketPacket theSockId = (CSocketPacket)asyn.AsyncState;
        //    //end receive...
        //    byte[] data = new byte[1024 * 5000];
        //    int receivedDataLength = theSockId.thisSocket.Receive(data);
        //    string stringData = Encoding.ASCII.GetString(data, 0, receivedDataLength);
        //    txtDataReceive.Invoke(new MethodInvoker(delegate
        //    {
        //        txtDataReceive.Text = txtDataReceive.Text + stringData;
        //    }));
        //    WaitForData(m_socWorker);
            
        //    //int bytesRead;
        //    //while ((bytesRead = theSockId.thisSocket.Receive(data)) > 0)
        //    //{
        //    //    //string stringData = Encoding.ASCII.GetString(data, 0, receivedDataLength);
        //    //}

        //    //delete record
        //    bool ret = false;
        //    if (stringData.Contains("DELETE "))
        //    {
        //        ret = DeleteRecord(stringData);
        //    }

        //    if (ret)
        //    {
        //        txtDataReceive.Invoke(new MethodInvoker(delegate
        //        {
        //            txtDataReceive.Text = txtDataReceive.Text + "One record was deleted..." + Environment.NewLine;
        //        }));
        //    }

        //    //if (theSockId.thisSocket.Available > 0) // .Connected)
        //    //{
        //    //    iRx = theSockId.thisSocket.EndReceive(asyn);
        //    //    char[] chars = new char[iRx + 1];
        //    //    System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
        //    //    int charLen = d.GetChars(theSockId.dataBuffer, 0, iRx, chars, 0);
        //    //    String strData = new System.String(chars);

        //    //    txtDataReceive.Invoke(new MethodInvoker(delegate
        //    //    {
        //    //        txtDataReceive.Text = txtDataReceive.Text + strData;
        //    //    }));
        //    //    WaitForData(m_socWorker);
        //    //}
        //}

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

            string query = @"SELECT 
                                [IntergerValue]
                                ,[StringValue]
                                ,[DateTimeValue]
                                ,[BooleanValue] 
                            FROM Sample";
            DataInteractor dataUtils = new DataInteractor();
            dt = dataUtils.LoadData(query);

            return dt;
        }

        //private DataTable GetData()
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
